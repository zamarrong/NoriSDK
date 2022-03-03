using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "grupos_descuento")]
    public class GrupoDescuento
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int grupo_socio_id { get; set; }
        [Column]
        public int socio_id { get; set; }
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

        public static Table<GrupoDescuento> GruposDescuento()
        {
            return Nori.CrearContexto().GetTable<GrupoDescuento>();
        }

        public GrupoDescuento()
        {
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static GrupoDescuento Obtener(int id)
        {
            try
            {
                return GruposDescuento().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new GrupoDescuento();
            }
        }

        public List<Linea> ObtenerLineas()
        {
            try
            {
                return Linea.Lineas().Where(x => x.grupo_descuento_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Linea>();
            }
        }

        public static decimal ObtenerDescuento(int articulo_id, int grupo_socio_id, int socio_id = 0)
        {
            try
            {
                GrupoDescuento grupo_descuento = GruposDescuento().Where(x => x.grupo_socio_id == grupo_socio_id || x.socio_id == socio_id).First();
                string query = "";
                string query_propiedades_articulos = "";
                List <Linea> lineas = grupo_descuento.ObtenerLineas();
                foreach (Linea linea in lineas)
                {
                    query += string.Format("{0} = 1 or ", linea.columna);
                    query_propiedades_articulos += string.Format("{0}, ", linea.columna);
                }

                query = query.Remove(query.Length - 4);
                query_propiedades_articulos = query_propiedades_articulos.Remove(query_propiedades_articulos.Length - 2);

                DataTable resultados = Utilidades.EjecutarQuery(string.Format("select top 1 {0} from articulos where id = {1} and ({2})", query_propiedades_articulos, articulo_id, query));

                query = "";
                for (int i = 0; i < resultados.Columns.Count; i++)
                {
                    if ((bool)resultados.Rows[0][i])
                        query += string.Format("'{0}',", resultados.Columns[i].ColumnName);
                }
                query = query.TrimEnd(',');

                query = string.Format("select max(descuento) from lineas_grupos_descuento join grupos_descuento on grupos_descuento.id = lineas_grupos_descuento.grupo_descuento_id where (grupos_descuento.grupo_socio_id = {0} or grupos_descuento.socio_id = {1}) and columna in ({2})", grupo_socio_id, socio_id, query);
                return Utilidades.EjecutarDecimal(query);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return 0;
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = GruposDescuento();
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

                var Tabla = GruposDescuento();
                GrupoDescuento grupo_descuento = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(grupo_descuento);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Linea
        [Table(Name = "lineas_grupos_descuento")]
        public class Linea
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int grupo_descuento_id { get; set; }
            [Column]
            public string tabla { get; set; }
            [Column]
            public string columna { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Linea> Lineas()
            {
                return Nori.CrearContexto().GetTable<Linea>();
            }

            public Linea()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Linea Obtener(int id)
            {
                try
                {
                    return Lineas().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Linea();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Lineas();
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

                    var Tabla = Lineas();
                    Linea linea = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(linea);
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
