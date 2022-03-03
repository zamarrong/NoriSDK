using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "sucursales")]
    public class Sucursal
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string servidor { get; set; }
        [Column]
        public string bd { get; set; }
        [Column]
        public string usuario { get; set; }
        [Column]
        public string contraseña { get; set; }
        [Column]
        public int horario { get; set; }
        [Column]
        public bool solo_subida { get; set; }
        [Column]
        public bool documentos_bajada { get; set; }
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

        public static Table<Sucursal> Sucursales()
        {
            return Nori.CrearContexto().GetTable<Sucursal>();
        }

        public Sucursal()
        {
            solo_subida = false;
            documentos_bajada = true;
            horario = 0;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Sucursal Obtener(int id)
        {
            try
            {
                return Sucursales().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Sucursal();
            }
        }

        public static Sucursal Obtener(string codigo)
        {
            try
            {
                return Sucursales().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Sucursal();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Sucursales();
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
                var Tabla = Sucursales();
                Sucursal sucursal = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(sucursal);
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
