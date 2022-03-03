using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "usuarios")]
    public class Usuario
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int almacen_id { get; set; }
        [Column]
        public int ubicacion_id { get; set; }
        [Column]
        public int departamento_id { get; set; }
        [Column]
        public int vendedor_id { get; set; }
        [Column]
        public int socio_id { get; set; }
        [Column]
        public int estado_id { get; set; }
        [Column]
        public int clase_expedicion_id { get; set; }
        [Column]
        public int sucursal_id { get; set; }
        [Column]
        public int codigo { get; set; }
        [Column]
        public char rol { get; set; }
        [Column]
        public string usuario { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string correo { get; set; }
        [Column]
        public string contraseña { get; set; }
        [Column(CanBeNull = true)]
        public string norma_reparto { get; set; }
        [Column]
        public string huella_digital { get; set; }
        [Column]
        public string escritorio { get; set; }
        [Column]
        public string sesion { get; internal set; }
        [Column]
        public string ultima_sesion { get; internal set; }
        [Column]
        public bool suscribir_autorizaciones { get; set; }
        [Column]
        public string dispositivo { get; set; }
        [Column]
        public bool activo { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Usuario> Usuarios()
        {
            return Nori.CrearContexto().GetTable<Usuario>();
        }

        public Usuario()
        {
            try
            {
                rol = Rol.ObtenerPredeterminado();
                norma_reparto = string.Empty;
                huella_digital = string.Empty;
                sesion = string.Empty;
                ultima_sesion = string.Empty;
                escritorio = string.Empty;
                dispositivo = string.Empty;
                activo = true;
                if (!Global.Usuario.IsNullOrEmpty())
                {
                    usuario_creacion_id = Global.Usuario.id;
                    usuario_actualizacion_id = Global.Usuario.id;
                }
                fecha_creacion = DateTime.Now;
                fecha_actualizacion = DateTime.Now;
            }
            catch { }
        }

        public static Usuario Obtener(int id)
        {
            try
            {
                return Usuarios().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Usuario();
            }
        }

        public static Usuario Obtener(string usuario)
        {
            try
            {
                return Usuarios().Where(x => x.usuario == usuario).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Usuario();
            }
        }

        public string ObtenerContraseña()
        {
            try
            {
                return Cifrado.Decifrar(contraseña);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return contraseña;
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Usuarios();
                contraseña = Cifrado.Cifrar(contraseña);
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        internal static bool AgregarPredeterminado()
        {
            try
            {
                var Tabla = Usuarios();
                Usuario usuario = new Usuario();
                usuario.usuario = "admin";
                usuario.nombre = "Administrador";
                usuario.contraseña = Cifrado.Cifrar(usuario.usuario);
                usuario.correo = "soporte@nori.mx";
                Tabla.InsertOnSubmit(usuario);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }


        public bool Actualizar()
        {
            try
            {
                contraseña = Cifrado.Cifrar(contraseña);
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
                var Tabla = Usuarios();
                Usuario usuario = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(usuario);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool LiberarSesion()
        {
            return Desconectar();
        }

        internal bool Conectar()
        {
            try
            {
                var Tabla = Usuarios();
                Usuario usuario = Tabla.Where(x => x.id == id).First();
                usuario.ultima_sesion = sesion;
                usuario.sesion = Global.Sesion;
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool Desconectar()
        {
            try
            {
                var Tabla = Usuarios();
                Usuario usuario = Tabla.Where(x => x.id == id).First();
                usuario.ultima_sesion = sesion;
                usuario.sesion = string.Empty;
                Tabla.Context.SubmitChanges();


                if (!Global.Estacion.Bascula.IsNullOrEmpty())
                        Global.Estacion.Bascula.Desconectar();

                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool VendedorForaneo()
        {
            try
            {
                return Vendedor.Vendedores().Any(x => x.id == vendedor_id && x.foraneo == true);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public List<CorreoElectronico> CorreosElectronicos()
        {
            try
            {
                return CorreoElectronico.CorreosElectronicos().Where(x => x.usuario_id == id && x.activo == true).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<CorreoElectronico>();
            }
        }

        public byte NivelAcceso()
        {
            switch(rol)
            {
                case 'A':
                    return 100;
                case 'S':
                    return 70;
                case 'L':
                    return 50;
                case 'V':
                    return 40;
                case 'C':
                    return 10;
                default:
                    return 0;
            }
        }

        #region Rol
        public class Rol
        {
            public char rol { get; set; }
            public string nombre { get; set; }

            public static char Administrador = 'A';
            public static char Supervisor = 'S';
            public static char Logistica = 'L';
            public static char Vendedor = 'V';
            public static char Cajero = 'C';
            public static char Sincronizador = 'X';

            public static List<Rol> Roles()
            {
                List<Rol> roles = new List<Rol>();

                Rol rol = new Rol();

                rol.rol = 'A';
                rol.nombre = "Administrador";
                roles.Add(rol);

                rol = new Rol();

                rol.rol = 'S';
                rol.nombre = "Supervisor";
                roles.Add(rol);

                rol = new Rol();

                rol.rol = 'L';
                rol.nombre = "Logística";
                roles.Add(rol);

                rol = new Rol();
                rol.rol = 'V';
                rol.nombre = "Vendedor";
                roles.Add(rol);

                rol = new Rol();
                rol.rol = 'C';
                rol.nombre = "Cajero";
                roles.Add(rol);

                rol = new Rol();
                rol.rol = 'X';
                rol.nombre = "Sincronizador";
                roles.Add(rol);

                return roles;
            }

            public static char ObtenerPredeterminado()
            {
                return 'A';
            }
        }
        #endregion

        #region Correos electrónicos
        [Table(Name = "correos_electronicos")]
        public class CorreoElectronico
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int usuario_id { get; set; }
            [Column]
            public string servidor { get; set; }
            [Column]
            public int puerto { get; set; }
            [Column]
            public bool ssl { get; set; }
            [Column]
            public string usuario { get; set; }
            [Column]
            public string contraseña { get; set; }
            [Column]
            public string remitente { get; set; }
            [Column]
            public string asunto { get; set; }
            [Column]
            public string mensaje { get; set; }
            [Column]
            public bool activo { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<CorreoElectronico> CorreosElectronicos()
            {
                return Nori.CrearContexto().GetTable<CorreoElectronico>();
            }

            public CorreoElectronico()
            {
                servidor = "smtp.gmail.com";
                puerto = 587;
                ssl = true;
                usuario = string.Empty;
                contraseña = string.Empty;
                remitente = string.Empty;
                asunto = string.Empty;
                mensaje = string.Empty;
                activo = true;
                usuario_id = Global.Usuario.id;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static CorreoElectronico Obtener(int id)
            {
                try
                {
                    return CorreosElectronicos().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new CorreoElectronico();
                }
            }


            public bool Agregar()
            {
                try
                {
                    var Tabla = CorreosElectronicos();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Actualizar()
            {
                try
                {
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                    var Tabla = CorreosElectronicos();
                    CorreoElectronico correo_electronico = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(correo_electronico);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Eliminar()
            {
                try
                {
                    var Tabla = CorreosElectronicos();
                    Tabla.Attach(this);
                    Tabla.DeleteOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
        }
        #endregion

        #region Series

        [Table(Name = "usuarios_series")]
        public class Serie
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int serie_id { get; set; }
            [Column]
            public int usuario_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Serie> Series()
            {
                return Nori.CrearContexto().GetTable<Serie>();
            }

            public Serie()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Serie Obtener(int id)
            {
                try
                {
                    return Series().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Serie();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Series();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Actualizar()
            {
                try
                {
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                    var Tabla = Series();
                    Serie serie = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(serie);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Eliminar()
            {
                try
                {
                    var Tabla = Series();
                    Tabla.Attach(this);
                    Tabla.DeleteOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
        }
        #endregion

        #region Tipos metodos de pago

        [Table(Name = "usuarios_tipos_metodos_pago")]
        public class TipoMetodoPago
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int tipo_metodo_pago_id { get; set; }
            [Column]
            public int usuario_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<TipoMetodoPago> TiposMetodosPago()
            {
                return Nori.CrearContexto().GetTable<TipoMetodoPago>();
            }

            public TipoMetodoPago()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static TipoMetodoPago Obtener(int id)
            {
                try
                {
                    return TiposMetodosPago().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new TipoMetodoPago();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = TiposMetodosPago();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Actualizar()
            {
                try
                {
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                    var Tabla = TiposMetodosPago();
                    TipoMetodoPago TipoMetodoPago = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(TipoMetodoPago);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Eliminar()
            {
                try
                {
                    var Tabla = TiposMetodosPago();
                    Tabla.Attach(this);
                    Tabla.DeleteOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
        }
        #endregion

        #region ListasPrecios

        [Table(Name = "usuarios_listas_precios")]
        public class ListaPrecio
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int lista_precio_id { get; set; }
            [Column]
            public int usuario_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<ListaPrecio> ListasPrecios()
            {
                return Nori.CrearContexto().GetTable<ListaPrecio>();
            }

            public ListaPrecio()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static ListaPrecio Obtener(int id)
            {
                try
                {
                    return ListasPrecios().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new ListaPrecio();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = ListasPrecios();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Actualizar()
            {
                try
                {
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                    var Tabla = ListasPrecios();
                    ListaPrecio lista_precio = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(lista_precio);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Eliminar()
            {
                try
                {
                    var Tabla = ListasPrecios();
                    Tabla.Attach(this);
                    Tabla.DeleteOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
        }
        #endregion

        #region Almacenes

        [Table(Name = "usuarios_almacenes")]
        public class Almacen
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int almacen_id { get; set; }
            [Column]
            public int usuario_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Almacen> Almacenes()
            {
                return Nori.CrearContexto().GetTable<Almacen>();
            }

            public Almacen()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Almacen Obtener(int id)
            {
                try
                {
                    return Almacenes().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Almacen();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Almacenes();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Actualizar()
            {
                try
                {
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                    var Tabla = Almacenes();
                    Almacen almacen = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(almacen);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public bool Eliminar()
            {
                try
                {
                    var Tabla = Almacenes();
                    Tabla.Attach(this);
                    Tabla.DeleteOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
        }
        #endregion
    }
}