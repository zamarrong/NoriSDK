using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "sincronizacion")]
    public class Sincronizacion
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string tabla { get; set; }
        [Column]
        public int registro { get; set; }
        [Column]
        public string error { get; set; }
        [Column]
        public DateTime fecha { get; set; }

        public static Table<Sincronizacion> Sincronizaciones()
        {
            return Nori.CrearContexto().GetTable<Sincronizacion>();
        }

        public Sincronizacion()
        {
            fecha = DateTime.Now;
        }

        public Sincronizacion(string tabla)
        {
            this.tabla = tabla;
            fecha = DateTime.Now;
        }

        public static Sincronizacion Obtener(int id)
        {
            Sincronizacion sincronizacion = new Sincronizacion();
            try
            {
                sincronizacion = Sincronizaciones().Where(x => x.id == id).First();
                return sincronizacion;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return sincronizacion;
            }
        }

        public static Sincronizacion Obtener(string tabla, int registro)
        {
            Sincronizacion sincronizacion = new Sincronizacion();
            try
            {
                sincronizacion = Sincronizaciones().Where(x => x.tabla == tabla && x.registro == registro).First();
                return sincronizacion;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);

                sincronizacion.tabla = tabla;
                sincronizacion.registro = registro;

                return sincronizacion;
            }
        }

        public bool Agregar()
        {
            try
            {
                if (registro == 0)
                    return false;

                if (Sincronizaciones().Any(x => x.tabla == tabla && x.registro == registro))
                    return false;

                var Tabla = Sincronizaciones();
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
                var Tabla = Sincronizaciones();
                Sincronizacion sincronizacion = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(sincronizacion);
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
                var Tabla = Sincronizaciones();
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

        [Table(Name = "sincronizacion_manual")]
        public class Manual
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public string tabla { get; set; }
            [Column]
            public string registro { get; set; }
            [Column]
            public string error { get; set; }
            [Column]
            public DateTime fecha { get; set; }

            public static Table<Manual> Manuales()
            {
                return Nori.CrearContexto().GetTable<Manual>();
            }

            public Manual()
            {
                error = string.Empty;
                fecha = DateTime.Now;
            }

            public Manual(string tabla)
            {
                this.tabla = tabla;
                error = string.Empty;
                fecha = DateTime.Now;
            }

            public static Manual Obtener(int id)
            {
                Manual manual = new Manual();
                try
                {
                    manual = Manuales().Where(x => x.id == id).First();
                    return manual;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return manual;
                }
            }
            public static List<Manual> Obtener(string tabla)
            {
                try
                {
                    return Manuales().Where(x => x.tabla == tabla).ToList();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new List<Manual>();
                }
            }

            public bool Agregar()
            {
                try
                {
                    if (registro.Length == 0 || tabla.Length == 0)
                        return false;

                    if (Manuales().Any(x => x.tabla == tabla && x.registro == registro))
                        return false;

                    var Tabla = Manuales();
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
                    var Tabla = Manuales();
                    Manual manual = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(manual);
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
                    var Tabla = Manuales();
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
    }
}