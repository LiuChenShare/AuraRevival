using System.Data;
using System.Data.SQLite;

namespace AuraRevival.DB.SQLite
{
    public class SQLiteDBContext
    {
        /// <summary>
        /// 创建数据类型
        /// </summary>
        public IDbConnection ConnBuilder;


        private string ConnectionString;

        public SQLiteDBContext(string path)
        {
            ConnectionString = $"Data Source={path};";
        }

        public SQLiteDBContext()
        {
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "AuraRevivalDB.db"));
            if (!File.Exists(path))
            {


                //var path2 = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Base.db"));
                //File.Copy(path2, path, true);

                //ConnectionString = $"Data Source={path};";
                //using (var conn = GetConn())
                //{
                //    DBUpdateHelper.DBUpdate(conn);
                //}
            }
            ConnectionString = $"Data Source={path};";
        }

        public SQLiteDBContext(int type, string code)
        {
            switch (type)
            {
                case 1: //采集器日志DB
                    var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", $"IotLog_{code}_.db"));
                    if (!File.Exists(path))
                    {
                        var path2 = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "JFBase.db"));
                        File.Copy(path2, path, true);

                        ConnectionString = $"Data Source={path};";
                        using (var conn = GetConn())
                        {
                            DBUpdateHelper.DBUpdate(conn);
                        }
                    }
                    ConnectionString = $"Data Source={path};";
                    break;
            }

        }

        /// <summary>
        /// 获取SqlConnection连接对象
        /// </summary>
        public IDbConnection GetConn()
        {
            ConnBuilder = null;
            try
            {
                ConnBuilder = new SQLiteConnection(ConnectionString);
                ConnBuilder.Open();
            }
            catch (Exception ex)
            {
                if (null != ConnBuilder)
                {
                    ConnBuilder.Dispose();
                    ConnBuilder = null;
                }
                throw ex;
            }
            return ConnBuilder;
        }


    }
}
