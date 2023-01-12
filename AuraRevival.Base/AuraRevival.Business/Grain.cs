using AuraRevival.Business;
using AuraRevival.Business.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRevival.Business
{
    public class Grain
    {

        #region 单例
        private static volatile Grain instance;
        private static object syncRoot = new Object();
        private Grain() { }
        public static Grain Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Grain();
                    }
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 世界
        /// </summary>
        public MainGame MainGame { get; set; } = MainGame.Instance;

        /// <summary>
        /// 建筑
        /// </summary>
        public List<IConstruct> Construct { get; set; } = new List<IConstruct>();
    }
}
