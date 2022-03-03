using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "etiquetas")]
    public class Etiqueta
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int tipo_etiqueta_id { get; set; }
        [Column]
        public int almacen_id { get; set; }
        [Column]
        public bool impreso { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public List<Partida> partidas { get; set; }

        public static Table<Etiqueta> Etiquetas()
        {
            return Nori.CrearContexto().GetTable<Etiqueta>();
        }

        public Etiqueta()
        {
            almacen_id = Global.Usuario.almacen_id;
            impreso = false;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;

            partidas = new List<Partida>();
        }

        public static Etiqueta Obtener(int id)
        {
            try
            {
                var etiqueta = Etiquetas().Where(x => x.id == id).First();
                etiqueta.partidas = etiqueta.ObtenerPartidas();

                return etiqueta;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Etiqueta();
            }
        }

        public static List<Etiqueta> ObtenerPorUsuario(int usuario_id)
        {
            try
            {
                return Etiquetas().Where(x => x.usuario_creacion_id == usuario_id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Etiqueta>();
            }
        }

        public List<Partida> ObtenerPartidas()
        {
            try
            {
                var partidas = Partida.Partidas().Where(x => x.etiqueta_id == id).ToList();
                partidas.ForEach(x => x.Obtener());
                return partidas;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Partida>();
            }
        }
 
        public bool AgregarPartida(string q)
        {
            try
            {
                Partida partida = new Partida();

                if (q.Contains("*"))
                {
                    partida.cantidad = int.Parse(q.Split('*')[0]);
                    q = q.Split('*')[1];
                }
                var articulo = Articulo.Articulos().Where(x => (x.sku == q || x.codigo_barras == q) && x.activo == true).Select(x => new { x.id, x.sku, x.unidad_medida_id, x.nombre}).FirstOrDefault();

                if (articulo.IsNullOrEmpty())
                {
                    var codigo_barras = Articulo.CodigoBarras.CodigosBarras().Where(x => x.codigo == q).FirstOrDefault();
                    if (codigo_barras.IsNullOrEmpty())
                    {
                        Global.Error = new Nori.Error("Artículo no encontrado.");
                        return false;
                    }
                    else
                    {
                        articulo = Articulo.Articulos().Where(x => x.id == codigo_barras.articulo_id && x.activo == true).Select(x => new { x.id, x.sku, x.unidad_medida_id, x.nombre }).FirstOrDefault();
                        if (articulo.IsNullOrEmpty())
                        {
                            Global.Error = new Nori.Error("Artículo no encontrado, inactivo.");
                            return false;
                        }
                        partida.unidad_medida_id = codigo_barras.unidad_medida_id;
                    }
                }
                else
                {
                    partida.unidad_medida_id = articulo.unidad_medida_id;
                }

                if (articulo.IsNullOrEmpty())
                {
                    Global.Error = new Nori.Error("Artículo no encontrado.");
                    return false;
                }

                partida.articulo_id = articulo.id;
                partida.sku = articulo.sku;
                partida.nombre = articulo.nombre;

                bool existente = false;

                if (partidas.Count > 0)
                {
                    if (partidas.Any(x => x.articulo_id == partida.articulo_id && x.unidad_medida_id == partida.unidad_medida_id))
                    {
                        int cantidad = partida.cantidad;
                        partida = partidas.Where(x => x.articulo_id == partida.articulo_id && x.unidad_medida_id == partida.unidad_medida_id).First();
                        partida.cantidad = partida.cantidad + cantidad;
                        existente = true;
                    }
                }

                if (!existente)
                    partidas.Add(partida);

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
                if (partidas.Count > 0)
                {
                    var Tabla = Etiquetas();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();

                    partidas.All(x => { x.etiqueta_id = id; return true; });
                    partidas.ForEach(x => x.Agregar());

                    return true;
                }
                else
                {
                    Global.Error = new Nori.Error("No se puede agregar una solicitud de etiquetas sin partidas.");
                    return false;
                }
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
                var Tabla = Etiquetas();
                Etiqueta etiqueta = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(etiqueta);
                Tabla.Context.SubmitChanges();

                partidas.Where(x => x.id == 0).All(x => { x.etiqueta_id = id; x.Agregar(); return true; });

                foreach (Partida partida in Partida.Partidas().Where(x => x.etiqueta_id == id).ToList())
                {
                    if (partidas.Any(x => x.articulo_id == partida.articulo_id))
                        partidas.Where(x => x.articulo_id == partida.articulo_id && x.etiqueta_id == id).First().Actualizar();
                    else
                        partida.Eliminar();
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Partidas
        [Table(Name = "partidas_etiquetas")]
        public class Partida
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int etiqueta_id { get; set; }
            [Column]
            public int unidad_medida_id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int cantidad { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public string sku { get; internal set; }
            public string nombre { get; internal set; }

            public static Table<Partida> Partidas()
            {
                return Nori.CrearContexto().GetTable<Partida>();
            }

            public Partida()
            {
                cantidad = 1;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Partida Obtener(int id)
            {
                try
                {
                    return Partidas().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Partida();
                }
            }

            public void Obtener()
            {
                try
                {
                    var articulo = Articulo.Articulos().Where(x => x.id == articulo_id).Select(x => new { x.sku, x.nombre }).First();
                    sku = articulo.sku;
                    nombre = articulo.nombre;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Partidas();
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
                    var Tabla = Partidas();
                    Partida partida = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(partida);
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
                    var Tabla = Partidas();
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
        #endregion

        #region Tipos
        [Table(Name = "tipos_etiquetas")]
        public class Tipo
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public string nombre{ get; set; }
            [Column]
            public string formato{ get; set; }
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
