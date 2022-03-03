using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "clases_expedicion")]
    public class ClaseExpedicion
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public short codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string sitio_web { get; set; }
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

        public static Table<ClaseExpedicion> ClasesExpedicion()
        {
            return Nori.CrearContexto().GetTable<ClaseExpedicion>();
        }

        public ClaseExpedicion()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static ClaseExpedicion Obtener(int id)
        {
            try
            {
                return ClasesExpedicion().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ClaseExpedicion();
            }
        }

        public static ClaseExpedicion Obtener(short codigo)
        {
            try
            {
                return ClasesExpedicion().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ClaseExpedicion();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = ClasesExpedicion();
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
                var Tabla = ClasesExpedicion();
                ClaseExpedicion clase_expedicion = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(clase_expedicion);
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
