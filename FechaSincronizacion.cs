using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "fechas_sincronizacion")]
    public class FechaSincronizacion
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string tabla { get; set; }
        [Column]
        public DateTime fecha { get; set; }

        public enum Tablas
        {
            almacenes,
            articulos,
            condiciones_pago,
            clases_expedicion,
            ciclos,
            descuentos,
            documentos,
            empresa,
            estados,
            fabricantes,
            grupos_articulos,
            grupos_socios,
            impuestos,
            impuestos_articulos,
            listas_precios,
            metodos_pago,
            monedas,
            pagos,
            paises,
            socios,
            series,
            tipos_cambio,
            vendedores,
        }

        public static Table<FechaSincronizacion> FechasSincronizacion()
        {
            return Nori.CrearContexto().GetTable<FechaSincronizacion>();
        }

        public FechaSincronizacion()
        {
            fecha = new DateTime(1900, 1, 1);
        }

        internal static void AgregarFechas()
        {
            foreach(string tabla in Enum.GetNames(typeof(Tablas)))
            {
                FechaSincronizacion fecha_sincronizacion = Obtener(tabla);
                if (fecha_sincronizacion.id == 0)
                {
                    fecha_sincronizacion.tabla = tabla;
                    fecha_sincronizacion.Agregar();
                }
            }
        }

        public static FechaSincronizacion Obtener(string tabla)
        {
            try
            {
                return FechasSincronizacion().Where(x => x.tabla == tabla).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new FechaSincronizacion();
            }
        }

        private bool Agregar()
        {
            try
            {
                var Tabla = FechasSincronizacion();
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

        public bool Actualizar(DateTime fecha)
        {
            try
            {
                this.fecha = fecha;
                var Tabla = FechasSincronizacion();
                FechaSincronizacion fecha_sincronizacion = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(fecha_sincronizacion);
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
