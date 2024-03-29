﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using AuraRevival.Business.Construct;
using AuraRevival.Business.DB;
using AuraRevival.Business.Entity;
using AuraRevival.Business.Goods;
using AuraRevival.Core;
using AuraRevival.DB;
using AuraRevival.DB.SQLite;

namespace AuraRevival.Business
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class MainGame
    {
        #region 单例
        private static volatile MainGame instance;
        private static object syncRoot = new Object();
        public MainGame() { }
        public static MainGame Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MainGame();
                    }
                }
                return instance;
            }
        }
        #endregion

        #region 事件
        public delegate Task TimeHandler(DateTime time);//声明委托
        /// <summary>秒事件 </summary>
        public event TimeHandler SecondsEvent;
        /// <summary>分事件 </summary>
        public event TimeHandler MinutesEvent;
        /// <summary>时事件 </summary>
        public event TimeHandler HoursEvent;
        /// <summary>日事件 </summary>
        public event TimeHandler DaysEvent;
        /// <summary>月事件 </summary>
        public event TimeHandler MonthsEvent;

        /// <summary>
        /// 消息委托
        /// </summary>
        /// <param name="type">来源类型:0-游戏公告，1-区块，2-建筑，3-实体</param>
        /// <param name="source">来源名称</param>
        /// <param name="content">消息内容</param>
        public delegate void MsgHandler(int type, string source, string content);
        public event MsgHandler MsgEvent;

        /// <summary>
        /// 实体移动委托
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="L_old">旧坐标</param>
        /// <param name="L_new">新坐标</param>
        public delegate void EntityMoveHandler(IEntity entity, Point? L_old, Point? L_new);
        public event EntityMoveHandler EntityMoveEvent;

        /// <summary>
        /// 区块更新委托
        /// </summary>
        /// <param name="blockId">区块id</param>
        public delegate void BlockUpdateHandler(Point blockId);
        /// <summary>
        /// 区块更新事件
        /// </summary>
        public event BlockUpdateHandler BlockUpdateEvent;

        /// <summary>
        /// 万能委托
        /// </summary>
        public delegate void AllHandler(object[] objects);
        /// <summary>
        /// 万能委托异步
        /// </summary>
        public delegate void AllTaskHandler(object[] objects);

        /// <summary>
        /// 单int值委托
        /// </summary>
        /// <param name="time"></param>
        public delegate void IntHandler(int index);
        #endregion

        /// <summary>
        /// 游戏Id
        /// </summary>
        [DBFieldType(typeof(Guid))]
        public Guid Id { get;private set; } = Guid.NewGuid();
        /// <summary>
        /// 游戏状态
        /// </summary>
        [DBFieldType(typeof(Int32))]
        public GameStateType GameState { get; private set; } = GameStateType.Init;

        [DBFieldType(typeof(Point))]
        public Point MapSize { get; private set; } = new Point(1000, 1000);


        //实例化Timer类，设置间隔时间为1秒；
        private readonly System.Timers.Timer GameTimer = new System.Timers.Timer(1000);
        /// <summary>
        /// 游戏时间
        /// </summary>
        public DateTime GameDate { get; private set; } = new DateTime();

        public void GameStart()
        {
            //SecondsEvent += ShowMsg;

            GameTimer.Elapsed += new System.Timers.ElapsedEventHandler(Execute);//到达时间的时候执行事件；
            GameTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            GameTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            GameTimer.Start(); //启动定时器
        }

        /// <summary>
        /// 初始化游戏
        /// </summary>
        /// <param name="constructBaseName">基地名称</param>
        public void Init(string constructBaseName)
        {
            if (GameState != GameStateType.Init)
            {
                Msg(0, "Server", $"游戏当前处于{GameState.GetDescription}状态，无法初始化");
            }

            //有没有存档
            Msg(0, "Server", "正在检测存档...");
            var aaa = DBResponse.GetMainGame();
            if (aaa == null)
            {
                #region 没有存档
                //没有存档
                Msg(0, "Server", "开始加载游戏...");
                Random ran = new Random((int)DateTime.Now.ToFileTimeUtc());

                #region 生成玩家基地
                int x = ran.Next(MapSize.X);
                int y = ran.Next(MapSize.Y);
                Point point = new Point(x, y);

                Block block;
                if (Grain.Instance.Blocks.Any(x => x.Id == point))
                    block = Grain.Instance.Blocks.Where(x => x.Id == point).FirstOrDefault();
                else
                    block = NewBlock(point);

                Construct_Base construct_Base = new Construct_Base(constructBaseName, point);
                Grain.Instance.Constructs.Add(construct_Base);
                block.Constructs.Add(construct_Base);
                #endregion

                #region 生成敌人兵营
                int x_Camp = x - 2;//ran.Next(MapSize.Item1);
                int y_Camp = y - 2;//ran.Next(MapSize.Item2);
                Point point_Camp = new Point(x_Camp, y_Camp);
                Block block_Camp;
                if (Grain.Instance.Blocks.Any(x => x.Id == point_Camp))
                    block_Camp = Grain.Instance.Blocks.Where(x => x.Id == point_Camp).FirstOrDefault();
                else
                    block_Camp = NewBlock(point_Camp);
                Construct_Camp construct_Camp = new Construct_Camp(null, point_Camp);
                Grain.Instance.Constructs.Add(construct_Camp);
                block_Camp.Constructs.Add(construct_Camp);
                #endregion

                Msg(0, "Server", "进入游戏");
                #endregion
            }
            else
            {
                //有存档
                Msg(0, "Server", "正在加载游戏主信息...");
                Id = aaa.Id;
                GameState = aaa.GameState;
                MapSize = aaa.MapSize;
                GameDate = aaa.GameDate;

                Msg(0, "Server", "正在加载区块信息...");
                var blocks = DBResponse.GetAllBlocks();
                Grain.Instance.Blocks = blocks;

                Msg(0, "Server", "正在加载建筑信息...");
                var constructs = DBResponse.GetAllConstructs();
                for (int i = 0; i < constructs.Count; i++)
                {
                    Msg(0, "Server", $"正在加载建筑信息({i + 1}/{constructs.Count})");
                    IConstruct _mtype = (IConstruct)Assembly.LoadFrom(constructs[i].AssemblyString).CreateInstance(constructs[i].TypeName);
                    _mtype.Resume(constructs[i]);
                    Grain.Instance.Constructs.Add(_mtype);

                    Block block;
                    if (Grain.Instance.Blocks.Any(x => x.Id == _mtype.Location))
                        block = Grain.Instance.Blocks.Where(x => x.Id == _mtype.Location).FirstOrDefault();
                    else
                        block = NewBlock(_mtype.Location);
                    block.Constructs.Add(_mtype);
                }

                Msg(0, "Server", "正在加载实体信息...");
                var entitys = DBResponse.GetAllEntitys();
                for (int i = 0; i < entitys.Count; i++)
                {
                    Msg(0, "Server", $"正在加载实体信息({i + 1}/{entitys.Count})");
                    IEntity _mtype = (IEntity)Assembly.LoadFrom(entitys[i].AssemblyString).CreateInstance(entitys[i].TypeName);
                    _mtype.Resume(entitys[i]);
                    Grain.Instance.Entities.Add(_mtype);

                    Block block;
                    if (Grain.Instance.Blocks.Any(x => x.Id == _mtype.Location))
                        block = Grain.Instance.Blocks.Where(x => x.Id == _mtype.Location).FirstOrDefault();
                    else
                        block = NewBlock(_mtype.Location);
                    block.AddEntities(_mtype);
                }

                Msg(0, "Server", "进入游戏");
            }
            return;

        }


        public Block NewBlock(Point point)
        {
            var block = new Block(point);
            Grain.Instance.Blocks.Add(block);
            return block;
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="type">来源类型:0-游戏公告，1-区块，2-建筑，3-实体</param>
        /// <param name="source">来源名称</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public Task Msg(int type, string source, string content)
        {
            MsgEvent?.Invoke(type, source, content);
            return Task.CompletedTask;
        }


        /// <summary>
        /// 实体移动
        /// </summary>
        /// <param name="entity">来源类型:0-游戏公告，1-区块，2-建筑，3-实体</param>
        /// <param name="L_old">旧坐标</param>
        /// <param name="L_new">新坐标</param>
        /// <returns></returns>
        public Task EntityMove(IEntity entity, Point? L_old, Point? L_new)
        {
            EntityMoveEvent?.Invoke(entity, L_old, L_new);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 区块更新
        /// </summary>
        /// <returns></returns>
        public Task BlockUpdate(Point blockId)
        {
            BlockUpdateEvent?.Invoke(blockId);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 暂停游戏
        /// </summary>
        public void PauseGame()
        {
            GameTimer.Enabled = false;
        }
        /// <summary>
        /// 继续游戏
        /// </summary>
        public void ProceedGame()
        {
            GameTimer.Enabled = true;
        }
        /// <summary>
        /// 保存游戏
        /// </summary>
        public void SaveGame()
        {
            PauseGame();
            DB.DBResponse.SaveMainGame(this);
            DB.DBResponse.SaveBlocks(Grain.Instance.Blocks);
            DB.DBResponse.SaveConstructs(Grain.Instance.Constructs);
            DB.DBResponse.SaveEntitys(Grain.Instance.Entities);
            ProceedGame();
        }

        /// <summary>
        /// 停止游戏
        /// </summary>
        public void StopGame()
        {

        }


        private void Execute(object source, System.Timers.ElapsedEventArgs e)
        {
            GameDate = GameDate.AddSeconds(1);
            SecondsEvent?.Invoke(GameDate);
            if (GameDate.Second == 0)
            {
                MinutesEvent?.Invoke(GameDate);
                if (GameDate.Minute == 0)
                {
                    HoursEvent?.Invoke(GameDate);
                    if (GameDate.Hour == 0)
                    {
                        DaysEvent?.Invoke(GameDate);
                        if (GameDate.Day == 1)
                        {
                            MonthsEvent?.Invoke(GameDate);
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GameStateType
    {
        /// <summary>
        /// GameOver
        /// </summary>
        [Description("GameOver")]
        GameOver = -1,



        /// <summary>
        /// 初始化
        /// </summary>
        [Description("初始化")]
        Init = 0,
        /// <summary>
        /// 加载中
        /// </summary>
        [Description("加载中")]
        Load = 1,
        /// <summary>
        /// 游戏中
        /// </summary>
        [Description("游戏中")]
        InGame = 2,
    }
}
