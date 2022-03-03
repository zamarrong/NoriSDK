using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "estaciones")]
    public class Estacion
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string impresora_tickets { get; set; }
        [Column]
        public string impresora_documentos { get; set; }
        [Column]
        public bool lector_huella { get; set; }
        [Column]
        public bool bascula { get; set; }
        [Column]
        public int bascula_id { get; set; }
        [Column]
        public bool impresion { get; set; }
        [Column]
        public bool envio_correo_automatico { get; set; }
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
        //Uso interno
        public Bascula Bascula { get; set; }

        public static Table<Estacion> Estaciones()
        {
            return Nori.CrearContexto().GetTable<Estacion>();
        }

        public Estacion()
        {
            codigo = Environment.MachineName;
            nombre = Environment.MachineName;
            impresora_tickets = string.Empty;
            impresora_documentos = string.Empty;
            lector_huella = false;
            bascula = false;
            impresion = true;
            envio_correo_automatico = false;
            activo = true;
            usuario_creacion_id = 1;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = 1;
            fecha_actualizacion = DateTime.Now;
        }

        public static Estacion Obtener(int id)
        {
            try
            {
                return Estaciones().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Estacion();
            }
        }

        public static Estacion Obtener(string codigo)
        {
            try
            {
                return Estaciones().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Estacion();
            }
        }

        public bool ObtenerBascula()
        {
            try
            {
                Bascula = Bascula.Obtener(bascula_id);
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }


        public bool Agregar()
        {
            try
            {
                var Tabla = Estaciones();
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
                var Tabla = Estaciones();
                Estacion estacion = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(estacion);
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
}
