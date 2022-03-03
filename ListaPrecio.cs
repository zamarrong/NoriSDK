using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "listas_precios")]
    public class ListaPrecio
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public short codigo { get; set; }
        [Column]
        public string nombre { get; set; }
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

        public static Table<ListaPrecio> ListasPrecios()
        {
            return Nori.CrearContexto().GetTable<ListaPrecio>();
        }

        public ListaPrecio()
        {
            activo = true;
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

        public static ListaPrecio Obtener(short codigo)
        {
            try
            {
                return ListasPrecios().Where(x => x.codigo == codigo).First();
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
    }
}