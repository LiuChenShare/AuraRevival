using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRevival
{
    /// <summary>
    /// 坐标
    /// </summary>
    public class Coor
    {
        /// <summary>
        /// 位于地图的坐标
        /// </summary>
        public Point CoorPoint { get; set; }
        /// <summary>
        /// 所在矩形
        /// </summary>
        public Rectangle Rectangle { get; set; }

        public Coor(int x, int y)
        {
            int padding = Util.Padding; 
            CoorPoint = new Point(x, y);
            Rectangle = new Rectangle(CoorPoint.X * padding, CoorPoint.Y * padding, padding, padding);
        }
        public Coor(Point point)
        {
            int padding = Util.Padding;
            CoorPoint = point;
            Rectangle = new Rectangle(CoorPoint.X * padding, CoorPoint.Y * padding, padding, padding);
        }
    }
}
