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



    }
}
