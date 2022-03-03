using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "documentos")]
    public class Documento
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int serie_id { get; set; }
        [Column]
        public int condicion_pago_id { get; set; }
        [Column]
        public int proyecto_id { get; set; }
        [Column]
        public int estacion_id { get; set; }
        [Column]
        public int moneda_id { get; set; }
        [Column]
        public decimal tipo_cambio { get; set; }
        [Column]
        public int metodo_pago_id { get; set; }
        [Column]
        public string cuenta_pago { get; set; }
        [Column]
        public int vendedor_id { get; set; }
        [Column]
        public int socio_id { get; set; }
        [Column]
        public int persona_contacto_id { get; set; }
        [Column]
        public string codigo_sn { get; set; }
        [Column]
        public int monedero_id { get; set; }
        [Column]
        public int direccion_facturacion_id { get; set; }
        [Column]
        public int direccion_envio_id { get; set; }
        [Column]
        public int direccion_origen_id { get; set; }
        [Column]
        public int distancia { get; set; }
        [Column]
        public decimal latitud { get; set; }
        [Column]
        public decimal longitud { get; set; }
        [Column]
        public int clase_expedicion_id { get; set; }
        [Column]
        public int chofer_id { get; set; }
        [Column]
        public int vehiculo_id { get; set; }
        [Column]
        public int remolque_id { get; set; }
        [Column]
        public int ruta_id { get; set; }
        [Column]
        public int supervisor_id { get; set; }
        [Column]
        public int causalidad_id { get; set; }
        [Column]
        public int canal_id { get; set; }
        [Column]
        public int lista_precio_id { get; set; }
        [Column]
        public int almacen_id { get; set; }
        [Column]
        public int almacen_destino_id { get; set; }
        [Column]
        public char tipo { get; set; }
        [Column]
        public string clase { get; set; }
        [Column]
        public char estado { get; set; }
        [Column]
        public int numero_documento { get; set; }
        [Column]
        public decimal subtotal { get; set; }
        [Column]
        public decimal porcentaje_descuento { get; set; }
        [Column]
        public decimal descuento { get; set; }
        [Column]
        public decimal impuesto { get; set; }
        [Column]
        public decimal total { get; set; }
        [Column]
        public decimal importe_aplicado { get; set; }
        [Column]
        public string orden_compra { get; set; }
        [Column]
        public string referencia { get; set; }
        [Column]
        public string comentario { get; set; }
        [Column]
        public bool servicio { get; set; }
        [Column]
        public bool cod { get; set; }
        [Column]
        public bool reserva { get; set; }
        [Column]
        public bool anticipo { get; set; }
        [Column]
        public decimal importe_aplicado_anticipo { get; set; }
        [Column]
        public bool impreso { get; set; }
        [Column]
        public bool cancelado { get; set; }
        [Column]
        public bool foraneo { get; set; }
        [Column]
        public bool seguimiento { get; set; }
        [Column]
        public int estado_seguimiento { get; set; }
        [Column]
        public bool generar_documento_electronico { get; set; }
        [Column]
        public bool global { get; set; }
        [Column]
        public string uso_principal { get; set; }
        [Column]
        public int identificador_externo { get; set; }
        [Column]
        public int numero_documento_externo { get; set; }
        [Column]
        public string pin { get; set; }
        [Column]
        public DateTime fecha_contabilizacion { get; set; }
        [Column]
        public DateTime fecha_vencimiento { get; set; }
        [Column]
        public DateTime fecha_documento { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }
        [Column]
        public DateTime? fecha_cancelacion { get; set; }
        [Column]
        public int usuario_cancelacion_id { get; set; }
        //Interno
        public List<Partida> partidas { get; set; }
        public List<Flujo> flujo { get; internal set; }
        public List<Referencia> referencias { get; set; }
        public List<Anexo> anexos { get; set; }
        public int numero_partidas { get; internal set; }
        public decimal cantidad_empaque { get; internal set; }
        public decimal cantidad_partidas { get; internal set; }

        public static Table<Documento> Documentos()
        {
            return Nori.CrearContexto().GetTable<Documento>();
        }

        public Documento()
        {
            estacion_id = Global.Estacion.id;
            moneda_id = Global.Configuracion.moneda_id;
            lista_precio_id = Global.Configuracion.lista_precio_id;
            tipo_cambio = 1;
            vendedor_id = Global.Usuario.vendedor_id;
            clase_expedicion_id = Global.Usuario.clase_expedicion_id;
            clase = Clase.ObtenerPredeterminado().clase;
            tipo = Clase.Clases().Where(x => x.clase == clase).First().tipo;
            estado = Estado.ObtenerPredeterminado();
            referencia = string.Empty;
            comentario = string.Empty;
            uso_principal = UsoCFDI.ObtenerPredeterminado();
            generar_documento_electronico = GenerarDocumentoElectronico();
            pin = Utilidades.CadenaAleatoria(6);
            fecha_contabilizacion = DateTime.Now;
            fecha_vencimiento = DateTime.Now;
            fecha_documento = DateTime.Now;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
            //Interno
            partidas = new List<Partida>();
            flujo = new List<Flujo>();
            referencias = new List<Referencia>();
            anexos = new List<Anexo>();
        }

        public Documento(string clase = null)
        {
            estacion_id = Global.Estacion.id;
            moneda_id = Global.Configuracion.moneda_id;
            lista_precio_id = Global.Configuracion.lista_precio_id;
            tipo_cambio = 1;
            vendedor_id = Global.Usuario.vendedor_id;
            clase_expedicion_id = Global.Usuario.clase_expedicion_id;
            this.clase = (clase.IsNullOrEmpty()) ? Clase.ObtenerPredeterminado().clase : clase;
            tipo = Clase.Clases().Where(x => x.clase == this.clase).First().tipo;
            estado = Estado.ObtenerPredeterminado();
            referencia = string.Empty;
            comentario = string.Empty;
            uso_principal = UsoCFDI.ObtenerPredeterminado();
            generar_documento_electronico = GenerarDocumentoElectronico();
            pin = Utilidades.CadenaAleatoria(6);
            fecha_contabilizacion = DateTime.Now;
            fecha_vencimiento = DateTime.Now;
            fecha_documento = DateTime.Now;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
            //Interno
            partidas = new List<Partida>();
            flujo = new List<Flujo>();
            referencias = new List<Referencia>();
            anexos = new List<Anexo>();
        }

        public void CopiarDe(Documento documento, string clase = null, bool restablecer_identificadores = true, bool copiar_flujo = false)
        {
            documento.CopyProperties(this);

            if (!clase.IsNullOrEmpty())
                this.clase = clase;

            if (restablecer_identificadores)
            {
                id = 0;
                serie_id = 0;
                numero_documento = 0;
                identificador_externo = 0;
                referencia = string.Empty;
                impreso = false;
            }

            estado = 'A';
            fecha_contabilizacion = DateTime.Now;
            fecha_vencimiento = DateTime.Now;
            fecha_documento = DateTime.Now;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;

            partidas = documento.partidas.Where(x => x.cantidad_pendiente > 0).ToList();
            partidas.All(x => { x.cantidad = x.cantidad_pendiente; x.cantidad_pendiente = x.cantidad; x.fecha_actualizacion = DateTime.Now; return true; });

            if (copiar_flujo)
                flujo = documento.flujo;
            else
                flujo = new List<Flujo>();
        }
        public void BorrarIdentificadores()
        {
            id = 0;
            identificador_externo = id;
            numero_documento = 0;
            partidas.All(x => { x.id = 0; x.documento_id = 0; return true; });
        }
        public bool EstablecerSocio(Socio socio)
        {
            try
            {
                socio_id = 0;
                if (socio.activo)
                {
                    if (socio.tipo.Equals('P') && tipo.Equals('V'))
                    {
                        Global.Error = new Nori.Error("El socio debe ser del tipo cliente.");
                        return false;
                    }
                    else if (socio.tipo.Equals('C') && tipo.Equals('C'))
                    {
                        Global.Error = new Nori.Error("El socio debe ser del tipo proveedor.");
                        return false;
                    }

                    socio_id = socio.id;
                    codigo_sn = socio.codigo;
                    persona_contacto_id = socio.persona_contacto_id;

                    int distintos = partidas.Select(x => x.documento_id).Distinct().Count();
                    if (!partidas.Any(x => x.documento_id != 0) || distintos > 1)
                    {
                        if (Global.Configuracion.vendedor_segun_usuario)
                            vendedor_id = (Global.Usuario.vendedor_id == 0) ? socio.vendedor_id : Global.Usuario.vendedor_id;
                        else
                            vendedor_id = (socio.vendedor_id == 0) ? Global.Usuario.vendedor_id : socio.vendedor_id;

                        try
                        {
                            if (Global.Configuracion.vendedor_segun_estacion)
                                vendedor_id = Documentos().Where(x => x.estacion_id == Global.Estacion.id).OrderByDescending(x => x.id).Select(x => x.vendedor_id).First();
                        } catch { }

                        metodo_pago_id = socio.metodo_pago_id;
                        condicion_pago_id = socio.condicion_pago_id;
                    }

                    if (Global.Configuracion.lista_precio_segun_usuario)
                    {
                        var lista_precio_usuario = Usuario.ListaPrecio.ListasPrecios().Where(x => x.usuario_id == Global.Usuario.id).Select(x => x.lista_precio_id).FirstOrDefault();
                        if (!lista_precio_usuario.IsNullOrEmpty())
                            lista_precio_id = lista_precio_usuario;
                    }

                    if (lista_precio_id != socio.lista_precio_id)
                    {
                        lista_precio_id = socio.lista_precio_id;
                        RecalcularTotalPartidas();
                    }
 
                    if (id == 0 || direccion_facturacion_id == 0)
                        direccion_facturacion_id = socio.direccion_facturacion_id;

                    if (direccion_envio_id == 0 || Socio.Direccion.Direcciones().Where(x => x.id == direccion_envio_id).Select(x => x.socio_id).First() != 0)
                        direccion_envio_id = socio.direccion_envio_id;

                    if (!partidas.Any(x => x.id != 0) && porcentaje_descuento == 0)
                        porcentaje_descuento = socio.porcentaje_descuento;

                    cuenta_pago = socio.cuenta_pago;
                    uso_principal = socio.uso_principal;

                    if (uso_principal.IsNullOrEmpty())
                        uso_principal = "G03";

                    if (clase.Equals("NC"))
                        uso_principal = "G02";

                    try
                    {
                        if (monedero_id == 0)
                            monedero_id = socio.monedero_id;

                        if (monedero_id == 0)
                            monedero_id = Monedero.Monederos().Where(x => x.socio_id == socio.id && x.predeterminado == true).Select(x => new { x.id }).First().id;
                    }
                    catch { monedero_id = 0; }

                    return true;
                }
                else
                {
                    Global.Error = new Nori.Error("El socio esta inactivo.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public static Documento Obtener(int id)
        {
            try
            {
                var documento = Documentos().Where(x => x.id == id).First();

                documento.partidas = documento.ObtenerPartidas();
                documento.partidas.All(x => { x.CalcularTotal(); return true; });
                documento.flujo = documento.ObtenerFlujo();
                documento.referencias = documento.ObtenerReferencias();

                documento.CalcularTotal();

                return documento;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Documento();
            }
        }
        public static Documento ObtenerPorNumero(string clase, int numero_documento)
        {
            try
            {
                var documento = Documentos().Where(x => x.clase == clase && x.numero_documento == numero_documento).First();

                documento.partidas = documento.ObtenerPartidas();
                documento.partidas.All(x => { x.CalcularTotal(); return true; });
                documento.flujo = documento.ObtenerFlujo();

                documento.CalcularTotal();

                return documento;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Documento();
            }
        }
        public static Documento ObtenerPorID(string clase, int id)
        {
            try
            {
                var documento = Documentos().Where(x => x.clase == clase && x.id == id).First();

                documento.partidas = documento.ObtenerPartidas();
                documento.partidas.All(x => { x.CalcularTotal(); return true; });
                documento.flujo = documento.ObtenerFlujo();

                documento.CalcularTotal();

                return documento;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Documento();
            }
        }
        public static Documento ObtenerPorIE(string clase, int identificador_externo)
        {
            try
            {
                var documento = Documentos().Where(x => x.clase == clase && x.identificador_externo == identificador_externo).First();

                documento.partidas = documento.ObtenerPartidas();
                documento.partidas.All(x => { x.CalcularTotal(); return true; });
                documento.flujo = documento.ObtenerFlujo();

                documento.CalcularTotal();

                return documento;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Documento();
            }
        }
        public List<Partida> ObtenerPartidas()
        {
            try
            {
                return Partida.Partidas().Where(x => x.documento_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }
        public List<Flujo> ObtenerFlujo()
        {
            try
            {
                return Flujo.Flujos().Where(x => x.documento_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }
        public List<Relacion> ObtenerRelaciones()
        {
            try
            {
                return Relacion.RelacionDocumentos().Where(x => x.documento_destino_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }
        public List<Referencia> ObtenerReferencias()
        {
            try
            {
                return Referencia.Referencias().Where(x => x.documento_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }
        public List<Anexo> ObtenerAnexos()
        {
            try
            {
                return Anexo.Anexos().Where(x => x.documento_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }
        public bool Agregar(bool borrar_identificadores = false)
        {
            try
            {
                Permiso permiso = Permiso.Obtener(Global.Usuario.id, clase);

                if (permiso.id != 0)
                {
                    if (!permiso.agregar)
                    {
                        Global.Error = new Nori.Error("No cuentas con el permiso suficiente para realizar esta acción.");
                        return false;
                    }
                }

                if (borrar_identificadores)
                    BorrarIdentificadores();

                if (id != 0)
                {
                    Global.Error = new Nori.Error("El documento ya cuenta con un identificador.");
                    return false;
                }

                if (!Clase.Clases().Any(x => x.clase == clase && x.tipo == tipo))
                {
                    Global.Error = new Nori.Error("La clase o tipo de documento no es válido.");
                    return false;
                }

                if (estacion_id == 0)
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido una estación.");
                    return false;
                }

                if (socio_id == 0 && !tipo.Equals('I'))
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido un socio.");
                    return false;
                }

                if (tipo.Equals('I') && (almacen_id == 0 || almacen_destino_id == 0))
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido un almacén de origen o destino.");
                    return false;
                }

                if (moneda_id == 0)
                {
                    Global.Error = new Nori.Error("Aún no se ha especificado una moneda.");
                    return false;
                }

                if (condicion_pago_id == 0 && (clase.Equals("FA") || clase.Equals("NC")) && identificador_externo == 0)
                {
                    Global.Error = new Nori.Error("Aún no se ha especificado una condición de pago.");
                    return false;
                }

                if (metodo_pago_id == 0 && (clase.Equals("FA") || clase.Equals("NC")) && identificador_externo == 0)
                {
                    Global.Error = new Nori.Error("Aún no se ha especificado un método de pago.");
                    return false;
                }

                if (metodo_pago_id != 0)
                {
                    char tipo_pago = MetodoPago.MetodosPago().Where(x => x.id == metodo_pago_id).Select(x => x.tipo).First();
                    if (tipo.Equals('C') && tipo_pago.Equals('E'))
                    {
                        Global.Error = new Nori.Error("No es posible establecer este método de pago para este documento, establezca un tipo de pago saliente.");
                        return false;
                    }
                    else if (tipo.Equals('V') && tipo_pago.Equals('S'))
                    {
                        Global.Error = new Nori.Error("No es posible establecer este método de pago para este documento, establezca un tipo de pago entrante.");
                        return false;
                    }
                }

                if (lista_precio_id == 0)
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido una lista de precio.");
                    return false;
                }

                if (vendedor_id == 0 && !tipo.Equals('I'))
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido un vendedor.");
                    return false;
                }
                else
                {
                    foraneo = Vendedor.Vendedores().Any(x => x.id == Global.Usuario.vendedor_id && x.foraneo == true);
                }

                if (partidas.Count <= 0)
                {
                    Global.Error = new Nori.Error("El documento no contiene partidas.");
                    return false;
                }

                CalcularTotal();

                if ((direccion_envio_id == 0 && direccion_facturacion_id == 0) && identificador_externo == 0 && tipo.Equals('V'))
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido la dirección de facturación y/o envío.");
                    return false;
                }

                CondicionesPago condicion_pago = CondicionesPago.Obtener(condicion_pago_id);

                if (id == 0 && fecha_vencimiento.ToShortDateString() == DateTime.Today.ToShortDateString())
                    fecha_vencimiento = fecha_documento.AddDays(condicion_pago.dias_extra);

                if (clase.Equals("FA") && condicion_pago.pago_requerido && flujo.Count == 0 && identificador_externo == 0)
                {
                    Global.Error = new Nori.Error("Es necesario liquidar el documento antes de guardar.");
                    return false;
                }

                if (flujo.Count > 0)
                {
                    Flujo pago = flujo.Where(x => x.codigo == "INVEN").OrderByDescending(x => x.tipo_cambio * x.importe).First();
                    MetodoPago.Tipo tipo_metodo_pago = MetodoPago.Tipo.Obtener(pago.tipo_metodo_pago_id);

                    metodo_pago_id = tipo_metodo_pago.metodo_pago_id;
                    cuenta_pago = pago.referencia;
                }

                if (total == 0)
                {
                    Global.Error = new Nori.Error("No es posible guardar un documento con total igual a cero.");
                    return false;
                }

                if (!comentario.IsNullOrEmpty())
                {
                    if (comentario.Length >= 254)
                        comentario = comentario.Substring(0, 254);
                }

                if (importe_aplicado > total)
                    importe_aplicado = total;

                if (importe_aplicado > 0 && clase.Equals("FA") && flujo.Count > 0)
                    importe_aplicado = 0;

                try
                {
                    List<int> documentos_ids = partidas.Where(x => x.documento_id != id).Select(x => x.documento_id).Distinct().ToList();
                    if (documentos_ids.Count > 0)
                    {
                        var documentos_relacionados = Documentos().Where(x => documentos_ids.Contains(x.id)).Select(x => new { x.id, x.socio_id, x.clase }).ToList();
                        foreach(var documento_relacionado in documentos_relacionados)
                        {
                            if (documento_relacionado.socio_id != socio_id && Socio.Socios().Any(x => x.id == socio_id && x.eventual == false))
                            {
                                if (clase.Equals("EN") || clase.Equals("FA") || clase.Equals("NC") || clase.Equals("ND") || clase.Equals("ST"))
                                {
                                    Global.Error = new Nori.Error(string.Format("El socio del documento base ({0}) y destino ({1}) no coinciden ({2} - {3})", documento_relacionado.socio_id, socio_id, documento_relacionado.clase, documento_relacionado.id));
                                    return false;
                                }
                                else
                                {
                                    partidas.All(x => { x.documento_id = 0; return true; });
                                }
                            }
                        }
                    }
                }
                catch { }

                if (clase.Equals("TS") || clase.Equals("ST"))
                {
                    if (partidas.Any(x => x.almacen_destino_id == 0))
                    {
                        Global.Error = new Nori.Error("Es necesario especificar un almacén de destino para todos los artículos.");
                        return false;
                    }

                    if (partidas.Any(x => x.almacen_destino_id == x.almacen_id && x.ubicacion_id == x.ubicacion_destino_id))
                    {
                        Global.Error = new Nori.Error("El almacén o ubicación de origen y destino no pueden ser iguales.");
                        return false;
                    }
                }

                if (partidas.Any(x => x.precio <= 0) && !Global.Configuracion.venta_articulo_precio_cero && tipo.Equals('V'))
                {
                    Global.Error = new Nori.Error("No es posible vender un artículo con precio <= 0.");
                    return false;
                }

                Serie serie = (serie_id == 0) ? Serie.ObtenerPredeterminado(clase) : Serie.Obtener(serie_id);

                if (serie.id == 0)
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido una serie predeterminada para esta clase de documento.");
                    return false;
                }

                if (serie.clase != clase)
                {
                    Global.Error = new Nori.Error("La clase de documento de la serie indicada y la del documento no coinciden.");
                    return false;
                }

                if (cod && clase != "PE")
                    cod = false;

                if (reserva && clase != "FA")
                    reserva = false;

                if (anticipo && clase != "FA")
                    anticipo = false;

                if (!anticipo && clase.Equals("AC"))
                    anticipo = true;

                serie_id = serie.id;
                numero_documento = serie.siguiente;

                if (identificador_externo > 0)
                {
                    if (ExisteIdentificadorExterno())
                    {
                        Global.Error = new Nori.Error("El identificador externo especificado ya existe para este tipo de documento.");
                        return false;
                    }

                    if (Utilidades.EjecutarEscalar(string.Format("select count(id) from documentos where clase = '{0}' and identificador_externo = {1}", clase, identificador_externo)) > 0)
                    {
                        Global.Error = new Nori.Error("El identificador externo especificado ya existe para esta clase de documento.");
                        return false;
                    }
                }

                CalcularTotal();

                var Tabla = Documentos();
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();

                if (id != 0)
                {
                    serie.EstablecerSiguiente();

                    partidas.All(x => { x.documento_base_id = x.documento_id; return true; });
                    List<int> documentos_relacionados = partidas.Where(x => x.documento_id != id && x.documento_id != 0).Select(x => x.documento_id).Distinct().ToList();
                    bool documento_relacion_es_reserva = false;
                    foreach (int documento_id in documentos_relacionados)
                    {
                        Relacion relacion = new Relacion();

                        relacion.documento_origen_id = documento_id;
                        relacion.documento_destino_id = id;

                        relacion.Agregar();
                        try
                        {
                            Documento documento_relacion = Obtener(relacion.documento_origen_id);

                            if (!documento_relacion_es_reserva)
                                documento_relacion_es_reserva = (documento_relacion.clase.Equals("FA") && documento_relacion.reserva) ? true : false;

                            foreach (Partida partida_relacion in documento_relacion.partidas)
                            {
                                try
                                {
                                    var partida_documento = partidas.Where(x => x.id == partida_relacion.id).Select(x => new { x.cantidad, x.total }).First();
                                    partida_relacion.cantidad_pendiente -= partida_documento.cantidad;

                                    if (partida_relacion.cantidad_pendiente < 0)
                                        partida_relacion.cantidad_pendiente = 0;

                                    if (clase.Equals("NC") || clase.Equals("ND"))
                                        importe_aplicado += partida_documento.total;

                                    partida_relacion.Actualizar();
                                }
                                catch { continue; }
                            }

                            if (documento_relacion.partidas.Sum(x => x.cantidad_pendiente) == 0)
                                documento_relacion.Cerrar();
                        }
                        catch { continue; }
                    }

                    partidas.All(x => { x.cantidad_pendiente = x.cantidad; x.documento_id = id; x.Agregar(); return true; });

                    if (flujo.Count > 0)
                    {
                        flujo.RemoveAll(x => x.id != 0);

                        flujo.All(x => { x.documento_id = id; x.Agregar(); return true; });

                        if (Math.Round(flujo.Where(x => x.id != 0 && x.tipo_metodo_pago_id != 0 && x.codigo == "INVEN").Sum(x => x.tipo_cambio * x.importe), 2) > Math.Round(total, 2))
                        {
                            Flujo cambio = new Flujo();

                            cambio.codigo = "RECAM";
                            cambio.documento_id = id;
                            cambio.importe = Math.Round(flujo.Where(x => x.tipo_metodo_pago_id != 0 && x.codigo == "INVEN").Sum(x => x.tipo_cambio * x.importe) - total, 2);
                            cambio.Agregar();

                            flujo.Add(cambio);
                        }

                        try
                        {
                            var tipos_metodos_pago = MetodoPago.Tipo.Tipos().Where(x => x.documento == true).Select(x => x.id).ToList();

                            foreach (Flujo flujo in flujo.Where(x => tipos_metodos_pago.Contains(x.tipo_metodo_pago_id)).ToList())
                            {
                                try
                                {
                                    Documento documento = Obtener(int.Parse(flujo.referencia));

                                    if (documento.id == int.Parse(flujo.referencia))
                                    {
                                        if (documento.clase.Equals("NC"))
                                        {
                                            documento.importe_aplicado += (flujo.tipo_cambio * flujo.importe);
                                            documento.Actualizar();

                                            if (Math.Round(documento.importe_aplicado, 2) >= documento.total)
                                                documento.Cerrar();
                                        }
                                    }
                                }
                                catch { continue; }
                            }
                        }
                        catch { }

                        //Monedero electrónico
                        try
                        {
                            foreach (Flujo flujo in flujo.Where(x => x.tipo_metodo_pago_id == Global.Configuracion.tipo_metodo_pago_monedero_id).ToList())
                            {
                                Monedero monedero = Monedero.Obtener(flujo.referencia);

                                Monedero.Partida partida_monedero = new Monedero.Partida();

                                partida_monedero.monedero_id = monedero.id;
                                partida_monedero.documento_id = id;
                                partida_monedero.partida_id = 0;
                                partida_monedero.importe = flujo.importe * -1;

                                partida_monedero.Agregar();
                            }
                        }
                        catch { }
                    }

                    try
                    {
                        if (referencias.Count > 0)
                        {
                            foreach (Referencia referencia in referencias)
                            {
                                referencia.documento_id = id;

                                if (referencia.Agregar())
                                {
                                    try
                                    {
                                        if (Documentos().Any(x => x.id == referencia.documento_referencia_id && x.anticipo == true))
                                        {
                                            Documento factura_anticipo = Obtener(referencia.documento_referencia_id);
                                            Documento nota_credito = new Documento();

                                            nota_credito.CopiarDe(factura_anticipo, "NC");
                                            nota_credito.partidas.First().total = (factura_anticipo.total > total) ? total : factura_anticipo.total;
                                            nota_credito.partidas.First().nombre = "Aplicación de anticipo";
                                            nota_credito.partidas.First().ModificarTotal();
                                            nota_credito.partidas.ForEach(x => { x.documento_id = 0; });

                                            Referencia referencia_factura = new Referencia();
                                            referencia_factura.documento_referencia_id = id;
                                            nota_credito.referencias.Add(referencia_factura);

                                            nota_credito.CalcularTotal();

                                            if (nota_credito.Agregar())
                                            {
                                                factura_anticipo.importe_aplicado_anticipo += nota_credito.total;
                                                factura_anticipo.Actualizar(false, false, true);
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        if (anexos.Count > 0)
                        {
                            foreach (Anexo anexo in anexos)
                            {
                                anexo.documento_id = id;
                                anexo.Agregar();
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        if (clase.Equals("FA") && identificador_externo == 0)
                            AgregarPagoFactura();
                    }
                    catch { }

                    try
                    {
                        if (monedero_id != 0 && clase.Equals("FA"))
                            GenerarPuntos();
                    }
                    catch { }

                    try
                    {
                        if (clase.Equals("FA") && condicion_pago.financiado && identificador_externo == 0)
                            AgregarFinanciamiento();
                    } catch { }

                    try
                    {
                        if (identificador_externo == 0)
                        {
                            AfectarInventario();
                            Socio.ActualizarBalance(socio_id);
                        }
                    } catch { }

                    try
                    {
                        if (documento_relacion_es_reserva)
                            Cerrar();

                        if ((clase.Equals("NC") || clase.Equals("ND")) && documentos_relacionados.Count > 0)
                            if (importe_aplicado >= total)
                                Cerrar();
                    } catch { }

                    if (Global.Configuracion.sap && identificador_externo == 0 && estado != 'B' && id != 0)
                    {
                        Sincronizacion sincronizacion = new Sincronizacion();
                        sincronizacion.tabla = "documentos";
                        sincronizacion.registro = id;
                        sincronizacion.Agregar();
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public static List<Documento> SepararDocumentoNoUsar(Documento documento)
        {
            try
            {
                List<Documento> documentos = new List<Documento>();
                List<int> monedas = documento.partidas.Select(x => x.moneda_id).Distinct().ToList();

                if (monedas.Count() == 1)
                {
                    if (documento.Agregar())
                        documentos.Add(documento);
                }
                else if (monedas.Count > 1)
                {
                    foreach (int moneda in monedas)
                    {
                        Documento nuevo_documento = new Documento();

                        documento.CopyProperties(nuevo_documento);
                        nuevo_documento.moneda_id = moneda;

                        try
                        {
                            if (moneda == Global.Configuracion.moneda_id)
                                nuevo_documento.tipo_cambio = 1;
                            else
                                nuevo_documento.tipo_cambio = (documento.partidas.Any(x => x.documento_id != 0 && x.moneda_id == moneda)) ? Documentos().Where(x => x.moneda_id == moneda && documento.partidas.Select(y => y.documento_id).Distinct().ToList().Contains(x.id)).Average(x => x.tipo_cambio) : TipoCambio.Venta(moneda);
                        }
                        catch
                        {
                            nuevo_documento.tipo_cambio = TipoCambio.Venta(moneda);
                        }

                        nuevo_documento.partidas = documento.partidas.Where(x => x.moneda_id == moneda).ToList();
                        nuevo_documento.CalcularTotal();

                        if (nuevo_documento.Agregar())
                            documentos.Add(nuevo_documento);
                    }
                }

                return documentos;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Documento>();
            }
        }
        public bool AgregarPartida(string q, decimal precio = 0)
        {
            try
            {
                Partida partida = new Partida();

                if (q.Contains("*"))
                {
                    partida.cantidad = decimal.Parse(q.Split('*')[0]);
                    q = q.Split('*')[1];
                }

                if (partida.cantidad <= 0)
                {
                    Global.Error = new Nori.Error("No es posible agregar partidas con cantidad <= 0.");
                    return false;
                }

                var articulo = Articulo.Articulos().Where(x => (x.sku == q || x.codigo_barras == q || x.id_adicional == q) && x.activo == true).Select(x => new { x.id, x.unidad_medida_id, x.unidad_medida_compra_id, x.unidad_medida_venta_id, x.clave_unidad, x.sku, x.nombre, x.codigo_barras, x.compra, x.venta, x.almacenable, x.pesable, x.almacen_id, x.tipo_empaque_id, x.cantidad_compra, x.cantidad_venta, x.cantidad_paquete, x.ultimo_precio_determinado }).FirstOrDefault();
                int unidad_medida_id = 0;
                switch (tipo)
                {
                    case 'C':
                        unidad_medida_id = articulo.unidad_medida_compra_id;
                        break;
                    case 'V':
                        unidad_medida_id = articulo.unidad_medida_venta_id;
                        break;
                    default:
                        unidad_medida_id = articulo.unidad_medida_id;
                        break;
                }

                if (articulo.IsNullOrEmpty())
                {
                    var codigo_barras = Articulo.CodigoBarras.CodigosBarras().Where(x => x.codigo == q).FirstOrDefault();
                    if (codigo_barras.IsNullOrEmpty())
                    {
                        Global.Error = new Nori.Error("Artículo no encontrado.");
                        return false;
                    }
                    else
                    {
                        articulo = Articulo.Articulos().Where(x => x.id == codigo_barras.articulo_id && x.activo == true).Select(x => new { x.id, x.unidad_medida_id, x.unidad_medida_compra_id, x.unidad_medida_venta_id, x.clave_unidad, x.sku, x.nombre, x.codigo_barras, x.compra, x.venta, x.almacenable, x.pesable, x.almacen_id, x.tipo_empaque_id, x.cantidad_compra, x.cantidad_venta, x.cantidad_paquete, x.ultimo_precio_determinado }).FirstOrDefault();
                        if (articulo.IsNullOrEmpty())
                        {
                            Global.Error = new Nori.Error("Artículo no encontrado, inactivo.");
                            return false;
                        }
                        partida.unidad_medida_id = codigo_barras.unidad_medida_id;
                        partida.codigo_barras = codigo_barras.codigo;
                    }
                }

                if (tipo == 'C' && !articulo.compra)
                {
                    Global.Error = new Nori.Error("El artículo no se puede comprar.");
                    return false;
                }
                else if (tipo == 'V' && !articulo.venta)
                {
                    Global.Error = new Nori.Error("El artículo no se puede vender.");
                    return false;
                }

                try
                {
                    if (articulo.pesable && Global.Estacion.bascula && Global.Estacion.bascula_id != 0)
                    {
                        decimal peso = Global.Estacion.Bascula.ObtenerPeso();
                        partida.cantidad = (peso > 0) ? peso : partida.cantidad;
                    }
                }
                catch { }

                partida.articulo_id = articulo.id;
                if (partida.unidad_medida_id == 0)
                    partida.unidad_medida_id = unidad_medida_id;

                bool existente = false;

                if (partidas.Count > 0 && Global.Configuracion.agrupar_partidas)
                {
                    if (partidas.Any(x => x.articulo_id == partida.articulo_id))
                    {
                        decimal cantidad = partida.cantidad;
                        partida = partidas.Where(x => x.articulo_id == partida.articulo_id).First();

                        var codigo_barras = Articulo.CodigoBarras.CodigosBarras().Where(x => x.articulo_id == articulo.id && x.codigo == q).FirstOrDefault();
                        if (!codigo_barras.IsNullOrEmpty() && partida.unidad_medida_id != 0)
                        {
                            if (partida.unidad_medida_id != codigo_barras.unidad_medida_id)
                            {
                                int grupo_unidad_medida_id = Articulo.Articulos().Where(x => x.id == partida.articulo_id).Select(x => x.grupo_unidad_medida_id).FirstOrDefault();
                                decimal equivalencia = UnidadMedida.Grupo.Linea.Lineas().Where(x => x.grupo_unidad_medida_id == grupo_unidad_medida_id && x.unidad_medida_id == partida.unidad_medida_id).Select(x => x.cantidad_base).FirstOrDefault();
                                cantidad /= equivalencia;
                            }
                        }

                        if (partida.id != 0 && !articulo.pesable && partida.documento_id != 0 && clase == "EN")
                        {
                            if (partida.cantidad + cantidad > Partida.Partidas().Where(x => x.id == partida.id).Select(x => x.cantidad_pendiente).FirstOrDefault())
                            {
                                Global.Error = new Nori.Error("No puede ingresar una cantidad mayor a la solicitada");
                                return false;
                            }
                        }

                        partida.fecha_lectura = DateTime.Now;
                        partida.cantidad += cantidad;
                        existente = true;
                    }
                }

                partida.clave_unidad = articulo.clave_unidad;
                partida.sku = articulo.sku;
                partida.nombre = articulo.nombre;
                if (partida.codigo_barras.IsNullOrEmpty())
                    partida.codigo_barras = articulo.codigo_barras;
                partida.cantidad_paquete = articulo.cantidad_paquete;// * ((tipo.Equals('C')) ? articulo.cantidad_compra : articulo.cantidad_venta);
                partida.norma_reparto = Global.Usuario.norma_reparto;

                if (!existente)
                {
                    if (articulo.almacenable)
                    {
                        if (Global.Usuario.almacen_id == 0)
                            partida.almacen_id = articulo.almacen_id;
                        else
                            partida.almacen_id = Global.Usuario.almacen_id;

                        if (partida.almacen_id == 0)
                        {
                            if (Usuario.Almacen.Almacenes().Any(x => x.usuario_id == Global.Usuario.id))
                            {
                                List<int> almacenes = Usuario.Almacen.Almacenes().Where(x => x.usuario_id == Global.Usuario.id).Select(x => x.almacen_id).ToList();
                                partida.almacen_id = Articulo.Inventario.Inventarios().Where(x => x.articulo_id == partida.articulo_id && almacenes.Contains(x.almacen_id) && x.activo == true).Select(x => x.almacen_id).FirstOrDefault();
                            }
                        }

                        if (partida.almacen_id == 0)
                        {
                            Global.Error = new Nori.Error("Aún no se ha establecido un almacén para esta partida.");
                            return false;
                        }

                        if (Almacen.Almacenes().Any(x => x.id == partida.almacen_id && x.ubicaciones == true) && !clase.Equals("ST"))
                        {
                            if (Global.Usuario.ubicacion_id == 0)
                                partida.ubicacion_id = Almacen.Almacenes().Where(x => x.id == partida.almacen_id).Select(x => x.ubicacion_id).First();
                            else
                                partida.ubicacion_id = Global.Usuario.ubicacion_id;

                            if (partida.ubicacion_id == 0)
                            {
                                Global.Error = new Nori.Error("Aún no se ha establecido una ubicación para esta partida.");
                                return false;
                            }
                        }

                        Articulo.Inventario articulo_inventario = Articulo.Inventario.Obtener(partida.articulo_id, partida.almacen_id);
                        if (!articulo_inventario.activo)
                        {
                            Global.Error = new Nori.Error("El artículo esta inactivo en este almacén.");
                            return false;
                        }

                        if (tipo.Equals('I'))
                        {
                            if (partidas.Count > 0)
                            {
                                if (partida.almacen_destino_id == 0)
                                {
                                    partida.almacen_id = partidas.Last().almacen_id;
                                    partida.almacen_destino_id = partidas.Last().almacen_destino_id;
                                }
                            }

                            partida.precio = articulo_inventario.costo;
                        }
                        else
                        {
                            partida.costo = articulo_inventario.costo;
                        }

                        partida.ObtenerStock();
                    }
                    else
                    {
                        partida.stock = 1000;
                    }

                    if (socio_id == 0 || lista_precio_id == 0)
                    {
                        Global.Error = new Nori.Error("Aún no se ha establecido el socio y/o la lista de precio.");
                        return false;
                    }

                    if (precio == 0 && partida.precio == 0)
                    {
                        if (!partida.ObtenerPrecio(socio_id, lista_precio_id))
                            return false;
                    }
                    else
                    {
                        if (!tipo.Equals('I'))
                        {
                            partida.precio = precio;

                            Impuesto impuesto = Articulo.ObtenerImpuesto(partida.articulo_id, socio_id);

                            if (impuesto.id != 0)
                            {
                                partida.impuesto_id = impuesto.id;
                                partida.porcentaje_impuesto = impuesto.porcentaje;
                            }
                        }
                    }

                    if (partida.precio <= 0 && !Global.Configuracion.venta_articulo_precio_cero && tipo.Equals('V'))
                    {
                        Global.Error = new Nori.Error("No es posible vender un artículo con precio <= 0.");
                        return false;
                    }

                    if (partida.moneda_id == 0)
                        partida.moneda_id = Global.Configuracion.moneda_id;

                    partidas.Add(partida);
                }

                partida.CalcularCantidadPendiente(id);

                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool AgregarPago()
        {
            try
            {
                Flujo pago = new Flujo();

                pago.codigo = (tipo.Equals('C')) ? "RECOM" : "INVEN";

                MetodoPago.Tipo tipo_metodo_pago = MetodoPago.Tipo.Obtener(MetodoPago.Obtener(metodo_pago_id).tipo_metodo_pago_id);

                if (tipo_metodo_pago.id != 0)
                {
                    pago.tipo_metodo_pago_id = tipo_metodo_pago.id;
                }

                pago.EstablecerTipoCambio();

                flujo.Add(pago);

                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool AgregarFlujos(List<Flujo> flujos)
        {
            try
            {
                flujo = flujos;
                
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool AgregarPago(int tipo_metodo_pago_id, decimal importe, string referencia = null)
        {
            try
            {
                Flujo pago = new Flujo();

                pago.codigo = (tipo.Equals('C')) ? "RECOM" : "INVEN";

                pago.tipo_metodo_pago_id = tipo_metodo_pago_id;
                pago.importe = importe;
                pago.referencia = referencia;

                pago.EstablecerTipoCambio();

                flujo.Add(pago);

                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool AgregarPagoFactura()
        {
            try
            {
                if (clase.Equals("FA"))
                {
                    List<Relacion> relaciones = ObtenerRelaciones();
                    List<Flujo> flujos = flujo.Where(x => x.id != 0).ToList();
                    List<MetodoPago.Tipo> tipos_metodos_pago = MetodoPago.Tipo.Tipos().ToList();

                    foreach (Relacion relacion in relaciones)
                    {
                        List<Flujo> flujos_relacion = Flujo.Flujos().Where(x => x.documento_id == relacion.documento_origen_id).ToList();
                        flujos_relacion.ForEach(x => flujos.Add(x));
                    }

                    if (flujos.Count > 0)
                    {
                        Pago pago = new Pago();
                        pago.flujo = flujos;

                        pago.socio_id = socio_id;
                        pago.total = flujos.Where(x => x.codigo == "INVEN").Sum(x => x.tipo_cambio * x.importe) - (flujos.Where(x => x.codigo == "RECAM").Sum(x => x.tipo_cambio * x.importe));
                        pago.comentario = "Pago desde NORI";

                        Pago.Partida partida = new Pago.Partida();

                        partida.documento_id = id;
                        partida.saldo = (total - importe_aplicado);
                        partida.importe = pago.total;

                        pago.partidas.Add(partida);

                        List<int> tipos_documentos = tipos_metodos_pago.Where(x => x.documento == true).Select(x => x.id).ToList();
                        //Flujo documentos
                        foreach (Flujo flujo in flujos.Where(x => tipos_documentos.Contains(x.tipo_metodo_pago_id)).ToList())
                        {
                            if (tipos_metodos_pago.Any(x => x.id == flujo.tipo_metodo_pago_id && x.documento == true))
                            {
                                Pago.Partida partida_nc = new Pago.Partida();

                                partida_nc.documento_id = int.Parse(flujo.referencia);
                                partida_nc.importe = flujo.importe * -1;

                                pago.partidas.Add(partida_nc);
                            }
                        }

                        //Flujo monedero
                        foreach (Flujo flujo in flujos.Where(x => x.tipo_metodo_pago_id == Global.Configuracion.tipo_metodo_pago_monedero_id))
                        {
                            Documento nota_credito = new Documento();

                            nota_credito.CopiarDe(this, "NC", true);
                            nota_credito.partidas.Clear();
                            nota_credito.AgregarPartida(MetodoPago.Tipo.Tipos().Where(x => x.id == flujo.tipo_metodo_pago_id).Select(x => x.codigo).First(), flujo.importe);
                            nota_credito.CalcularTotal();

                            if (nota_credito.Agregar())
                            {
                                Pago.Partida partida_nc = new Pago.Partida();

                                partida_nc.documento_id = nota_credito.id;
                                partida_nc.saldo = (nota_credito.total - nota_credito.importe_aplicado);
                                partida_nc.importe = flujo.importe * -1;

                                pago.partidas.Add(partida_nc);
                            }
                        }

                        return pago.Agregar();
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool AgregarFinanciamiento()
        {
            try
            {
                if (clase.Equals("FA"))
                {
                    CondicionesPago condicion_pago = CondicionesPago.Obtener(condicion_pago_id);
                    if (condicion_pago.plazos <= 0)
                        condicion_pago.plazos = 1;

                    Pago pago = new Pago();

                    pago.financiado = true;
                    pago.socio_id = socio_id;
                    pago.fecha_contabilizacion = fecha_contabilizacion;
                    pago.fecha_documento = fecha_documento;
                    pago.fecha_vencimiento = fecha_documento.AddDays(condicion_pago.dias_extra);
                    decimal saldo = Documentos().Where(x => x.id == id).Sum(x => x.total - x.importe_aplicado);
                    decimal intereses = (condicion_pago.porcentaje_interes <= 0) ? 0 : saldo * (condicion_pago.porcentaje_interes / 100);
                    pago.total = saldo + intereses;
                    DateTime fecha_vencimiento = pago.fecha_documento;

                    for(int i = 0; i < condicion_pago.plazos; i++)
                    {
                        Pago.Partida partida = new Pago.Partida();

                        partida.documento_id = id;
                        partida.saldo = (pago.total / condicion_pago.plazos);
                        partida.intereses = 0;
                        fecha_vencimiento = fecha_vencimiento.AddDays(condicion_pago.dias_extra / (condicion_pago.plazos));
                        partida.fecha_vencimiento = fecha_vencimiento;
                        partida.importe = 0;
                        pago.partidas.Add(partida);
                    }

                    pago.Agregar();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public void CalcularTotal()
        {
            try
            {
                tipo = Clase.Clases().Where(x => x.clase == clase).First().tipo;

                if (partidas.Count > 0)
                {
                    partidas.Where(x => x.tipo_cambio != (1 / tipo_cambio)).ToList().ForEach(x => x.ModificarTipoCambio(x.ObtenerTipoCambio(moneda_id, tipo_cambio) / tipo_cambio));

                    numero_partidas = partidas.Count();
                    cantidad_partidas = partidas.Sum(x => x.cantidad);
                    cantidad_empaque = partidas.Sum(x => x.cantidad_empaque);

                    subtotal = partidas.Sum(x => x.subtotal);

                    if (porcentaje_descuento != 0)
                        descuento = Math.Round(((porcentaje_descuento / 100) * subtotal), 2);

                    //Descuento:
                    //if (porcentaje_descuento > 0)
                    //{
                    //    descuento = Math.Round(((porcentaje_descuento / 100) * subtotal), 2);
                    //}
                    //else if (descuento > 0)
                    //{
                    //    porcentaje_descuento = (descuento / partidas.Sum(x => x.total)) * 100;
                    //    goto Descuento;
                    //}

                    decimal impuesto_partidas = partidas.Sum(x => x.cantidad * x.impuesto);
                    decimal descuento_impuesto = Math.Round((porcentaje_descuento / 100) * impuesto_partidas, 2);

                    impuesto = impuesto_partidas - descuento_impuesto;

                    total = (subtotal - descuento) + impuesto;

                    if (flujo.Count > 0)
                        importe_aplicado = flujo.Where(x => x.documento_id == id && x.tipo_metodo_pago_id != 0 && x.codigo == "INVEN").Sum(x => x.tipo_cambio * x.importe) - flujo.Where(x => x.tipo_metodo_pago_id != 0 && x.codigo == "RECAM").Sum(x => x.tipo_cambio * x.importe);
                    else if (identificador_externo == 0 && flujo.Count == 0 && id == 0)
                        importe_aplicado = 0;
                }
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
            }
        }
        public void RecalcularTotalPartidas(bool actualizar_precios = true)
        {
            if (actualizar_precios)
                partidas.Where(x => x.documento_id == 0).ToList().ForEach(x => x.ObtenerPrecio(socio_id, lista_precio_id));

            partidas.ForEach(x => x.CalcularTotal());
            CalcularTotal();
        }
        private void AfectarInventario(bool cancelacion = false, List<Partida> partidas = null)
        {
            try
            {
                if (partidas == null)
                    partidas = this.partidas.Where(x => x.id != 0).ToList();

                if (partidas.Count > 0)
                {
                    foreach (Partida partida in partidas)
                    {
                        var articulo = Articulo.Articulos().Where(x => x.id == partida.articulo_id).Select(x => new { x.almacenable, x.grupo_unidad_medida_id }).First();

                        if (articulo.almacenable)
                        {
                            Articulo.Inventario inventario = Articulo.Inventario.Obtener(partida.articulo_id, partida.almacen_id);
                            Articulo.Inventario.Ubicacion inventario_ubicacion = Articulo.Inventario.Ubicacion.Obtener(partida.articulo_id, partida.almacen_id, partida.ubicacion_id);
                            Articulo.Inventario inventario_destino = Articulo.Inventario.Obtener(partida.articulo_id, partida.almacen_destino_id);
                            Articulo.Inventario.Ubicacion inventario_ubicacion_destino = Articulo.Inventario.Ubicacion.Obtener(partida.articulo_id, partida.almacen_destino_id, partida.ubicacion_destino_id);

                            if (cancelacion)
                                partida.cantidad *= -1;

                            decimal equivalencia = (articulo.grupo_unidad_medida_id == 0) ? 1 : UnidadMedida.Grupo.Linea.Lineas().Where(x => x.grupo_unidad_medida_id == articulo.grupo_unidad_medida_id && x.unidad_medida_id == partida.unidad_medida_id).Select(x => x.cantidad_base).FirstOrDefault();
                            decimal cantidad = partida.cantidad * equivalencia;

                            switch (clase)
                            {
                                case "OC":
                                    inventario.pedido += cantidad;
                                    break;
                                case "EM":
                                    inventario.stock += cantidad;
                                    inventario_ubicacion.stock += cantidad;
                                    break;
                                case "DM":
                                    inventario.stock -= cantidad;
                                    inventario_ubicacion.stock -= cantidad;
                                    break;
                                case "PE":
                                    inventario.comprometido += cantidad;
                                    break;
                                case "EN":
                                    inventario.stock -= cantidad;
                                    inventario_ubicacion.stock -= cantidad;
                                    break;
                                case "DV":
                                    inventario.stock += cantidad;
                                    inventario_ubicacion.stock += cantidad;
                                    break;
                                case "AC":
                                    inventario.stock -= cantidad;
                                    inventario_ubicacion.stock -= cantidad;
                                    break;
                                case "FA":
                                    if (reserva)
                                    {
                                        inventario.comprometido += cantidad;
                                    }
                                    else
                                    {
                                        inventario.stock -= cantidad;
                                        inventario_ubicacion.stock -= cantidad;
                                    }
                                    break;
                                case "NC":
                                    inventario.stock += cantidad;
                                    inventario_ubicacion.stock += cantidad;
                                    break;
                                case "ND":
                                    inventario.stock -= cantidad;
                                    inventario_ubicacion.stock -= cantidad;
                                    break;
                                case "ST":
                                    inventario.comprometido += cantidad;
                                    inventario_destino.pedido += cantidad;
                                    break;
                                case "TS":
                                    inventario.stock -= cantidad;
                                    inventario_destino.stock += cantidad;
                                    inventario_ubicacion.stock -= cantidad;
                                    inventario_ubicacion_destino.stock += cantidad;
                                    break;
                                case "AE":
                                    inventario.stock += cantidad;
                                    inventario_ubicacion.stock += cantidad;
                                    break;
                                case "AS":
                                    inventario.stock -= cantidad;
                                    inventario_ubicacion.stock -= cantidad;
                                    break;
                            }

                            if (partida.documento_id != 0)
                            {
                                DataTable relaciones = Utilidades.EjecutarQuery(string.Format("SELECT t1.id, t0.clase, t1.cantidad FROM documentos t0 join partidas t1 on t0.id = t1.documento_id WHERE t1.documento_id in (SELECT documento_origen_id FROM relacion_documentos WHERE documento_destino_id = {0}) and articulo_id = {1} and t1.fecha_creacion = '{2}'", partida.documento_id, partida.articulo_id, partida.fecha_creacion.ToString("yyyyMMdd HH:mm:ss.fff")));
                                if (relaciones.Rows.Count > 0)
                                {
                                    foreach (DataRow relacion in relaciones.Rows)
                                    {
                                        decimal relacion_cantidad = (decimal)relacion["cantidad"];
                                        if (cancelacion)
                                        {
                                            relacion_cantidad *= -1;
                                            partida.cantidad *= -1;
                                        }

                                        switch ((string)relacion["clase"])
                                        {
                                            case "OC":
                                                inventario.pedido -= relacion_cantidad;
                                                break;
                                            case "PE":
                                                inventario.comprometido -= relacion_cantidad;
                                                break;
                                            case "EN":
                                                if (clase.Equals("FA"))
                                                {
                                                    inventario.stock += partida.cantidad;
                                                    inventario_ubicacion.stock += partida.cantidad;
                                                }
                                                break;
                                            case "FA":
                                                if (clase.Equals("EN"))
                                                    inventario.comprometido -= relacion_cantidad;
                                                break;
                                            case "ST":
                                                inventario.comprometido -= relacion_cantidad;
                                                inventario_destino.pedido -= partida.cantidad;
                                                break;
                                        }

                                        //Pendiente evaluar
                                        //if (cancelacion)
                                        //{
                                        //    Partida partida_relacionada = Partida.Obtener((int)relacion["id"]);
                                        //    partida_relacionada.cantidad_pendiente += partida.cantidad;
                                        //    partida_relacionada.Actualizar();
                                        //}
                                    }
                                }
                            }

                            if (inventario.id != 0)
                            {
                                inventario.Actualizar();
                            }
                            else
                            {
                                inventario.articulo_id = partida.articulo_id;
                                inventario.almacen_id = partida.almacen_id;
                                inventario.Agregar();
                            }

                            if (inventario_destino.id != 0)
                                inventario_destino.Actualizar();

                            if (inventario_ubicacion.id != 0)
                                inventario_ubicacion.Actualizar();

                            if (inventario_ubicacion_destino.id != 0)
                                inventario_ubicacion_destino.Actualizar();
                        }
                    }
                }
            }
            catch
            {
                //
            }
        }
        public bool GenerarPuntos()
        {
            try
            {
                if (clase.Equals("FA"))
                {
                    if (monedero_id != 0)
                    {
                        Monedero monedero = Monedero.Obtener(monedero_id);
                        if (monedero.id != 0)
                        {
                            decimal multiplicaodr_socio = Socio.Socios().Where(x => x.id == monedero.socio_id).Select(x => x.multiplicador_puntos).First();
                            if (multiplicaodr_socio > 0)
                            {
                                if (partidas.Count > 0)
                                {
                                    List<Monedero.Partida> partidas_monedero = new List<Monedero.Partida>();
                                    foreach (Partida partida in partidas)
                                    {
                                        try
                                        {
                                            decimal multiplicador_articulo = (Articulo.Precio.Precios().Where(x => x.articulo_id == partida.articulo_id && x.lista_precio_id == lista_precio_id).Select(x => x.multiplicador_puntos).First() / 100);
                                            Monedero.Partida partida_monedero = new Monedero.Partida();
                                            partida_monedero.monedero_id = monedero.id;
                                            partida_monedero.documento_id = id;
                                            partida_monedero.partida_id = partida.id;
                                            partida_monedero.importe = (partida.total * multiplicador_articulo) * multiplicaodr_socio;
                                            partidas_monedero.Add(partida_monedero);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }

                                    if (partidas_monedero.Count > 0)
                                    {
                                        partidas_monedero.ForEach(x => x.Agregar());
                                    }

                                    return true;
                                }
                            }
                        }
                        else
                        {
                            Global.Error = new Nori.Error("El monedero especificado no es válido");
                        }
                    }
                    else
                    {
                        Global.Error = new Nori.Error("El documento no tiene asignado ningún monedero electrónico");
                    }
                }
                else
                {
                    Global.Error = new Nori.Error("Solo se pueden generar puntos de documentos del tipo factura");
                }
                return false;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool GenerarDocumentoElectronico()
        {
            if (EsDocumentoElectronico() && Global.Configuracion.generar_documento_electronico_automaticamente && clase != "TS" && clase != "EN")
                return true;
            else
                return false;
        }
        public bool EsDocumentoElectronico()
        {
            switch (clase)
            {
                case "EN":
                    return true;
                case "AC":
                    return true;
                case "FA":
                    return true;
                case "NC":
                    return true;
                case "ND":
                    return true;
                case "TS":
                    return true;
                default:
                    return false;
            }
        }
        public DocumentoElectronico DocumentoElectronico()
        {
            DocumentoElectronico documento_electronico = new DocumentoElectronico(id);
            try
            {
                if (EsDocumentoElectronico() && id != 0)
                    documento_electronico = NoriSDK.DocumentoElectronico.DocumentosElectronicos().Where(x => x.documento_id == id && x.estado != 'S').First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
            }
            return documento_electronico;
        }
        public bool Cerrar()
        {
            try
            {
                var Tabla = Documentos();
                Documento documento = Tabla.Where(x => x.id == id).First();

                if (documento.estado == 'C')
                {
                    Global.Error = new Nori.Error("Este documento ya ha sido cerrado y no puede modificarse");
                    return false;
                }

                if (documento.cancelado == true)
                {
                    Global.Error = new Nori.Error("Este documento ha sido cancelado y no puede modificarse");
                    return false;
                }

                documento.estado = 'C';
                documento.usuario_actualizacion_id = Global.Usuario.id;
                documento.fecha_actualizacion = DateTime.Now;

                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool Abrir()
        {
            try
            {
                var Tabla = Documentos();
                Documento documento = Tabla.Where(x => x.id == id).First();

                documento.estado = 'A';
                documento.cancelado = false;
                documento.usuario_actualizacion_id = Global.Usuario.id;
                documento.fecha_actualizacion = DateTime.Now;

                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool Cancelar(bool agregar_sincronizacion)
        {
            try
            {
                var Tabla = Documentos();
                Documento documento = Tabla.Where(x => x.id == id).First();

                if (!documento.clase.Equals("FA") && !documento.clase.Equals("NC") && !documento.clase.Equals("EN") && !documento.clase.Equals("PE") && !documento.clase.Equals("DV"))
                {
                    Global.Error = new Nori.Error("La acción solicitada no esta disponible para este tipo de documento.");
                    return false;
                }

                Permiso permiso = Permiso.Obtener(Global.Usuario.id, clase);

                if (permiso.id != 0)
                {
                    if (!permiso.cancelar)
                    {
                        Global.Error = new Nori.Error("No cuentas con el permiso suficiente para realizar esta acción.");
                        return false;
                    }
                }

                if (Global.Usuario.NivelAcceso() <= 50)
                {
                    Global.Error = new Nori.Error("No cuentas con permisos suficientes para realizar esta acción.");
                    return false;
                }

                if (documento.estado == 'C')
                {
                    Global.Error = new Nori.Error("Este documento ya ha sido cerrado y no puede modificarse.");
                    return false;
                }

                if (documento.cancelado)
                {
                    Global.Error = new Nori.Error("Este documento ha sido cancelado y no puede modificarse.");
                    return false;
                }

                if (Utilidades.EjecutarEscalar(string.Format("select count(relacion_documentos.id) from relacion_documentos join documentos on documentos.id = relacion_documentos.documento_destino_id where relacion_documentos.documento_origen_id = {0} and documentos.cancelado = 0", id)) != 0)
                {
                    Global.Error = new Nori.Error("Otros documentos dependen de este, primero cancele los documentos superiores y vuelva a intentar.");
                    return false;
                }

                documento.estado = 'C';
                documento.cancelado = true;
                documento.comentario = comentario;
                documento.usuario_actualizacion_id = Global.Usuario.id;
                documento.fecha_actualizacion = DateTime.Now;
                documento.fecha_cancelacion = DateTime.Now;
                documento.usuario_cancelacion_id = Global.Usuario.id;

                Tabla.Context.SubmitChanges();

                if (Global.Configuracion.sap)
                {
                    if (identificador_externo == 0)
                    {
                        Sincronizacion sincronizacion = Sincronizacion.Obtener(clase, id);
                        if (sincronizacion.id != 0)
                            sincronizacion.Eliminar();
                    }
                    else if (agregar_sincronizacion)
                    {
                        Sincronizacion sincronizacion = new Sincronizacion();
                        sincronizacion.tabla = "documentos";
                        sincronizacion.registro = id;
                        sincronizacion.Agregar();
                    }
                }

                try
                {
                    foreach (Relacion relacion in ObtenerRelaciones())
                    {
                        Documento documento_relacion = Obtener(relacion.documento_origen_id);
                        foreach (Partida partida_relacion in documento_relacion.partidas)
                        {
                            try
                            {
                                var partida_documento = partidas.Where(x => x.articulo_id == partida_relacion.articulo_id && x.fecha_creacion == partida_relacion.fecha_creacion).Select(x => new { x.cantidad, x.total }).First();
                                partida_relacion.cantidad_pendiente += partida_documento.cantidad;

                                if (partida_relacion.cantidad_pendiente > partida_relacion.cantidad)
                                    partida_relacion.cantidad_pendiente = partida_relacion.cantidad;

                                if (clase.Equals("NC") || clase.Equals("ND"))
                                    documento.importe_aplicado -= partida_documento.total;

                                partida_relacion.Actualizar();
                            }
                            catch { continue; }
                        }

                        if (documento_relacion.partidas.Sum(x => x.cantidad_pendiente) > 0)
                            documento_relacion.Abrir();
                    }
                }
                catch { }

                try
                {
                    flujo.All(x => { x.Eliminar(); return true; });
                } catch { }

                Socio.ActualizarBalance(socio_id);
                AfectarInventario(true);
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool Impreso()
        {
            try
            {
                var Tabla = Documentos();
                Documento documento = Tabla.Where(x => x.id == id).First();

                if (documento.impreso)
                    return false;

                documento.impreso = true;

                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool ActualizarIdentificadoresExternos()
        {
            try
            {
                var Tabla = Documentos();
                Documento documento = Tabla.Where(x => x.id == id).First();

                if (identificador_externo == 0)
                {
                    Global.Error = new Nori.Error("Es necesario establecer un identificador.");
                    return false;
                }

                documento.usuario_actualizacion_id = Global.Usuario.id;
                documento.fecha_actualizacion = DateTime.Now;

                documento.identificador_externo = identificador_externo;
                documento.numero_documento_externo = numero_documento_externo;

                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool ExisteIdentificadorExterno()
        {
            try
            {
                return Documentos().Any(x => x.identificador_externo == identificador_externo && x.clase == clase);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public bool Actualizar(bool actualizar_partidas = false, bool agregar_sincronizacion = true, bool forzar = false)
        {
            try
            {
                Permiso permiso = Permiso.Obtener(Global.Usuario.id, clase);

                if (permiso.id != 0)
                {
                    if (!permiso.actualizar)
                    {
                        Global.Error = new Nori.Error("No cuentas con el permiso suficiente para realizar esta acción.");
                        return false;
                    }
                }

                var Tabla = Documentos();
                Documento documento = Tabla.Where(x => x.id == id).First();

                //CAMBIAR
                //if (documento.fecha_actualizacion != fecha_actualizacion && agregar_sincronizacion)
                //{
                //    Global.Error = new Nori.Error("El documento fue actualizado por otro usuario o proceso, es necesario recargarlo.");
                //    return false;
                //}

                if (!forzar)
                {
                    if (Global.Usuario.VendedorForaneo() && identificador_externo != 0)
                    {
                        Global.Error = new Nori.Error("No es posible actualizar este objeto de forma foránea.");
                        return false;
                    }

                    if (documento.identificador_externo != 0)
                        identificador_externo = documento.identificador_externo;

                    if (documento.cancelado == true && agregar_sincronizacion == true)
                    {
                        Global.Error = new Nori.Error("Este documento ha sido cancelado y no puede modificarse.");
                        return false;
                    }

                    if (documento.estado == 'C' && agregar_sincronizacion == true)
                        actualizar_partidas = false;

                    if (referencias.Count > 0)
                    {
                        foreach (Referencia referencia in referencias.Where(x => x.id == 0).ToList())
                        {
                            referencia.documento_id = id;
                            referencia.Agregar();
                        }
                    }

                    if (anexos.Count > 0)
                    {
                        foreach (Anexo anexo in anexos.Where(x => x.id == 0).ToList())
                        {
                            anexo.documento_id = id;
                            anexo.Agregar();
                        }
                    }

                    if (documento.clase.Equals("EM") || documento.clase.Equals("FA") || documento.clase.Equals("AC") || documento.clase.Equals("DV") || documento.clase.Equals("EN") || documento.tipo.Equals("NC") || documento.tipo.Equals("ND") || documento.tipo.Equals("TS"))
                        actualizar_partidas = false;

                    if (partidas.Count == 0)
                        actualizar_partidas = true;

                    if (actualizar_partidas)
                    {
                        List<Partida> partidas_nuevas = partidas.Where(x => x.id == 0).ToList();
                        partidas.Where(x => x.id == 0).All(x => { x.documento_id = id; x.Agregar(); return true; });

                        foreach (Partida partida in Partida.Partidas().Where(x => x.documento_id == id).ToList())
                        {
                            if (partidas.Any(x => x.id == partida.id))
                                partidas.Where(x => x.id == partida.id).First().Actualizar();
                            else
                                partida.Eliminar();
                        }

                        if (agregar_sincronizacion || !Global.Configuracion.sap)
                            AfectarInventario(false, partidas_nuevas);
                    }

                    CalcularTotal();

                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                }

                this.CopyProperties(documento);

                if (documento.identificador_externo != 0 && identificador_externo == 0)
                {
                    identificador_externo = documento.identificador_externo;
                    numero_documento_externo = documento.numero_documento_externo;
                }

                Tabla.Context.SubmitChanges();

                if (Global.Configuracion.sap && agregar_sincronizacion)
                {
                    Sincronizacion sincronizacion = new Sincronizacion();
                    sincronizacion.tabla = "documentos";
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
        public static bool FacturarEntregas(int serie_id, bool socio_predeterminado, bool credito)
        {
            try
            {
                if (Global.Usuario.rol.Equals('A'))
                {
                    List<int> socios = (socio_predeterminado) ? new List<int> { Global.Usuario.socio_id } : Documentos().Where(x => x.socio_id != Global.Usuario.socio_id && x.clase == "EN" && x.estado == 'A' && x.cancelado == false && x.serie_id == serie_id).Select(x => x.socio_id).Distinct().ToList();

                    if (socios.Count > 1)
                        socios = Socio.Socios().Where(x => socios.Contains(x.id)).OrderBy(x => x.codigo).Select(x => x.id).ToList();

                    foreach (int socio in socios)
                    {
                        bool entregas_pendientes = (credito) ? Documentos().Any(x => x.socio_id == socio && x.clase == "EN" && x.estado == 'A' && x.cancelado == false && x.serie_id == serie_id && x.importe_aplicado == 0) : Documentos().Any(x => x.socio_id == socio && x.clase == "EN" && x.estado == 'A' && x.cancelado == false && x.serie_id == serie_id && x.importe_aplicado > 0);

                        if (entregas_pendientes)
                        {
                            List<DateTime> fechas = (credito) ? Documentos().Where(x => x.socio_id == socio && x.clase == "EN" && x.estado == 'A' && x.cancelado == false && x.importe_aplicado == 0 && x.serie_id == serie_id).OrderBy(x => x.fecha_creacion).Select(x => x.fecha_documento).Distinct().ToList() : Documentos().Where(x => x.socio_id == socio && x.clase == "EN" && x.estado == 'A' && x.cancelado == false && x.importe_aplicado > 0 && x.serie_id == serie_id).OrderBy(x => x.fecha_creacion).Select(x => x.fecha_documento).Distinct().ToList();
                            foreach (DateTime fecha in fechas)
                            {
                                List<int> entregas = (credito) ? Documentos().Where(x => x.socio_id == socio && x.clase == "EN" && x.estado == 'A' && x.cancelado == false && x.importe_aplicado == 0 && x.serie_id == serie_id && x.fecha_documento == fecha).OrderBy(x => x.fecha_creacion).Select(x => x.id).ToList() : Documentos().Where(x => x.socio_id == socio && x.clase == "EN" && x.estado == 'A' && x.cancelado == false && x.importe_aplicado > 0 && x.serie_id == serie_id && x.fecha_documento == fecha).OrderBy(x => x.fecha_creacion).Select(x => x.id).ToList();

                                if (entregas.Count > 0)
                                {
                                    Documento documento = new Documento();

                                    documento.clase = "FA";
                                    documento.serie_id = Serie.Obtener(serie_id).serie_facturacion_id;
                                    documento.EstablecerSocio(Socio.Obtener(socio));

                                    List<Partida> partidas = Partida.Partidas().Where(x => entregas.Contains(x.documento_id) && x.cantidad_pendiente > 0).ToList();
                                    decimal importe_aplicado = Documentos().Where(x => entregas.Contains(x.id)).Sum(x => x.importe_aplicado);
                                    var descuentos = Documentos().Where(x => entregas.Contains(x.id) && x.porcentaje_descuento > 0).Select(x => new { x.id, x.porcentaje_descuento }).ToList();

                                    foreach (var descuento in descuentos)
                                    {
                                        partidas.Where(x => x.documento_id == descuento.id).ToList().All(x => { x.porcentaje_descuento = (x.porcentaje_descuento + descuento.porcentaje_descuento); x.CalcularTotal(); return true; });
                                    }

                                    partidas.All(x => { x.cantidad = x.cantidad_pendiente; x.cantidad_pendiente = x.cantidad; return true; });

                                    documento.partidas.Clear();
                                    documento.partidas = partidas;

                                    documento.importe_aplicado = importe_aplicado;

                                    if (credito)
                                        entregas.ForEach(x => documento.comentario += ", " + x);
                                    else
                                        documento.comentario = string.Format("Factura global del día: {0}", fecha.ToShortDateString());

                                    documento.generar_documento_electronico = documento.GenerarDocumentoElectronico();
                                    documento.Agregar();

                                    //Separa documentos
                                    //if (credito)
                                        //SepararDocumento(documento);
                                    //else
                                       //documento.Agregar();
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    return true;
                }
                else
                {
                    Global.Error = new Nori.Error("No es posible realizar esta acción con el permiso actual.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Partida
        [Table(Name = "partidas")]
        public class Partida
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int documento_id { get; set; }
            [Column]
            public int impuesto_id { get; set; }
            [Column]
            public int moneda_id { get; set; }
            [Column]
            public int almacen_id { get; set; }
            [Column]
            public int ubicacion_id { get; set; }
            [Column]
            public int almacen_destino_id { get; set; }
            [Column]
            public int ubicacion_destino_id { get; set; }
            [Column]
            public int unidad_medida_id { get; set; }
            [Column]
            public int articulo_id { get; set; }
            [Column]
            public int tipo_empaque_id { get; set; }
            [Column]
            public int tipo_tara_id { get; set; }
            [Column]
            public string sku { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]
            public string codigo_barras { get; set; }
            [Column]
            public decimal cantidad { get; set; }
            [Column]
            public decimal cantidad_empaque { get; set; }
            [Column]
            public decimal cantidad_paquete { get; set; }
            [Column]
            public decimal cantidad_pendiente { get; set; }
            [Column]
            public decimal tipo_cambio { get; set; }
            [Column]
            public decimal precio { get; set; }
            [Column]
            public decimal costo { get; set; }
            [Column]
            public decimal precio_pieza { get; set; }
            [Column]
            public decimal porcentaje_descuento { get; set; }
            [Column]
            public decimal descuento { get; set; }
            [Column]
            public decimal porcentaje_impuesto { get; set; }
            [Column]
            public decimal impuesto { get; set; }
            [Column]
            public decimal subtotal { get; set; }
            [Column]
            public decimal total { get; set; }
            [Column]
            public string comentario { get; set; }
            [Column(CanBeNull = true)]
            public string norma_reparto { get; set; }
            [Column(CanBeNull = true)]
            public string numero_pedimento { get; set; }
            [Column]
            public int documento_base_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }
            [Column]
            public DateTime? fecha_lectura { get; set; }
            [Column]
            public decimal stock { get; set; }
            [Column]
            public int lista_precio_id { get; set; }
            //Uso interno
            public string clave_unidad { get; set; }
            public decimal diferencia {
                get
                {
                    return cantidad - stock;
                }
            }

            public static Table<Partida> Partidas()
            {
                return Nori.CrearContexto().GetTable<Partida>();
            }

            public Partida()
            {
                impuesto_id = Global.Configuracion.impuesto_id;
                almacen_id = Global.Usuario.almacen_id;
                almacen_destino_id = 0;
                cantidad = 1;
                cantidad_empaque = 0;
                comentario = string.Empty;
                norma_reparto = string.Empty;
                clave_unidad = string.Empty;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
                fecha_lectura = DateTime.Now;
            }

            public static Partida Obtener(int id)
            {
                try
                {
                    Partida partida = Partidas().Where(x => x.id == id).First();
                    partida.CalcularTotal();
                    return partida;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Partida();
                }
            }

            public static Partida ObtenerPorSKU(int documento_id, string sku)
            {
                try
                {
                    Partida partida = Partidas().Where(x => x.documento_id == documento_id && x.sku == sku).First();
                    partida.CalcularTotal();
                    return partida;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Partida();
                }
            }

            public bool Agregar()
            {
                try
                {
                    if (cantidad == 0)
                        return false;

                    if (codigo_barras.IsNullOrEmpty())
                        codigo_barras = string.Empty;

                    var Tabla = Partidas();
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
                    var Tabla = Partidas();
                    Partida partida = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(partida);
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
                    var Tabla = Partidas();
                    Partida partida = Tabla.Where(x => x.id == id).First();
                    Tabla.DeleteOnSubmit(partida);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
            internal bool ObtenerPrecio(int socio_id, int lista_precio_id)
            {
                try
                {
                    var precios = Articulo.Articulos().Where(x => x.id == articulo_id).Select(x => new { x.ultimo_precio_compra, x.ultimo_precio_determinado }).First();

                    if (lista_precio_id == -2)
                    {
                        moneda_id = Global.Configuracion.moneda_id;
                        tipo_cambio = 1;
                        precio = precios.ultimo_precio_determinado;
                    }
                    else if (lista_precio_id == -1)
                    {
                        moneda_id = Global.Configuracion.moneda_id;
                        tipo_cambio = 1;
                        precio = precios.ultimo_precio_compra;
                    }
                    else
                    {
                        Articulo.Precio precio = Articulo.Precio.Obtener(articulo_id, lista_precio_id, unidad_medida_id);
                        this.lista_precio_id = lista_precio_id;
                        moneda_id = precio.moneda_id;

                        if (moneda_id == 0)
                        {
                            Global.Error = new Nori.Error("No se ha establecido una moneda para este precio.");
                            return false;
                        }

                        tipo_cambio = ObtenerTipoCambio(moneda_id, tipo_cambio);
                        this.precio = precio.precio;

                        if (porcentaje_descuento == 0)
                            porcentaje_descuento = Articulo.ObtenerDescuento(articulo_id, socio_id, lista_precio_id, cantidad);
                    }

                    Impuesto impuesto = Articulo.ObtenerImpuesto(articulo_id, socio_id);

                    if (impuesto.id != 0)
                    {
                        impuesto_id = impuesto.id;
                        porcentaje_impuesto = impuesto.porcentaje;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
            public bool ObtenerPrecioUdM()
            {
                try
                {
                    Articulo.Precio precio = Articulo.Precio.Obtener(articulo_id, lista_precio_id, unidad_medida_id);
                    moneda_id = precio.moneda_id;

                    if (moneda_id == 0)
                    {
                        Global.Error = new Nori.Error("No se ha establecido una moneda para este precio.");
                        return false;
                    }

                    tipo_cambio = ObtenerTipoCambio(moneda_id, tipo_cambio);
                    this.precio = precio.precio;

                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
            public void ObtenerCodigoBarrasUdM()
            {
                try
                {
                    codigo_barras = Articulo.CodigoBarras.CodigosBarras().Where(x => x.articulo_id == articulo_id && x.unidad_medida_id == unidad_medida_id).Select(x => x.codigo).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                }
            }
            public bool ObtenerPrecio(int socio_id)
            {
                try
                {
                    Articulo.Precio precio = Articulo.Precio.Obtener(articulo_id, lista_precio_id, unidad_medida_id);
                    moneda_id = precio.moneda_id;

                    if (moneda_id == 0)
                    {
                        Global.Error = new Nori.Error("No se ha establecido una moneda para este precio.");
                        return false;
                    }

                    tipo_cambio = ObtenerTipoCambio(moneda_id, tipo_cambio);
                    this.precio = precio.precio;

                    if (porcentaje_descuento == 0)
                        porcentaje_descuento = Articulo.ObtenerDescuento(articulo_id, socio_id, lista_precio_id, cantidad);

                    Impuesto impuesto = Articulo.ObtenerImpuesto(articulo_id, socio_id);

                    if (impuesto.id != 0)
                    {
                        impuesto_id = impuesto.id;
                        porcentaje_impuesto = impuesto.porcentaje;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
            public void ObtenerDescuento(int socio_id)
            {
                try
                {
                    porcentaje_descuento = Articulo.ObtenerDescuento(articulo_id, socio_id, lista_precio_id, cantidad);
                }
                catch { }
            }
            public decimal ObtenerStock()
            {
                try
                {
                    if (Articulo.Articulos().Any(x => x.id == articulo_id && x.almacenable == false))
                    {
                        stock = 10000;
                    }
                    else
                    {
                        if (Almacen.Almacenes().Any(x => x.id == almacen_id && x.ubicaciones == true))
                        {
                            stock = Articulo.Inventario.Ubicacion.Ubicaciones().Where(x => x.articulo_id == articulo_id && x.almacen_id == almacen_id && x.ubicacion_id == ubicacion_id).Select(x => x.stock).First();
                        }
                        else
                        {
                            int grupo_unidad_medida_id = Articulo.Articulos().Where(x => x.id == articulo_id).Select(x => x.grupo_unidad_medida_id).FirstOrDefault();
                            decimal equivalencia = UnidadMedida.Grupo.Linea.Lineas().Where(x => x.grupo_unidad_medida_id == grupo_unidad_medida_id && x.unidad_medida_id == unidad_medida_id).Select(x => x.cantidad_base).FirstOrDefault();
                            stock = Articulo.Inventario.Inventarios().Where(x => x.articulo_id == articulo_id && x.almacen_id == almacen_id).Select(x => x.stock).First();
                            if (equivalencia != 0)
                                stock /= equivalencia;
                        }
                    }

                    return stock;
                }
                catch
                {
                    stock = 0;
                    return stock;
                }
            }

            public bool VerificarExistencia()
            {
                try
                {
                    if (Articulo.Articulos().Where(x => x.id == articulo_id).Select(x => x.almacenable).First())
                    {
                        ObtenerStock();
                        
                        if (stock < cantidad)
                        {
                            Global.Error = new Nori.Error(string.Format("La cantidad recae en un inventario negativo (Stock actual {0} del artículo {1}).", stock, sku));
                            return false;
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

            public void CalcularCantidadPendiente(int documento_id)
            {
                try
                {
                    //if (!Documentos().Any(x => (x.clase == "OC" || x.clase == "PE") && x.id == this.documento_id))
                    if (!Documentos().Any(x => x.id == this.documento_id))
                    {
                        if (id != 0)
                        {
                            if (Relacion.RelacionDocumentos().Any(x => x.documento_origen_id == documento_id))
                            {
                                List<int> documentos_destino = Relacion.RelacionDocumentos().Where(x => x.documento_origen_id == documento_id).Select(x => x.documento_destino_id).ToList();
                                if (documentos_destino.Count > 0)
                                {
                                    decimal cantidad_entregada = Partidas().Where(x => documentos_destino.Contains(x.documento_id) && x.articulo_id == articulo_id && x.fecha_creacion == fecha_creacion).Sum(x => x.cantidad);

                                    cantidad_pendiente = cantidad - cantidad_entregada;

                                    if (cantidad > cantidad_pendiente)
                                        cantidad = cantidad_pendiente;
                                }
                            }
                            else
                            {
                                cantidad_pendiente = cantidad;
                            }
                        }
                        else
                        {
                            cantidad_pendiente = cantidad;
                        }
                    }
                    else
                    {
                        cantidad_pendiente = cantidad;
                    }
                }
                catch { cantidad_pendiente = cantidad; }

                CalcularTotal();
            }

            public void CalcularTotal()
            {
                try
                {
                    decimal precio_por_tc = Math.Round(precio * tipo_cambio, 2);
                    cantidad = Math.Round(cantidad, 4);

                    precio_pieza = (cantidad_paquete != 0) ? precio / cantidad_paquete : precio;
                    porcentaje_descuento = (porcentaje_descuento > 100) ? 100 : porcentaje_descuento;

                    descuento = (porcentaje_descuento / 100) * precio_por_tc;
                    impuesto = (porcentaje_impuesto / 100) * (precio_por_tc - descuento);
                    subtotal = cantidad * (precio_por_tc - descuento);

                    total = cantidad * impuesto + subtotal;
                } catch { }
            }

            public void ModificarUnidadMedida()
            {
                
                try
                {
                    ObtenerStock();
                    ObtenerPrecioUdM();
                    ObtenerCodigoBarrasUdM();
                    CalcularTotal();
                } catch { }
            }

            public void ModificarTipoCambio(decimal tipo_cambio)
            {
                this.tipo_cambio = tipo_cambio;
                CalcularTotal();
            }

            public decimal ObtenerTipoCambio(int moneda_documento, decimal tipo_cambio_documento)
            {
                try
                {
                    if (Global.Configuracion.moneda_id != moneda_id)
                        if (moneda_id != moneda_documento)
                        {
                            if (documento_id != 0)
                                return Documentos().Where(x => x.id == documento_id).Select(x => new { x.tipo_cambio }).First().tipo_cambio;

                            return TipoCambio.Venta(moneda_id);
                        }
                        else
                        {
                            return tipo_cambio_documento;
                        }
                    else
                        return 1;
                }
                catch
                {
                    return 1;
                }
            }

            public void ModificarTotal()
            {
                try
                {
                    decimal total = Math.Round(cantidad, 4) * ((Math.Round(precio * tipo_cambio, 2)) + Math.Round(impuesto, 2));

                    porcentaje_descuento = 100 - ((this.total / total) * 100);
                    if (porcentaje_descuento > 100)
                        porcentaje_descuento = 100;
                    else if (porcentaje_descuento <= 0)
                        porcentaje_descuento = 0;

                    CalcularTotal();
                } catch { }
            }

            public bool Modificable()
            {
                return (id != 0 && cantidad_pendiente == 0) ? false : true;
            }
        }
        #endregion

        #region Relacion
        [Table(Name = "relacion_documentos")]
        public class Relacion
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int documento_origen_id { get; set; }
            [Column]
            public int documento_destino_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Relacion> RelacionDocumentos()
            {
                return Nori.CrearContexto().GetTable<Relacion>();
            }

            public Relacion()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Relacion Obtener(int documento_orignen_id, int documento_destino_id)
            {
                try
                {
                    return RelacionDocumentos().Where(x => x.documento_origen_id == documento_orignen_id && x.documento_destino_id == documento_destino_id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Relacion();
                }
            }

            public bool Agregar()
            {
                try
                {
                    if (documento_origen_id == 0 || documento_destino_id == 0)
                    {
                        Global.Error = new Nori.Error("Debe especificar el documento de origen y destino.");
                        return false;
                    }

                    var Tabla = RelacionDocumentos();
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

            public bool Eliminar()
            {
                try
                {
                    if (!Global.Usuario.rol.Equals('A'))
                    {
                        Global.Error = new Nori.Error("No cuentas con los suficientes privilegios para realizar esta acción.");
                        return false;
                    }

                    int documento_base_id = documento_origen_id;

                    var Tabla = RelacionDocumentos();
                    Tabla.Attach(this);
                    Tabla.DeleteOnSubmit(this);
                    Tabla.Context.SubmitChanges();

                    List<Partida> partidas = Documento.Partida.Partidas().Where(x => x.documento_base_id == documento_base_id).ToList();
                    partidas.All(x => { x.documento_base_id = 0; x.Actualizar(); return true; });

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

        #region Referencia
        [Table(Name = "referencias_documentos")]
        public class Referencia
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int documento_id { get; set; }
            [Column]
            public int documento_referencia_id { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Referencia> Referencias()
            {
                return Nori.CrearContexto().GetTable<Referencia>();
            }

            public Referencia()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Referencia Obtener(int documento_id, int documento_referencia_id)
            {
                try
                {
                    return Referencias().Where(x => x.documento_id == documento_id && x.documento_referencia_id == documento_referencia_id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Referencia();
                }
            }

            public bool Agregar()
            {
                try
                {
                    if (documento_id == 0 || documento_referencia_id == 0)
                    {
                        Global.Error = new Nori.Error("Debe especificar la relación.");
                        return false;
                    }

                    if (Referencias().Any(x => x.documento_id == documento_id && x.documento_referencia_id == documento_referencia_id))
                    {
                        Global.Error = new Nori.Error("La referencia especificada ya existe.");
                        return false;
                    }

                    var Tabla = Referencias();
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

            public bool Eliminar()
            {
                try
                {
                    if (!Global.Usuario.rol.Equals('A'))
                    {
                        Global.Error = new Nori.Error("No cuentas con los suficientes privilegios para realizar esta acción.");
                        return false;
                    }

                    var Tabla = Referencias();
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

        #region Anexo
        [Table(Name = "anexos_documentos")]
        public class Anexo
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int documento_id { get; set; }
            [Column]
            public string anexo { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Anexo> Anexos()
            {
                return Nori.CrearContexto().GetTable<Anexo>();
            }

            public Anexo()
            {
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Anexo Obtener(int id)
            {
                try
                {
                    return Anexos().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Anexo();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Anexos();
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
        }
        #endregion

        #region Tipo
        public class Tipo
        {
            public char tipo { get; set; }
            public string nombre { get; set; }
            public static List<Tipo> Tipos()
            {
                List<Tipo> tipos = new List<Tipo>();

                Tipo clase = new Tipo();

                clase.tipo = 'V';
                clase.nombre = "Venta";
                tipos.Add(clase);

                clase = new Tipo();

                clase.tipo = 'C';
                clase.nombre = "Compra";
                tipos.Add(clase);

                clase = new Tipo();

                clase.tipo = 'I';
                clase.nombre = "Operación de inventario";
                tipos.Add(clase);

                return tipos;
            }

            public static char ObtenerPredeterminado()
            {
                return 'V';
            }
        }
        #endregion

        #region Clase
        public class Clase
        {
            public char tipo { get; set; }
            public string clase { get; set; }
            public string nombre { get; set; }
            public static List<Clase> Clases()
            {
                List<Clase> clases = new List<Clase>();

                //Ventas
                Clase clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "CO";
                clase.nombre = "Cotización";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "PE";
                clase.nombre = "Pedido";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "EN";
                clase.nombre = "Entrega";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "DV";
                clase.nombre = "Devolución";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "AC";
                clase.nombre = "Anticipo de cliente";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "FA";
                clase.nombre = "Factura de cliente";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "NC";
                clase.nombre = "Nota de crédito";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "ND";
                clase.nombre = "Nota de débito";
                clases.Add(clase);

                //Pagos
                clase = new Clase();

                clase.tipo = 'P';
                clase.clase = "PR";
                clase.nombre = "Pago recibido";
                clases.Add(clase);

                //Operaciones de stock
                clase = new Clase();

                clase.tipo = 'I';
                clase.clase = "ST";
                clase.nombre = "Solicitud de traslado";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'I';
                clase.clase = "TS";
                clase.nombre = "Transferencia de stock";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'I';
                clase.clase = "AE";
                clase.nombre = "Ajuste de entrada";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'I';
                clase.clase = "AS";
                clase.nombre = "Ajuste de salida";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'I';
                clase.clase = "IF";
                clase.nombre = "Inventario físico";
                clases.Add(clase);

                //Compras
                clase = new Clase();

                clase.tipo = 'C';
                clase.clase = "CC";
                clase.nombre = "Cotización de compra";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'C';
                clase.clase = "OC";
                clase.nombre = "Orden de compra";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'C';
                clase.clase = "EM";
                clase.nombre = "Entrada de mercancías";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'C';
                clase.clase = "DM";
                clase.nombre = "Devolución de mercancías";
                clases.Add(clase);

                clase = new Clase();

                clase.tipo = 'C';
                clase.clase = "FP";
                clase.nombre = "Factura de proveedor";
                clases.Add(clase);

                return clases;
            }
            public static Clase ObtenerPredeterminado()
            {
                Clase clase = new Clase();

                clase.tipo = 'V';
                clase.clase = "EN";
                clase.nombre = "Entrega";

                return clase;
            }

            public List<string> CopiarA()
            {
                List<string> clases = new List<string>();

                bool foraneo = Global.Usuario.VendedorForaneo();

                if (foraneo)
                    if (clase != "CO")
                        return clases;

                switch (clase)
                {
                    //Compras
                    case "CC":
                        clases.AddRange(new string[] { "OC", "EM" });
                        break;
                    case "OC":
                        clases.AddRange(new string[] { "EM" });
                        break;
                    case "EM":
                        clases.AddRange(new string[] { "DM" });
                        break;
                    //Ventas
                    case "CO":
                        if (foraneo)
                            clases.AddRange(new string[] { "PE" });
                        else
                            clases.AddRange(new string[] { "PE", "EN", "FA" });
                        break;
                    case "PE":
                        clases.AddRange(new string[] { "EN", "FA" });
                        break;
                    case "EN":
                        clases.AddRange(new string[] { "DV", "FA" });
                        break;
                    case "DV":
                        clases.AddRange(new string[] { "NC", "EN" });
                        break;
                    case "FA":
                        clases.AddRange(new string[] { "NC", "EN" });
                        break;
                    //Inventario
                    case "ST":
                        clases.AddRange(new string[] { "TS" });
                        break;
                }

                return clases;
            }
            public List<string> CopiarDe()
            {
                List<string> clases = new List<string>();

                switch (clase)
                {
                    //Compras
                    case "OC":
                        clases.AddRange(new string[] { "CC" });
                        break;
                    case "EM":
                        clases.AddRange(new string[] { "OC" });
                        break;
                    case "DM":
                        clases.AddRange(new string[] { "EM" });
                        break;
                    //Ventas
                    case "PE":
                        clases.AddRange(new string[] { "CO" });
                        break;
                    case "EN":
                        clases.AddRange(new string[] { "CO", "PE", "DV" });
                        break;
                    case "FA":
                        clases.AddRange(new string[] { "CO", "PE", "EN" });
                        break;
                    case "DV":
                        clases.AddRange(new string[] { "EN" });
                        break;
                    case "NC":
                        clases.AddRange(new string[] { "DV", "FA" });
                        break;
                    case "ND":
                        clases.AddRange(new string[] { "CO", "PE", "EN" });
                        break;
                    //Inventario
                    case "TS":
                        clases.AddRange(new string[] { "ST" });
                        break;
                }

                return clases;
            }
        }
        #endregion

        #region Estado
        public class Estado
        {
            public char estado { get; set; }
            public string nombre { get; set; }
            public static List<Estado> Estados()
            {
                List<Estado> estados = new List<Estado>();

                Estado estado = new Estado();

                estado.estado = 'A';
                estado.nombre = "Abierto";
                estados.Add(estado);

                estado = new Estado();

                estado.estado = 'C';
                estado.nombre = "Cerrado";
                estados.Add(estado);

                estado = new Estado();

                estado.estado = 'B';
                estado.nombre = "Borrador";
                estados.Add(estado);

                estado = new Estado();

                estado.estado = 'P';
                estado.nombre = "Pendiente";
                estados.Add(estado);

                return estados;
            }

            public static char ObtenerPredeterminado()
            {
                return 'A';
            }
        }
        #endregion

        #region UsoCFDI
        public class UsoCFDI
        {
            public string uso { get; set; }
            public string nombre { get; set; }

            public UsoCFDI(string uso, string nombre)
            {
                this.uso = uso;
                this.nombre = nombre;
            }
            public static List<UsoCFDI> UsosCFDI()
            {
                List<UsoCFDI> usos_cfdi = new List<UsoCFDI>();
                usos_cfdi.AddRange(new UsoCFDI[] {
                    new UsoCFDI("G01", "Adquisición de mercancias"),
                    new UsoCFDI("G02", "Devoluciones, descuentos o bonificaciones"),
                    new UsoCFDI("G03", "Gastos en general"),
                    new UsoCFDI("I01", "Construcciones"),
                    new UsoCFDI("I02", "Mobilario y equipo de oficina por inversiones"),
                    new UsoCFDI("I03", "Equipo de transporte"),
                    new UsoCFDI("I04", "Equipo de computo y accesorios"),
                    new UsoCFDI("I05", "Dados, troqueles, moldes, matrices y herramental"),
                    new UsoCFDI("I06", "Comunicaciones telefónicas"),
                    new UsoCFDI("I07", "Comunicaciones satelitales"),
                    new UsoCFDI("I08", "Otra maquinaria y equipo"),
                    new UsoCFDI("D01", "Honorarios médicos, dentales y gastos hospitalarios."),
                    new UsoCFDI("D02", "Gastos médicos por incapacidad o discapacidad"),
                    new UsoCFDI("D03", "Gastos funerales."),
                    new UsoCFDI("D04", "Donativos."),
                    new UsoCFDI("D05", "Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación)."),
                    new UsoCFDI("D06", "Aportaciones voluntarias al SAR."),
                    new UsoCFDI("D07", "Primas por seguros de gastos médicos."),
                    new UsoCFDI("D08", "Gastos de transportación escolar obligatoria."),
                    new UsoCFDI("D09", "Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones."),
                    new UsoCFDI("D10", "Pagos por servicios educativos (colegiaturas)"),
                    new UsoCFDI("P01", "Por definir")
                });
                return usos_cfdi;
            }

            public static string ObtenerPredeterminado()
            {
                return "G03";
            }
        }
        #endregion
    }
}