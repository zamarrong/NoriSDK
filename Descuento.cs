using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "descuentos")]
    public class Descuento
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
        public int socio_id { get; set; }
        [Column]
        public int unidad_medida_id { get; set; }
        [Column]
        public decimal descuento { get; set; }
        [Column]
        public decimal precio { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Descuento> Descuentos()
        {
            return Nori.CrearContexto().GetTable<Descuento>();
        }

        public Descuento()
        {
            articulo_id = 0;
            lista_precio_id = 0;
            moneda_id = 0;
            socio_id = 0;
            unidad_medida_id = 0;
            descuento = 0;
            precio = 0;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Descuento Obtener(int articulo_id, int socio_id, int lista_precio_id)
        {
            try
            {
                return Descuentos().Where(x => x.articulo_id == articulo_id && (x.socio_id == socio_id || x.socio_id == 0) && x.lista_precio_id == lista_precio_id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Descuento();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Descuentos();
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

                var Tabla = Descuentos();
                Descuento descuento = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(descuento);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Periodo
        [Table(Name = "descuentos_periodo")]
        public class Periodo
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
            public int socio_id { get; set; }
            [Column]
            public int unidad_medida_id { get; set; }
            [Column]
            public decimal descuento { get; set; }
            [Column]
            public decimal precio { get; set; }
            [Column]
            public DateTime fecha_inicio { get; set; }
            [Column]
            public DateTime fecha_fin { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Periodo> Descuentos()
            {
                return Nori.CrearContexto().GetTable<Periodo>();
            }

            public Periodo()
            {
                articulo_id = 0;
                moneda_id = 0;
                socio_id = 0;
                unidad_medida_id = 0;
                descuento = 0;
                precio = 0;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }
            public static Periodo Obtener(int articulo_id, int socio_id, int lista_precio_id)
            {
                try
                {
                    return Descuentos().Where(x => x.articulo_id == articulo_id && (x.socio_id == socio_id || x.socio_id == 0) && x.lista_precio_id == lista_precio_id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Periodo();
                }
            }

            public static Periodo ObtenerPeriodo(int articulo_id, int socio_id, int lista_precio_id)
            {
                try
                {
                    return Descuentos().Where(x => x.articulo_id == articulo_id && (x.socio_id == socio_id || x.socio_id == 0) && x.lista_precio_id == lista_precio_id && x.fecha_inicio <= DateTime.Now && x.fecha_fin >= DateTime.Now).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Periodo();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Descuentos();
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

                    var Tabla = Descuentos();
                    Periodo descuento_periodo = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(descuento_periodo);
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

        #region Cantidad
        [Table(Name = "descuentos_cantidad")]
        public class Cantidad
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int linea1 { get; set; }
            [Column]
            public int linea2 { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int moneda_id { get; set; }
            [Column]
            public int socio_id { get; set; }
            [Column]
            public int unidad_medida_id { get; set; }
            [Column]
            public decimal cantidad { get; set; }
            [Column]
            public decimal descuento { get; set; }
            [Column]
            public decimal precio { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Cantidad> Descuentos()
            {
                return Nori.CrearContexto().GetTable<Cantidad>();
            }

            public Cantidad()
            {
                articulo_id = 0;
                moneda_id = 0;
                socio_id = 0;
                unidad_medida_id = 0;
                descuento = 0;
                precio = 0;
                linea1 = linea2 = 0;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }
            public static Cantidad Obtener(int articulo_id, int socio_id, int linea1, int linea2)
            {
                try
                {
                    return Descuentos().Where(x => x.articulo_id == articulo_id && x.socio_id == socio_id && x.linea1 == linea1 && x.linea2 == linea2).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Cantidad();
                }
            }

            public static Cantidad Obtener(decimal cantidad, int articulo_id, int socio_id)
            {
                try
                {
                    return Descuentos().Where(x => x.articulo_id == articulo_id && (x.socio_id == socio_id || x.socio_id == 0) && cantidad >= x.cantidad).OrderByDescending(x => x.cantidad).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Cantidad();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Descuentos();
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

                    var Tabla = Descuentos();
                    Cantidad descuento_cantidad = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(descuento_cantidad);
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
