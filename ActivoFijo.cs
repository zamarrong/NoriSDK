using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "activos_fijos")]
    public class ActivoFijo
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int estatus_id { get; set; }
        [Column]
        public int grupo_articulo_id { get; set; }
        [Column]
        public int propietario_id { get; set; }
        [Column]
        public int fabricante_id { get; set; }
        [Column]
        public int socio_id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string serie { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string descripcion { get; set; }
        [Column]
        public string marca { get; set; }
        [Column]
        public string modelo { get; set; }
        [Column]
        public string comentario { get; set; }
        [Column]
        public bool activo { get; set; }
        [Column]
        public DateTime fecha_adquisicion { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<ActivoFijo> ActivosFijos()
        {
            return Nori.CrearContexto().GetTable<ActivoFijo>();
        }

        public ActivoFijo()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static ActivoFijo Obtener(int id)
        {
            try
            {
                return ActivosFijos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ActivoFijo();
            }
        }

        public static ActivoFijo Obtener(string codigo)
        {
            try
            {
                return ActivosFijos().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ActivoFijo();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = ActivosFijos();
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
                var Tabla = ActivosFijos();
                ActivoFijo activo_fijo = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(activo_fijo);
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