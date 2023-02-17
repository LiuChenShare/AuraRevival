using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AuraRevival.Business.Battle
{
    /// <summary>
    /// 战斗状态
    /// </summary>
    public enum BattleStateType
    {
        /// <summary>
        /// 空闲
        /// </summary>
        [Description("空闲")]
        Default = 0,
        /// <summary>
        /// 战斗中
        /// </summary>
        [Description("战斗中")]
        InBattle = 1,

        /// <summary>
        /// 战斗结束
        /// </summary>
        [Description("战斗结束")]
        BattleOver = 2,

        /// <summary>
        /// 已结算
        /// </summary>
        [Description("已结算")]
        Settled = 3,
    }
}
