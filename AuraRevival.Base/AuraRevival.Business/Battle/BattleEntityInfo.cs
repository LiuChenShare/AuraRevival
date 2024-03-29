﻿using AuraRevival.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AuraRevival.Business.MainGame;

namespace AuraRevival.Business.Battle
{
    /// <summary>
    /// 战斗内设备属性
    /// </summary>
    public class BattleEntityInfo
    {
        public IEntity Entity { get; set; }
        /// <summary>
        /// 敌人
        /// </summary>
        List<Guid> FoeIds { get; set; }

        /// <summary> 攻击事件 </summary>
        public event AllHandler BattleEvent;

        /// <summary> 行动值 </summary>
        public int _tallyBattle = 0;
        /// <summary> 普通攻击所需行动值 </summary>
        public int _total_A;

        /// <summary> 行动值模板 </summary>
        private int _tallyBattleMax;
        /// <summary> 当前指令 </summary>
        private int _scriptCode = -1;



        public BattleEntityInfo(IEntity entity, List<Guid> foeIds)
        {
            Entity = entity;
            FoeIds = foeIds;
            _tallyBattleMax = 20 * (100 / (100 + Entity.Agile));

            Entity.ScriptEvent((int)ScriptComd.Entity_Battle, null);
        }

        public void RoundEvent(int round)
        {

            if (Entity.State != EntityStateType.InBattle)
                return;

            if (_tallyBattle < _tallyBattleMax)
                _tallyBattle++;

            //执行攻击
            if (_tallyBattle == _tallyBattleMax)
            {
                if (FoeIds.Any())
                {
                    _tallyBattle = 0;


                    BattleEvent.Invoke(new object[] { Entity.Id, FoeIds.First() });
                }
            }
        }

        /// <summary>
        /// 结算
        /// </summary>
        public int Settlement(int hurt)
        {
            var health = Entity.HP - hurt > 0 ? Entity.HP - hurt : 0;

            Entity.RestockHP(hurt);


            Grain.Instance.MainGame.Msg(3, $"{Entity.Name}", $"当前HP{Entity.HP}");

            return health;
        }

        /// <summary>
        /// 移除敌人
        /// </summary>
        /// <param name="foeIds"></param>
        public void RemoveFoeIds(List<Guid> foeIds)
        {
            foreach (var id in foeIds)
            {
                if (FoeIds.Contains(id))
                    FoeIds.Remove(id);
            }
        }

        /// <summary>
        /// 增加敌人
        /// </summary>
        /// <param name="foeIds"></param>
        public void AddFoeIds(List<Guid> foeIds)
        {

            foreach (var id in foeIds)
            {
                if (!FoeIds.Contains(id))
                {
                    FoeIds.Add(id);
                }
            }
        }

        public void EndBattle()
        {
            Entity.ScriptEvent((int)ScriptComd.Entity_BattleOut, null);

            if (Entity.State == EntityStateType.InBattle)
                Entity.State= EntityStateType.Default;

        }
    }
}
