using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "configuracion")]
    public class Configuracion
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public bool formulario_panel { get; set; }
        [Column]
        public bool venta_articulo_precio_cero { get; set; }
        [Column]
        public bool venta_precio_menor_costo { get; set; }
        [Column]
        public bool venta_articulo_stock_cero { get; set; }
        [Column]
        public bool generar_documento_electronico_automaticamente { get; set; }
        [Column]
        public bool documento_borrador { get; set; }
        [Column]
        public bool agrupar_partidas { get; set; }
        [Column]
        public bool vendedor_segun_usuario { get; set; }
        [Column]
        public bool pedimentos { get; set; }
        [Column]
        public int tipo_metodo_pago_monedero_id { get; set; }
        [Column]
        public int condicion_pago_id { get; set; }
        [Column]
        public int metodo_pago_id { get; set; }
        [Column]
        public int departamento_id { get; set; }
        [Column]
        public int fabricante_id { get; set; }
        [Column]
        public int grupo_articulo_id { get; set; }
        [Column]
        public int impuesto_id { get; set; }
        [Column]
        public int lista_precio_id { get; set; }
        [Column]
        public int moneda_id{ get; set; }
        [Column]
        public int unidad_medida_id { get; set; }
        [Column]
        public int zona_id { get; set; }
        [Column]
        public int certificado_id { get; set; }
        [Column]
        public int dia_semana { get; set; }
        [Column]
        public string directorio_informes { get; set; }
        [Column]
        public string directorio_documentos { get; set; }
        [Column]
        public string directorio_anexos { get; set; }
        [Column]
        public string directorio_imagenes { get; set; }
        [Column]
        public string directorio_xml { get; set; }
        [Column]
        public string directorio_huellas { get; set; }
        [Column]
        public string tema { get; set; }
        [Column(CanBeNull = true)]
        public string api_url { get; set; }
        [Column]
        public bool sap { get; set; }
        [Column]
        public bool vendedor_segun_estacion { get; set; }
        [Column]
        public bool lista_precio_segun_usuario { get; set; }
        [Column]
        public bool documentos_modo_nuevo { get; set; }
        [Column]
        public bool inventario_sap { get; set; }
        [Column]
        public bool seleccionar_sucursal { get; set; }
        //<Timbrado>
        [Column]
        public int pac { get; set; }
        [Column]
        public bool timbrado_modo_prueba { get; set; }
        [Column]
        public string timbrado_usuario { get; set; }
        [Column]
        public string timbrado_contraseña { get; set; }
        //</Timbrado>
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Configuracion> Configuraciones()
        {
            return Nori.CrearContexto().GetTable<Configuracion>();
        }

        public Configuracion()
        {
            formulario_panel = false;
            venta_articulo_precio_cero = false;
            venta_articulo_stock_cero = false;
            venta_precio_menor_costo = true;
            documento_borrador = true;
            generar_documento_electronico_automaticamente = false;
            directorio_informes = directorio_documentos = directorio_imagenes = directorio_xml = directorio_huellas = directorio_anexos = string.Empty;
            tema = string.Empty;
            api_url = string.Empty;
            sap = false;
            seleccionar_sucursal = false;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Configuracion Obtener()
        {
            try
            {
                return Configuraciones().First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                Configuracion configuracion = new Configuracion();
                configuracion.Agregar();
                return configuracion;
            }
        }

        internal bool Agregar()
        {
            try
            {
                var Tabla = Configuraciones();
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
                var Tabla = Configuraciones();
                Configuracion configuracion = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(configuracion);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region SAP
        [Table(Name = "sap")]
        public class SAP
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public string servidor { get; set; }
            [Column]
            public string servidor_licencias { get; set; }
            [Column]
            public int tipo_servidor_bd { get; set; }
            [Column]
            public string bd { get; set; }
            [Column]
            public string usuario_bd { get; set; }
            [Column]
            public string contraseña_bd { get; set; }
            [Column]
            public string usuario { get; set; }
            [Column]
            public string contraseña { get; set; }
            [Column]
            public bool confiable { get; set; }
            [Column(DbType="Time NOT NULL")]
            public TimeSpan hora_sincronizacion_general { get; set; }
            [Column]
            public bool facturar_entregas { get; set; }
            [Column]
            public bool generar_ajuste_inventario { get; set; }
            [Column]
            public string numero_cuenta_ajuste_inventario { get; set; }
            [Column(CanBeNull = true)]
            public string api_url { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            internal static Table<SAP> SAPs()
            {
                return Nori.CrearContexto().GetTable<SAP>();
            }

            public SAP()
            {
                servidor = "localhost";
                servidor_licencias = servidor;
                tipo_servidor_bd = 1;
                bd = "SBODEMOMX";
                usuario_bd = "sa";
                contraseña_bd = string.Empty;
                usuario = "manager";
                contraseña = "manager";
                confiable = false;
                hora_sincronizacion_general = DateTime.Now.TimeOfDay;
                facturar_entregas = false;
                generar_ajuste_inventario = true;
                api_url = string.Empty;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static SAP Obtener()
            {
                try
                {
                    return SAPs().First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new SAP();
                }
            }

            internal bool Agregar()
            {
                try
                {
                    var Tabla = SAPs();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();
                    FechaSincronizacion.AgregarFechas();
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
                    var Tabla = SAPs();
                    SAP sap = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(sap);
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
