using AuraRevival.Business.Construct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AuraRevival.Business.Entity
{
    /// <summary>
    /// 实体（默认）
    /// </summary>
    public class Entity_Default : IEntity
    {
        #region 公用
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Point Location { get; protected set; }
        public string Description { get; protected set; }
        public int Type { get; protected set; }
        public Guid MId { get; protected set; }
        public EntityStateType State { get; protected set; } = EntityStateType.Default;
        public List<EntityCharacterType> Characters { get; protected set; } = new List<EntityCharacterType>();

        /// <summary> 地图行动值 </summary>
        public int _tallyMap { get; protected set; }
        /// <summary> 行动值模板 </summary>
        public int _tallyMapTep { get; protected set; }
        /// <summary> 当前指令 </summary>
        public int _scriptCode { get; protected set; } = -1;


        #region 等级相关

        public int Level { get; protected set; }

        public int Exp { get; protected set; }
        /// <summary>
        /// 经验值（上限）
        /// </summary>
        public int ExpMax { get; protected set; }
        #endregion


        #region 战斗相关
        /// <summary>
        /// 力量
        /// </summary>
        public int Power { get; protected set; }
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get; protected set; }
        /// <summary>
        /// 生命值
        /// </summary>
        public int HP { get; protected set; }
        /// <summary>
        /// 生命值（最大值）
        /// </summary>
        public virtual int HPMax
        {
            get
            {
                var a = 1.55;
                return (int)(Power * a);
            }
            protected set { HPMax = value; }
        }
        #endregion


        public string AssemblyString { get; protected set; }
        public string TypeName { get; protected set; }
        #endregion




        #region 自定义

        #endregion


        public virtual void Init(string name, int type, Guid mid, Point location)
        {
            AssemblyString = GetType().Module.Name;
            TypeName = GetType().FullName;

            Id = Guid.NewGuid();
            Name = string.IsNullOrWhiteSpace(name) ? EntityHelper.GetRandomName() : name;
            Type = type;
            Location = location;
            Level = 1;
            MId = mid;
            ExpMax = 100;
            Exp = 0;
            _tallyMapTep = 5;
            _tallyMap = 0;

           
                Random rnd = new Random((int)DateTime.Now.ToFileTimeUtc());
                Power = rnd.Next(1, 11);
                Agile = rnd.Next(1, 11);
                HP = HPMax;
            


            //注册秒事件
            Grain.Instance.MainGame.SecondsEvent += SecondsEventExecute;
            Grain.Instance.MainGame.MinutesEvent += MinutesEventExecute;


            Block block = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == Location);
            block?.AddEntities(this);
            Grain.Instance.Entities.Add(this);


            MainGame.Instance.EntityMove(this, null, block?.Id);
        }
        public virtual bool ScriptEvent(int scriptCode, object obj)
        {
            _scriptCode = _scriptCode != -1 ? _scriptCode : scriptCode;
            switch (scriptCode)
            {
                case (int)ScriptComd.Entity_Move: //移动
                    {
                        _scriptCode = -1;
                        bool result = _script_2_00(obj);
                        return result;
                    }
                case (int)ScriptComd.Entity_Battle: //进入战斗
                    {
                        _scriptCode = -1;
                        bool result = _script_2_01(obj);
                        return result;
                    }
                case (int)ScriptComd.Entity_BattleOut: //退出战斗
                    {
                        _scriptCode = -1;
                        bool result = _script_2_02(obj);
                        return result;
                    }
                case (int)ScriptComd.Entity_AutoMove: //自动移动
                    {
                        _scriptCode = -1;
                        bool result = _script_2_03(obj);
                        return result;
                    }
                case (int)ScriptComd.Entity_AutoMoveOut: //退出自动移动
                    {
                        _scriptCode = -1;
                        bool result = _script_2_04(obj);
                        return result;
                    }
                default:
                    break;
            }
            _scriptCode = -1;
            return false;
        }

        public virtual async Task SecondsEventExecute(DateTime time)
        {
            if (State == EntityStateType.Die)
                return;

            if (Characters.Contains(EntityCharacterType.AutoMove))
            {
                if (_tallyMap >= 1)
                {
                    string[] move = new string[] { "W", "A", "S", "D" };
                    Random ran = new Random((int)DateTime.Now.ToFileTimeUtc());
                    var inde = ran.Next(0, 4);
                    _script_2_00(move[inde]);
                }
            }

        }

        public virtual async Task MinutesEventExecute(DateTime time)
        {
            if (_tallyMap < _tallyMapTep)
                _tallyMap++;


            //回血
            if (State == EntityStateType.Default && HP < HPMax)
            {
                HP++;

                _ = Grain.Instance.MainGame.Msg(3, $"{Name}", $"回血！当前HP：{HP}");
            }
        }


        protected virtual void LevelUp()
        {
            _tallyMapTep++;
            _tallyMap = _tallyMapTep;

            ExpMax = (int)(ExpMax * 1.5);

            Random rnd = new Random((int)DateTime.Now.ToFileTimeUtc());
            Power += rnd.Next(0, 3);
            Agile += rnd.Next(0, 3);
            HP = HPMax;

        }

        #region 指令

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="obj">移动指令</param>
        /// <returns></returns>
        protected virtual bool _script_2_00(object obj)
        {
            bool result = false;
            if (_tallyMap < 1)
                return result;

            Block block_Old = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == Location);

            string key = obj as string;
            switch (key)
            {
                case "A":
                    if (Location.X - 1 < 0)
                        break;
                    Location = new Point(Location.X - 1, Location.Y);
                    result = true;
                    break;
                case "W":
                    if (Location.Y - 1 < 0)
                        break;
                    Location = new Point(Location.X, Location.Y - 1);
                    result = true;
                    break;
                case "D":
                    if (Location.X + 1 > MainGame.Instance.MapSize.X)
                        break;
                    Location = new Point(Location.X + 1, Location.Y);
                    result = true;
                    break;
                case "S":
                    if (Location.Y + 1 > MainGame.Instance.MapSize.Y)
                        break;
                    Location = new Point(Location.X, Location.Y + 1);
                    result = true;
                    break;
                default:
                    break;
            }

            if (!result)
                return result;

            Block block_New = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == Location);

            block_New ??= MainGame.Instance.NewBlock(Location);

            block_New?.AddEntities(this);
            block_Old?.Entities?.Remove(this);

            MainGame.Instance.EntityMove(this, block_Old.Id, block_New.Id);

            _tallyMap--;

            return result;
        }

        /// <summary>
        /// 进入战斗
        /// </summary>
        /// <param name="obj">指令内容</param>
        /// <returns></returns>
        protected virtual bool _script_2_01(object obj)
        {
            bool result = false;
            if(State == EntityStateType.Die) 
                return result;

            if (State == EntityStateType.InBattle)
                result = true;

            if (State == EntityStateType.Default)
            {
                State = EntityStateType.InBattle;
                result = true;
            }

            return result;
        }
        /// <summary>
        /// 退出战斗
        /// </summary>
        /// <param name="obj">指令内容</param>
        /// <returns></returns>
        protected virtual bool _script_2_02(object obj)
        {
            bool result = false;
            if (State == EntityStateType.Die)
                return result;

            if (State == EntityStateType.Default)
                result = true;

            if (State == EntityStateType.InBattle)
            {
                State = EntityStateType.Default;
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 开启自动移动
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual bool _script_2_03(object obj)
        {
            if (!Characters.Contains(EntityCharacterType.AutoMove))
                Characters.Add(EntityCharacterType.AutoMove);
            return true;
        }

        /// <summary>
        /// 关闭自动移动
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual bool _script_2_04(object obj)
        {
            if (Characters.Contains(EntityCharacterType.AutoMove))
                Characters.RemoveAll(x => x == EntityCharacterType.AutoMove);
            return true;
        }
        #endregion

        public virtual void SetHP(int hp)
        {
            HP = hp;

            if (hp == 0)
            {
                State = EntityStateType.Die;

                Block block_New = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == Location);

                block_New?.Entities?.Remove(this);

                MainGame.Instance.EntityMove(this, block_New.Id, null);
            }
        }

        /// <summary>
        /// 增加经验
        /// </summary>
        /// <param name="exp"></param>
        public virtual void SetExp(int exp)
        {
            Exp += exp;

            //升级
            if(Exp >= ExpMax) {
                Exp -= ExpMax;
                Level++;
                LevelUp();

                Grain.Instance.MainGame.Msg(3, Name, $"升级至{Level}级！！！");
            }

        }
    }
}
