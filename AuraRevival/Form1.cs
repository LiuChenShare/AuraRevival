using AuraRevival.Business;

namespace AuraRevival
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("请输入内容","提示", MessageBoxButtons.OK);
            }
            else
            {
                this.Hide();
                Grain.Instance.MainGame.Init("textBox1.Text");
                Form3 form = new();
                form.ShowDialog();
                Close();
            }
        }
    }
}