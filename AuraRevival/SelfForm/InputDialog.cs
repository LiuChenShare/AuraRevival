using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRevival.SelfForm
{
    public static class InputDialog
    {
        public static DialogResult Show(out string strText, string tips = "请输入")
        {
            string strTemp = string.Empty;

            FrmInputDialog inputDialog = new(tips)
            {
                TextHandler = (str) => { strTemp = str; }
            };

            DialogResult result = inputDialog.ShowDialog();
            strText = strTemp;

            return result;
        }
    }
}
