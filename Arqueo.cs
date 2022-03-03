using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace NoriSDK
{
    [Table(Name = "arqueos")]
    public class Arqueo
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int estacion_id { get; set; }
        [Column]
        public int flujo_id { get; set; }
        [Column]
        public int tipo_metodo_pago_id { get; set; }
        [Column]
        public decimal cantidad { get; set; }
        [Column]
        public decimal factor { get; set; }
        [Column]
        public decimal producto { get; set; }
        [Column]
        public decimal tipo_cambio { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Arqueo> Arqueos()
        {
            return Nori.CrearContexto().GetTable<Arqueo>();
        }

        public Arqueo()
        {
            estacion_id = Global.Estacion.id;
            cantidad = 1;
            factor = 0;
            producto = 0;
            tipo_cambio = 1;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public bool Validar(string valor)
        {
            try
            {
                if (valor.Contains("*"))
                {
                    cantidad = decimal.Parse(valor.Split('*')[0]);
                    valor = valor.Split('*')[1];
                }

                factor = decimal.Parse(valor);
                Calcular();

                if (producto <= 0)
                {
                    Global.Error = new Nori.Error("El producto de la operación no puede ser <= 0.");
                    return false;
                }

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
                var Tabla = Arqueos();
                EstablecerTipoCambio();
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

        public void Calcular()
        {
            producto = cantidad * factor;
        }
    }
}
