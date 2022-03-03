using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "vehiculos")]
    public class Vehiculo
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public int modelo { get; set; }
        [Column]
        public string configuracion_vehicular { get; set; }
        [Column]
        public string numero_placas { get; set; }
        [Column]
        public string permiso_sct { get; set; }
        [Column]
        public string numero_permiso_sct { get; set; }
        [Column]
        public string aseguradora { get; set; }
        [Column]
        public string numero_poliza { get; set; }
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

        public static Table<Vehiculo> Vehiculos()
        {
            return Nori.CrearContexto().GetTable<Vehiculo>();
        }

        public Vehiculo()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Vehiculo Obtener(int id)
        {
            try
            {
                return Vehiculos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Vehiculo();
            }
        }

        public static Vehiculo Obtener(string codigo)
        {
            try
            {
                return Vehiculos().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Vehiculo();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Vehiculos();
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
                var Tabla = Vehiculos();
                Vehiculo vehiculo = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(vehiculo);
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