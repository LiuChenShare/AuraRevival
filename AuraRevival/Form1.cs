using AuraRevival.Business;
using static System.Net.Mime.MediaTypeNames;

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

        //private void btnOK_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(textBox1.Text))
        //    {
        //        MessageBox.Show("����������","��ʾ", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        this.Hide();
        //        Grain.Instance.MainGame.Init(textBox1.Text);
        //        Form3 form = new();
        //        form.ShowDialog();
        //        Close();
        //    }
        //}

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            LoadBtn.Enabled = false;

            MainGame.Instance.MsgEvent += ShowMsg;
            Grain.Instance.MainGame.Init("����");

            Form3 form = new();

        }

        /// <summary>
        /// չʾ��Ϣ
        /// </summary>
        /// <param name="type">��Դ����:0-��Ϸ���棬1-���飬2-������3-ʵ��</param>
        /// <param name="source">��Դ����</param>
        /// <param name="content">��Ϣ����</param>
        private void ShowMsg(int type, string source, string content)
        {
            LoadMsg.Invoke(new Action(() =>
            {
                if (type != 0) return;

                if (content == "������Ϸ")
                {

                    this.Hide();
                    Form3 form = new();
                    form.ShowDialog();
                    Close();
                }

                LoadMsg.Text = content;

            }));
        }
    }
}