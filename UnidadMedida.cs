using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "unidades_medida")]
    public class UnidadMedida
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string codigo { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string descripcion { get; set; }
        [Column]
        public string clave_unidad { get; set; }
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

        public static Table<UnidadMedida> UnidadesMedida()
        {
            return Nori.CrearContexto().GetTable<UnidadMedida>();
        }

        public UnidadMedida()
        {
            codigo = "-1";
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static UnidadMedida Obtener(int id)
        {
            try
            {
                return UnidadesMedida().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new UnidadMedida();
            }
        }

        public static UnidadMedida Obtener(string codigo)
        {
            try
            {
                return UnidadesMedida().Where(x => x.codigo == codigo).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new UnidadMedida();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = UnidadesMedida();
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
                var Tabla = UnidadesMedida();
                UnidadMedida unidad_medida = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(unidad_medida);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        #region Grupos
        [Table(Name = "grupos_unidades_medida")]
        public class Grupo
        {
            [Column(IsDbGenerated = true, IsPrimaryKey = true)]
            public int id { get; set; }
            [Column]
            public int unidad_medida_base_id { get; set; }
            [Column]
            public string codigo { get; set; }
            [Column]
            public string nombre { get; set; }
            [Column]
            public string descripcion { get; set; }
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

            public static Table<Grupo> Grupos()
            {
                return Nori.CrearContexto().GetTable<Grupo>();
            }

            public Grupo()
            {
                activo = true;
                usuario_creacion_id = Global.Usuario.id;
                fecha_creacion = DateTime.Now;
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
            }

            public static Grupo Obtener(int id)
            {
                try
                {
                    return Grupos().Where(x => x.id == id).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Grupo();
                }
            }

            public static Grupo Obtener(string codigo)
            {
                try
                {
                    return Grupos().Where(x => x.codigo == codigo).First();
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return new Grupo();
                }
            }

            public bool Agregar()
            {
                try
                {
                    var Tabla = Grupos();
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
                    var Tabla = Grupos();
                    Grupo grupo = Tabla.Where(x => x.id == id).First();
                    this.CopyProperties(grupo);
                    Tabla.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.Error = new Nori.Error(ex.Message);
                    return false;
                }
            }

            [Table(Name = "lineas_grupos_unidades_medida")]
            public class Linea
            {
                [Column(IsDbGenerated = true, IsPrimaryKey = true)]
                public int id { get; set; }
                [Column]
                public int grupo_unidad_medida_id { get; set; }
                [Column]
                public int unidad_medida_id { get; set; }
                [Column]
                public decimal cantidad { get; set; }
                [Column]
                public decimal cantidad_base { get; set; }
                [Column]
                public int usuario_creacion_id { get; set; }
                [Column]
                public DateTime fecha_creacion { get; set; }
                [Column]
                public int usuario_actualizacion_id { get; set; }
                [Column]
                public DateTime fecha_actualizacion { get; set; }

                public static Table<Linea> Lineas()
                {
                    return Nori.CrearContexto().GetTable<Linea>();
                }

                public Linea()
                {
                    usuario_creacion_id = Global.Usuario.id;
                    fecha_creacion = DateTime.Now;
                    usuario_actualizacion_id = Global.Usuario.id;
                    fecha_actualizacion = DateTime.Now;
                }

                public static Linea Obtener(int id)
                {
                    try
                    {
                        return Lineas().Where(x => x.id == id).First();
                    }
                    catch (Exception ex)
                    {
                        Global.Error = new Nori.Error(ex.Message);
                        return new Linea();
                    }
                }

                public static Linea Obtener(int grupo_unidad_medida_id, int unidad_medida_id)
                {
                    try
                    {
                        return Lineas().Where(x => x.grupo_unidad_medida_id == grupo_unidad_medida_id && x.unidad_medida_id == unidad_medida_id).First();
                    }
                    catch (Exception ex)
                    {
                        Global.Error = new Nori.Error(ex.Message);
                        return new Linea();
                    }
                }

                public bool Agregar()
                {
                    try
                    {
                        var Tabla = Lineas();
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
                        var Tabla = Lineas();
                        Linea linea = Tabla.Where(x => x.id == id).First();
                        this.CopyProperties(linea);
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
        #endregion
    }
}