using System;
using System.Collections.Generic;
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
        public Tuple<int, int> Id { get; private set; }
        //public (int,int) Id { get;private set; }

         


        public Block(Tuple<int, int> id)
        {
            Id = id;
        }
        public Block(int x, int y)
        {
            Id = new Tuple<int, int>(x, y);
            //Id = Tuple.Create(x, y);
        }


    }
}
