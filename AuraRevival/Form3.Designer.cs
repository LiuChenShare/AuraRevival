
namespace AuraRevival
{
    partial class Form3
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_Top = new System.Windows.Forms.Panel();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.listView2 = new System.Windows.Forms.ListView();
            this.panel_Fill = new System.Windows.Forms.Panel();
            this.panel_MapView = new System.Windows.Forms.Panel();
            this.panel_Map = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_Right = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel_Left.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel_Fill.SuspendLayout();
            this.panel_MapView.SuspendLayout();
            this.panel_Right.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(722, 44);
            this.panel_Top.TabIndex = 0;
            // 
            // panel_Left
            // 
            this.panel_Left.Controls.Add(this.flowLayoutPanel1);
            this.panel_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Left.Location = new System.Drawing.Point(0, 44);
            this.panel_Left.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(217, 418);
            this.panel_Left.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.listView2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(217, 305);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // listView2
            // 
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.Name = "listView2";
            this.listView2.Scrollable = false;
            this.listView2.ShowItemToolTips = true;
            this.listView2.Size = new System.Drawing.Size(207, 271);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Tile;
            // 
            // panel_Fill
            // 
            this.panel_Fill.Controls.Add(this.panel_MapView);
            this.panel_Fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Fill.Location = new System.Drawing.Point(217, 44);
            this.panel_Fill.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Fill.Name = "panel_Fill";
            this.panel_Fill.Size = new System.Drawing.Size(330, 418);
            this.panel_Fill.TabIndex = 2;
            // 
            // panel_MapView
            // 
            this.panel_MapView.BackColor = System.Drawing.SystemColors.Control;
            this.panel_MapView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MapView.Controls.Add(this.panel_Map);
            this.panel_MapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MapView.Location = new System.Drawing.Point(0, 0);
            this.panel_MapView.Margin = new System.Windows.Forms.Padding(0);
            this.panel_MapView.Name = "panel_MapView";
            this.panel_MapView.Size = new System.Drawing.Size(330, 418);
            this.panel_MapView.TabIndex = 1;
            // 
            // panel_Map
            // 
            this.panel_Map.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel_Map.Location = new System.Drawing.Point(-1100, -1100);
            this.panel_Map.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Map.Name = "panel_Map";
            this.panel_Map.Size = new System.Drawing.Size(2500, 2500);
            this.panel_Map.TabIndex = 0;
            this.panel_Map.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel_Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Map_MouseDown);
            this.panel_Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Map_MouseUp);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(15, 212);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(88, 33);
            this.button8.TabIndex = 9;
            this.button8.Text = "回到基地";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(40, 158);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(40, 33);
            this.button7.TabIndex = 8;
            this.button7.Text = "↓";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(40, 76);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(40, 33);
            this.button6.TabIndex = 7;
            this.button6.Text = "↑";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(63, 117);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 33);
            this.button5.TabIndex = 6;
            this.button5.Text = "→";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 117);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 33);
            this.button1.TabIndex = 5;
            this.button1.Text = "←";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel_Right
            // 
            this.panel_Right.Controls.Add(this.label1);
            this.panel_Right.Controls.Add(this.button8);
            this.panel_Right.Controls.Add(this.button6);
            this.panel_Right.Controls.Add(this.button7);
            this.panel_Right.Controls.Add(this.button1);
            this.panel_Right.Controls.Add(this.button5);
            this.panel_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Right.Location = new System.Drawing.Point(547, 44);
            this.panel_Right.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(175, 418);
            this.panel_Right.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "yyyy-MM-dd HH:mm:ss";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Controls.Add(this.listView1);
            this.panel_Bottom.Controls.Add(this.textBox1);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 462);
            this.panel_Bottom.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(722, 200);
            this.panel_Bottom.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(190, 10);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(874, 171);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(38, 10);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 30);
            this.textBox1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "属性";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "信息";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 662);
            this.Controls.Add(this.panel_Fill);
            this.Controls.Add(this.panel_Right);
            this.Controls.Add(this.panel_Left);
            this.Controls.Add(this.panel_Top);
            this.Controls.Add(this.panel_Bottom);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.panel_Left.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel_Fill.ResumeLayout(false);
            this.panel_MapView.ResumeLayout(false);
            this.panel_Right.ResumeLayout(false);
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.Panel panel_Fill;
        private System.Windows.Forms.Panel panel_Right;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Panel panel_Map;
        private System.Windows.Forms.Panel panel_MapView;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox1;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel1;
        private ListView listView2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}

