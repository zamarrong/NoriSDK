using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace NoriSDK
{
    #region Articulo
    [Table(Name = "articulos")]
    public class Articulo
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int grupo_articulo_id { get; set; }
        [Column]
        public int grupo_unidad_medida_id { get; set; }
        [Column]
        public int unidad_medida_id { get; set; }
        [Column]
        public int unidad_medida_compra_id { get; set; }
        [Column]
        public int unidad_medida_venta_id { get; set; }
        [Column]
        public int fabricante_id { get; set; }
        [Column]
        public int socio_id { get; set; }
        [Column]
        public int almacen_id { get; set; }
        [Column]
        public int tipo_empaque_id { get; set; }
        [Column]
        public bool almacenable { get; set; }
        [Column]
        public bool venta { get; set; }
        [Column]
        public bool compra { get; set; }
        [Column]
        public bool canjeable { get; set; }
        [Column]
        public char seguimiento { get; set; }
        [Column]
        public short dias_garantia { get; set; }
        [Column]
        public string sku { get; set; }
        [Column]
        public string sku_fabricante { get; set; }
        [Column]
        public decimal pedido_minimo { get; set; }
        [Column]
        public string id_adicional { get; set; }
        [Column]
        public string codigo_clasificacion { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string nombre_api { get; set; }
        [Column]
        public string descripcion { get; set; }
        [Column]
        public string clave_unidad { get; set; }
        [Column]
        public string imagen { get; set; }
        [Column]
        public string codigo_barras { get; set; }
        [Column]
        public decimal peso { get; set; }
        [Column]
        public decimal cantidad_compra { get; set; }
        [Column]
        public decimal cantidad_venta { get; set; }
        [Column]
        public decimal cantidad_paquete_compra { get; set; }
        [Column]
        public decimal cantidad_paquete { get; set; }
        [Column]
        public bool pesable { get; set; }
        [Column]
        public decimal ajuste_maximo { get; set; }
        [Column]
        public decimal ajuste_minimo { get; set; }
        [Column]
        public decimal ultimo_precio_compra { get; set; }
        [Column]
        public decimal ultimo_precio_determinado { get; set; }
        //Impuestos
        [Column]
        public bool sujeto_retencion { get; set; }
        [Column]
        public bool sujeto_impuesto { get; set; }
        [Column]
        public bool ieps { get; set; }
        [Column]
        public int impuesto_compra_id { get; set; }
        [Column]
        public int impuesto_venta_id { get; set; }
        //Impuestos
        [Column]
        public string comentario { get; set; }
        [Column]
        public bool api { get; set; }
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
        [Column]
        public bool propiedad_1 { get; set; }
        [Column]
        public bool propiedad_2 { get; set; }
        [Column]
        public bool propiedad_3 { get; set; }
        [Column]
        public bool propiedad_4 { get; set; }
        [Column]
        public bool propiedad_5 { get; set; }
        [Column]
        public bool propiedad_6 { get; set; }
        [Column]
        public bool propiedad_7 { get; set; }
        [Column]
        public bool propiedad_8 { get; set; }
        [Column]
        public bool propiedad_9 { get; set; }
        [Column]
        public bool propiedad_10 { get; set; }
        [Column]
        public bool propiedad_11 { get; set; }
        [Column]
        public bool propiedad_12 { get; set; }
        [Column]
        public bool propiedad_13 { get; set; }
        [Column]
        public bool propiedad_14 { get; set; }
        [Column]
        public bool propiedad_15 { get; set; }
        [Column]
        public bool propiedad_16 { get; set; }
        [Column]
        public bool propiedad_17 { get; set; }
        [Column]
        public bool propiedad_18 { get; set; }
        [Column]
        public bool propiedad_19 { get; set; }
        [Column]
        public bool propiedad_20 { get; set; }
        [Column]
        public bool propiedad_21 { get; set; }
        [Column]
        public bool propiedad_22 { get; set; }
        [Column]
        public bool propiedad_23 { get; set; }
        [Column]
        public bool propiedad_24 { get; set; }
        [Column]
        public bool propiedad_25 { get; set; }
        [Column]
        public bool propiedad_26 { get; set; }
        [Column]
        public bool propiedad_27 { get; set; }
        [Column]
        public bool propiedad_28 { get; set; }
        [Column]
        public bool propiedad_29 { get; set; }
        [Column]
        public bool propiedad_30 { get; set; }
        [Column]
        public bool propiedad_31 { get; set; }
        [Column]
        public bool propiedad_32 { get; set; }
        public decimal stock { get; set; }

        public static Table<Articulo> Articulos()
        {
            return Nori.CrearContexto().GetTable<Articulo>();
        }

        public Articulo()
        {
            grupo_articulo_id = Global.Configuracion.grupo_articulo_id;
            unidad_medida_id = Global.Configuracion.unidad_medida_id;
            fabricante_id = Global.Configuracion.fabricante_id;
            impuesto_compra_id = Global.Configuracion.impuesto_id;
            impuesto_venta_id = Global.Configuracion.impuesto_id;
            seguimiento = 'N';
            clave_unidad = "H87";
            codigo_clasificacion = "01010101";
            imagen = string.Empty;
            compra = venta = almacenable = true;
            sujeto_retencion = true;
            sujeto_impuesto = true;
            nombre_api = comentario = string.Empty;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }
    
        public static Articulo Obtener(int id)
        {
            try
            {
                return Articulos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Articulo();
            }
        }

        public static Articulo Obtener(string sku)
        {
            try
            {
                return Articulos().Where(x => x.sku == sku).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Articulo();
            }
        }

        public static Impuesto ObtenerImpuesto(int articulo_id, int socio_id)
        {
            Impuesto impuesto = new Impuesto();
            try
            {
                var articulo = Articulos().Where(x => x.id == articulo_id).Select(x => new { x.sujeto_impuesto, x.impuesto_compra_id, x.impuesto_venta_id }).First();
                var socio = Socio.Socios().Where(x => x.id == socio_id).Select(x => new { x.tipo, x.direccion_envio_id }).First();

                if (articulo.sujeto_impuesto)
                {
                    var impuesto_articulo = ImpuestoArticulo.ObtenerPorArticulo(articulo_id);

                    if (impuesto_articulo.impuesto_id != 0)
                        return Impuesto.Obtener(impuesto_articulo.impuesto_id);

                    if (articulo.impuesto_compra_id != 0 && socio.tipo.Equals('P'))
                        return Impuesto.Obtener(articulo.impuesto_compra_id);
                    else if (articulo.impuesto_venta_id != 0 && (socio.tipo.Equals('C') || socio.tipo.Equals('L')))
                        return Impuesto.Obtener(articulo.impuesto_venta_id);

                    if (socio.direccion_envio_id != 0)
                    {
                        Socio.Direccion direccion = Socio.Direccion.Obtener(socio.direccion_envio_id);
                        if (direccion.impuesto_id != 0)
                            return Impuesto.Obtener(direccion.impuesto_id);
                    }

                    if (Global.Configuracion.impuesto_id != 0)
                        return Impuesto.Obtener(Global.Configuracion.impuesto_id);
                }

                return impuesto;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return impuesto;
            }
        }

        public static decimal ObtenerDescuento(int articulo_id, int socio_id, int lista_precio_id, decimal cantidad = 0)
        {
            try
            {
                try
                {
                    int grupo_socio_id = Socio.Socios().Where(x => x.id == socio_id).Select(x => x.grupo_socio_id).First();
                    decimal grupo_descuento = GrupoDescuento.ObtenerDescuento(articulo_id, grupo_socio_id, socio_id);

                    if (grupo_descuento > 0)
                        return grupo_descuento;
                }
                catch { }

                var descuento_cantidad = Descuento.Cantidad.Obtener(cantidad, articulo_id, socio_id);

                if (descuento_cantidad.id != 0)
                    return descuento_cantidad.descuento;

                var descuento_periodo = Descuento.Periodo.ObtenerPeriodo(articulo_id, socio_id, lista_precio_id);

                if (descuento_periodo.id != 0)
                    return descuento_periodo.descuento;

                var descuento = Descuento.Obtener(articulo_id, socio_id, lista_precio_id);

                if (descuento.id != 0)
                    return descuento.descuento;

                return 0;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return 0;
            }
        }

        public decimal Stock()
        {
            try
            {
                return (id != 0 && almacenable) ? Inventario.Inventarios().Where(x => x.articulo_id == id).Sum(x => x.stock) : 0;
            }
            catch { return 0; }
        }

        public List<Ubicacion> Ubicaciones()
        {
            return  Ubicacion.Ubicaciones().Where(x => x.articulo_id == id).ToList();
        }

        public List<Inventario> Inventarios()
        {
            return Inventario.Inventarios().Where(x => x.articulo_id == id).ToList();
        }

        public List<Precio> Precios()
        {
            return Precio.Precios().Where(x => x.articulo_id == id).ToList();
        }

        public List<CodigoBarras> CodigosBarras()
        {
            return CodigoBarras.CodigosBarras().Where(x => x.articulo_id == id).ToList();
        }

        public static List<UnidadMedida> UnidadesMedida(int articulo_id)
        {
            try
            {
                int grupo_unidad_medida_id = Articulos().Where(x => x.id == articulo_id).Select(x => x.grupo_unidad_medida_id).FirstOrDefault();
                List<int> unidades_medida = UnidadMedida.Grupo.Linea.Lineas().Where(x => x.grupo_unidad_medida_id == grupo_unidad_medida_id).Select(x => x.unidad_medida_id).Distinct().ToList();
                return UnidadMedida.UnidadesMedida().Where(x => unidades_medida.Contains(x.id)).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<UnidadMedida>();
            }
        }

        public List<Ruta> Rutas()
        {
            return Ruta.Rutas().Where(x => x.articulo_id == id).ToList();
        }

        public List<Grupo> Grupos()
        {
            return Grupo.Grupos().Where(x => x.articulo_id == id).ToList();
        }

        public static string ObtenerImagen(int id)
        {
            try
            {
                return Articulos().Where(x => x.id == id).Select(x => new { x.imagen }).First().imagen;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }

        public static string ObtenerComentario(int id)
        {
            try
            {
                return Articulos().Where(x => x.id == id).Select(x => new { x.comentario }).First().comentario;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Articulos();
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();

                if (almacen_id != 0)
                {
                    if(!Inventario.Inventarios().Any(x => x.articulo_id == id && x.almacen_id == almacen_id))
                    {
                        Inventario inventario = new Inventario();
                        inventario.articulo_id = id;
                        inventario.almacen_id = almacen_id;
                        inventario.Agregar();
                    }
                }

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
                var Tabla = Articulos();
                Articulo articulo = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(articulo);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Precio
        [Table(Name = "precios")]
        public class Precio
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int lista_precio_id { get; set; }
            [Column]
            public int moneda_id { get; set; }
            [Column]
            public int unidad_medida_id { get; set; }
            [Column]
            public decimal precio { get; set; }
            [Column]
            public decimal multiplicador_puntos { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Precio> Precios()
            {
                return Nori.CrearContexto().GetTable<Precio>();
            }

            public Precio()
            {
                lista_precio_id = Global.Configuracion.lista_precio_id;
                moneda_id = Global.Configuracion.moneda_id;
                precio = 0;
                multiplicador_puntos = 0;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Precio Obtener(int articulo_id, int lista_precio_id, int unidad_medida_id = 0)
            {
                try
                {
                    //Precio precio = (unidad_medida_id.Equals(0)) ? Precios().Where(x => x.articulo_id == articulo_id && x.lista_precio_id == lista_precio_id).FirstOrDefault() : Precios().Where(x => x.articulo_id == articulo_id && x.lista_precio_id == lista_precio_id && x.unidad_medida_id == unidad_medida_id).FirstOrDefault();

                    return Precios().Where(x => x.articulo_id == articulo_id && x.lista_precio_id == lista_precio_id && x.unidad_medida_id == unidad_medida_id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Precio();
                }
            }

            public static List<UnidadMedida> UnidadesMedidas(int articulo_id, int lista_precio_id)
            {
                try
                {
                    List<int> unidades_medida = Precios().Where(x => x.articulo_id == articulo_id && x.lista_precio_id == lista_precio_id).Select(x => x.unidad_medida_id).Distinct().ToList();
                    return UnidadMedida.UnidadesMedida().Where(x => unidades_medida.Contains(x.id)).ToList();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new List<UnidadMedida>();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Precios();

                    if (articulo_id == 0 || lista_precio_id == 0)
                        return false;

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
                    var Tabla = Precios();
                    Precio precio = Tabla.Where(x => x.id == id).First();

                    if (precio.precio != this.precio || precio.unidad_medida_id != this.unidad_medida_id || precio.moneda_id != this.moneda_id || precio.multiplicador_puntos != this.multiplicador_puntos)
                    {
                        this.CopyProperties(precio);
                        Tabla.Context.SubmitChanges();
                    }

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

        #region Codigo de barras
        [Table(Name = "codigos_barras")]
        public class CodigoBarras
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int unidad_medida_id { get; set; }
            [Column]
            public string codigo { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]

            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<CodigoBarras> CodigosBarras()
            {
                return Nori.CrearContexto().GetTable<CodigoBarras>();
            }

            public CodigoBarras()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static CodigoBarras Obtener(int id)
            {
                try
                {
                    return CodigosBarras().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new CodigoBarras();
                }
            }

            public static CodigoBarras Obtener(string codigo)
            {
                try
                {
                    return CodigosBarras().Where(x => x.codigo == codigo).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new CodigoBarras();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = CodigosBarras();
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
                    var Tabla = CodigosBarras();
                    CodigoBarras codigo_barras = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(codigo_barras);
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
                    var Tabla = CodigosBarras();
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

        #region Ubicacion

        [Table(Name = "ubicaciones_articulos")]
        public class Ubicacion
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int almacen_id { get; set; }
            [Column]
            public string ubicacion { get; set; }
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

            public static Table<Ubicacion> Ubicaciones()
            {
                return Nori.CrearContexto().GetTable<Ubicacion>();
            }

            public Ubicacion()
            {
                almacen_id = Global.Usuario.almacen_id;
                activo = true;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Ubicacion Obtener(int id)
            {
                try
                {
                    return Ubicaciones().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Ubicacion();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Ubicaciones();
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
                    var Tabla = Ubicaciones();
                    Ubicacion ubicacion = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(ubicacion);
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
                    var Tabla = Ubicaciones();
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

        #region Inventario
        [Table(Name = "inventario")]
        public class Inventario
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int almacen_id { get; set; }
            [Column]
            public decimal stock { get; set; }
            [Column]
            public decimal comprometido { get; set; }
            [Column]
            public decimal pedido { get; set; }
            [Column]
            public decimal disponible { get; set; }
            [Column]
            public decimal stock_minimo { get; set; }
            [Column]
            public decimal stock_maximo { get; set; }
            [Column]
            public decimal punto_reorden { get; set; }
            [Column]
            public decimal costo { get; set; }
            [Column]
            public bool activo { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Inventario> Inventarios()
            {
                return Nori.CrearContexto().GetTable<Inventario>();
            }

            public Inventario()
            {
                stock = comprometido = pedido = disponible = stock_minimo = stock_maximo = punto_reorden = costo = 0;
                activo = true;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Inventario Obtener(int articulo_id, int almacen_id)
            {
                try
                {
                    return Inventarios().Where(x => x.articulo_id == articulo_id && x.almacen_id == almacen_id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Inventario();
                }
            }

            public bool Agregar()
            {
                try
                {
                    disponible = (stock + pedido) - comprometido;
                    var Tabla = Inventarios();
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
                    disponible = (stock + pedido) - comprometido;
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                    var Tabla = Inventarios();
                    Inventario inventario = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(inventario);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            #region Ubicacion
            [Table(Name = "inventario_ubicaciones")]
            public class Ubicacion
            {
                [Column(IsDbGenerated = true, IsPrimaryKey = true)]
                public int id { get; set; }
                [Column]
                public int articulo_id { get; set; }
                [Column]
                public int almacen_id { get; set; }
                [Column]
                public int ubicacion_id { get; set; }
                [Column]
                public decimal stock { get; set; }
                [Column]
                public DateTime fecha_creacion { get; set; }
                [Column]
                public int usuario_actualizacion_id { get; set; }
                [Column]
                public DateTime fecha_actualizacion { get; set; }

                public static Table<Ubicacion> Ubicaciones()
                {
                    return Nori.CrearContexto().GetTable<Ubicacion>();
                }

                public Ubicacion()
                {
                    stock = 0;
                    fecha_creacion = DateTime.Now;
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                }

                public static Ubicacion Obtener(int articulo_id, int almacen_id, int ubicacion_id)
                {
                    try
                    {
                        return Ubicaciones().Where(x => x.articulo_id == articulo_id && x.almacen_id == almacen_id && x.ubicacion_id == ubicacion_id).First();
                    }
                    catch (Exception ex)
                    {
                        Global.Error = new Nori.Error(ex.Message);
                        return new Ubicacion();
                    }
                }

                public bool Agregar()
                {
                    try
                    {
                        var Tabla = Ubicaciones();
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
                        var Tabla = Ubicaciones();
                        Ubicacion ubicacion = Tabla.Where(x => x.id == id).First();
                        this.CopyProperties(ubicacion);
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
        #endregion

        #region Ruta
        [Table(Name = "articulos_rutas")]
        public class Ruta
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int ruta_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Ruta> Rutas()
            {
                return Nori.CrearContexto().GetTable<Ruta>();
            }

            public Ruta()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Ruta Obtener(int id)
            {
                try
                {
                    return Rutas().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Ruta();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Rutas();

                    if (Rutas().Any(x => x.articulo_id == articulo_id && x.ruta_id == ruta_id))
                        return false;

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
                    var Tabla = Rutas();
                    Ruta ruta = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(ruta);
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

        #region Grupo
        [Table(Name = "articulos_grupos")]
        public class Grupo
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int grupo_articulo_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Grupo> Grupos()
            {
                return Nori.CrearContexto().GetTable<Grupo>();
            }

            public Grupo()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Grupo Obtener(int id)
            {
                try
                {
                    return Grupos().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Grupo();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Grupos();

                    if (Grupos().Any(x => x.articulo_id == articulo_id && x.grupo_articulo_id == grupo_articulo_id))
                        return false;

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
                    var Tabla = Grupos();
                    Grupo grupo = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(grupo);
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
    #endregion
}
