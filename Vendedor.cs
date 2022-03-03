using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "vendedores")]
    public class Vendedor
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int zona_id { get; set; }
        [Column]
        public int ruta_id { get; set; }
        [Column]
        public int codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public decimal porcentaje_comision { get; set; }
        [Column]
        public bool foraneo { get; set; }
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

        public static Table<Vendedor> Vendedores()
        {
            return Nori.CrearContexto().GetTable<Vendedor>();
        }

        public Vendedor()
        {
            zona_id = Global.Configuracion.zona_id;
            porcentaje_comision = 0;
            foraneo = false;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Vendedor Obtener(int id)
        {
            try
            {
                return Vendedores().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Vendedor();
            }
        }

        public static Vendedor ObtenerPorCodigo(int codigo)
        {
            try
            {
                return Vendedores().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Vendedor();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Vendedores();
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
                var Tabla = Vendedores();
                Vendedor vendedor = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(vendedor);
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
