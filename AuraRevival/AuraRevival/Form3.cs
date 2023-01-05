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
    public partial class Form3 : Form
    {
        /// <summary>
        /// 上次点击的坐标
        /// </summary>
        public Coor CoorOld { get; set; } = new Coor(0, 0);

        public Form3()
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
            //FormRefresh();
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

        /// <summary>
        /// 向左
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel1.Location = new Point(panel1.Location.X + padding, panel1.Location.Y);
            ButtonRefresh();
        }
        /// <summary>
        /// 向右
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel1.Location = new Point(panel1.Location.X - padding, panel1.Location.Y);
            ButtonRefresh();
        }
        /// <summary>
        /// 向上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel1.Location = new Point(panel1.Location.X, panel1.Location.Y + padding);
            ButtonRefresh();
        }
        /// <summary>
        /// 向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel1.Location = new Point(panel1.Location.X, panel1.Location.Y - padding);
            ButtonRefresh();
        }
        /// <summary>
        /// 归零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, 0);
            ButtonRefresh();
        }

        /// <summary>
        /// 地图刷新
        /// </summary>
        private void FormRefresh()
        {
            Graphics g = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Green);
            Pen penRed = new Pen(Color.Red);
            Coor coor111111 = new Coor(1, 1);

            //框定展示的范围
            Rectangle rec = new Rectangle(-panel1.Location.X, -panel1.Location.Y, panel2.Width, panel2.Height);
            int maxHeight = rec.Y + rec.Height;
            int maxWidth = rec.X + rec.Width;

            //循环绘制多条横线
            for (int i = rec.Y; i <= maxHeight; i += 25)
            {
                g.DrawLine(pen, rec.X, i, maxWidth, i);
            }
            //循环绘制多条竖线
            for (int i = rec.X; i <= maxWidth; i += 25)
            {
                g.DrawLine(pen, i, rec.Y, i, maxHeight);
            }

            //在地图(1,1)的位置放个图片
            g.DrawImage(Image.FromFile(@".\Image\雪景.png"), coor111111.Rectangle.X, coor111111.Rectangle.Y, coor111111.Rectangle.Width, coor111111.Rectangle.Height);

            //当前选中的地方画矩形
            g.DrawRectangle(penRed, CoorOld.Rectangle);
            
            g.Dispose();
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        private void ButtonRefresh()
        {
            int padding = Util.Padding;
            button1.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

            if (panel1.Location.X > (-padding))
            {
                button1.Enabled = false;
            }
            if (panel1.Location.X < (panel2.Width - panel1.Width - padding))
            {
                button5.Enabled = false;
            }
            if (panel1.Location.Y > (-padding))
            {
                button6.Enabled = false;
            }
            if (panel1.Location.Y < (panel2.Height - panel1.Height - padding))
            {
                button7.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormRefresh();
        }

    }
}
