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
        /// 实体_移动
        /// </summary>
        [Description("移动")]
        Entity_Move = 200,

        /// <summary>
        /// 实体_进入战斗状态
        /// </summary>
        [Description("进入战斗状态")]
        Entity_Battle = 201,
    }
}
