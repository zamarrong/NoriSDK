using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "pagos")]
    public class Pago
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int serie_id { get; set; }
        [Column]
        public int numero_documento { get; set; }
        [Column]
        public int socio_id { get; set; }
        [Column]
        public int metodo_pago_id { get; set; }
        [Column]
        public int moneda_id { get; set; }
        [Column]
        public decimal tipo_cambio { get; set; }
        [Column]
        public decimal total { get; set; }
        [Column]
        public string referencia { get; set; }
        [Column]
        public string comentario { get; set; }
        [Column]
        public bool cuenta { get; set; }
        [Column]
        public bool financiado { get; set; }
        [Column]
        public bool cancelado { get; set; }
        [Column]
        public DateTime fecha_contabilizacion { get; set; }
        [Column]
        public DateTime fecha_vencimiento { get; set; }
        [Column]
        public DateTime fecha_documento { get; set; }
        [Column]
        public int identificador_externo { get; set; }
        [Column]
        public int numero_documento_externo { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }
        //Interno
        public List<Partida> partidas { get; internal set; }
        public List<Flujo> flujo { get; internal set; }

        public static Table<Pago> Pagos()
        {
            return Nori.CrearContexto().GetTable<Pago>();
        }

        public Pago()
        {
            moneda_id = Global.Configuracion.moneda_id;
            tipo_cambio = 1;
            cuenta = false;
            financiado = false;
            cancelado = false;
            fecha_contabilizacion = DateTime.Now;
            fecha_vencimiento = DateTime.Now;
            fecha_documento = DateTime.Now;
            partidas = new List<Partida>();
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;

            partidas = new List<Partida>();
            flujo = new List<Flujo>();
        }

        public static Pago Obtener(int id)
        {
            try
            {
                var pago = Pagos().Where(x => x.id == id).First();
                pago.partidas = pago.ObtenerPartidas();
                pago.flujo = pago.ObtenerFlujo();
                return pago;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Pago();
            }
        }
        public List<Partida> ObtenerPartidas()
        {
            try
            {
                return Partida.Partidas().Where(x => x.pago_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Partida>();
            }
        }

        public List<Flujo> ObtenerFlujo()
        {
            try
            {
                return (financiado) ? new List<Flujo>() : Flujo.Flujos().Where(x => x.pago_id == id && x.tipo_metodo_pago_id != 0).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Flujo>();
            }
        }

        public bool Agregar()
        {
            try
            {
                if (flujo.Count == 0 && !financiado)
                {
                    Global.Error = new Nori.Error("No se han indicado los medios de pago.");
                    return false;
                }

                if (partidas.Count == 0 && !cuenta)
                {
                    Global.Error = new Nori.Error("No se han indicado los documentos a pagar.");
                    return false;
                }


                if (!partidas.Any(x => x.saldo > x.importe))
                    if (financiado)
                        financiado = false;

                Serie serie = (serie_id == 0) ? Serie.ObtenerPredeterminado("PR") : Serie.Obtener(serie_id);

                if (serie.id == 0)
                {
                    Global.Error = new Nori.Error("Aún no se ha establecido una serie predeterminada para este tipo de documento.");
                    return false;
                }

                if (serie.clase != "PR")
                {
                    Global.Error = new Nori.Error("El tipo de documento de la serie indicada y la del documento no coinciden.");
                    return false;
                }
                if (numero_documento == 0)
                    numero_documento = serie.siguiente;

                serie_id = serie.id;

                var Tabla = Pagos();
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();

                partidas.Where(x => x.id == 0).ToList().ForEach(x => { x.pago_id = id; x.Agregar(); });
                flujo.Where(x => x.id == 0).ToList().ForEach(x => { x.pago_id = id; x.Agregar(); });
                flujo.Where(x => x.id != 0).ToList().ForEach(x => { x.pago_id = id; x.Actualizar(); });

                serie.EstablecerSiguiente();
                Socio.ActualizarBalance(socio_id);

                if (Global.Configuracion.sap && identificador_externo == 0 && id != 0)
                {
                    Sincronizacion sincronizacion = new Sincronizacion();
                    sincronizacion.tabla = "pagos";
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

        public bool Actualizar(bool agregar_sincronizacion = true, bool actualizar_listas = true)
        {
            try
            {
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
                var Tabla = Pagos();
                Pago pago = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(pago);
                Tabla.Context.SubmitChanges();

                if (financiado && actualizar_listas)
                {
                    partidas.Where(x => x.id != 0 && x.importe > 0).ToList().ForEach(x => { x.pago_id = id; x.Actualizar(); });
                    flujo.Where(x => x.id == 0).ToList().ForEach(x => { x.pago_id = id; x.Agregar(); });
                    flujo.Where(x => x.id != 0).ToList().ForEach(x => { x.pago_id = id; x.Actualizar(); });
                }

                Socio.ActualizarBalance(socio_id);

                if (Global.Configuracion.sap && agregar_sincronizacion)
                {
                    Sincronizacion sincronizacion = new Sincronizacion();
                    sincronizacion.tabla = "pagos";
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

        public bool Cancelar(bool agregar_sincronizacion)
        {
            try
            {
                var Tabla = Pagos();
                Pago pago = Tabla.Where(x => x.id == id).First();
                Permiso permiso = Permiso.Obtener(Global.Usuario.id, "PR");

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

                if (pago.cancelado == true)
                {
                    Global.Error = new Nori.Error("Este documento ha sido cancelado y no puede modificarse.");
                    return false;
                }

                pago.cancelado = true;
                pago.comentario = comentario;
                pago.usuario_actualizacion_id = Global.Usuario.id;
                pago.fecha_actualizacion = DateTime.Now;

                Tabla.Context.SubmitChanges();

                if (Global.Configuracion.sap)
                {
                    if (identificador_externo == 0)
                    {
                        Sincronizacion sincronizacion = Sincronizacion.Obtener("pagos", id);
                        if (sincronizacion.id != 0)
                            sincronizacion.Eliminar();
                    }
                    else if (agregar_sincronizacion)
                    {
                        Sincronizacion sincronizacion = new Sincronizacion();
                        sincronizacion.tabla = "pagos";
                        sincronizacion.registro = id;
                        sincronizacion.Agregar();
                    }
                }

                try
                {
                    foreach (Flujo flujo_pago in flujo)
                    {
                        flujo_pago.documento_id = 0;
                        flujo_pago.Actualizar();
                    }

                    foreach (Partida partida in partidas)
                    {
                        try
                        {
                            var TablaDocumentos = Documento.Documentos();
                            Documento documento = TablaDocumentos.Where(x => x.id == partida.documento_id).First();
                            documento.estado = 'A';
                            documento.importe_aplicado -= partida.tipo_cambio * partida.importe;
                            TablaDocumentos.Context.SubmitChanges();
                        }
                        catch { continue; }
                    }
                }
                catch { }

                try
                {
                    flujo.All(x => { x.Eliminar(); return true; });
                }
                catch { }

                Socio.ActualizarBalance(socio_id);
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
                var Tabla = Pagos();
                Pago pago = Tabla.Where(x => x.id == id).First();
                pago.identificador_externo = identificador_externo;
                pago.numero_documento_externo = numero_documento_externo;
                Tabla.Context.SubmitChanges();
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

                pago.codigo = (Socio.Socios().Where(x => x.id == socio_id).Select(x => new { x.tipo }).First().tipo == 'P') ? "RECOM" : "INVEN";

                MetodoPago.Tipo tipo_metodo_pago = MetodoPago.Tipo.Obtener(MetodoPago.Obtener(Global.Configuracion.metodo_pago_id).tipo_metodo_pago_id);

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

        public bool AgregarPago(int tipo_metodo_pago_id, decimal importe, string referencia = null)
        {
            try
            {
                Flujo pago = new Flujo();

                pago.codigo = (Socio.Socios().Where(x => x.id == socio_id).Select(x => new { x.tipo }).First().tipo == 'P') ? "RECOM" : "INVEN";

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

        public DocumentoElectronico DocumentoElectronico()
        {
            DocumentoElectronico documento_electronico = new DocumentoElectronico(id, true);
            try
            {
                documento_electronico = NoriSDK.DocumentoElectronico.DocumentosElectronicos().Where(x => x.pago_id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
            }
            return documento_electronico;
        }

        public void Calcular()
        {
            try
            {
                if (flujo.Count > 0)
                    total = flujo.Where(x => x.tipo_metodo_pago_id != 0 && x.codigo == "INVEN").Sum(x => x.tipo_cambio * x.importe) - flujo.Where(x => x.tipo_metodo_pago_id != 0 && x.codigo == "RECAM").Sum(x => x.tipo_cambio * x.importe);
                else
                    total = 0;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
            }
        }

        [Table(Name = "partidas_pagos")]
        public class Partida
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int pago_id { get; set; }
            [Column]
            public int documento_id { get; set; }
            [Column]
            public decimal saldo { get; set; }
            [Column]
            public decimal intereses { get; set; }
            [Column]
            public decimal tipo_cambio { get; set; }
            [Column]
            public decimal importe { get; set; }
            [Column]
            public DateTime fecha_vencimiento { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }
            //Internal
            public bool selecionado { get; set; }
            public int numero_documento { get; internal set; }

            public static Table<Partida> Partidas()
            {
                return Nori.CrearContexto().GetTable<Partida>();
            }

            public Partida()
            {
                tipo_cambio = 1;
                fecha_vencimiento = DateTime.Now;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Partida Obtener(int id)
            {
                try
                {
                    return Partidas().Where(x => x.id == id).First();
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
                    if (saldo == 0)
                        saldo = importe;

                    var Tabla = Partidas();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();

                    try
                    {
                        if (importe > 0)
                        {
                            var TablaDocumentos = Documento.Documentos();
                            Documento documento = TablaDocumentos.Where(x => x.id == documento_id).First();

                            decimal importe_aplicado = (documento.clase.Equals("NC")) ? importe * -1 : importe;
                            documento.importe_aplicado += importe_aplicado;

                            if (documento.importe_aplicado > documento.total)
                                documento.importe_aplicado = documento.total;

                            if (documento.importe_aplicado >= documento.total && (documento.clase.Equals("FA") || documento.clase.Equals("NC") || documento.clase.Equals("ND")))
                                if (!documento.reserva)
                                    documento.estado = 'C';

                            TablaDocumentos.Context.SubmitChanges();
                        }
                    }
                    catch { }

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
                    partida.importe += importe;
                    partida.intereses = intereses;
                    //partida.saldo += intereses;

                    Tabla.Context.SubmitChanges();

                    try
                    {
                        if (importe > 0)
                        {
                            var TablaDocumentos = Documento.Documentos();
                            Documento documento = TablaDocumentos.Where(x => x.id == documento_id).First();

                            decimal importe_aplicado = (documento.clase.Equals("NC")) ? importe * -1 : importe;
                            documento.importe_aplicado += importe_aplicado;

                            if (documento.importe_aplicado > documento.total)
                                documento.importe_aplicado = documento.total;

                            if (documento.importe_aplicado >= documento.total && (documento.clase.Equals("FA") || documento.clase.Equals("NC") || documento.clase.Equals("ND")))
                            {
                                if (!documento.reserva)
                                {
                                    documento.estado = 'C';
                                }
                                else
                                {
                                    bool financiado = Pagos().Any(x => x.financiado == true && x.id == pago_id);
                                    if (financiado)
                                    {
                                        Documento entrega = new Documento();
                                        documento.CopiarDe(Documento.Obtener(documento_id), "EN", true);

                                        if (documento.Agregar())
                                            documento.estado = 'C';
                                        else
                                            Console.WriteLine(Global.Error.Message);
                                    }
                                }
                            }


                            TablaDocumentos.Context.SubmitChanges();
                        }
                    }
                    catch { }

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
}
