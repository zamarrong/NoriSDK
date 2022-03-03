using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "monedas")]
    public class Moneda
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public char simbolo { get; set; }
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

        public static Table<Moneda> Monedas()
        {
            return Nori.CrearContexto().GetTable<Moneda>();
        }

        public Moneda()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Moneda Obtener(int id)
        {
            try
            {
                return Monedas().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Moneda();
            }
        }

        public static Moneda Obtener(string codigo)
        {
            Moneda moneda = new Moneda();
            try
            {
                return Monedas().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Moneda();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Monedas();
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
                var Tabla = Monedas();
                Moneda moneda = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(moneda);
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
