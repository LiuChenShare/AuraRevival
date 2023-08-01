using AuraRevival.Business.Entity;
using AuraRevival.Business.Goods;
using AuraRevival.Business.Goods.Goods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuraRevival.Business.Construct
{
    /// <summary>
    /// 营地(建筑)
    /// </summary>
    public class Construct_Camp : Construct_Default
    {
        #region 公用
        #endregion

        #region 自定义

        #endregion

        public Construct_Camp() { }

        public Construct_Camp(string name, Point  location)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = EntityHelper.GetRandomNation();

            Id = Guid.NewGuid();
            Type = ConstructType.Camp;
            Name = name;
            Level = 1;
            Location = location;


            //注册秒事件
            Grain.Instance.MainGame.SecondsEvent += SecondsEventExecute;
            Grain.Instance.MainGame.MinutesEvent += MinutesEventExecute;

            _levelConfig = new Dictionary<int, Construct_Default>
            {
                { 1, new Construct_Camp() { Description = "这个是你最后的希望", _tallyMapTep = 60 } },
                { 2, new Construct_Camp() { Description = "稍微有了点起色", _tallyMapTep = 55 } },
                { 3, new Construct_Camp() { Description = "还不错呦", _tallyMapTep = 54 } },
                { 4, new Construct_Camp() { Description = "拥有了一战之力", _tallyMapTep = 53 } }
            };
            LevelRefresh(Level);

            AssemblyString = GetType().Module.Name;
            TypeName = GetType().FullName;
        }


        public override async Task SecondsEventExecute(DateTime time)
        {
            if (_scriptCode != -1)
            {
                Thread thread = new Thread(() =>
                {
                    switch (_scriptCode)
                    {
                        case (int)ScriptComd.Construct_UpLevel:
                            {
                                _tallyMap--;
                                if (_tallyMap <= 0)
                                {
                                    _tallyMap = _tallyMapTep;
                                    Level = Level+1;
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
            //初始化一个敌人
            IEntity entity = new Entity_Default();
            entity.Init(EntityHelper.GetRandomName(), 2, Id, Location);
            entity.ScriptEvent((int)ScriptComd.Entity_AutoMove, null);
        }

        public override bool ScriptEvent(int scriptCode, object obj)
        {
            _scriptCode = scriptCode;
            switch (scriptCode)
            {
                case (int)ScriptComd.Construct_UpLevel: //升级
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
            
        }

    }
}
