using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "condiciones_pago")]
    public class CondicionesPago
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int lista_precio_id { get; set; }
        [Column]
        public short codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public decimal porcentaje_descuento_total { get; set; }
        [Column]
        public decimal porcentaje_interes { get; set; }
        [Column]
        public decimal porcentaje_interes_moratorio { get; set; }
        [Column]
        public short dias_extra { get; set; }
        [Column]
        public int plazos { get; set; }
        [Column]
        public short dias_tolerancia { get; set; }
        [Column]
        public decimal limite_maximo { get; set; }
        [Column]
        public decimal limite_comprometido { get; set; }
        [Column]
        public bool financiado { get; set; }
        [Column]
        public bool pago_requerido { get; set; }
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

        public static Table<CondicionesPago> CondicionesPagos()
        {
            return Nori.CrearContexto().GetTable<CondicionesPago>();
        }

        public CondicionesPago()
        {
            lista_precio_id = Global.Configuracion.lista_precio_id;
            porcentaje_descuento_total = 0;
            porcentaje_interes = porcentaje_interes_moratorio = 0;
            dias_extra = 0;
            plazos = 1;
            dias_tolerancia = 0;
            limite_maximo = 0;
            limite_comprometido = 0;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static CondicionesPago Obtener(int id)
        {
            try
            {
                return CondicionesPagos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new CondicionesPago();
            }
        }

        public static CondicionesPago Obtener(short codigo)
        {
            try
            {
                return CondicionesPagos().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new CondicionesPago();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = CondicionesPagos();
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
                var Tabla = CondicionesPagos();
                CondicionesPago condicionespago = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(condicionespago);
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
