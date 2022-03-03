using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "empresa")]
    public class Empresa
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string nombre_fiscal { get; set; }
        [Column]
        public string nombre_comercial { get; set; }
        [Column]
        public string eslogan { get; set; }
        [Column]
        public string logotipo { get; set; }
        [Column]
        public string rfc { get; set; }
        [Column]
        public string regimen_fiscal { get; set; }
        [Column]
        public char tipo_persona { get; set; }
        [Column]
        public string telefono { get; set; }
        [Column]
        public string telefono2 { get; set; }
        [Column]
        public string correo { get; set; }
        [Column]
        public string sitio_web { get; set; }
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
        public string estado { get; set; }
        [Column]
        public string pais { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }

        public static Table<Empresa> Empresas()
        {
            return Nori.CrearContexto().GetTable<Empresa>();
        }

        public Empresa()
        {
            nombre_fiscal = "Nombre fiscal";
            nombre_comercial = "Nombre comercial";
            eslogan = string.Empty;
            logotipo = string.Empty;
            rfc = "XAXX010101000";
            regimen_fiscal = string.Empty;
            tipo_persona = TipoPersona.ObtenerPredeterminado();
            telefono = string.Empty;
            telefono2 = string.Empty;
            correo = "ejemplo@nori.com";
            sitio_web = string.Empty;
            calle = string.Empty;
            colonia = string.Empty;
            numero_exterior = "S/N";
            numero_interior = string.Empty;
            cp = "00000";
            ciudad = string.Empty;
            municipio = string.Empty;
            estado = string.Empty;
            pais = "México";
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static Empresa Obtener()
        { 
            try
            {
                return Empresas().First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Empresa();
            }
        }

        internal bool Agregar()
        {
            try
            {
                var Tabla = Empresas();
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
                var Tabla = Empresas();
                Empresa empresa = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(empresa);
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
                return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", calle, numero_exterior, numero_interior, colonia, ciudad, cp, municipio, estado, pais);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return string.Empty;
            }
        }

        #region TipoPersona
        public class TipoPersona
        {
            public char tipo { get; set; }
            public string nombre { get; set; }
            public static List<TipoPersona> Tipos()
            {
                List<TipoPersona> tipos = new List<TipoPersona>();

                TipoPersona tipo = new TipoPersona();

                tipo.tipo = 'F';
                tipo.nombre = "Persona física";
                tipos.Add(tipo);

                tipo = new TipoPersona();

                tipo.tipo = 'M';
                tipo.nombre = "Persona moral";
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
}
