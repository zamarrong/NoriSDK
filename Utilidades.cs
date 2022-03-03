using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace NoriSDK
{
    public class Utilidades
    {
        public static Dictionary<String, Object> ConvertirModeloDiccionario(Object modelo)
        {
            return modelo.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.GetSetMethod(true).IsPublic).ToDictionary(prop => prop.Name, prop => prop.GetValue(modelo, null));
        }

        public static DataTable EjecutarQuery(string query)
        {
            try
            {
                DB db = new DB();
                return db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }

        public static int EjecutarEscalar(string query)
        {
            try
            {
                DB db = new DB();
                return db.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return 0;
            }
        }

        public static decimal EjecutarDecimal(string query)
        {
            try
            {
                DB db = new DB();
                return db.ExecuteDecimal(query);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return 0;
            }
        }

        public static int EjecutarProcedimiento(string nombre, Dictionary<string, object> parametros)
        {
            try
            {
                DB db = new DB();
                return db.ExecuteStoredProcedure(nombre, parametros);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return 0;
            }
        }

        public static string EjecutarEscalarString(string query)
        {
            try
            {
                DB db = new DB();
                return db.ExecuteScalarString(query);
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return string.Empty;
            }
        }

        public static DataTable Busqueda(string tabla, object objeto = null, object parametros = null, string rawQuery = null)
        {
            try
            {
                DB db = new DB();
                string cmd = (rawQuery == null) ? Utilidades.GeneraQuerys(0, tabla, objeto, parametros) : rawQuery;
                DataTable objs = db.ExecuteQuery(cmd);
                return objs;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Esta función genera un query
        /// </summary>
        /// <param name="tipo">0 = SELCECT, 1 = INSERT, 2 = UPDATE, 3 = DELETE</param>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="objeto"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static string GeneraQuerys(int tipo, String tabla, Object objeto, Object parametros = null)
        {
            //0 = SELECT
            //1 = INSERT
            //2 = UPDATE
            //3 = DELETE
            String query = string.Empty;
            String p = string.Empty;
            String v = string.Empty;
            bool usar_like = false;
            string separador = "AND";
            string operador = "=";

            try {
                switch (tipo)
                {
                    case 0:
                        int i = 0;
                        string gq = string.Empty;
                        string oq = string.Empty;
                        query += "SELECT ";

                        if (parametros != null)
                        {
                            if (parametros.GetType().GetProperty("top") != null)
                                query += " TOP " + parametros.GetType().GetProperty("top").GetValue(parametros) + " ";

                            if (parametros.GetType().GetProperty("fields") != null)
                                query += parametros.GetType().GetProperty("fields").GetValue(parametros);
                            else
                                query += "*";

                            if (parametros.GetType().GetProperty("separator") != null)
                                separador = (string)parametros.GetType().GetProperty("separator").GetValue(parametros);
                            else
                                separador = "AND";

                            if (parametros.GetType().GetProperty("separador") != null)
                                separador = (string)parametros.GetType().GetProperty("separador").GetValue(parametros);
                            else
                                separador = "AND";

                            if (parametros.GetType().GetProperty("operador") != null)
                                operador = (string)parametros.GetType().GetProperty("operador").GetValue(parametros);
                            else
                                operador = "=";

                            if (parametros.GetType().GetProperty("operator") != null)
                                operador = (string)parametros.GetType().GetProperty("operator").GetValue(parametros);
                            else
                                operador = "=";

                            if (parametros.GetType().GetProperty("like") != null)
                                usar_like = (bool)parametros.GetType().GetProperty("like").GetValue(parametros);

                            if (parametros.GetType().GetProperty("group_by") != null)
                                gq += " GROUP BY " + parametros.GetType().GetProperty("group_by").GetValue(parametros);

                            if (parametros.GetType().GetProperty("order_by") != null)
                                oq += " ORDER BY " + parametros.GetType().GetProperty("order_by").GetValue(parametros);
                                if (parametros.GetType().GetProperty("order") != null)
                                    oq += " " + parametros.GetType().GetProperty("order").GetValue(parametros);
                        }
                        else
                        {
                            query += "*";
                        }

                        query += " FROM " + tabla;

                        if (objeto != null)
                        {
                            query += " WHERE ";
                            foreach (var prop in objeto.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                            {
                                if (prop.GetValue(objeto, null) != null)
                                {
                                    if (!prop.GetValue(objeto).IsNullOrEmpty())
                                    {
                                        if (i > 0)
                                        {
                                            query += " " + separador + " ";
                                        }
                                        query += prop.Name + ((prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime)) ? Concatenar(usar_like, operador, (string)prop.GetValue(objeto)) : " " + operador + " " + prop.GetValue(objeto));
                                        i++;
                                    }
                                }
                            }
                        }
                        query += gq + oq;
                        break;
                    case 1:
                        foreach (var prop in objeto.GetType().GetProperties())
                        {
                            if (prop.GetValue(objeto, null) != null && prop.Name != "id" && prop.GetSetMethod(true).IsPublic)
                            {
                                p += prop.Name + ",";
                                v += "@" + prop.Name + ",";
                            }
                        }
                        query += "INSERT INTO " + tabla + " (" + p.TrimEnd(',') + ") VALUES(" + v.TrimEnd(',') + ")";
                        break;
                    case 2:
                        foreach (var prop in objeto.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            if (prop.GetValue(objeto, null) != null && prop.Name != "id" && prop.GetSetMethod(true).IsPublic)
                            {
                                p += prop.Name + " = @" + prop.Name + ",";
                            }
                        }

                        query += "UPDATE " + tabla + " SET " + p.TrimEnd(',') + " WHERE ";

                        i = 0;
                        foreach (var prop in parametros.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            if (prop.GetValue(parametros, null) != null)
                            {
                                if (i > 0)
                                {
                                    query += " " + separador + " ";
                                }
                                query += prop.Name + ((prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime)) ? Concatenar(usar_like, operador, (string)prop.GetValue(objeto)) : " " + operador + " " + prop.GetValue(parametros));
                            }
                            i++;
                        }
                        break;
                    case 3:
                        query += "DELETE FROM " + tabla + " WHERE ";

                        i = 0;
                        foreach (var prop in objeto.GetType().GetProperties())
                        {
                            if (prop.GetValue(objeto, null) != null)
                            {
                                if (i > 0)
                                {
                                    query += " " + separador + " ";
                                }
                                query += prop.Name + ((prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime)) ? Concatenar(usar_like, operador, (string)prop.GetValue(objeto)) : " " + operador + " " + prop.GetValue(objeto));
                            }
                            i++;
                        }
                        break;
                }
                return query;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return null;
            }
        }

        public static string GeneraQueryUnion(string q1, string q2)
        {
            return "SELECT * FROM(" + q1 + ") T0  UNION ALL SELECT * FROM (" + q2 + ") T1";
        }

        private static string Concatenar(bool like, string op, string str)
        {
            return (like) ? " LIKE '%" + str + "%'" : " " + op + " '" + str + "'";
        }

        public static string CadenaAleatoria(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
    }
}
