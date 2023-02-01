using AuraRevival.Core;
using System;
using System.ComponentModel;

namespace AuraRevival.Business.Goods
{
    /// <summary>
    /// 物品
    /// </summary>
    public interface IGoods
    {
        /// <summary>
        /// Id
        /// </summary>
        Guid Id { get => Id; set => Id = value; }
        /// <summary>
        /// 编码
        /// </summary>
        int Code { get => Code; set => Code = value; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get => Name; set => Name = value; }

        /// <summary>
        /// 等级
        /// </summary>
        int Level { get => Level; set => Level = value; }

        /// <summary>
        /// 类型
        /// </summary>
        GoodsType Type { get => Type; set => Type = value; }

        /// <summary>
        /// 数量
        /// </summary>
        int Count { get => 1; set => Count = Count; }

        /// <summary>
        /// 耐久
        /// </summary>
        int Durable { get => -1; set => Durable = Durable; }
        /// <summary>
        /// 耐久最大值
        /// </summary>
        int DurableMax { get => -1; set => Durable = Durable; }

        string GetDescription()
        {
            return Type.GetDescription();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="count">数量</param>
        void Init(int code, string name, int count = 1);
    }


}
