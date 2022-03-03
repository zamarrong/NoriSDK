using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "flujo")]
    public class Flujo
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int documento_id { get; set; }
        [Column]
        public int pago_id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public int estacion_id { get; set; }
        [Column]
        public int tipo_metodo_pago_id { get; set; }
        [Column]
        public decimal tipo_cambio { get; set; }
        [Column]
        public decimal importe { get; set; }
        [Column]
        public string referencia { get; set; }
        [Column]
        public string comentario { get; set; }
        [Column]
        public int autorizacion_id { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Flujo> Flujos()
        {
            return Nori.CrearContexto().GetTable<Flujo>();
        }

        public Flujo()
        {
            try
            {
                tipo_metodo_pago_id = MetodoPago.MetodosPago().Where(x => x.id == Global.Configuracion.metodo_pago_id).Select(x => x.tipo_metodo_pago_id).First();
            }
            catch { }
            tipo_cambio = 1;
            estacion_id = Global.Estacion.id;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Flujo Obtener(int id)
        {
            try
            {
                return Flujos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Flujo();
            }
        }

        public bool Agregar()
        {
            try
            {
                if (id != 0)
                {
                    Global.Error = new Nori.Error("Flujo existente.");
                    return false;
                }

                if (codigo.IsNullOrEmpty())
                {
                    Global.Error = new Nori.Error("El código del flujo es obligatorio.");
                    return false;
                }

                if (tipo_cambio == 0)
                    EstablecerTipoCambio();

                if (importe == 0)
                {
                    Global.Error = new Nori.Error("No se puede agregar un flujo con importe cero.");
                    return false;
                }

                var Tabla = Flujos();
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
                var Tabla = Flujos();
                Flujo flujo = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(flujo);
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
                var Tabla = Flujos();
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

        public void EstablecerTipoCambio()
        {
            try
            {
                MetodoPago.Tipo tipo_metodo_pago = MetodoPago.Tipo.Obtener(tipo_metodo_pago_id);
                tipo_cambio = tipo_metodo_pago.ObtenerTipoCambio();
            }
            catch
            {
                tipo_cambio = 1;
            }
        }

        #region Conceptos
        [Table(Name = "conceptos_flujo")]
        public class ConceptoFlujo
        {
            public static Table<ConceptoFlujo> ConceptosFlujo = Nori.CrearContexto().GetTable<ConceptoFlujo>();
            [Column]
            public char tipo { get; set; }
            [Column(IsPrimaryKey = true)]
            public string codigo { get; set; }
            [Column]
            public string nombre { get; set; }

            public static ConceptoFlujo Obtener(string codigo)
            {
                try
                {
                    return ConceptosFlujo.Where(x => x.codigo == codigo).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return null;
                }
            }

            public static List<ConceptoFlujo> Listar()
            {
                return ConceptosFlujo.ToList();
            }
        }
        #endregion
    }
}