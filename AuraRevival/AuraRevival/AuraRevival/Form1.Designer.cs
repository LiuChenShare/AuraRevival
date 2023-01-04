
namespace AuraRevival
{
    partial class Form1
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
            this.panel_Top = new System.Windows.Forms.Panel();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.panel_Fill = new System.Windows.Forms.Panel();
            this.panel_Right = new System.Windows.Forms.Panel();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_Fill.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(925, 31);
            this.panel_Top.TabIndex = 0;
            // 
            // panel_Left
            // 
            this.panel_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Left.Location = new System.Drawing.Point(0, 31);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(58, 670);
            this.panel_Left.TabIndex = 1;
            // 
            // panel_Fill
            // 
            this.panel_Fill.Controls.Add(this.button1);
            this.panel_Fill.Controls.Add(this.panel1);
            this.panel_Fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Fill.Location = new System.Drawing.Point(58, 31);
            this.panel_Fill.Name = "panel_Fill";
            this.panel_Fill.Size = new System.Drawing.Size(792, 670);
            this.panel_Fill.TabIndex = 2;
            // 
            // panel_Right
            // 
            this.panel_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Right.Location = new System.Drawing.Point(850, 31);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(75, 670);
            this.panel_Right.TabIndex = 0;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 701);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(925, 31);
            this.panel_Bottom.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(47, 37);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 603);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(671, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 732);
            this.Controls.Add(this.panel_Fill);
            this.Controls.Add(this.panel_Right);
            this.Controls.Add(this.panel_Left);
            this.Controls.Add(this.panel_Top);
            this.Controls.Add(this.panel_Bottom);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel_Fill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.Panel panel_Fill;
        private System.Windows.Forms.Panel panel_Right;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}

