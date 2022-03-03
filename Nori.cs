using System;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;

namespace NoriSDK
{
    public class Nori
    {
        public Usuario UsuarioAutenticado { get; internal set; }
        public Empresa Empresa { get; internal set; }
        public Configuracion Configuracion { get; internal set; }
        public Configuracion.SAP SAP { get; internal set; }
        public Estacion Estacion { get; internal set; }
        public SqlConnectionStringBuilder Conexion { get; set; }
        public Nori(string cadena = "")
        {
            Conexion = new SqlConnectionStringBuilder(cadena);
            Global.Usuario = new Usuario();
        }

        public static DataContext CrearContexto()
        {
            DataContext contexto = new DataContext(Global.CadenaConexion);
            return contexto;
        }

        public bool Conectar()
        {
            Global.CadenaConexion = Conexion.ConnectionString;
            DataContext con = CrearContexto();
            try
            {
                con.Connection.Open();
                if (con.Connection.State == ConnectionState.Open)
                {
                    Inicializar();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
            finally
            {
                con.Connection.Close();
            }
        }
        public bool Autenticar(Usuario usuario, bool api = false)
        {
            try
            {
                if (Usuario.Usuarios().Count() == 0)
                    Usuario.AgregarPredeterminado();

                if (!api)
                {
                    if (usuario.huella_digital.Length > 0)
                        usuario = Usuario.Usuarios().Where(x => x.usuario == usuario.usuario).First();
                    else
                        usuario = Usuario.Usuarios().Where(x => x.usuario == usuario.usuario && x.contraseña == Cifrado.Cifrar(usuario.contraseña)).First();
                }

                UsuarioAutenticado = usuario;

                if (UsuarioAutenticado.rol == 'X')
                {
                    if ((UsuarioAutenticado.activo == true && UsuarioAutenticado.id != 0 && UsuarioAutenticado.sesion.IsNullOrEmpty()) || (UsuarioAutenticado.activo == true && UsuarioAutenticado.id != 0 && UsuarioAutenticado.sesion == Global.Sesion) || (UsuarioAutenticado.activo == true && UsuarioAutenticado.id != 0 && Vendedor.Obtener(UsuarioAutenticado.vendedor_id).foraneo) || api)
                    {
                        Global.Usuario = UsuarioAutenticado;
                        UsuarioAutenticado.Conectar();
                        return true;
                    }
                    else
                    {
                        if (UsuarioAutenticado.id != 0 && UsuarioAutenticado.activo == false)
                            Global.Error = new Error("El usuario esta inactivo.");
                        else if (UsuarioAutenticado.sesion != Global.Sesion)
                            Global.Error = new Error("La última sesión y la actual no coinciden, solicite autorización para acceder.");
                        else
                            Global.Error = new Error("Usuario y/o contraseña incorrectos.");

                        return false;
                    }
                }
                else
                {
                    if ((UsuarioAutenticado.activo == true && UsuarioAutenticado.id != 0))
                    {
                        Global.Usuario = UsuarioAutenticado;
                        UsuarioAutenticado.Conectar();
                        return true;
                    }
                    else
                    {
                        if (UsuarioAutenticado.id != 0 && UsuarioAutenticado.activo == false)
                            Global.Error = new Error("El usuario esta inactivo.");
                        else
                            Global.Error = new Error("Usuario y/o contraseña incorrectos.");

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Error = new Error(ex.Message);
                return false;
            }
        }

        public bool EstablecerUsuario(Usuario usuario)
        {
            try
            {
                Global.Usuario = usuario;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  Esta opción permite inicializar la empresa.
        /// </summary>
        internal void Inicializar()
        {
            //Obtiene la empresa o crea una si no existe
            Empresa = new Empresa();
            Empresa = Empresa.Obtener();
            if (Empresa.id == 0)
                Empresa.Agregar();
            //Obtiene la configuración o crea una si no existe (Incluye SAP)
            Configuracion = new Configuracion();
            Configuracion = Configuracion.Obtener();
            if (Configuracion.id == 0)
                Configuracion.Agregar();
            SAP = new Configuracion.SAP();
            SAP = Configuracion.SAP.Obtener();
            if (SAP.id == 0)
                SAP.Agregar();
            Global.Configuracion = Configuracion;
            //Obtiene una estación de trabajo o crea una si no existe
            Estacion = new Estacion();
            Estacion = Estacion.Obtener(Estacion.codigo);
            if (Estacion.id == 0)
                Estacion.Agregar();
            Global.Estacion = Estacion;
            //Obtiene la sesión única del usuario
            Global.Sesion = GetLogonSid.getLogonSid();
        }
        public static Exception ObtenerUltimoError()
        {
            return Global.Error;
        }
        public class Error : Exception
        {
            public Error() : base() {}
            public Error(string message) : base(message) {}
            public Error(string message, Exception inner) : base(message, inner) {}
            protected Error(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            {}
        }
        class GetLogonSid
        {
            private const int UOI_USER_SID = 4;

            [DllImport("user32.dll")]
            static extern bool GetUserObjectInformation(IntPtr hObj, int nIndex, [MarshalAs(UnmanagedType.LPArray)] byte[] pvInfo, int nLength, out uint lpnLengthNeeded);


            [DllImport("user32.dll")]
            private static extern IntPtr GetThreadDesktop(int dwThreadId);

            [DllImport("kernel32.dll")]
            public static extern int GetCurrentThreadId();

            [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
            static extern bool ConvertSidToStringSid([MarshalAs(UnmanagedType.LPArray)] byte[] pSID, out IntPtr ptrSid);

            public static string getLogonSid()
            {
                string sidString = string.Empty;
                IntPtr hdesk = GetThreadDesktop(GetCurrentThreadId());
                byte[] buf = new byte[100];
                uint lengthNeeded;
                GetUserObjectInformation(hdesk, UOI_USER_SID, buf, 100, out lengthNeeded);
                IntPtr ptrSid;
                if (!ConvertSidToStringSid(buf, out ptrSid))
                    throw new System.ComponentModel.Win32Exception();
                try
                {
                    sidString = Marshal.PtrToStringAuto(ptrSid);
                }
                catch
                {
                }
                return sidString;
            }
        }
    }
}
