using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ToolsDev.Utilities.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class UtilityHelper
    {
        /// <summary>
        /// Get AppConfig value
        /// </summary>
        /// <param name="tagName">AppConfig tagName</param>
        /// <returns></returns>
        public static string GetAppSetting(string tagName)
        {
            string output = ConfigurationManager.AppSettings[tagName];

            if (string.IsNullOrEmpty(output))
            {
                return "";
            }
            return output;
        }

        /// <summary>
        /// Get ConnectionString value
        /// </summary>
        /// <param name="tagName">AppConfig tagName</param>
        /// <returns></returns>
        public static string GetConnectionString(string tagName)
        {
            var output = ConfigurationManager.ConnectionStrings[tagName];

            if (output == null)
            {
                return "";
            }
            return output.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringList"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this List<string> stringList) where T : class
        {
            List<T> deserialized = new List<T>();
            foreach (var item in stringList)
            {
                var objectValue = JsonConvert.DeserializeObject<T>(item);
                deserialized.Add(objectValue);
            }
            return deserialized;
        }

        /// <summary>
        /// object convert to json string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            if (obj == null)
            {
                return "";
            }

            string jsonString = JsonConvert.SerializeObject(obj);
            return jsonString;
        }

        /// <summary>
        /// json string convert to object
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T ToJsonFormat<T>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return default;
            }

            T obj = JsonConvert.DeserializeObject<T>(jsonString);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(this string hexString)
        {
            return Enumerable.Range(0, hexString.Length).Where(x => x % 2 == 0).Select(s => Convert.ToByte(hexString.Substring(s, 2), 16)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ByteArrayToString(this byte[] b)
        {
            return BitConverter.ToString(b);
        }

        /// <summary>
        /// Convert DataTable to List object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDateTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (pro.PropertyType.Name.ToLower() == "int")
                            pro.SetValue(obj, int.Parse(dr[column.ColumnName].ToString()), null);
                        else if (pro.PropertyType.Name.ToLower() == "decimal")
                            pro.SetValue(obj, decimal.Parse(dr[column.ColumnName].ToString()), null);
                        else
                            pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else continue;
                }
            }

            return obj;
        }

        /// <summary>
        /// Get Enum Description
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescription(this object source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }
    }
}
