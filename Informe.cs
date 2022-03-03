using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "informes")]
    public class Informe
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string descripcion { get; set; }
        [Column]
        public string informe { get; set; }
        [Column]
        public string tipo { get; set; }
        [Column]
        public char tipo_informe { get; set; }
        [Column]
        public int copias { get; set; }
        [Column]
        public int informe_sequencia_id { get; set; }
        [Column]
        public string impresora { get; set; }
        [Column]
        public bool almacen { get; set; }
        [Column]
        public bool predeterminado { get; set; }
        [Column]
        public bool predeterminado_correo_electronico { get; set; }
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

        public static Table<Informe> Informes()
        {
            return Nori.CrearContexto().GetTable<Informe>();
        }
        public Informe()
        {
            tipo = Tipo.ObtenerPredeterminado();
            tipo_informe = TipoInforme.ObtenerPredeterminado();
            copias = 1;
            informe_sequencia_id = 0;
            almacen = false;
            predeterminado = false;
            predeterminado_correo_electronico = true;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Informe Obtener(int id)
        {
            try
            {
                return Informes().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Informe();
            }
        }

        public static Informe ObtenerPredeterminado(string tipo)
        {
            try
            {
                return Informes().Where(x => x.tipo == tipo && x.predeterminado == true).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Informe();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Informes();
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
                var Tabla = Informes();
                Informe informe = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(informe);
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
                var Tabla = Informes();
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

        #region Tipo
        public class Tipo
        {
            public string tipo { get; set; }
            public string nombre { get; set; }
            public static List<Tipo> Tipos()
            {
                List<Tipo> tipos = new List<Tipo>();

                //Sistema

                Tipo tipo = new Tipo();

                tipo.tipo = "US";
                tipo.nombre = "Usuario";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "SI";
                tipo.nombre = "Sistema";
                tipos.Add(tipo);

                //POS 

                tipo = new Tipo();

                tipo.tipo = "IE";
                tipo.nombre = "Ingreso / Egreso";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "AR";
                tipo.nombre = "Arqueo";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "CX";
                tipo.nombre = "Corte X";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "CZ";
                tipo.nombre = "Corte Z";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "CD";
                tipo.nombre = "Cierre del día";
                tipos.Add(tipo);

                //Socios

                tipo = new Tipo();

                tipo.tipo = "SN";
                tipo.nombre = "Socio de negocio";
                tipos.Add(tipo);

                //Ventas

                tipo = new Tipo();

                tipo.tipo = "CO";
                tipo.nombre = "Cotización";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "PE";
                tipo.nombre = "Pedido";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "EN";
                tipo.nombre = "Entrega";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "DV";
                tipo.nombre = "Devolución";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "AC";
                tipo.nombre = "Anticipo de cliente";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "FA";
                tipo.nombre = "Factura de cliente";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "NC";
                tipo.nombre = "Nota de crédito";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "ND";
                tipo.nombre = "Nota de débito";
                tipos.Add(tipo);

                //Pagos

                tipo = new Tipo();

                tipo.tipo = "PR";
                tipo.nombre = "Pago recibido";
                tipos.Add(tipo);

                //Compras

                tipo = new Tipo();

                tipo.tipo = "CC";
                tipo.nombre = "Cotización de compra";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "OC";
                tipo.nombre = "Orden de compra";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "PP";
                tipo.nombre = "Pedido de proveedor";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "EM";
                tipo.nombre = "Entrada de mercancía";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "DM";
                tipo.nombre = "Devolución de mercancía";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "FP";
                tipo.nombre = "Factura de proveedor";
                tipos.Add(tipo);

                //Inventario

                tipo = new Tipo();

                tipo.tipo = "ST";
                tipo.nombre = "Solicitud de traslado";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "TS";
                tipo.nombre = "Traslado";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "AE";
                tipo.nombre = "Ajuste de entrada";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "AS";
                tipo.nombre = "Ajuste de salida";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = "IF";
                tipo.nombre = "Inventario físico";
                tipos.Add(tipo);

                return tipos;
            }

            public static string ObtenerPredeterminado()
            {
                return "US";
            }
        }
        #endregion

        #region TipoInforme
        public class TipoInforme
        {
            public char tipo { get; set; }
            public string nombre { get; set; }
            public static List<TipoInforme> TipoInformes()
            {
                List<TipoInforme> tipos = new List<TipoInforme>();

                TipoInforme tipo = new TipoInforme();

                tipo.tipo = 'D';
                tipo.nombre = "Documento";
                tipos.Add(tipo);

                tipo = new TipoInforme();

                tipo.tipo = 'T';
                tipo.nombre = "Ticket";
                tipos.Add(tipo);

                return tipos;
            }

            public static char ObtenerPredeterminado()
            {
                return 'T';
            }
        }
        #endregion
    }
}
