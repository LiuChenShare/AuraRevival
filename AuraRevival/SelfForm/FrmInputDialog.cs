using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AuraRevival.SelfForm
{
    public partial class FrmInputDialog : Form
    {
        public delegate void TextEventHandler(string strText);

        public TextEventHandler TextHandler;
        public FrmInputDialog(string tips = "请输入")
        {
            InitializeComponent();
            label1.Text = tips;
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (null != TextHandler)
            {
                TextHandler.Invoke(textBox1.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Keys.Enter == (Keys)e.KeyChar)
            {
                if (null != TextHandler)
                {
                    TextHandler.Invoke(textBox1.Text);
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
    
}
