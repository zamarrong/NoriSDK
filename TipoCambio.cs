using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "tipos_cambio")]
    public class TipoCambio
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int moneda_id { get; set; }
        [Column]
        public DateTime fecha { get; set; }
        [Column]
        public decimal compra { get; set; }
        [Column]
        public decimal venta { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<TipoCambio> TiposCambio()
        {
            return Nori.CrearContexto().GetTable<TipoCambio>();
        }

        public TipoCambio()
        {
            moneda_id = Global.Configuracion.moneda_id;
            fecha = DateTime.Now;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static TipoCambio Obtener(int id)
        {
            try
            {
                return TiposCambio().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new TipoCambio();
            }
        }

        public static TipoCambio ObtenerTipoCambio(int moneda_id)
        {
            try
            {
                return TiposCambio().Where(x => x.moneda_id == moneda_id).OrderByDescending(x => x.id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new TipoCambio();
            }
        }
        public static TipoCambio ObtenerTipoCambioXFecha(DateTime fecha, int moneda_id)
        {
            try
            {
                return TiposCambio().Where(x => x.fecha == fecha).Where(x => x.moneda_id == moneda_id).OrderByDescending(x => x.id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new TipoCambio();
            }
        }

        public static decimal Compra(int moneda_id)
        {
            try
            {
                return TiposCambio().Where(x => x.moneda_id == moneda_id).OrderByDescending(x => x.id).Select(x => new { x.compra }).First().compra;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return 0;
            }
        }

        public static decimal Venta(int moneda_id)
        {
            try
            {
                return TiposCambio().Where(x => x.moneda_id == moneda_id).OrderByDescending(x => x.id).Select(x => new { x.venta}).First().venta;
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
                var Tabla = TiposCambio();
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
                var Tabla = TiposCambio();
                TipoCambio tipo_cambio = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(tipo_cambio);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public static decimal Convertir(int moneda_origen_id, int moneda_destino_id, char tipo = 'V', decimal cantidad = 1)
        {
            try
            {
                TipoCambio origen = ObtenerTipoCambio(moneda_origen_id);
                TipoCambio destino = ObtenerTipoCambio(moneda_destino_id);
                switch (tipo)
                {
                    case 'V':
                        return (origen.venta * cantidad) / destino.venta;
                    case 'C':
                        return (origen.compra * cantidad) / destino.compra;
                    default:
                        return 0;
                }
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return 0;
            }
        }
    }
}
