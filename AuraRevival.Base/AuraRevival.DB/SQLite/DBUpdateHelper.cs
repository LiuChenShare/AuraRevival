using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AuraRevival.DB.SQLite
{
    public static class DBUpdateHelper
    {
        public static void DbVersionCheck()
        {

            //主库
            var dBContext = new SQLiteDBContext();
            using (var conn = dBContext.GetConn())
            {
                DBUpdate(conn);
            }
            ////日志库
            //string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            //DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            //FileInfo[] files = directoryInfo.GetFiles();
            //HashSet<string> result = new HashSet<string>();

            //foreach (var file in files)
            //{
            //    if (file.Name.Contains("IotLog_") && file.Name.Contains("_.db"))
            //    {
            //        var param = file.Name.Split('_', StringSplitOptions.RemoveEmptyEntries);
            //        if (param.Count() < 3)
            //            continue;

            //        var dBContextLog = new SQLiteDBContext(1, param[1]);
            //        using (var conn = dBContextLog.GetConn())
            //        {
            //            DBUpdate(conn);
            //        }
            //    }
            //}
        }

        public static void DBUpdate( System.Data.IDbConnection conn)
        {
            int maxVesion = SQLiteDBScript.DBScript.Keys.Max();
            var sql = @"SELECT Version FROM DBVersion ORDER BY Version DESC; ";
            var sqlUpdate = @"INSERT INTO DBVersion VALUES (@Version,@CreateTime); ";
            var sqlSerch = @"SELECT count(*) FROM sqlite_master WHERE type='table' AND name='DBVersion';";
            var table = conn.QueryFirstOrDefault<int>(sqlSerch);
            int version = -1;
            if (table >0 ) 
                version = conn.Query<int>(sql)?.FirstOrDefault() ?? -1;

            if (version < maxVesion)
            {
                for (var v = version + 1; v <= maxVesion; v++)
                {
                    if (SQLiteDBScript.DBScript.ContainsKey(v))
                    {
                        conn.Execute(SQLiteDBScript.DBScript[v]);
                        conn.Execute(sqlUpdate, new { Version = v, CreateTime = DateTime.Now });
                    }
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static List<T> GetModelFromSql<T>(this IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var aa = connection.QueryToDataTable(sql, param, transaction, commandTimeout, commandType);


            return GetModelFromDB<T>(aa);
        }


        public static List<T> GetModelFromDB<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// 将DataRow转换成实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T GetItem<T>(DataRow dr)
        {
            try
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        {
                            if (dr[column.ColumnName] == DBNull.Value)
                            {
                                pro.SetValue(obj, null, null);
                                break;
                            }
                            else
                            {
                                //根据类型特殊处理的方式
                                switch (pro.PropertyType.Name)
                                {
                                    case "Guid":
                                        pro.SetValue(obj, Guid.Parse(dr[column.ColumnName].ToString()), null);
                                        break;
                                    //case "Int32":
                                    //    pro.SetValue(obj, int.Parse(dr[column.ColumnName].ToString()), null);
                                    //    break;
                                    case "Int32":
                                        pro.SetValue(obj, int.Parse(dr[column.ColumnName].ToString()), null);
                                        break;
                                    case "String":
                                    case "DateTime":
                                        pro.SetValue(obj, dr[column.ColumnName], null);
                                        break;
                                    default:
                                        if (pro.PropertyType.BaseType != null && pro.PropertyType.BaseType.Name == "Enum")
                                        {
                                            pro.SetValue(obj, int.Parse(dr[column.ColumnName].ToString()), null);
                                        }
                                        else
                                        {
                                            //pro.SetValue(obj, dr[column.ColumnName], null);
                                            MethodInfo mi = typeof(DBUpdateHelper).GetMethod("Deserialize").MakeGenericMethod(pro.PropertyType);
                                            var value = mi.Invoke(null, new object[] { dr[column.ColumnName].ToString() });
                                            pro.SetValue(obj, value, null);
                                        }
                                        break;
                                }

                                #region 其他方式
                                ////默认方式
                                //pro.SetValue(obj, dr[column.ColumnName], null);
                                //break;

                                ////根据类型特殊处理的方式
                                //switch (pro.PropertyType.Name)
                                //{
                                //    case "Guid":
                                //        pro.SetValue(obj, Guid.Parse(dr[column.ColumnName].ToString()), null);
                                //        break;
                                //    default:
                                //        pro.SetValue(obj, dr[column.ColumnName], null);
                                //        break;
                                //}


                                ////根据自定义特性标记来处理
                                //DBFieldTypeAttribute? dbFieldType = (DBFieldTypeAttribute?)pro.GetCustomAttribute(typeof(DBFieldTypeAttribute));
                                //if (dbFieldType != null)
                                //{
                                //    switch (dbFieldType.Type.Name)
                                //    {
                                //        case "Guid":
                                //            pro.SetValue(obj, Guid.Parse(dr[column.ColumnName].ToString()), null);
                                //            break;
                                //        case "Int32":
                                //            pro.SetValue(obj, int.Parse(dr[column.ColumnName].ToString()), null);
                                //            break;
                                //        default:
                                //            ////var tttt = dbFieldType.Type;
                                //            var bbb = JsonSerializer.Deserialize<object>(dr[column.ColumnName].ToString());

                                //            MethodInfo mi = typeof(DBUpdateHelper).GetMethod("Deserialize").MakeGenericMethod(dbFieldType.Type);
                                //            var aaa = mi.Invoke(null, new object[] { dr[column.ColumnName].ToString() });
                                //            pro.SetValue(obj, aaa, null);
                                //            break;
                                //    }

                                //    break;
                                //}
                                #endregion
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
             }
        }


        /// <summary>
        ///  返回查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable QueryToDataTable(this IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dataReader = connection.ExecuteReader(sql, param, transaction, commandTimeout, commandType))
            {
                var datatable = new DataTable();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    DataColumn mydc = new DataColumn();    //关键的一步
                    mydc.DataType = dataReader.GetFieldType(i);
                    mydc.ColumnName = dataReader.GetName(i);
                    datatable.Columns.Add(mydc);          //关键的第二步
                }

                while (dataReader.Read())
                {
                    DataRow myDataRow = datatable.NewRow();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        myDataRow[i] = dataReader[i].ToString();
                    }
                    datatable.Rows.Add(myDataRow);
                }
                ///关闭数据读取器
                dataReader.Close();
                return datatable;
            }
        }

        /// <summary>
        ///  返回查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonStr)
        {
            return JsonSerializer.Deserialize<T>(jsonStr);
        }
    }

}
