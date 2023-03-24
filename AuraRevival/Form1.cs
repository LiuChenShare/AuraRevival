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
        //        MessageBox.Show("请输入内容","提示", MessageBoxButtons.OK);
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
            Grain.Instance.MainGame.Init("基地");

            Form3 form = new();

        }

        /// <summary>
        /// 展示消息
        /// </summary>
        /// <param name="type">来源类型:0-游戏公告，1-区块，2-建筑，3-实体</param>
        /// <param name="source">来源名称</param>
        /// <param name="content">消息内容</param>
        private void ShowMsg(int type, string source, string content)
        {
            LoadMsg.Invoke(new Action(() =>
            {
                if (type != 0) return;

                if (content == "进入游戏")
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