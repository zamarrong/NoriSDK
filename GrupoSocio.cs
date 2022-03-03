using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "grupos_socios")]
    public class GrupoSocio
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public char tipo { get; set; }
        [Column]
        public short codigo { get; set; }
        [Column]
        public string nombre { get; set; }
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

        public static Table<GrupoSocio> GruposSocios()
        {
            return Nori.CrearContexto().GetTable<GrupoSocio>();
        }

        public GrupoSocio()
        {
            tipo = 'C';
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static GrupoSocio Obtener(int id)
        {
            try
            {
                return GruposSocios().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new GrupoSocio();
            }
        }

        public static GrupoSocio Obtener(short codigo)
        {
            try
            {
                return GruposSocios().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new GrupoSocio();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = GruposSocios();
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
                var Tabla = GruposSocios();
                GrupoSocio grupo_socio = GruposSocios().Where(x => x.id == id).First();
                this.CopyProperties(grupo_socio);
                GruposSocios().Context.SubmitChanges();
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

                return tipos;
            }
        }
        #endregion
    }
}
