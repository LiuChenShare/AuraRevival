using AuraRevival.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        /// <summary>
        /// 页面加载结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Shown(object sender, EventArgs e)
        {
            panel_Map.Size = new Size(Grain.Instance.MainGame.MapSize.Item1 * 25 + 1, Grain.Instance.MainGame.MapSize.Item2 * 25 + 1);
            GoHome();

            listView1.LabelWrap = true;
            listView1.Columns.Add(new ColumnHeader()
            {
                Name = "ColumnHeader1",
                Text = "消息",
                Width = listView1.Width,
            });
            //listView1.Scrollable
            //FormRefresh();
            MainGame.Instance.GameStart();
            MainGame.Instance.SecondsEvent += ShowMsg;
        }


        /// <summary>
        /// 当鼠标指针位于控件上并按下鼠标键时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_Map_MouseDown(object sender, MouseEventArgs e)
        {
            int padding = Util.Padding;
            Coor coor = new Coor(e.X / padding, e.Y / padding);

            #region 上次点击的位置标绿
            Graphics g2 = panel_Map.CreateGraphics();
            Pen pen2 = new Pen(Color.Green);

            //废弃上面的方法，直接画矩形
            g2.DrawRectangle(pen2, CoorOld.Rectangle);
            g2.Dispose();
            #endregion

            #region 点击的位置标红
            Graphics g = panel_Map.CreateGraphics();
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
            panel_Map.Location = new Point(panel_Map.Location.X + padding, panel_Map.Location.Y);
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
            panel_Map.Location = new Point(panel_Map.Location.X - padding, panel_Map.Location.Y);
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
            panel_Map.Location = new Point(panel_Map.Location.X, panel_Map.Location.Y + padding);
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
            panel_Map.Location = new Point(panel_Map.Location.X, panel_Map.Location.Y - padding);
            ButtonRefresh();
        }

        /// <summary>
        /// 归零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            panel_Map.Location = new Point(0, 0);
            ButtonRefresh();
        }

        /// <summary>
        /// 地图刷新
        /// </summary>
        private void FormRefresh()
        {
            Graphics g = panel_Map.CreateGraphics();
            Pen pen = new Pen(Color.Green);
            Pen penRed = new Pen(Color.Red);
            Coor coor111111 = new Coor(1, 1);

            //框定展示的范围
            Rectangle rec = new Rectangle(-panel_Map.Location.X, -panel_Map.Location.Y, panel_MapView.Width, panel_MapView.Height);
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

            ButtonRefresh();
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

            if (panel_Map.Location.X > (-padding))
            {
                button1.Enabled = false;
            }
            if (panel_Map.Location.X < (panel_MapView.Width - panel_Map.Width - padding))
            {
                button5.Enabled = false;
            }
            if (panel_Map.Location.Y > (-padding))
            {
                button6.Enabled = false;
            }
            if (panel_Map.Location.Y < (panel_MapView.Height - panel_Map.Height - padding))
            {
                button7.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormRefresh();
        }

        private void panel_Map_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                //contextMenuStrip1.Show(this, e.Location);
                //contextMenuStrip1.Show(this,5,5);
                contextMenuStrip1.Show((Control)sender, e.Location);
            }
        }

        private void ShowMsg(DateTime time)
        {
            listView1.Invoke(new Action(() =>
            {
                if (listView1.Items.Count > 100)
                {
                    listView1.Items.RemoveAt(0);
                }
                listView1.Items.Add($"{DateTime.Now.ToString("HH:mm:ss")} - [ShowMsg]：嘀嘀嘀，已经 {time.ToString("yyyy-MM-dd HH:mm:ss")} 了");

                listView1.Focus(); //聚焦光标
                //listView1.Items[listView1.Items.Count - 1].Selected = true; //选中最后一行
                listView1.Items[listView1.Items.Count - 1].EnsureVisible(); ;//显示内容自动滚动到最后一行
            }));
        }

        /// <summary>
        /// 地图跳转至基地位置
        /// </summary>
        private void GoHome()
        {
            AuraRevival.Business.Construct.Construct_Base construct = Grain.Instance.GetConstructBase();
            //获得基地在界面的坐标
            Coor constructCoor = new(construct.Location);
            //计算界面能展示多少格子
            int viewXMax = panel_MapView.Width / Util.Padding;
            int viewYMax = panel_MapView.Height / Util.Padding;
            //获得地图的偏移坐标
            int x = construct.Location.X - viewXMax / 2;
            int y = construct.Location.Y - viewYMax / 2;
            //获得应该放在界面左上角的格子信息
            Coor zeroCoor = new(x, y);
            //移动地图，使基地在中间
            panel_Map.Location = new Point(-zeroCoor.Rectangle.X, -zeroCoor.Rectangle.Y);
            //基地绘图
            Graphics g = panel_Map.CreateGraphics();
            g.DrawImage(Image.FromFile(Util.房子_蓝), constructCoor.Rectangle.X, constructCoor.Rectangle.Y, constructCoor.Rectangle.Width, constructCoor.Rectangle.Height);
            g.Dispose();
        }
    }
}
