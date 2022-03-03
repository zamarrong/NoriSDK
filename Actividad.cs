using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "actividades")]
    public class Actividad
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int socio_id { get; set; }
        [Column]
        public int persona_contacto_id { get; set; }
        [Column]
        public int vendedor_id { get; set; }
        [Column]
        public int tipo_actividad_id { get; set; }
        [Column(CanBeNull = true)]
        public string comentarios { get; set; }
        [Column(CanBeNull = true)]
        public string notas { get; set; }
        [Column]
        public decimal latitud { get; set; }
        [Column]
        public decimal longitud { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Actividad> Actividades()
        {
            return Nori.CrearContexto().GetTable<Actividad>();
        }

        public Actividad()
        {
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Actividad Obtener(int id)
        {
            try
            {
                return Actividades().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Actividad();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Actividades();
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
                var Tabla = Actividades();
                Actividad actividad = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(actividad);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Tipos
        [Table(Name = "tipos_actividades")]
        public class Tipo
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
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

            public static Table<Tipo> Tipos()
            {
                return Nori.CrearContexto().GetTable<Tipo>();
            }

            public Tipo()
            {
                activo = true;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Tipo Obtener(int id)
            {
                try
                {
                    return Tipos().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Tipo();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Tipos();
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
                    var Tabla = Tipos();
                    Tipo tipo = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(tipo);
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
        #endregion
    }
}