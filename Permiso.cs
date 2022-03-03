using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "permisos")]
    public class Permiso
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int usuario_id { get; set; }
        [Column]
        public string objeto { get; set; }
        [Column]
        public bool agregar { get; set; }
        [Column]
        public bool actualizar { get; set; }
        [Column]
        public bool cancelar { get; set; }
        [Column]
        public bool eliminar { get; set; }
        [Column]
        public bool autorizacion { get; set; }
        [Column]
        public int usuario_autorizacion_id { get; set; }
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

        public static Table<Permiso> Permisos()
        {
            return Nori.CrearContexto().GetTable<Permiso>();
        }

        public Permiso()
        {
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Permiso Obtener(int id)
        {
            try
            {
                return Permisos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Permiso();
            }
        }

        public static Permiso Obtener(int usuario_id, string objeto, bool autorizacion = false)
        {
            try
            {
                return Permisos().Where(x => x.activo == true && x.usuario_id == usuario_id && x.objeto == objeto && x.autorizacion == autorizacion).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Permiso();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Permisos();
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
                var Tabla = Permisos();
                Permiso permiso = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(permiso);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool Eliminar()
        {
            try
            {
                var Tabla = Permisos();
                Tabla.Attach(this);
                Tabla.DeleteOnSubmit(this);
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
