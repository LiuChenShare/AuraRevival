using AuraRevival.Business.Construct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AuraRevival.Business.Entity
{
    /// <summary>
    /// 实体（主角）
    /// </summary>
    public class Entity_Leader : Entity_Default
    {


        public override void Init(string name, int type, Guid mid, Point location)
        {
            base.Init(name, type, mid, location);
            
            Power = 10;
            Agile = 10;
            HP = HPMax;

            Type = 0;
        }

        protected override void LevelUp()
        {
            _tallyMapTep++;
            _tallyMap = _tallyMapTep;

            ExpMax = (int)(ExpMax * 1.5);

            Power += 2;
            Agile += 2;
            HP = HPMax;

        }

        #region 指令

        #endregion

    }
}
