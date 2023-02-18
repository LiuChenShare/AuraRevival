using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AuraRevival.Business.Construct;
using AuraRevival.Business.Entity;

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
        private MainGame() { }
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
        public delegate void TimeHandler(DateTime time);//声明委托
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
        /// 单int值委托
        /// </summary>
        /// <param name="time"></param>
        public delegate void IntHandler(int index);
        #endregion

        /// <summary>
        /// 游戏状态
        /// -1-GameOver
        /// 0-初始化
        /// 1-进行中
        /// </summary>
        public int GameState { get; set; } = 0;

        public Tuple<int, int> MapSize { get; set; } = new Tuple<int, int>(1000, 1000);

        /// <summary>
        /// 建筑
        /// 1.Id
        /// 2.Name
        /// 3.Type
        /// </summary>
        public List<Tuple<Guid, string, ConstructType>> Entitys = new List<Tuple<Guid, string, ConstructType>>();
        /// <summary>
        /// 建筑
        /// </summary>
        public List<IConstruct> Constructs = new List<IConstruct>();

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
        /// 
        /// </summary>
        /// <param name="constructBaseName">基地名称</param>
        public void Init(string constructBaseName)
        {
            if (GameState == 0)
            {
                Random ran = new Random();
                int x = ran.Next(MapSize.Item1);
                int y = ran.Next(MapSize.Item2);
                Point point = new Point(x, y);


                Block block;
                if (Grain.Instance.Blocks.Any(x => x.Id == point))
                    block = Grain.Instance.Blocks.Where(x => x.Id == point).FirstOrDefault();
                else
                    block = NewBlock(point);

                Construct_Base construct_Base = new Construct_Base(constructBaseName, point);


                Constructs.Add(construct_Base);
                Grain.Instance.Constructs.Add(construct_Base);
                block.Constructs.Add(construct_Base);
            }
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

}
