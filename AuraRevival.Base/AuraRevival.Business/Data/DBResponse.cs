using AuraRevival.Core;
using AuraRevival.DB.SQLite;
using Dapper;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AuraRevival.Business.DB
{
    public class DBResponse
    {
        /// <summary>
        /// 获取设备下的采集站编码
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        public static List<string> GetDeviceIotCode(int id)
        {
            List<string> iotCodes = null;
            var dBContext = new SQLiteDBContext();
            using (var conn = dBContext.GetConn())
            {
                try
                {
                    var sql = "SELECT Code FROM IotInfo WHERE DevId=@id;"; ;
                    iotCodes = conn.Query<string>(sql, new { id })?.ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("DBResponse", $"Mqtt Error 查询物设备异常", ex);
                }
            }
            return iotCodes;
        }

        #region Game
        /// <summary>
        /// 获取游戏
        /// </summary>
        /// <returns></returns>
        public static MainGame GetMainGame()
        {
            var dBContext = new SQLiteDBContext();
            using var conn = dBContext.GetConn();

            try
            {
                var sql_Get = "Select * FROM MainGame;";
                //var aaa = conn.QueryFirstOrDefault<MainGame>(sql_Get);
                var aaa = conn.GetModelFromSql<MainGame>(sql_Get).FirstOrDefault();
                return aaa;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("DBResponse", "GetMainGame Error", ex);
                //LogHelper.WriteLog(GetType().FullName, "SaveMainGame Error", ex);
                return null;
            }
        }

        /// <summary>
        /// 保存游戏
        /// </summary>
        /// <param name="mainGame"></param>
        /// <returns></returns>
        public static bool SaveMainGame(MainGame mainGame)
        {
            var dBContext = new SQLiteDBContext();
            using var conn = dBContext.GetConn();
            var transaction = conn.BeginTransaction();

            try
            {
                var sql_Get = "Select * FROM MainGame WHERE Id=@Id;";
                var sql_InsertOrUpdate = @"UPDATE MainGame SET GameState=@GameState, MapSize=@MapSize, GameDate=@GameDate WHERE Id=@Id;";
                var aaa = conn.QueryFirstOrDefault<MainGame>(sql_Get, new { mainGame.Id });
                if (aaa == null)//Update
                    sql_InsertOrUpdate = @"INSERT INTO MainGame(ID,GameState,MapSize,GameDate) VALUES (@Id, @GameState, @MapSize, @GameDate);";

                conn.Execute(sql_InsertOrUpdate,
                    new
                    {
                        Id = mainGame.Id.ToString(),
                        GameState = mainGame.GameState,
                        MapSize = JsonSerializer.Serialize(mainGame.MapSize),
                        GameDate = mainGame.GameDate
                    }, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("DBResponse", "SaveMainGame Error", ex);
                //LogHelper.WriteLog(GetType().FullName, "SaveMainGame Error", ex);
                transaction.Rollback();
                return false;
            }
            return true;
        }
        #endregion

        #region Block
        /// <summary>
        /// 获取所有区块
        /// </summary>
        /// <returns></returns>
        public static List<Block> GetAllBlocks()
        {

            var dBContext = new SQLiteDBContext();
            using var conn = dBContext.GetConn();

            try
            {
                var sql_Get = "Select * FROM Block;";
                //var aaa = conn.QueryFirstOrDefault<MainGame>(sql_Get);
                var result = conn.GetModelFromSql<Block>(sql_Get);
                return result;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("DBResponse", "GetAllBlocks Error", ex);
                //LogHelper.WriteLog(GetType().FullName, "SaveMainGame Error", ex);
                return null;
            }
        }


        /// <summary>
        /// 保存区块
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static bool SaveBlocks(List<Block> blocks)
        {
            var dBContext = new SQLiteDBContext();
            using var conn = dBContext.GetConn();

            List<Attachment> files = new List<Attachment>();
            try
            {
                foreach(var block in blocks)
                {
                    var sql_Get = "Select * FROM Block WHERE Id=@Id;";
                    var sql_InsertOrUpdate = @"UPDATE Block SET GameState=@GameState, MapSize=@MapSize, GameDate=@GameDate WHERE Id=@Id;";
                    var aaa = conn.QueryFirstOrDefault<MainGame>(sql_Get, new { Id = JsonSerializer.Serialize(block.Id) });
                    if (aaa == null)//Update
                        sql_InsertOrUpdate = @"INSERT INTO Block(Id) VALUES (@Id);";
                    else
                        continue;

                    conn.Execute(sql_InsertOrUpdate,
                        new
                        {
                            Id = JsonSerializer.Serialize(block.Id)
                        });

                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("DBResponse", "SaveBlocks Error", ex);
                //LogHelper.WriteLog(GetType().FullName, "SaveMainGame Error", ex);
                return false;
            }
            return true;
        }
        #endregion

        #region Construct

        #endregion
    }
}
