using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "mesas")]
    public class Mesa
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int comedor_id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public int ancho { get; set; }
        [Column]
        public int alto { get; set; }
        [Column]
        public int x { get; set; }
        [Column]
        public int y { get; set; }
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

        public static Table<Mesa> Mesas()
        {
            return Nori.CrearContexto().GetTable<Mesa>();
        }

        public Mesa()
        {
            codigo = "0";
            nombre = "Sin asignar";
            ancho = 120;
            alto = 60;
            x = 0;
            y = 0;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Mesa Obtener(int id)
        {
            try
            {
                return Mesas().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Mesa();
            }
        }

        public static Mesa Obtener(string codigo)
        {
            try
            {
                return Mesas().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Mesa();
            }
        }

        public static List<Mesa> ObtenerPorComedor(int id)
        {
            try
            {
                return Mesas().Where(x => x.comedor_id == id).ToList();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new List<Mesa>();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Mesas();
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
                var Tabla = Mesas();
                Mesa mesa = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(mesa);
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
