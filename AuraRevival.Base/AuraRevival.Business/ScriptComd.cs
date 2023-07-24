using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AuraRevival.Business
{
    /// <summary>
    /// 脚本指令
    /// </summary>
    public enum ScriptComd
    {


        /// <summary>
        /// 建筑_升级
        /// </summary>
        [Description("升级")]
        Construct_UpLevel = 200000100,

        #region 实体
        /// <summary>
        /// 实体_移动
        /// </summary>
        [Description("移动")]
        Entity_Move = 300000100,

        /// <summary>
        /// 实体_进入战斗状态
        /// </summary>
        [Description("进入战斗状态")]
        Entity_Battle = 300000101,
        /// <summary>
        /// 实体_退出战斗状态
        /// </summary>
        [Description("进入战斗状态")]
        Entity_BattleOut = 300000102,

        /// <summary>
        /// 实体_进入自动移动状态
        /// </summary>
        [Description("进入自动移动状态")]
        Entity_AutoMove = 300000103,
        /// <summary>
        /// 实体_退出自动移动状态
        /// </summary>
        [Description("进入自动移动状态")]
        Entity_AutoMoveOut = 300000104,
        /// <summary>
        /// 实体_停止移动状态
        /// </summary>
        [Description("停止移动状态")]
        Entity_MoveFeature_Stop = 300000105,
        /// <summary>
        /// 实体_进入默认移动状态
        /// </summary>
        [Description("进入默认移动状态")]
        Entity_MoveFeature_Default = 300000106,

        #endregion

    }
}
