using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRevival.Core
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogHelper
    {
        private static readonly string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log"));

        /// <summary>
        /// 写入日志
        /// </summary>
        public static void WriteLog(string type, string content)
        {
            try
            {
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                var path = Path.GetFullPath(Path.Combine(_path, DateTime.Now.ToString("yyyy-MM-dd") + ".txt"));

                var str = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {type} - {content}\r\n";

                File.AppendAllText(path, str);
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        public static void WriteLog(string type, string content, Exception ex)
        {
            try
            {
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                var path = Path.GetFullPath(Path.Combine(_path, DateTime.Now.ToString("yyyy-MM-dd") + ".txt"));

                var str = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {type} - {content} --> \r\n{ex}\r\n";

                File.AppendAllText(path, str);
            }
            catch (Exception e)
            {

            }
        }
    }
}
