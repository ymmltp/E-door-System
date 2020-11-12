using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Text;

namespace E_door_System.Models
{
    public class MysqlHelper
    {
        private static string strConn = ConfigurationManager.ConnectionStrings["eDoor"].ToString();
        //csv批量上传
        public int MySQLBulkLoader(DataTable dt)
        {
            if (string.IsNullOrEmpty(dt.TableName)) throw new Exception("Datatable Error...");
            if (dt.Rows.Count == 0) return 0;
            int insertCount = 0;
            string csv = DataTableToCsv(dt);
            string tmpPath = Path.GetTempFileName();
            File.WriteAllText(tmpPath, csv);
            using (MySqlConnection conn = new MySqlConnection(strConn))
                try
                {
                    conn.Open();
                    MySqlBulkLoader bulk = new MySqlBulkLoader(conn)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = tmpPath,
                        NumberOfLinesToSkip = 0,
                        TableName = dt.TableName,
                    };
                    insertCount = bulk.Load();
                    return insertCount;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    conn.Dispose();
                }
        }
        private static string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。  
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。  
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。  
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}