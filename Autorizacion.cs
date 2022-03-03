using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "autorizaciones")]
    public class Autorizacion
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int estacion_id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string referencia { get; set; }
        [Column]
        public string comentario { get; set; }
        [Column]
        public bool autorizado { get; set; }
        [Column]
        public bool pendiente{ get; set; }
        [Column]
        public int usuario_autorizacion_id { get; set; }
        [Column]
        public int estacion_autorizacion_id { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Autorizacion> Autorizaciones()
        {
            return Nori.CrearContexto().GetTable<Autorizacion>();
        }

        public Autorizacion()
        {
            estacion_id = Global.Estacion.id;
            referencia = string.Empty;
            comentario = string.Empty;
            autorizado = false;
            pendiente = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Autorizacion Obtener(int id)
        {
            try
            {
                return Autorizaciones().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Autorizacion();
            }
        }

        public static Autorizacion ObtenerAutorizacionPendiente()
        {
            try
            {
                string query_sucursal = (Global.Usuario.sucursal_id == 0) ? "" : string.Format("AND t2.sucursal_id = {0}", Global.Usuario.sucursal_id);
                int id = Utilidades.EjecutarEscalar(string.Format("SELECT T0.id FROM autorizaciones t0 join conceptos_autorizaciones t1 ON t0.codigo = t1.codigo join usuarios t2 on t0.usuario_creacion_id = t2.id WHERE pendiente = 1 AND (t1.nivel_acceso = {0} OR t1.usuario_autorizacion_id = {1}) {2}", Global.Usuario.NivelAcceso(), Global.Usuario.id, query_sucursal));
                if (id != 0)
                    return Obtener(id);
                else
                    return null;
            } catch { return null; }
        }

        public bool Verificar()
        {
            try
            {
                Autorizacion autorizacion = Autorizaciones().Where(x => x.id == id).First();
                autorizacion.CopyProperties(this);
                return true;
            }
            catch { return false; }
        }

        public bool Agregar()
        {
            try
            {
                var concepto_autorizacion = ConceptoAutorizacion.ConceptosAutorizaciones().Where(x => x.codigo == codigo).Select(x => new { x.operador, x.nivel_acceso }).First();

                if (concepto_autorizacion.operador.Operator(Global.Usuario.NivelAcceso(), concepto_autorizacion.nivel_acceso))
                {
                    pendiente = false;
                    autorizado = (codigo.Equals("CRUDO")) ? false : true;
                    estacion_autorizacion_id = Global.Estacion.id;
                    usuario_autorizacion_id = Global.Usuario.id;
                }

                var Tabla = Autorizaciones();
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


        public static bool Autorizado(Usuario usuario, string codigo)
        {
            try
            {
                var concepto_autorizacion = ConceptoAutorizacion.ConceptosAutorizaciones().Where(x => x.codigo == codigo).Select(x => new { x.operador, x.nivel_acceso }).First();

                if (concepto_autorizacion.operador.Operator(usuario.NivelAcceso(), concepto_autorizacion.nivel_acceso))
                    return true;
                else
                    return false;
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
                pendiente = false;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
                var Tabla = Autorizaciones();
                Autorizacion autorizacion = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(autorizacion);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Conceptos
        [Table(Name = "conceptos_autorizaciones")]
        public class ConceptoAutorizacion
        {
            [Column(IsPrimaryKey = true)]
            public string codigo { get; set; }
            [Column]
            public string operador { get; set; }
            [Column]
            public byte nivel_acceso { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]
            public int usuario_autorizacion_id { get; set; }

            public static Table<ConceptoAutorizacion> ConceptosAutorizaciones()
            {
                return Nori.CrearContexto().GetTable<ConceptoAutorizacion>();
            }

            public static ConceptoAutorizacion Obtener(string codigo)
            {
                try
                {
                    return ConceptosAutorizaciones().Where(x => x.codigo == codigo).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return null;
                }
            }

            public bool Actualizar()
            {
                try
                {
                    var Tabla = ConceptosAutorizaciones();
                    Autorizacion.ConceptoAutorizacion concepto_autorizacion = Tabla.Where(x => x.codigo == codigo).First();
                    this.CopyProperties(concepto_autorizacion);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            public static List<ConceptoAutorizacion> Listar()
            {
                return ConceptosAutorizaciones().ToList();
            }
        }
        #endregion
    }
}
