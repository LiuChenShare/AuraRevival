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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int padding = 25;
            Graphics g = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Green);

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
        }
    }
}
