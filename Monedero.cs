using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "monederos")]
    public class Monedero
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public int socio_id { get; set; }
        [Column]
        public string folio { get; set; }
        public decimal saldo
        {
            get
            {
                try
                {
                    return Partida.Partidas().Where(x => x.monedero_id == id).Sum(x => x.importe);
                }
                catch { return 0; }
            }
        }
        [Column]
        public bool predeterminado { get; set; }
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

        public static Table<Monedero> Monederos()
        {
            return Nori.CrearContexto().GetTable<Monedero>();
        }
        public Monedero()
        {
            folio = string.Empty;
            predeterminado = true;
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Monedero Obtener(int id)
        {
            try
            {
                return Monederos().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Monedero();
            }
        }

        public static Monedero Obtener(string folio)
        {
            try
            {
                return Monederos().Where(x => x.folio == folio).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Monedero();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Monederos();
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
                var Tabla = Monederos();
                Monedero monedero = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(monedero);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        [Table(Name = "partidas_monedero")]
        public class Partida
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int monedero_id { get; set; }
            [Column]
            public int documento_id { get; set; }
            [Column]
            public int partida_id { get; set; }
            [Column]
            public decimal importe { get; set; }
            [Column]
            public int usuario_creacion_id { get; set; }
            [Column]
            public DateTime fecha_creacion { get; set; }
            [Column]
            public int usuario_actualizacion_id { get; set; }
            [Column]
            public DateTime fecha_actualizacion { get; set; }

            public static Table<Partida> Partidas()
            {
                return Nori.CrearContexto().GetTable<Partida>();
            }
            public Partida()
            {
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
                    if (importe == 0)
                    {
                        Global.Error = new Nori.Error("No es posible agregar una partida con importe 0.");
                        return false;
                    }

                    if (Partidas().Any(x => x.documento_id == documento_id && x.partida_id == partida_id))
                    {
                        Global.Error = new Nori.Error("No es posible agregar una partida duplicada.");
                        return false;
                    }

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
        }
    }
}
