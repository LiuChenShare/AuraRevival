using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AuraRevival.Business.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IEntity
    {
        Guid Id { get => Id; set => Id = value; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get => Name; set => Name = value; }

        /// <summary>
        /// 说明
        /// </summary>
        string Description { get => Description; set => Description = value; }

        /// <summary>
        /// 等级
        /// </summary>
        int Level { get => Level; set => Level = value; }

        /// <summary>
        /// 类型
        /// </summary>
        int Type{get=> Type; set => Type = value; }

        /// <summary>
        /// 区块id
        /// </summary>
        Point Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }




        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="location">位置</param>
        void Init(string name, Point location);
        /// <summary>
        /// 执行脚本事件
        /// </summary>
        /// <param name="time"></param>
        bool ScriptEvent(int scriptCode, object obj) => false;
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
    }
}
