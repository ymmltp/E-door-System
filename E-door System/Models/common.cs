using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Linq;

namespace E_door_System.Models
{
    public class common
    {
        /// <summary>
        /// json to datatable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                    ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                    if (arrayList.Count > 0)
                    {
                        foreach (Dictionary<string, object> dictionary in arrayList)
                        {
                            if (dictionary.Keys.Count<string>() == 0)
                            {
                                result = dataTable;
                                return result;
                            }
                            if (dataTable.Columns.Count == 0)
                            {
                                foreach (string current in dictionary.Keys)
                                {
                                    dataTable.Columns.Add(current, Type.GetType("System.String"));
                                }
                            }
                            DataRow dataRow = dataTable.NewRow();
                            foreach (string current in dictionary.Keys)
                            {
                                dataRow[current] = dictionary[current];
                            }

                            dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                        }
                    }
                }
                else {
                    throw new Exception("数据不能为空");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            result = dataTable;
            return result;
        }

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        private static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        private static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
    }
}