using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "consultas_personalizadas")]
    public class ConsultaPersonalizada
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string contexto { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string query { get; set; }
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

        public static Table<ConsultaPersonalizada> ConsultasPersonalizadas()
        {
            return Nori.CrearContexto().GetTable<ConsultaPersonalizada>();
        }

        public ConsultaPersonalizada()
        {
            contexto = string.Empty;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public DataTable Ejecutar()
        {
            try
            {
                return Utilidades.EjecutarQuery(query);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }

        public static ConsultaPersonalizada Obtener(int id)
        {
            try
            {
                return ConsultasPersonalizadas().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ConsultaPersonalizada();
            }
        }

        public static ConsultaPersonalizada Obtener(string contexto)
        {
            try
            {
                return ConsultasPersonalizadas().Where(x => x.contexto == contexto).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new ConsultaPersonalizada();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = ConsultasPersonalizadas();
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
                var Tabla = ConsultasPersonalizadas();
                ConsultaPersonalizada consultapersonalizada = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(consultapersonalizada);
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
