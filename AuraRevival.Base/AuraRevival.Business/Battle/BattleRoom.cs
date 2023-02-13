using AuraRevival.Business.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static AuraRevival.Business.MainGame;

namespace AuraRevival.Business.Battle
{
    public class BattleRoom
    {
        public Guid Id { get; private set; } = Guid.Empty;

        /// <summary>
        /// 区块id
        /// </summary>
        public Point BlockId { get; set; }

        public List<BattleEntityInfo> BattleEntities { get; private set; } = new List<BattleEntityInfo>();
        public int Round { get; private set; } = 0;

        public BattleStateType State { get; private set; } = BattleStateType.Default;


        /// <summary>回合事件 </summary>
        public event TimeHandler RoundEvent;

        public BattleRoom()
        {


            MainGame.Instance.SecondsEvent += RoundEventExecute;


        }

        public void AddEntity(List<IEntity> entities)
        {
            List<Guid> mIds = entities.Select(x => x.MId).ToList();
            Dictionary<Guid,List<Guid>> group = new Dictionary<Guid,List<Guid>>();
            foreach (var mid in mIds)
            {
                group.Add(mid, entities.Where(x => x.MId != mid).Select(x => x.Id).ToList());
            }
            foreach (var entity in entities)
            {
                BattleEntityInfo battleEntity = new BattleEntityInfo(entity, group[entity.MId]);
                BattleEntities.Add(battleEntity);
                RoundEvent += battleEntity.RoundEvent;
                battleEntity.BattleEvent += BattleEventExecute;
            }
        }


        public void BattleStart()
        {
            if (State == BattleStateType.Default)
            {
                State = BattleStateType.InBattle;
            }
        }


        private void RoundEventExecute(DateTime time)
        {
            if (State == BattleStateType.InBattle)
            {
                Round++;
                RoundEvent?.Invoke(time);
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

            //判断是否miss
            var a = defender.Entity.Agile - attacker.Entity.Agile;

            //判断伤害
            var b = attacker.Entity.Power  / 10;
            var c = (defender.Entity.Power - attacker.Entity.Power) / 20;
            var hurtMax = b - c > 0 ? b - c : 1;
            Random ran = new Random((int)DateTime.Now.ToFileTimeUtc());
            int hurt = ran.Next(1, hurtMax);

            //声明结算

            Grain.Instance.MainGame.Msg(1, $"({BlockId})", $"{attacker.Entity.Name} 攻击 {defender.Entity.Name} ，伤害{hurt}");
            var health = defender.Settlement(hurt);
            if (health == 0)
            {
                RoundEvent -= defender.RoundEvent;

                foreach (var en in BattleEntities)
                {
                    if (en.Entity.MId == defenderId || en.Entity.State == EntityStateType.Die)
                        continue;

                    en.RemoveFoeIds(new List<Guid>() { defenderId });
                }
            }
        }
    }
}
