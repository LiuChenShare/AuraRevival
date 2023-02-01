using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AuraRevival.Business.Goods
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum GoodsType
    {
        /// <summary>
        /// 物品
        /// </summary>
        [Description("物品")]
        Goods = 0,
        /// <summary>
        /// 装备
        /// </summary>
        [Description("装备")]
        Equipment = 1,
    }
}
