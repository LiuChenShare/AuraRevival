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
    /// 普通建筑
    /// </summary>
    public class Construct_Default : IConstruct
    {
        #region 公用
        public Guid Id {get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public ConstructType Type { get; set; } = ConstructType.Default;
        public int Level { get; set; } = 1;
        public Point Location { get; set; }
        public List<IGoods> Goods { get; protected set; } = new List<IGoods>();

        public int _tallyMap;
        public int _tallyMapTep;
        public int _scriptCode = -1;
        #endregion

        #region 自定义

        #endregion

        protected Construct_Default() { }

        protected readonly Dictionary<int, Construct_Default> _levelConfig;

        public Construct_Default(string name, Point location)
        {
            Id = Guid.NewGuid();
            Type = ConstructType.Default;
            Name = name;
            Level = 1;
            Location = location;


            //注册秒事件
            Grain.Instance.MainGame.SecondsEvent += SecondsEventExecute;
            Grain.Instance.MainGame.SecondsEvent += MinutesEventExecute;

            _levelConfig = new Dictionary<int, Construct_Default>
            {
                { 1, new Construct_Default() { Description = "这个是你最后的希望", _tallyMapTep = 60 } },
                { 2, new Construct_Default() { Description = "稍微有了点起色", _tallyMapTep = 55 } },
                { 3, new Construct_Default() { Description = "还不错呦", _tallyMapTep = 54 } },
                { 4, new Construct_Default() { Description = "拥有了一战之力", _tallyMapTep = 53 } }
            };
            LevelRefresh(Level);

        }


        public virtual async Task SecondsEventExecute(DateTime time)
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

        public virtual async Task MinutesEventExecute(DateTime time)
        {

        }

        public virtual bool ScriptEvent(int scriptCode, object obj)
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
                        //开始数秒升级
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
        public virtual void ProductionSeconds()
        {
            //IGoods goods = new Goods_Default();
            //goods.Init(1, "木头", 5);
            //IGoods goods2 = new Goods_Default();
            //goods2.Init(2, "石头", 1);

            //AddGoods(new List<IGoods>() { goods, goods2 });
        }

        /// <summary>
        /// 保存物品
        /// </summary>
        /// <param name="goodslist"></param>
        /// <returns></returns>
        public virtual bool AddGoods(List<IGoods> goodslist)
        {
            foreach(var item in goodslist)
            {
                switch (item.Type)
                {
                    case GoodsType.Goods:
                        IGoods gds = Goods.FirstOrDefault(x => x.Type == GoodsType.Goods && x.Code == item.Code);
                        //IGoods gds = null;
                        //foreach(var jjj in Goods)
                        //{
                        //    if(jjj.Type == GoodsType.Goods && jjj.Code == item.Code)
                        //    {
                        //        gds = jjj;
                        //        break;
                        //    }
                        //}
                        if (gds != null)
                            gds.Count += item.Count;
                        else
                            Goods.Add(item);
                        break;
                    case GoodsType.Equipment:
                        Goods.Add(item);
                        break;
                }
            }

            //Grain.Instance.MainGame.Msg(2, Name, $"仓库更新了");
            return true;
        }
        /// <summary>
        /// 移除物品
        /// </summary>
        /// <param name="goodslist"></param>
        /// <returns></returns>
        public virtual bool RemoveGoods(List<IGoods> goodslist)
        {
            int i = 0;
            bool reult = true;
            List<IGoods> goodsListRecycle = new List<IGoods>();//回收站

            //尝试移除
            for (i = 0; i < goodslist.Count; i++)
            {

                switch (goodslist[i].Type)
                {
                    case GoodsType.Goods:
                        var gdsGoods = Goods.FirstOrDefault(x => x.Type == GoodsType.Goods && x.Name == goodslist[i].Name);
                        if (gdsGoods != null && gdsGoods.Count >= goodslist[i].Count)
                        {
                            gdsGoods.Count -= goodslist[i].Count;
                            goodsListRecycle.Add(goodslist[i]);
                        }
                        else
                            reult = false;
                        break;
                    case GoodsType.Equipment:
                        var gdsEquipment = Goods.Where(x => x.Type == GoodsType.Goods && x.Name == goodslist[i].Name).OrderBy(x => x.Durable).FirstOrDefault();
                        if (gdsEquipment != null)
                        {
                            Goods.Remove(gdsEquipment);
                            goodsListRecycle.Add(gdsEquipment);
                        }
                        else
                            reult = false;
                        break;
                }
                if (!reult)
                    break;
            }

            //如果移除失败了，需要把回收站还原回去
            if (!reult)
            {
                foreach (var item in goodslist)
                {
                    switch (item.Type)
                    {
                        case GoodsType.Goods:
                            var gds = Goods.FirstOrDefault(x => x.Type == GoodsType.Goods && x.Name == item.Name);
                            if (gds != null)
                                gds.Count += item.Count;
                            else
                                Goods.Add(item);
                            break;
                        case GoodsType.Equipment:
                            Goods.Add(item);
                            break;
                    }
                }
            }
            else
                Grain.Instance.MainGame.Msg(2, Name, $"仓库更新了");

            return reult;
        }


        protected virtual void LevelRefresh(int level)
        {
            if (_levelConfig.ContainsKey(level))
            {
                var levelConfig = _levelConfig[level];
                Description = levelConfig.Description;
                _tallyMapTep = levelConfig._tallyMapTep;
            }
            _tallyMap = _tallyMapTep;
        }

    }
}
