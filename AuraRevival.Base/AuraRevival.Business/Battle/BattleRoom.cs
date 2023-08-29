using AuraRevival.Business.Entity;
using AuraRevival.Business.Goods.Goods;
using AuraRevival.Business.Goods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AuraRevival.Business.MainGame;

namespace AuraRevival.Business.Battle
{
    public class BattleRoom
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// 区块id
        /// </summary>
        public Point BlockId { get; set; }

        public List<BattleEntityInfo> BattleEntities { get; private set; } = new List<BattleEntityInfo>();
        public int Round { get; private set; } = 0;

        public BattleStateType State { get; private set; } = BattleStateType.Default;


        /// <summary>回合事件 </summary>
        public event IntHandler RoundEvent;

        public BattleRoom(Point id,List<IEntity> entities)
        {
            BlockId = id;
            AddEntity(entities);
        }

        public void AddEntity(List<IEntity> entities)
        {
            if (!BattleEntities.Any())//初始
            {
                Grain.Instance.MainGame.Msg(1, $"{BlockId}", $"{string.Join("、", entities.Select(x => x.Name))} 开始战斗");
            }
            else//中途加入
            {
                Grain.Instance.MainGame.Msg(1, $"{BlockId}", $"{string.Join("、", entities.Select(x => x.Name))} 加入战斗");
            }

            //现有的派系
            var entities_old = BattleEntities.Select(x => x.Entity).ToList();
            entities_old.AddRange(entities);
            List<Guid> mIds = entities.Select(x => x.MId).Distinct().ToList();
            Dictionary<Guid, List<Guid>> group = new Dictionary<Guid, List<Guid>>();
            foreach (var mid in mIds)
            {
                group.Add(mid, entities_old.Where(x => x.MId != mid).Select(x => x.Id).ToList());
            }
            //加入战斗
            foreach (var entity in entities)
            {
                BattleEntityInfo battleEntity = new BattleEntityInfo(entity, group[entity.MId]);
                BattleEntities.Add(battleEntity);
                RoundEvent += battleEntity.RoundEvent;

                //battleEntity.BattleEvent += async (object[] ar) => { };

                battleEntity.BattleEvent += BattleEventExecute;
            }
            //给老人声明新加入的敌人
            foreach (var entity in entities)
            {
                foreach (var en in BattleEntities)
                {
                    if (en.Entity.MId == entity.MId || en.Entity.State == EntityStateType.Die)
                        continue;

                    en.AddFoeIds(new List<Guid>() { entity.Id });
                }
            }
        }


        public void BattleStart()
        {
            if (State == BattleStateType.Default)
            {
                State = BattleStateType.InBattle;
                MainGame.Instance.SecondsEvent += RoundEventExecute;
            }
        }


        private async Task RoundEventExecute(DateTime time)
        {
            if (State == BattleStateType.InBattle)
            {
                if(BattleEntities.Select(x => x.Entity.MId).Distinct().Count() < 2)
                {
                    State = BattleStateType.BattleOver;
                    MainGame.Instance.SecondsEvent -= RoundEventExecute;

                    MainGame.Instance.BlockUpdate(BlockId);

                    foreach (var en in BattleEntities)
                    {
                        en.EndBattle();
                    }

                    return;
                }


                Round++;
                RoundEvent?.Invoke(Round);
            }
        }

        /// <summary>
        /// 攻击事件
        /// </summary>
        /// <param name="objects"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BattleEventExecute(object[] objects)
        {
            Guid attackerId = (Guid)objects[0];//攻击者
            Guid defenderId = (Guid)objects[1];//防守者

            BattleEntityInfo attacker = BattleEntities.FirstOrDefault(x => x.Entity.Id == attackerId);
            BattleEntityInfo defender = BattleEntities.FirstOrDefault(x => x.Entity.Id == defenderId);
            if (attacker == null || defender == null)
                throw new Exception("找不到参与方");

            Random ran = new Random((int)DateTime.Now.ToFileTimeUtc());
            //判断是否miss
            var a = ((defender.Entity.Agile - attacker.Entity.Agile) / (float)attacker.Entity.Agile) * 100;
            if (a < 1) a = 1;
            if (a > 99) a = 99;
            Grain.Instance.MainGame.Msg(1, $"{BlockId}", $"{attacker.Entity.Name} 攻击 {defender.Entity.Name} ，MISS几率{a}%");
            var aa = ran.Next(0, 100);
            if (aa <= a)
            {
                Grain.Instance.MainGame.Msg(1, $"{BlockId}", $"{attacker.Entity.Name} 攻击 {defender.Entity.Name} ，MISS了");
                return;
            }

            //判断伤害
            var b = attacker.Entity.Power  / 10;
            var c = (defender.Entity.Power - attacker.Entity.Power) / 20;
            var hurtMax = b - c > 0 ? b - c : 1;
            int hurt = ran.Next(1, hurtMax);

            //声明结算

            Grain.Instance.MainGame.Msg(1, $"{BlockId}", $"{attacker.Entity.Name} 攻击 {defender.Entity.Name} ，伤害{hurt}");
            var health = defender.Settlement(hurt);
            if (health == 0)
            {
                RoundEvent -= defender.RoundEvent;

                BattleEntities.Remove(defender);

                foreach (var en in BattleEntities)
                {
                    if (en.Entity.MId == defenderId || en.Entity.State == EntityStateType.Die)
                        continue;

                    en.RemoveFoeIds(new List<Guid>() { defenderId });
                }

                //给经验
                var epxNum = defender.Entity.Power + defender.Entity.Agile;
                attacker.Entity.SetExp(epxNum);
                Grain.Instance.MainGame.Msg(3, $"{attacker.Entity.Name}", $"获得 {epxNum} 经验，当前{attacker.Entity.Exp}/{attacker.Entity.ExpMax}");

                //给掉落物品
                AuraRevival.Business.Construct.Construct_Base construct = Grain.Instance.GetConstructBase();
                if (attacker.Entity.MId == construct.Id)
                {
                    IGoods goodWood = new Goods_Default();
                    goodWood.Init(1, "木头", defender.Entity.Agile);
                    IGoods goodStone = new Goods_Default();
                    goodStone.Init(2, "石头", defender.Entity.Power);
                    List<IGoods> goods = new List<IGoods>() { goodWood, goodStone };
                    construct.AddGoods(goods);
                    StringBuilder builder = new StringBuilder();
                    foreach (var item in goods)
                    {
                        builder.Append($"{item.Name}*{item.Count} ");
                    }
                    Grain.Instance.MainGame.Msg(2, $"{construct.Name}", $"{attacker.Entity.Name} 斩获了 {builder.ToString()}");
                }
            }

        }
        
    }
}
