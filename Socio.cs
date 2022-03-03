using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "socios")]
    public class Socio
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int lista_precio_id { get; set; }
        [Column]
        public int moneda_id { get; set; }
        [Column]
        public int condicion_pago_id { get; set; }
        [Column]
        public int grupo_socio_id { get; set; }
        [Column]
        public int metodo_pago_id { get; set; }
        [Column]
        public int vendedor_id { get; set; }
        [Column]
        public int propietario_id { get; set; }
        [Column]
        public int persona_contacto_id { get; set; }
        [Column]
        public int direccion_facturacion_id { get; set; }
        [Column]
        public int direccion_envio_id { get; set; }
        [Column]
        public decimal latitud { get; set; }
        [Column]
        public decimal longitud { get; set; }
        [Column]
        public char frecuencia { get; set; }
        [Column]
        public int ruta_id { get; set; }
        [Column]
        public int orden_ruta { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public char tipo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string nombre_comercial { get; set; }
        [Column]
        public string rfc { get; set; }
        [Column]
        public string curp { get; set; }
        [Column]
        public string telefono { get; set; }
        [Column]
        public string telefono2 { get; set; }
        [Column]
        public string celular { get; set; }
        [Column]
        public string correo { get; set; }
        [Column]
        public string sitio_web { get; set; }
        [Column]
        public string imagen { get; set; }
        [Column]
        public decimal balance { get; set; }
        [Column]
        public decimal porcentaje_interes_retraso { get; set; }
        [Column]
        public decimal porcentaje_descuento { get; set; }
        [Column]
        public decimal limite_credito { get; set; }
        [Column]
        public string cuenta { get; set; }
        [Column]
        public string cuenta_pago { get; set; }
        [Column]
        public bool orden_compra { get; set; }
        [Column]
        public decimal multiplicador_puntos { get; set; }
        [Column]
        public string uso_principal { get; set; }
        [Column]
        public bool eventual { get; set; }
        [Column]
        public int socio_eventual_id { get; set; }
        [Column]
        public int monedero_id { get; set; }
        [Column]
        public bool api { get; set; }
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
        [Column]
        public bool propiedad_1 { get; set; }
        [Column]
        public bool propiedad_2 { get; set; }
        [Column]
        public bool propiedad_3 { get; set; }
        [Column]
        public bool propiedad_4 { get; set; }
        [Column]
        public bool propiedad_5 { get; set; }
        [Column]
        public bool propiedad_6 { get; set; }
        [Column]
        public bool propiedad_7 { get; set; }
        [Column]
        public bool propiedad_8 { get; set; }
        [Column]
        public bool propiedad_9 { get; set; }
        [Column]
        public bool propiedad_10 { get; set; }
        [Column]
        public bool propiedad_11 { get; set; }
        [Column]
        public bool propiedad_12 { get; set; }
        [Column]
        public bool propiedad_13 { get; set; }
        [Column]
        public bool propiedad_14 { get; set; }
        [Column]
        public bool propiedad_15 { get; set; }
        [Column]
        public bool propiedad_16 { get; set; }
        [Column]
        public bool propiedad_17 { get; set; }
        [Column]
        public bool propiedad_18 { get; set; }
        [Column]
        public bool propiedad_19 { get; set; }
        [Column]
        public bool propiedad_20 { get; set; }
        [Column]
        public bool propiedad_21 { get; set; }
        [Column]
        public bool propiedad_22 { get; set; }
        [Column]
        public bool propiedad_23 { get; set; }
        [Column]
        public bool propiedad_24 { get; set; }
        [Column]
        public bool propiedad_25 { get; set; }
        [Column]
        public bool propiedad_26 { get; set; }
        [Column]
        public bool propiedad_27 { get; set; }
        [Column]
        public bool propiedad_28 { get; set; }
        [Column]
        public bool propiedad_29 { get; set; }
        [Column]
        public bool propiedad_30 { get; set; }
        [Column]
        public bool propiedad_31 { get; set; }
        [Column]
        public bool propiedad_32 { get; set; }

        public static Table<Socio> Socios()
        {
            return Nori.CrearContexto().GetTable<Socio>();
        }

        public Socio()
        {
            lista_precio_id = Global.Configuracion.lista_precio_id;
            moneda_id = Global.Configuracion.moneda_id;
            condicion_pago_id = Global.Configuracion.condicion_pago_id;
            metodo_pago_id = Global.Configuracion.metodo_pago_id;
            vendedor_id = Global.Usuario.vendedor_id;
            tipo = Tipo.ObtenerPredeterminado();
            rfc = "XAXX010101000";
            curp = string.Empty;
            telefono = string.Empty;
            telefono2 = string.Empty;
            celular = string.Empty;
            correo = string.Empty;
            sitio_web = string.Empty;
            imagen = string.Empty;
            cuenta = string.Empty;
            cuenta_pago = string.Empty;
            orden_compra = false;
            uso_principal = Documento.UsoCFDI.ObtenerPredeterminado();
            eventual = false;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Socio Obtener(int id)
        {
            try
            {
                return Socios().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Socio();
            }
        }

        public static Socio Obtener(string codigo)
        {
            try
            {
                return Socios().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Socio();
            }
        }

        public List<Direccion> Direcciones()
        {
            try
            {
                return Direccion.Direcciones().Where(x => x.socio_id == id).ToList();
            }
            catch
            {
                return new List<Direccion>();
            }
        }

        public List<PersonaContacto> PersonasContacto()
        {
            try
            {
                return PersonaContacto.PersonasContacto().Where(x => x.socio_id == id).ToList();
            }
            catch
            {
                return new List<PersonaContacto>();
            }
        }

        public Monedero Monedero()
        {
            try
            {
                return NoriSDK.Monedero.Monederos().Where(x => x.socio_id == id && x.predeterminado == true).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Monedero();
            }
        }
        public bool Agregar(bool agregar_sincronizacion = true)
        {
            try
            {
                Permiso permiso = Permiso.Obtener(Global.Usuario.id, "SN");

                if (permiso.id != 0)
                {
                    if (!permiso.agregar)
                    {
                        Global.Error = new Nori.Error("No cuentas con el permiso suficiente para realizar esta acción.");
                        return false;
                    }
                }

                if (eventual && socio_eventual_id == 0)
                    socio_eventual_id = Global.Usuario.socio_id;

                var Tabla = Socios();
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();

                if (Global.Configuracion.sap && agregar_sincronizacion && !eventual)
                {
                    Sincronizacion sincronizacion = new Sincronizacion();
                    sincronizacion.tabla = "socios";
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
                if (agregar_sincronizacion)
                {
                    Permiso permiso = Permiso.Obtener(Global.Usuario.id, "SN");

                    if (permiso.id != 0)
                    {
                        if (!permiso.actualizar)
                        {
                            Global.Error = new Nori.Error("No cuentas con el permiso suficiente para realizar esta acción.");
                            return false;
                        }
                    }
                }

                if (eventual && socio_eventual_id == 0)
                    socio_eventual_id = Global.Usuario.socio_id;

                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;

                if (Global.Usuario.VendedorForaneo() && agregar_sincronizacion)
                {
                    Global.Error = new Nori.Error("No es posible actualizar este objeto de forma foránea.");
                    return false;
                }

                var Tabla = Socios();
                Socio socio = Tabla.Where(x => x.id == id).First();

                if (Global.Configuracion.sap && agregar_sincronizacion && !eventual)
                {
                    Sincronizacion sincronizacion = new Sincronizacion();
                    sincronizacion.tabla = "socios";
                    sincronizacion.registro = id;
                    sincronizacion.Agregar();
                }

                this.CopyProperties(socio);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool ActualizarBalance(decimal balance)
        {
            try
            {
                var Tabla = Socios();
                Socio socio = Tabla.Where(x => x.id == id).First();
                socio.balance = balance;
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }
        public static bool ActualizarBalance(int socio_id)
        {
            try
            {
                var Tabla = Socios();
                Socio socio = Tabla.Where(x => x.id == socio_id).First();
                socio.balance = (socio.tipo.Equals('C')) ? Utilidades.EjecutarDecimal(string.Format("select isnull(sum((case clase when ('NC') then (total - importe_aplicado) * -1 else (total - importe_aplicado) end) * tipo_cambio), 0) from documentos where estado = 'A' and clase in ('NC', 'FA') and socio_id = {0}", socio.id)) : Utilidades.EjecutarDecimal(string.Format("select isnull(sum((case clase when ('DM') then (total - importe_aplicado) * -1 else (total - importe_aplicado) end) * tipo_cambio), 0) from documentos where estado = 'A' and clase in ('DM', 'EM') and socio_id = {0}", socio.id));
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool DocumentosVencidos()
        {
            try
            {
                return Documento.Documentos().Any(x => x.socio_id == id && x.clase == "FA" && x.fecha_vencimiento < DateTime.Today && x.importe_aplicado != x.total && x.estado == 'A');
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return true;
            }
        }

        #region Tipo
        public class Tipo
        {
            public char tipo { get; set; }
            public string nombre { get; set; }
            public static List<Tipo> Tipos()
            {
                List<Tipo> tipos = new List<Tipo>();

                Tipo tipo = new Tipo();

                tipo.tipo = 'C';
                tipo.nombre = "Cliente";
                tipos.Add(tipo);

                tipo = new Tipo();

                tipo.tipo = 'P';
                tipo.nombre = "Proveedor";
                tipos.Add(tipo);

                tipo = new Tipo();
                tipo.tipo = 'L';
                tipo.nombre = "Lead";
                tipos.Add(tipo);

                return tipos;
            }

            public static char ObtenerPredeterminado()
            {
                return 'C';
            }
        }
        #endregion

        #region Frecuencia
        public class Frecuencia
        {
            public char frecuencia { get; set; }
            public string nombre { get; set; }
            public static List<Frecuencia> Frequencias()
            {
                List<Frecuencia> frecuencias = new List<Frecuencia>();

                Frecuencia frecuencia = new Frecuencia();

                frecuencia.frecuencia = 'D';
                frecuencia.nombre = "Diaria";
                frecuencias.Add(frecuencia);

                frecuencia = new Frecuencia();
            
                frecuencia.frecuencia = 'S';
                frecuencia.nombre = "Semanal";
                frecuencias.Add(frecuencia);

                frecuencia = new Frecuencia();
                frecuencia.frecuencia = 'Q';
                frecuencia.nombre = "Quincenal";
                frecuencias.Add(frecuencia);

                frecuencia = new Frecuencia();
                frecuencia.frecuencia = 'M';
                frecuencia.nombre = "Mensual";
                frecuencias.Add(frecuencia);

                return frecuencias;
            }

            public static char ObtenerPredeterminado()
            {
                return 'C';
            }
        }
        #endregion

        #region Direccion
        [Table(Name = "direcciones")]
        public class Direccion
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int socio_id { get; set; }
            [Column]
            public int impuesto_id { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]
            public char tipo { get; set; }
            [Column]
            public string calle { get; set; }
            [Column]
            public string numero_exterior { get; set; }
            [Column]
            public string numero_interior { get; set; }
            [Column]
            public string cp { get; set; }
            [Column]
            public string colonia { get; set; }
            [Column]
            public string ciudad { get; set; }
            [Column]
            public string municipio { get; set; }
            [Column]
            public string entre_calles { get; set; }
            [Column]
            public string referencias { get; set; }
            [Column]
            public int estado_id { get; set; }
            [Column]
            public int pais_id { get; set; }
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

            public static Table<Direccion> Direcciones()
            {
                return Nori.CrearContexto().GetTable<Direccion>();
            }

            public Direccion()
            {
                impuesto_id = Global.Configuracion.impuesto_id;
                tipo = Tipo.ObtenerPredeterminado();
                calle = string.Empty;
                colonia = string.Empty;
                numero_exterior = "S/N";
                numero_interior = string.Empty;
                cp = "00000";
                ciudad = string.Empty;
                municipio = string.Empty;
                estado_id = Global.Usuario.estado_id;
                pais_id = (estado_id != 0) ? PaisEstado() : 0;
                activo = true;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Direccion Obtener(int id)
            {
                try
                {
                    return Direcciones().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Direccion();
                }
            }

            public static Direccion Obtener(int socio_id, string nombre)
            {
                try
                {
                    return Direcciones().Where(x => x.socio_id == socio_id && x.nombre == nombre).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Direccion();
                }
            }

            public bool Agregar(bool predeterminado = false)
            {
                try
                {
                    if (nombre.IsNullOrEmpty())
                    {
                        Global.Error = new Nori.Error("Aún no se ha especificado un nombre (alias) a la dirección.");
                        return false;
                    }

                    var Tabla = Direcciones();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();

                    if (predeterminado)
                    {
                        Socio socio = Socio.Obtener(socio_id);
                        if (socio.direccion_facturacion_id == 0 && tipo.Equals('F'))
                            socio.direccion_facturacion_id = id;
                        else if (socio.direccion_envio_id == 0 && tipo.Equals('E'))
                            socio.direccion_envio_id = id;

                        socio.Actualizar(false);
                    }

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
                    var Tabla = Direcciones();
                    Direccion direccion = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(direccion);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }
            
            public string Bloque()
            {
                try
                {
                    return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", calle, numero_exterior, numero_interior, entre_calles, referencias, colonia, ciudad, cp, municipio, Estado(), Pais());
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return string.Empty;
                }
            }

            public string Estado()
            {
                try
                {
                    return NoriSDK.Estado.Estados().Where(x => x.id == estado_id).Select(x => new { x.nombre }).First().nombre;
                }
                catch
                {
                    return string.Empty;
                }
            }

            public string CodigoEstado()
            {
                try
                {
                    return NoriSDK.Estado.Estados().Where(x => x.id == estado_id).Select(x => new { x.codigo }).First().codigo;
                }
                catch
                {
                    return string.Empty;
                }
            }

            public int PaisEstado()
            {
                try
                {
                    return NoriSDK.Estado.Estados().Where(x => x.id == estado_id).Select(x =>  x.pais_id).First();
                }
                catch
                {
                    return 0;
                }
            }

            public string Pais()
            {
                try
                {
                    return NoriSDK.Pais.Paises().Where(x => x.id == pais_id).Select(x => new { x.nombre }).First().nombre;
                }
                catch
                {
                    return string.Empty;
                }
            }

            #region Tipo
            public class Tipo
            {
                public char tipo { get; set; }
                public string nombre { get; set; }
                public static List<Tipo> Tipos()
                {
                    List<Tipo> tipos = new List<Tipo>();

                    Tipo tipo = new Tipo();

                    tipo.tipo = 'F';
                    tipo.nombre = "Facturación";
                    tipos.Add(tipo);

                    tipo = new Tipo();

                    tipo.tipo = 'E';
                    tipo.nombre = "Envío";
                    tipos.Add(tipo);

                    return tipos;
                }

                public static char ObtenerPredeterminado()
                {
                    return 'F';
                }
            }
            #endregion
        }
        #endregion

        #region PersonaContacto
        [Table(Name = "personas_contacto")]
        public class PersonaContacto
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public short codigo { get; set; }
            [Column]
            public int socio_id { get; set; }
            [Column]
            public char genero { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]
            public string nombre_persona { get; set; }
            [Column]
            public string titulo { get; set; }
            [Column]
            public string posicion { get; set; }
            [Column]
            public string direccion { get; set; }
            [Column]
            public string telefono { get; set; }
            [Column]
            public string celular { get; set; }
            [Column]
            public string correo { get; set; }
            [Column]
            public string observaciones { get; set; }
            [Column]
            public string huella_digital { get; set; }
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

            public static Table<PersonaContacto> PersonasContacto()
            {
               return Nori.CrearContexto().GetTable<PersonaContacto>();
            }

            public PersonaContacto()
            {
                nombre = string.Empty;
                genero = Genero.ObtenerPredeterminado();
                huella_digital = string.Empty;
                activo = true;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static PersonaContacto Obtener(int id)
            {
                try
                {
                    return PersonasContacto().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new PersonaContacto();
                }
            }

            public static PersonaContacto Obtener(int socio_id, short codigo)
            {
                try
                {
                    return PersonasContacto().Where(x => x.socio_id == socio_id && x.codigo == codigo).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new PersonaContacto();
                }
            }

            public static short ObtenerSiguienteCodigo()
            {
                try
                {
                    short codigo = PersonasContacto().OrderByDescending(x => x.id).First().codigo;
                    codigo = (short)(codigo + 1);
                    return codigo;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return 0;
                }
            }

            public bool Agregar(bool predeterminado = false)
            {
                try
                {
                    var Tabla = PersonasContacto();
                    Tabla.InsertOnSubmit(this);
                    Tabla.Context.SubmitChanges();

                    if (predeterminado)
                    {
                        Socio socio = Socio.Obtener(socio_id);
                        if (socio.persona_contacto_id == 0)
                            socio.persona_contacto_id = id;

                        socio.Actualizar(false);
                    }

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
                    var Tabla = PersonasContacto();
                    PersonaContacto persona_contacto = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(persona_contacto);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            #region Genero
            public class Genero
            {
                public char genero { get; set; }
                public string nombre { get; set; }
                public static List<Genero> Generos()
                {
                    List<Genero> generos = new List<Genero>();

                    Genero genero = new Genero();

                    genero.genero = 'M';
                    genero.nombre = "Masculino";
                    generos.Add(genero);

                    genero = new Genero();

                    genero.genero = 'F';
                    genero.nombre = "Femenino";
                    generos.Add(genero);

                    return generos;
                }

                public static char ObtenerPredeterminado()
                {
                    return 'F';
                }
            }
            #endregion
        }
        #endregion
    }
}
