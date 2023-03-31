﻿using AuraRevival.Business.Construct;
using AuraRevival.Core;
using AuraRevival.DB.SQLite;
using Dapper;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

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
                //var sql_Get = $"Select * FROM MainGame WHERE Id='{mainGame.Id}';";
                var sql_Get = "Select * FROM MainGame WHERE Id=@Id;";
                var sql_InsertOrUpdate = @"UPDATE MainGame SET GameState=@GameState, MapSize=@MapSize, GameDate=@GameDate WHERE Id=@Id;";
                var aaa = conn.GetModelFromSql<MainGame>(sql_Get, new { Id=mainGame.Id });
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
        /// <summary>
        /// 获取所有建筑
        /// </summary>
        /// <returns></returns>
        public static List<Construct_Default> GetAllConstructs()
        {

            var dBContext = new SQLiteDBContext();
            using var conn = dBContext.GetConn();

            try
            {
                var sql_Get = "Select * FROM Construct;";
                //var aaa = conn.QueryFirstOrDefault<MainGame>(sql_Get);
                var result = conn.GetModelFromSql<Construct_Default>(sql_Get);
                return result;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("DBResponse", "GetAllConstructs Error", ex);
                //LogHelper.WriteLog(GetType().FullName, "SaveMainGame Error", ex);
                return null;
            }
        }


        /// <summary>
        /// 保存建筑
        /// </summary>
        /// <param name="constructs"></param>
        /// <returns></returns>
        public static bool SaveConstructs(List<IConstruct> constructs)
        {
            var dBContext = new SQLiteDBContext();
            using var conn = dBContext.GetConn();

            try
            {
                foreach (var construct in constructs)
                {
                    var sql_Get = "Select * FROM Construct WHERE Id=@Id;";
                    var sql_InsertOrUpdate = @"UPDATE Construct SET Name=@Name, Description=@Description, Type=@Type, Level=@Level, Location=@Location, _tallyMap=@_tallyMap, _tallyMapTep=@_tallyMapTep,
                                                                    _scriptCode=@_scriptCode, AssemblyString=@AssemblyString, TypeName=@TypeName WHERE Id=@Id;";
                    var aaa = conn.QueryFirstOrDefault<IConstruct>(sql_Get, new { construct.Id });
                    if (aaa == null)//Update
                        sql_InsertOrUpdate = @"INSERT INTO Construct(Id, Name, Description, Type, Level, Location, _tallyMap, _tallyMapTep, _scriptCode, AssemblyString, TypeName)
                                                            VALUES (@Id, @Name, @Description, @Type, @Level, @Location, @_tallyMap, @_tallyMapTep, @_scriptCode, @AssemblyString, @TypeName);";
                    else
                        continue;

                    conn.Execute(sql_InsertOrUpdate,
                        new
                        {
                            Id = construct.Id.ToString(),
                            Name = construct.Name,
                            Description = construct.Description,
                            Type = construct.Type,
                            Level = construct.Level,
                            Location = JsonSerializer.Serialize(construct.Location),
                            _tallyMap = construct._tallyMap,
                            _tallyMapTep = construct._tallyMapTep,
                            _scriptCode = construct._scriptCode,
                            construct.AssemblyString,
                            construct.TypeName,
                        });

                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("DBResponse", "SaveConstructs Error", ex);
                //LogHelper.WriteLog(GetType().FullName, "SaveMainGame Error", ex);
                return false;
            }
            return true;
        }
        #endregion
    }
}
