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
        /// 类型
        /// </summary>
        int Type { get => Type; set => Type = value; }

        /// <summary>
        /// 区块id
        /// </summary>
        Point Location { get => Location; set => Location = value; }

        /// <summary>
        /// 君主id
        /// </summary>
        Guid MId { get => MId; set => MId = value; }

        /// <summary>
        /// 状态
        /// </summary>
        EntityStateType State { get => State; set => State = value; }

        #region 等级相关

        /// <summary>
        /// 等级
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 经验值
        /// </summary>
        int Exp { get; }
        /// <summary>
        /// 经验值（上限）
        /// </summary>
        int ExpMax { get; }

        /// <summary>
        /// 增加经验
        /// </summary>
        /// <param name="exp"></param>
        void SetExp(int exp);
        #endregion


        #region 战斗相关
        /// <summary>
        /// 力量
        /// </summary>
        int Power { get => Power; set => Power = value; }
        /// <summary>
        /// 敏捷
        /// </summary>
        int Agile { get => Agile; set => Agile = value; }
        /// <summary>
        /// 生命值
        /// </summary>
        int HP { get => HP; set => HP = value; }
        void SetHP(int hp);
        /// <summary>
        /// 生命值（最大值）
        /// </summary>
        int HPMax { get => HPMax; set => HPMax = value; }
        #endregion


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="mid">君主id</param>
        /// <param name="location">位置</param>
        void Init(string name, int type, Guid mid, Point location);
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
