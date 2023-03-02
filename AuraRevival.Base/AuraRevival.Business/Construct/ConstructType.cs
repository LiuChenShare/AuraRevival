using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AuraRevival.Business.Construct
{
    /// <summary>
    /// 建筑类型
    /// </summary>
    public enum ConstructType
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Default = 0,
        /// <summary>
        /// 基地
        /// </summary>
        [Description("基地")]
        Base = 1,
        /// <summary>
        /// 军营
        /// </summary>
        [Description("军营")]
        Camp = 2,

    }
}
