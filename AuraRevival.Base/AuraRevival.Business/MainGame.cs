using System;
using System.Collections.Generic;
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
        #endregion

        /// <summary> 实体 </summary>
        public List<IEntity> Entitys = new List<IEntity>();

        //实例化Timer类，设置间隔时间为1秒；
        private System.Timers.Timer GameTimer = new System.Timers.Timer(1000);
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

            Construct_Base _Base = new Construct_Base("123");


            GameTimer.Start(); //启动定时器
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
