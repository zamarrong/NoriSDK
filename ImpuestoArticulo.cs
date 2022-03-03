using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "impuestos_articulos")]
    public class ImpuestoArticulo
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int articulo_id { get; set; }
        [Column]
        public int impuesto_id { get; set; }
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

        public static Table<ImpuestoArticulo> ImpuestosArticulos()
        {
            return Nori.CrearContexto().GetTable<ImpuestoArticulo>();
        }

        public ImpuestoArticulo()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static ImpuestoArticulo Obtener(int id)
        {
            try
            {
                return ImpuestosArticulos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ImpuestoArticulo();
            }
        }
        public static ImpuestoArticulo ObtenerPorArticulo(int articulo_id)
        {
            try
            {
                return ImpuestosArticulos().Where(x => x.articulo_id == articulo_id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ImpuestoArticulo();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = ImpuestosArticulos();
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
                var Tabla = ImpuestosArticulos();
                ImpuestoArticulo impuesto_articulo = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(impuesto_articulo);
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
