using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "relacion_documentos")]
    public class RelacionDocumento
    {
        public static Table<RelacionDocumento> relacion_documentos = Nori.CrearContexto().GetTable<RelacionDocumento>();
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int documento_origen_id { get; set; }
        [Column]
        public int documento_destino_id { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public RelacionDocumento()
        {
            documento_destino_id = 0;
            documento_origen_id = 0;
            usuario_creacion_id = Global.UsuarioID;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.UsuarioID;
            fecha_actualizacion = DateTime.Now;
        }

        public RelacionDocumento Obtener(int id)
        {
            RelacionDocumento relacion_documento = new RelacionDocumento();
            try
            {
                relacion_documento = relacion_documentos.Where(x => x.id == id).First();
                return relacion_documento;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return relacion_documento;
            }
        }
        public RelacionDocumento ObtenerPosterios(int id)
        {
            RelacionDocumento relacion_documento = new RelacionDocumento();
            try
            {
                relacion_documento = relacion_documentos.Where(x => x.documento_origen_id == id).First();
                return relacion_documento;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return relacion_documento;
            }
        }
        public RelacionDocumento ObtenerAnterior(int id)
        {
            RelacionDocumento relacion_documento = new RelacionDocumento();
            try
            {
                relacion_documento = relacion_documentos.Where(x => x.documento_destino_id == id).First();
                return relacion_documento;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return relacion_documento;
            }
        }
        public bool Agregar()
        {
            try
            {
                relacion_documentos.InsertOnSubmit(this);
                relacion_documentos.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
            finally
            {
                relacion_documentos = Nori.CrearContexto().GetTable<RelacionDocumento>();
            }
        }

        public bool Actualizar()
        {
            try
            {
                usuario_actualizacion_id = Global.UsuarioID;
                fecha_actualizacion = DateTime.Now;
                relacion_documentos.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
            finally
            {
                relacion_documentos = Nori.CrearContexto().GetTable<RelacionDocumento>();
            }
        }
    }
}
