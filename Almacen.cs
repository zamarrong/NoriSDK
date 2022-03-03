using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "almacenes")]
    public class Almacen
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string impresora { get; set; }
        [Column]
        public string numero_cuenta_ajuste_inventario { get; set; }
        [Column]
        public bool ubicaciones { get; set; }
        [Column]
        public int ubicacion_id { get; set; }
        [Column]
        public bool transito { get; set; }
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

        public static Table<Almacen> Almacenes()
        {
            return Nori.CrearContexto().GetTable<Almacen>();
        }

        public Almacen()
        {
            numero_cuenta_ajuste_inventario = string.Empty;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Almacen Obtener(int id)
        {
            try
            {
                return Almacenes().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Almacen();
            }
        }

        public static Almacen Obtener(string codigo)
        {
            try
            {
                return Almacenes().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Almacen();
            }
        }

        public List<Ubicacion> Ubicaciones()
        {
            try
            {
                return Ubicacion.Ubicaciones().Where(x => x.almacen_id == id && x.activo == true).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Ubicacion>();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Almacenes();
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
                var Tabla = Almacenes();
                Almacen almacen = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(almacen);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Ubicacion

        [Table(Name = "ubicaciones")]
        public class Ubicacion
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int codigo { get; set; }
            [Column]
            public int almacen_id { get; set; }
            [Column]
            public string ubicacion { get; set; }
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

            public static Table<Ubicacion> Ubicaciones()
            {
                return Nori.CrearContexto().GetTable<Ubicacion>();
            }

            public Ubicacion()
            {
                almacen_id = Global.Usuario.almacen_id;
                activo = true;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Ubicacion Obtener(int id)
            {
                try
                {
                    return Ubicaciones().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Ubicacion();
                }
            }

            public static Ubicacion ObtenerPorCodigo(int codigo)
            {
                try
                {
                    return Ubicaciones().Where(x => x.codigo == codigo).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Ubicacion();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Ubicaciones();
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
                    var Tabla = Ubicaciones();
                    Ubicacion ubicacion = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(ubicacion);
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
                    var Tabla = Ubicaciones();
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
}