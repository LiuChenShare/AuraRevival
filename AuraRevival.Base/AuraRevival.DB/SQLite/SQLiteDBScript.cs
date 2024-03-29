﻿namespace AuraRevival.DB.SQLite
{
    public static class SQLiteDBScript
    {
        //public static int Version { get; set; }

        public static Dictionary<int, string> DBScript { get; set; } = new Dictionary<int, string>()
        {
                {1,@"--1.创建数据库基础表
                    CREATE TABLE IF NOT EXISTS DBVersion (
                        Version integer NOT NULL,
                        CreateTime NVARCHAR NOT NULL
                    );
                    --2.创建游戏主表
                    CREATE TABLE IF NOT EXISTS MainGame (
                        Id NVARCHAR NOT NULL,
                        GameState integer,
                        MapSize NVARCHAR,
                        GameDate TIMESTAMP,
                        PRIMARY KEY (""Id"")
                    );
                    --3.创建区块表
                    CREATE TABLE IF NOT EXISTS ""Block"" (
                        ""Id"" NVARCHAR NOT NULL,
                        PRIMARY KEY (""Id"")
                    );
                    --4.创建建筑表
                    CREATE TABLE IF NOT EXISTS ""Construct"" (
                        ""Id"" NVARCHAR NOT NULL,
                        ""Name"" NVARCHAR,
                        ""Description"" NVARCHAR,
                        ""Type"" integer,
                        ""Level"" integer,
                        ""Location"" NVARCHAR,
                        ""_tallyMap"" integer,
                        ""_tallyMapTep"" integer,
                        ""_scriptCode"" integer,
                        ""AssemblyString"" NVARCHAR,
                        ""TypeName"" NVARCHAR,
                        PRIMARY KEY (""Id"")
                    );
                    --5.创建实体表
                    CREATE TABLE IF NOT EXISTS ""Entity"" (
                        ""Id"" NVARCHAR NOT NULL,
                        ""Name"" NVARCHAR,
                        ""Location"" NVARCHAR,
                        ""Description"" NVARCHAR,
                        ""Type"" integer,
                        ""MId"" NVARCHAR NOT NULL,
                        ""State"" integer,
                        ""Characters"" NVARCHAR,
                        ""_scriptCode"" integer,

                        --等级相关
                        ""Level"" integer,
                        ""Exp"" integer,
                        ""ExpMax"" integer,

                        --战斗相关
                        ""Power"" integer,
                        ""Agile"" integer,
                        ""HP"" integer,
                        
                        --地图移动相关
                        ""_tallyMap"" integer,
                        ""_tallyMapTep"" integer,
                        ""MoveFeature"" integer,
                        ""DestLocation"" NVARCHAR,

                        --程序集
                        ""AssemblyString"" NVARCHAR,
                        ""TypeName"" NVARCHAR,

                        PRIMARY KEY (""Id"")
                    );
                    
                        --ALTER TABLE IotInfo  ADD COLUMN CollectorInfo TEXT;"
                    },
            //    { 2,@"--1.将表名改为临时表
            //        ALTER TABLE RestartIOTPlan RENAME TO _RestartIOTPlan_old;

            //        --2.创建新表
            //        CREATE TABLE RestartIOTPlan (
            //          ID NVARCHAR NOT NULL,
            //          Name NVARCHAR,
            //          ComplteTime TIMESTAMP,
            //          CreationTime TIMESTAMP,
            //          Description NVARCHAR,
            //          IOTNum INTEGER,
            //          FileNum INTEGER,
            //          CompleteCount integer,
            //          SuccessCount integer
            //        );

            //        --3.导入数据
            //        INSERT INTO RestartIOTPlan (ID, Name, ComplteTime, CreationTime, Description, IOTNum, FileNum, CompleteCount, SuccessCount) SELECT ID, Name, ComplteTime, CreationTime, Description, IOTNum, FileNum, CompleteCount, SuccessCount FROM _RestartIOTPlan_old;

            //        --4.删除临时表(可选)

            //        DROP TABLE _RestartIOTPlan_old;"},

            //    {3,@"ALTER TABLE IotInfo  ADD COLUMN CollectorInfo TEXT;"},

            //    {4,@"ALTER TABLE Device  ADD COLUMN TMS_ModbusSlavePort NVARCHAR;"},

            //    {5,@"ALTER TABLE IotInfo  ADD COLUMN NetConfig TEXT;"},

            //};
        };
} }
