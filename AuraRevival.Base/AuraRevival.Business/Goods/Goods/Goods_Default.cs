using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AuraRevival.Business.Goods.Goods
{
    /// <summary>
    /// 普通物品
    /// </summary>
    public class Goods_Default : IGoods
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public GoodsType Type { get; private set; }
        public string Name { get; private set; }
        public int Count { get; set; }


        public void Init(int code, string name, int count = 1)
        {
            Id = Guid.NewGuid();
            Code = code;
            Type = GoodsType.Goods;
            Name = name;
            Count = count > 0 ? count : 1;
        }

        public void XXX()
        {
            IGoods _mtype = (IGoods)Assembly.Load("AuraRevival.Business").CreateInstance("Goods_Base");
            //IGoods _mtype2 = (IGoods)Assembly.Load("AuraRevival.Business").CreateInstance("Goods_Base",true, BindingFlags.Default, null, new object[] { null}, null,);
        }
    }
}
