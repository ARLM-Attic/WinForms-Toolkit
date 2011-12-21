using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace WindowsFormsToolkit.Data
{
    public static class DataTableConvertionExtensionMethods
    {
        /// <summary>
        /// Convert obj to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this T obj) 
            where T:new()
        {
            return AutoBinding<T>.DataTableFromObject(obj);
        }

        /// <summary>
        /// Convert obj to DataRow
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static DataRow ToDataRow<T>(this T obj)
            where T : new()
        {
            return AutoBinding<T>.DataRowFromObject(obj);
        }

        /// <summary>
        /// Convert obj to DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(this T obj)
            where T : new()
        {
            return DataTableConvertionExtensionMethods.ToDataSet<T>(obj, "defaultDataset");
        }

        /// <summary>
        /// Convert obj to DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="dataSetName">Name of DataSet</param>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(this T obj, string dataSetName)
            where T : new()
        {
            DataSet ds = new DataSet(dataSetName);
            ds.Tables.Add(DataTableConvertionExtensionMethods.ToDataTable<T>(obj));
            return ds;
        }

        /// <summary>
        /// Convert DataRow to object of type TObj
        /// </summary>
        /// <typeparam name="TObj">The type of the obj.</typeparam>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static TObj ToObject<TObj>(this DataRow row)
            where TObj : new()
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }
            return AutoBinding<TObj>.ObjectFromDataRow(row);
        }

        /// <summary>
        /// Convert DataTable to List of TObj
        /// </summary>
        /// <typeparam name="TObj">The type of the obj.</typeparam>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static List<TObj> ToList<TObj>(this DataTable table)
            where TObj : new()
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return AutoBinding<TObj>.CollectionFromDataTable(table);
        }

        private static class AutoBinding<TBinding>
            where TBinding : new()
        {

            /// <summary>
            /// Create object from DataRow
            /// </summary>
            /// <param name="row">The row.</param>
            /// <returns></returns>
            public static TBinding ObjectFromDataRow(DataRow row) 
            {
                TBinding obj = new TBinding();

                Type t = typeof(TBinding);
                // parcours la liste des propriétés du type T
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (pi.CanWrite && row[pi.Name] != null && !Convert.IsDBNull(row[pi.Name]))
                    {
                        pi.SetValue(obj, row[pi.Name], null);
                    }
                }

                return obj;
            }

            /// <summary>
            /// Crée une collection d'objet de type T à partir de la DataTable fournit
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="table"></param>
            /// <returns></returns>
            public static List<TBinding> CollectionFromDataTable(DataTable table)
            {
                List<TBinding> col = new List<TBinding>();
                foreach (DataRow row in table.Rows)
                {
                    col.Add(ObjectFromDataRow(row));
                }

                return col;
            }

            /// <summary>
            /// Crée un DataRow par rapport à l'objet obj de type T
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static DataRow DataRowFromObject(TBinding obj)
            {
                return DataTableFromObject(obj).Rows[0];
            }

            /// <summary>
            /// Charge le contenu de l'objet obj dans row
            /// </summary>
            /// <typeparam name="T">Type de obj</typeparam>
            /// <param name="obj">Objet de type T</param>
            /// <param name="row">DataRow à renseigner</param>
            public static void LoadObjectInDataRow(TBinding obj, DataRow row)
            {
                Type t = typeof(TBinding);

                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (row.Table.Columns.Contains(pi.Name))
                    {
                        row[pi.Name] = pi.GetValue(obj, null);
                    }
                }
            }

            /// <summary>
            /// Crée une DataTable de l'objet obj de type T
            /// </summary>
            /// <typeparam name="T">Type de l'objet obj</typeparam>
            /// <param name="obj">Objet de type T</param>
            /// <returns></returns>
            public static DataTable DataTableFromObject(TBinding obj)
            {
                DataTable table = new DataTable();
                Type t = typeof(TBinding);

                table = CreateEmpyDataTableFromType(typeof(TBinding));
                DataRow row = table.NewRow();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (pi.CanWrite)
                    {
                        row[pi.Name] = pi.GetValue(obj, null);
                    }
                }

                table.Rows.Add(row);

                return table;
            }

            /// <summary>
            /// Crée une DataTable vierge à partir du type t
            /// </summary>
            /// <param name="t"></param>
            /// <returns></returns>
            public static DataTable CreateEmpyDataTableFromType(Type t)
            {
                DataTable table = new DataTable();
                table.TableName = t.Name;
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (pi.CanWrite)
                    {
                        table.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                return table;
            }
        }
    }
}
