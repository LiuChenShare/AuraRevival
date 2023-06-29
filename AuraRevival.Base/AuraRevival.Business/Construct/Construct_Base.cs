using AuraRevival.Business.Entity;
using AuraRevival.Business.Goods;
using AuraRevival.Business.Goods.Goods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace AuraRevival.Business.Construct
{
    /// <summary>
    /// 基地(建筑)
    /// </summary>
    public class Construct_Base : Construct_Default
    {
        #region 自定义
        #endregion

        public Construct_Base() { }

        public Construct_Base(string name, Point  location)
        {
            Id = Guid.NewGuid();
            Type = ConstructType.Base;
            Name = name;
            Level = 1;
            Location = location;


            //注册秒事件
            Grain.Instance.MainGame.SecondsEvent += SecondsEventExecute;
            Grain.Instance.MainGame.SecondsEvent += MinutesEventExecute;

            _levelConfig = new Dictionary<int, Construct_Default>
            {
                { 1, new Construct_Base() { Description = "这个是你最后的希望", _tallyMapTep = 60 } },
                { 2, new Construct_Base() { Description = "稍微有了点起色", _tallyMapTep = 55 } },
                { 3, new Construct_Base() { Description = "还不错呦", _tallyMapTep = 54 } },
                { 4, new Construct_Base() { Description = "拥有了一战之力", _tallyMapTep = 53 } }
            };
            LevelRefresh(Level);

            AssemblyString = GetType().Module.Name;
            TypeName = GetType().FullName;


            //初始化一个实体
            IEntity entity = new Entity_Leader();
            entity.Init("英雄",0, Id, Location);
        }


        public override async Task SecondsEventExecute(DateTime time)
        {
            if (_scriptCode != -1)
            {
                Thread thread = new Thread(() =>
                {
                    switch (_scriptCode)
                    {
                        case 1:
                            {
                                _tallyMap--;
                                if (_tallyMap <= 0)
                                {
                                    _tallyMap = _tallyMapTep;
                                    Level++;
                                    LevelRefresh(Level);
                                    _scriptCode = -1;
                                    Grain.Instance.MainGame.Msg(2,Name,$"升级为{Level}级");
                                }
                                break;
                            }
                        default:
                            break;
                    }

                });
                thread.Start();
            }

            //生产
            ProductionSeconds();
        }

        public override async Task MinutesEventExecute(DateTime time)
        {

        }

        public override bool ScriptEvent(int scriptCode, object obj)
        {
            _scriptCode = scriptCode;
            switch (scriptCode)
            {
                case 1: //升级
                    {
                        var levelMax = _levelConfig.Keys.Max();
                        if (Level + 1 > levelMax)
                            break;
                        //UpLevel();
                        return true;
                    }
                default:
                    break;
            }
            _scriptCode = -1;
            return false;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 每秒生产业务
        /// </summary>
        public override void ProductionSeconds()
        {
            IGoods goods = new Goods_Default();
            goods.Init(1, "木头", 5);
            IGoods goods2 = new Goods_Default();
            goods2.Init(2, "石头", 1);

            AddGoods(new List<IGoods>() { goods, goods2 });
        }

    }
}
