using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "series")]
    public class Serie
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string clase { get; set; }
        [Column]
        public short codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string prefijo { get; set; }
        [Column]
        public string subfijo { get; set; }
        [Column]
        public int inicial { get; set; }
        [Column]
        public int siguiente { get; set; }
        [Column]
        public int final { get; set; }
        [Column]
        public bool predeterminado { get; set; }
        [Column]
        public bool predeterminado_cancelacion { get; set; }
        [Column]
        public bool digital { get; set; }
        [Column]
        public bool facturar_automaticamente { get; set; }
        [Column]
        public int serie_facturacion_id { get; set; }
        [Column]
        public int almacen_id { get; set; }
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

        public static Table<Serie> Series()
        {
            return Nori.CrearContexto().GetTable<Serie>();
        }

        public Serie()
        {
            clase = Documento.Clase.ObtenerPredeterminado().clase;
            nombre = "Serie";
            prefijo = string.Empty;
            subfijo = string.Empty;
            inicial = 100000;
            siguiente = inicial + 1;
            final = 999999;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Serie Obtener(int id)
        {
            try
            {
                return Series().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Serie();
            }
        }
        public static Serie Obtener(short codigo)
        {
            try
            {
                return Series().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Serie();
            }
        }
        public static Serie ObtenerDocumentos(string clase)
        {
            try
            {
                return Series().Where(x => x.clase == clase && x.activo == true).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Serie();
            }
        }

        public static Serie ObtenerPredeterminado(string clase)
        {
            try
            {
                int serie_id = Utilidades.EjecutarEscalar(string.Format("select serie_id from usuarios_series inner join series on series.id = usuarios_series.serie_id where series.clase = '{0}' and usuarios_series.usuario_id = {1}", clase, Global.Usuario.id));
                
                if (Series().Any(x => x.clase == clase && x.almacen_id == Global.Usuario.almacen_id))
                {
                    serie_id = Series().Where(x => x.clase == clase && x.almacen_id == Global.Usuario.almacen_id).Select(x => new { x.id }).First().id;
                }

                return (serie_id == 0) ? Series().Where(x => x.clase == clase && x.predeterminado == true && x.activo == true).First() : Series().Where(x => x.id == serie_id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Serie();
            }
        }

        public static List<Serie> ObtenerSeriesPorDocumento(string clase)
        {
            try
            {
                return Series().Where(x => x.clase == clase && x.activo == true).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Serie>();
            }
        }

        public bool EstablecerSiguiente()
        {
            try
            {
                var Tabla = Series();
                Serie serie = Tabla.Where(x => x.id == id).First();
                serie.siguiente += 1;
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool Agregar()
        {
            try
            {
                if ((facturar_automaticamente || serie_facturacion_id != 0) && !clase.Equals("EN"))
                {
                    facturar_automaticamente = false;
                    serie_facturacion_id = 0;
                }

                var Tabla = Series();
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
                if ((facturar_automaticamente || serie_facturacion_id != 0) && !clase.Equals("EN"))
                {
                    facturar_automaticamente = false;
                    serie_facturacion_id = 0;
                }

                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
                var Tabla = Series();
                Serie serie = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(serie);
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
