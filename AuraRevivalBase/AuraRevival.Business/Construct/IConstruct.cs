using System;

namespace AuraRevival.Business.Construct
{
    /// <summary>
    /// 建筑
    /// </summary>
    interface IConstruct
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        ConstructType Type { get; set; }

        //int Level { get => Level; set => Level = Level; }

        //int Xxxxxxxx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //IConstruct New();

        /// <summary>
        /// 执行秒事件
        /// </summary>
        /// <param name="time"></param>
        void ConstructSecondsEventExecute(DateTime time);

        /// <summary>
        /// 执行脚本事件
        /// </summary>
        /// <param name="time"></param>
        //bool ScriptEvent(int scriptCode, object obj) => throw new NotImplementedException();

        //public void Xxx() => throw new NotImplementedException();
    }
}


//void ShowMsg(DateTime time)
//{
//    listView1.Invoke(new Action(() =>
//    {
//        if (listView1.Items.Count > 100)
//        {
//            listView1.Items.RemoveAt(0);
//        }
//        listView1.Items.Add($"{DateTime.Now.ToString("HH:mm:ss")} - [ShowMsg]：嘀嘀嘀，已经 {time.ToString("yyyy-MM-dd HH:mm:ss")} 了");

//        listView1.Focus(); //聚焦光标
//                           //listView1.Items[listView1.Items.Count - 1].Selected = true; //选中最后一行
//        listView1.Items[listView1.Items.Count - 1].EnsureVisible(); ;//显示内容自动滚动到最后一行
//    }));
//}