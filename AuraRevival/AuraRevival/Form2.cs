using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuraRevival
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// 上次点击的坐标
        /// </summary>
        public Coor CoorOld { get; set; } = new Coor(0, 0);

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        { 
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 页面加载结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Shown(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            Graphics g = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Green);
            Coor coor = new Coor(1, 1);


            //循环绘制多条横线
            for (int i = 0; i <= panel1.Height / padding; i++)
            {

                //画线的方法，第一个参数为起始点X的坐标，第二个参数为起始

                //点Y的坐标；第三个参数为终点X的坐标，第四个参数为终

                //点Y的坐标；

                g.DrawLine(pen, 0, padding * i, panel1.Width, padding * i);

            }
            //循环绘制多条竖线
            for (int i = 0; i <= this.Width / padding; i++)
            {
                g.DrawLine(pen, padding * i, 0, padding * i, panel1.Height);
            }

            //在地图(1,1)的位置放个图片
            g.DrawImage(Image.FromFile(@".\Image\雪景.png"), coor.Rectangle.X, coor.Rectangle.Y, coor.Rectangle.Width, coor.Rectangle.Height);

            g.Dispose();

        }

        /// <summary>
        /// 当鼠标指针位于控件上并按下鼠标键时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            int padding = Util.Padding;
            Coor coor = new Coor(e.X/ padding, e.Y / padding);

            #region 上次点击的位置标绿
            Graphics g2 = panel1.CreateGraphics();
            Pen pen2 = new Pen(Color.Green);

            //废弃上面的方法，直接画矩形
            g2.DrawRectangle(pen2, CoorOld.Rectangle);
            g2.Dispose();
            #endregion

            #region 点击的位置标红
            Graphics g = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Red);

            ////两横
            //g.DrawLine(pen, coor.Rectangle.Location, new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y));
            //g.DrawLine(pen, new Point(coor.Rectangle.Location.X, coor.Rectangle.Location.Y + 1), new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y + 1));
            ////两竖
            //g.DrawLine(pen, coor.Rectangle.Location, new Point(coor.Rectangle.Location.X, coor.Rectangle.Location.Y + 1));
            //g.DrawLine(pen, new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y), new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y + 1));

            //废弃上面的方法，直接画矩形
            g.DrawRectangle(pen, coor.Rectangle); 
            g.Dispose();
            #endregion

            CoorOld = coor;
        }
    }
}
