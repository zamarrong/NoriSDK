using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "metodos_pago")]
    public class MetodoPago
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public int tipo_metodo_pago_id { get; set; }
        [Column]
        public char tipo { get; set; }
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

        public static Table<MetodoPago> MetodosPago()
        {
            return Nori.CrearContexto().GetTable<MetodoPago>();
        }

        public MetodoPago()
        {
            tipo_metodo_pago_id = 0;
            activo = true;
            tipo = 'E';
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static MetodoPago Obtener(int id)
        {
            try
            {
                return MetodosPago().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new MetodoPago();
            }
        }

        public static MetodoPago Obtener(string codigo)
        {
            try
            {
                return MetodosPago().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new MetodoPago();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = MetodosPago();
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
                var Tabla = MetodosPago();
                MetodoPago metodo_pago = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(metodo_pago);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Tipo
        [Table(Name = "tipos_metodos_pago")]
        public class Tipo
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int metodo_pago_id { get; set; }
            [Column]
            public int moneda_id { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]
            public string clase { get; set; }
            [Column]
            public string cuenta_contable { get; set; }
            [Column]
            public string codigo { get; set; }
            [Column]
            public char tipo_cambio { get; set; }
            [Column]
            public bool referencia { get; set; }
            [Column]
            public bool cambio { get; set; }
            [Column]
            public bool canjeable { get; set; }
            [Column]
            public bool documento{ get; set; }
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
                metodo_pago_id = Global.Configuracion.metodo_pago_id;
                moneda_id = Global.Configuracion.moneda_id;
                nombre = string.Empty;
                cuenta_contable = string.Empty;
                codigo = string.Empty;
                clase = Clase.ObtenerPredeterminado();
                tipo_cambio = TipoCambio.ObtenerPredeterminado();
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

            public decimal ObtenerTipoCambio()
            {
                try
                {
                    if (tipo_cambio.Equals('C'))
                        return NoriSDK.TipoCambio.Compra(moneda_id);
                    else
                        return NoriSDK.TipoCambio.Venta(moneda_id);
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

            #region Clase
            public class Clase
            {
                public string clase { get; set; }
                public string nombre { get; set; }
                public static List<Clase> Clases()
                {
                    List<Clase> clases = new List<Clase>();

                    Clase clase = new Clase();

                    clase.clase = "EF";
                    clase.nombre = "Fondos en efectivo";
                    clases.Add(clase);

                    clase = new Clase();

                    clase.clase = "CH";
                    clase.nombre = "Cheque";
                    clases.Add(clase);

                    clase = new Clase();

                    clase.clase = "TR";
                    clase.nombre = "Transferencia bancaria";
                    clases.Add(clase);

                    clase = new Clase();

                    clase.clase = "TC";
                    clase.nombre = "Tarjeta de crédito / débito";
                    clases.Add(clase);

                    clase = new Clase();

                    clase.clase = "NA";
                    clase.nombre = "No aplica";
                    clases.Add(clase);

                    return clases;
                }

                public static string ObtenerPredeterminado()
                {
                    return "EF";
                }
            }
            #endregion

            #region TipoCambio
            public class TipoCambio
            {
                public char tipo { get; set; }
                public string nombre { get; set; }
                public static List<TipoCambio> Tipos()
                {
                    List<TipoCambio> tipos = new List<TipoCambio>();

                    TipoCambio tipo = new TipoCambio();

                    tipo.tipo = 'C';
                    tipo.nombre = "Compra";
                    tipos.Add(tipo);

                    tipo = new TipoCambio();

                    tipo.tipo = 'V';
                    tipo.nombre = "Venta";
                    tipos.Add(tipo);

                    return tipos;
                }

                public static char ObtenerPredeterminado()
                {
                    return 'V';
                }
            }
            #endregion
        }
        #endregion
    }
}
