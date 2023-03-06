using AuraRevival.Business.Goods;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AuraRevival.Business.Construct
{
    /// <summary>
    /// 建筑
    /// </summary>
    public interface IConstruct
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        ConstructType Type { get; set; }

        /// <summary>
        /// 区块id
        /// </summary>
        Point Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// 等级
        /// </summary>
        int Level { get => Level; set => Level = value; }


        /// <summary> 行动值 </summary>
        int _tallyMap { get => _tallyMap; set => _tallyMap = value; }
        /// <summary> 行动值模板 </summary>
        int _tallyMapTep { get => _tallyMapTep; set => _tallyMapTep = value; }
        /// <summary> 当前指令 </summary>
        int _scriptCode { get => _scriptCode; set => _scriptCode = value; }

        /// <summary> 程序集 </summary>
        string AssemblyString { get => AssemblyString; set => AssemblyString = value; }
        /// <summary> 类型名称 </summary>
        string TypeName { get => TypeName; set => TypeName = value; }

        /// <summary>
        /// 仓库
        /// </summary>
        List<IGoods> Goods { get => Goods; }

        //int Xxxxxxxx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //IConstruct New();

        /// <summary>
        /// 执行秒事件
        /// </summary>
        /// <param name="time"></param>
        void SecondsEventExecute(DateTime time) { }
        /// <summary>
        /// 执行分事件
        /// </summary>
        /// <param name="time"></param>
        void MinutesEventExecute(DateTime time) { }

        /// <summary>
        /// 执行脚本事件
        /// </summary>
        /// <param name="time"></param>
        bool ScriptEvent(int scriptCode, object obj) => throw new NotImplementedException();

        /// <summary>
        /// 保存物品
        /// </summary>
        /// <param name="goodslist"></param>
        /// <returns></returns>
        bool AddGoods(List<IGoods> goodslist) => false;
        /// <summary>
        /// 移除物品
        /// </summary>
        /// <param name="goodslist"></param>
        /// <returns></returns>
        bool RemoveGoods(List<IGoods> goodslist) => false;
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