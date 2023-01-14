using AuraRevival.Business.Construct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AuraRevival.Business
{
    /// <summary>
    /// 块
    /// </summary>
    public class Block
    {
        /// <summary>
        /// 区块id
        /// </summary>
        //public Tuple<int, int> Id { get;  set; }
        //public (int,int) Id { get; set; }
        public Point Id { get; set; }


        public Block(Point id)
        {
            Id = id;
        }
        public Block(int x, int y)
        {
            Id = new Point(x, y);
            //Id = Tuple.Create(x, y);
        }

        /// <summary>
        /// 建筑
        /// </summary>
        public List<IConstruct> Constructs = new List<IConstruct>();

    }
}
