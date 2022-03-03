using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace NoriSDK
{
    public static class Extensiones
    {
        public static bool Operator(this string logic, int x, int y)
        {
            switch (logic)
            {
                case ">": return x > y;
                case ">=": return x >= y;
                case "<": return x < y;
                case "<=": return x <= y;
                case "==": return x == y;
                default: throw new Exception("Lógica invalida");
            }
        }
        public static void ClearChangeSet(this DataContext db)
        {
            // Get the current change set
            ChangeSet pendingChanges = db.GetChangeSet();

            // Iterate through pending inserts and delete
            foreach (object obj in pendingChanges.Inserts)
            {
                var tableToDeleteFrom = db.GetTable(obj.GetType());
                tableToDeleteFrom.DeleteOnSubmit(obj);
            }

            // Iterate through pending deletes and insert
            foreach (object obj in pendingChanges.Deletes)
            {
                var tableToInsertInto = db.GetTable(obj.GetType());
                tableToInsertInto.InsertOnSubmit(obj);
            }

            // Restore all updates with original values
            db.Refresh(RefreshMode.OverwriteCurrentValues, pendingChanges.Updates);
        }
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (DataRow row in table.Rows)
                {
                    T obj = new T();

                    foreach (DataColumn column in row.Table.Columns)
                    {
                        try
                        {
                            var RowValue = row[column.ColumnName];
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(column.ColumnName);
                            if (propertyInfo.PropertyType.IsEnum)
                                propertyInfo.SetValue(obj, Convert.ChangeType(RowValue, typeof(int)), null);
                            else
                                propertyInfo.SetValue(obj, Convert.ChangeType(RowValue, propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            Global.Error = new Nori.Error(ex.Message);
                            continue;
                        }
                    }
                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable ToDataTable<T>(this IList<T> data)
        {

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static void InitProperties(object obj)
        {
            foreach (var prop in obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite))
            {
                var type = prop.PropertyType;
                var constr = type.GetConstructor(Type.EmptyTypes); //find paramless const
                if (type.IsClass && constr != null)
                {
                    var propInst = Activator.CreateInstance(type);
                    prop.SetValue(obj, propInst, null);
                    InitProperties(propInst);
                }
            }
        }
        public static bool IsNullOrEmpty(this object obj)
        {
            return obj == null || String.IsNullOrWhiteSpace(obj.ToString());
        }

        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                try
                {
                    if (!srcProp.CanRead)
                    {
                        continue;
                    }
                    PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                    if (targetProperty == null)
                    {
                        continue;
                    }
                    if (!targetProperty.CanWrite)
                    {
                        continue;
                    }
                    if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                    {
                        continue;
                    }
                    if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                    {
                        continue;
                    }
                    if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                    {
                        continue;
                    }
                    // Passed all tests, lets set the value
                    if (targetProperty.GetValue(destination, null) != srcProp.GetValue(source, null))
                        targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
                }
                catch { continue; }
            }
        }

        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
