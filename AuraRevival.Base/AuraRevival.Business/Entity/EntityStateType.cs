using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AuraRevival.Business.Entity
{
    /// <summary>
    /// 实体状态
    /// </summary>
    public enum EntityStateType
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
        /// 死亡
        /// </summary>
        [Description("死亡")]
        Die = 2,
    }
}
