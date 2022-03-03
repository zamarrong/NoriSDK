using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    #region DocumentoElectronico
    [Table(Name = "documentos_electronicos")]
    public class DocumentoElectronico
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int documento_id { get; set; }
        [Column]
        public int pago_id { get; set; }
        [Column]
        public char estado { get; set; }
        [Column]
        public string folio_fiscal { get; set; }
        [Column]
        public string cadena_original { get; set; }
        [Column]
        public string sello_CFD { get; set; }
        [Column(CanBeNull = true)]
        public string sello_SAT { get; set; }
        [Column]
        public DateTime fecha_timbrado { get; set; }
        [Column]
        public string mensaje { get; set; }
        [Column]
        public string motivo { get; set; }
        [Column]
        public string folio_fiscal_sustitucion { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<DocumentoElectronico> DocumentosElectronicos()
        {
            return Nori.CrearContexto().GetTable<DocumentoElectronico>();
        }

        public DocumentoElectronico()
        {
            estado = 'N';
            folio_fiscal = string.Empty;
            cadena_original = string.Empty;
            sello_CFD = string.Empty;
            sello_SAT = string.Empty;
            mensaje = string.Empty;
            motivo = "02";
            folio_fiscal_sustitucion = string.Empty;
            fecha_timbrado = DateTime.Now;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public DocumentoElectronico(int id, bool pago = false)
        {
            if (pago)
                pago_id = id;
            else
                documento_id = id;

            folio_fiscal = string.Empty;
            estado = 'N';
            cadena_original = string.Empty;
            sello_CFD = string.Empty;
            sello_SAT = string.Empty;
            mensaje = string.Empty;
            fecha_timbrado = DateTime.Now;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static DocumentoElectronico Obtener(int id)
        {
            try
            {
                return DocumentosElectronicos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new DocumentoElectronico();
            }
        }

        public static DocumentoElectronico ObtenerPorDocumento(int documento_id)
        {
            try
            {
                return DocumentosElectronicos().Where(x => x.documento_id == documento_id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new DocumentoElectronico();
            }
        }
        public static DocumentoElectronico Obtener(string folio_fiscal)
        {
            try
            {
                return DocumentosElectronicos().Where(x => x.folio_fiscal == folio_fiscal).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new DocumentoElectronico();
            }
        }

        public bool Agregar(bool agregar_sincronizacion = true)
        {
            try
            {
                if (documento_id == 0 && pago_id == 0)
                {
                    Global.Error = new Nori.Error("El documento o pago, debe guardarse antes que el documento electrónico");
                    return false;
                }

                var Tabla = DocumentosElectronicos();
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();

                if (!mensaje.IsNullOrEmpty())
                {
                    if (mensaje.Length >= 254)
                        mensaje = mensaje.Substring(0, 253);
                }

                if (Global.Configuracion.sap && estado.Equals('A') && agregar_sincronizacion && id != 0)
                {
                    Sincronizacion sincronizacion = new Sincronizacion();
                    sincronizacion.tabla = "documentos_electronicos";
                    sincronizacion.registro = id;
                    sincronizacion.Agregar();
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool Actualizar(bool agregar_sincronizacion = true)
        {
            try
            {
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
                var Tabla = DocumentosElectronicos();
                DocumentoElectronico documento_electronico = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(documento_electronico);
                if (folio_fiscal.Length > 0 && estado.Equals('E'))
                    documento_electronico.estado = 'A';
                if (!mensaje.IsNullOrEmpty())
                {
                    if (mensaje.Length >= 254)
                        mensaje = mensaje.Substring(0, 254);
                }

                Tabla.Context.SubmitChanges();

                if (Global.Configuracion.sap && estado.Equals('A') && agregar_sincronizacion && id != 0)
                {
                    Sincronizacion sincronizacion = new Sincronizacion();
                    sincronizacion.tabla = "documentos_electronicos";
                    sincronizacion.registro = id;
                    sincronizacion.Agregar();
                }

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
                var Tabla = DocumentosElectronicos();
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
}
