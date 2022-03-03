using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "grupos_articulos")]
    public class GrupoArticulo
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int ciclo_id { get; set; }
        [Column]
        public int grupo_superior_id { get; set; }
        [Column]
        public short codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string numero_cuenta_ajuste_inventario { get; set; }
        [Column]
        public int? color { get; set; }
        [Column]
        public int orden { get; set; }
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

        public static Table<GrupoArticulo> GruposArticulos()
        {
            return Nori.CrearContexto().GetTable<GrupoArticulo>();
        }

        public GrupoArticulo()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static GrupoArticulo Obtener(int id)
        {
            try
            {
                return GruposArticulos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new GrupoArticulo();
            }
        }

        public static GrupoArticulo Obtener(short codigo)
        {
            try
            {
                return GruposArticulos().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new GrupoArticulo();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = GruposArticulos();
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
                var Tabla = GruposArticulos();
                GrupoArticulo grupo_articulo = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(grupo_articulo);
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
