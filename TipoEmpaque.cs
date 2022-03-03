using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "tipos_empaques")]
    public class TipoEmpaque
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public decimal largo { get; set; }
        [Column]
        public decimal alto { get; set; }
        [Column]
        public decimal ancho { get; set; }
        [Column]
        public decimal volumen { get; set; }
        [Column]
        public decimal peso { get; set; }
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

        public static Table<TipoEmpaque> TiposEmpaques()
        {
            return Nori.CrearContexto().GetTable<TipoEmpaque>();
        }

        public TipoEmpaque()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static TipoEmpaque Obtener(int id)
        {
            try
            {
                return TiposEmpaques().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new TipoEmpaque();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = TiposEmpaques();
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
                var Tabla = TiposEmpaques();
                TipoEmpaque tipo_empaque = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(tipo_empaque);
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
