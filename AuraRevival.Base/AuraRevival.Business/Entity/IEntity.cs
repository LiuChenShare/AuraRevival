using AuraRevival.Business.Construct;
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
        
        /// <summary>
        /// 特征
        /// </summary>
        List<EntityCharacterType> Characters { get; }

        /// <summary> 当前指令 </summary>
        int _scriptCode { get => _scriptCode; set => _scriptCode = value; }


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
        
        #region 方法
        /// <summary>
        /// 增加经验
        /// </summary>
        /// <param name="exp"></param>
        void SetExp(int exp);
        #endregion

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
        /// <summary>
        /// 生命值（最大值）
        /// </summary>
        int HPMax { get => HPMax; set => HPMax = value; }

        #region 方法
        /// <summary>
        /// 补充血量
        /// </summary>
        /// <param name="hurt"></param>
        void RestockHP(int hurt);
        #endregion

        #endregion


        #region 地图移动相关

        /// <summary> 地图行动值 </summary>
        int _tallyMap { get => _tallyMap; set => _tallyMap = value; }
        /// <summary> 行动值模板 </summary>
        int _tallyMapTep { get => _tallyMapTep; set => _tallyMapTep = value; }
        /// <summary> 移动特征 </summary>
        MoveFeature MoveFeature { get => MoveFeature; set => MoveFeature = value; }
        /// <summary> 目的地地区块id </summary>
        Point? DestLocation { get => DestLocation; set => DestLocation = value; }
        #endregion

        #region 程序集
        /// <summary> 程序集 </summary>
        string AssemblyString { get => AssemblyString; set => AssemblyString = value; }
        /// <summary> 类型名称 </summary>
        string TypeName { get => TypeName; set => TypeName = value; }
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


        /// <summary>
        /// 恢复（重新启动）
        /// </summary>
        /// <param name="entity"></param>
        void Resume(IEntity entity);
    }
}
