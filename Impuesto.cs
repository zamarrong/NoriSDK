using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "impuestos")]
    public class Impuesto
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public char tipo_factor { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public bool compra { get; set; }
        [Column]
        public bool venta { get; set; }
        [Column]
        public decimal porcentaje { get; set; }
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
        public List<Linea> lineas { get; set; }

        public static Table<Impuesto> Impuestos()
        {
            return Nori.CrearContexto().GetTable<Impuesto>();
        }

        public Impuesto()
        {
            tipo_factor = TipoFactor.ObtenerPredeterminado();
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
            lineas = new List<Linea>();
        }

        public static Impuesto Obtener(int id)
        {
            try
            {
                return Impuestos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Impuesto();
            }
        }

        public static Impuesto Obtener(string codigo)
        {
            try
            {
                return Impuestos().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Impuesto();
            }
        }


        public void ObtenerLineas()
        {
            try
            {
                lineas = Linea.Lineas().Where(x => x.impuesto_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                lineas = new List<Linea>();
            }
        }

        public static List<Linea> ObtenerLineas(int impuesto_id)
        {
            try
            {
                return Linea.Lineas().Where(x => x.impuesto_id == impuesto_id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Linea>();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Impuestos();
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();

                lineas.ForEach(x => x.Agregar());

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
                var Tabla = Impuestos();
                Impuesto impuesto = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(impuesto);
                Tabla.Context.SubmitChanges();

                if (lineas != null)
                {
                    foreach (Linea linea in lineas)
                    {
                        bool l = (linea.id == 0) ? linea.Agregar() : linea.Actualizar();
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

        #region TipoFactor
        public class TipoFactor
        {
            public char tipo { get; set; }
            public string nombre { get; set; }
            public static List<TipoFactor> Tipos()
            {
                List<TipoFactor> tipos = new List<TipoFactor>();

                TipoFactor tipo = new TipoFactor();

                tipo.tipo = 'T';
                tipo.nombre = "Tasa";
                tipos.Add(tipo);

                tipo = new TipoFactor();

                tipo.tipo = 'C';
                tipo.nombre = "Cuota";
                tipos.Add(tipo);

                tipo = new TipoFactor();

                tipo.tipo = 'E';
                tipo.nombre = "Exento";
                tipos.Add(tipo);

                return tipos;
            }

            public static char ObtenerPredeterminado()
            {
                return 'T';
            }
        }
        #endregion

        [Table(Name = "lineas_impuestos")]
        public class Linea
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int impuesto_id { get; set; }
            [Column]
            public string codigo { get; set; }
            [Column]
            public string impuesto { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]
            public decimal porcentaje { get; set; }
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
                impuesto = Impuesto.ObtenerPredeterminado();
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

            public static Linea Obtener(int impuesto_id, string codigo)
            {
                try
                {
                    return Lineas().Where(x => x.impuesto_id == impuesto_id && x.codigo == codigo).First();
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

            #region Impuestos
            public class Impuesto
            {
                public string nombre { get; set; }
                public string codigo { get; set; }
                public bool retencion { get; set; }
                public bool traslado { get; set; }
                public static List<Impuesto> Impuestos()
                {
                    List<Impuesto> impuestos = new List<Impuesto>();

                    Impuesto impuesto = new Impuesto();

                    impuesto.nombre = "ISR";
                    impuesto.codigo = "001";
                    impuesto.retencion = false;
                    impuesto.traslado = true;
                    impuestos.Add(impuesto);

                    impuesto = new Impuesto();

                    impuesto.nombre = "IVA";
                    impuesto.codigo = "002";
                    impuesto.retencion = true;
                    impuesto.traslado = true;
                    impuestos.Add(impuesto);

                    impuesto = new Impuesto();

                    impuesto.nombre = "IEPS";
                    impuesto.codigo = "003";
                    impuesto.retencion = true;
                    impuesto.traslado = true;

                    impuestos.Add(impuesto);

                    return impuestos;
                }

                public static string ObtenerPredeterminado()
                {
                    return "IVA";
                }
            }
            #endregion
        }
    }
}
