using AuraRevival.Business.Construct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace AuraRevival.Business.Entity
{
    /// <summary>
    /// 实体（默认）
    /// </summary>
    public class Entity_Default : IEntity
    {
        #region 公用
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Point Location { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }
        public int Type { get; private set; }
        #endregion




        #region 自定义

        /// <summary> 地图行动值 </summary>
        public int _tallyMap;
        /// <summary> 行动值模板 </summary>
        private int _tallyMapTep;
        /// <summary> 当前指令 </summary>
        private int _scriptCode = -1;


        private Dictionary<int, Entity_Default> _levelConfig;
        #endregion


        public void Init(string name,Point location)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
            Level = 1;


            //注册秒事件
            Grain.Instance.MainGame.SecondsEvent += SecondsEventExecute;
            Grain.Instance.MainGame.MinutesEvent += MinutesEventExecute;

            _levelConfig = new Dictionary<int, Entity_Default>
            {
                { 1, new Entity_Default() { Description = "这个是你最后的希望", _tallyMapTep = 5 } },
                { 2, new Entity_Default() { Description = "稍微有了点起色", _tallyMapTep = 7 } },
                { 3, new Entity_Default() { Description = "还不错呦", _tallyMapTep = 10 } },
                { 4, new Entity_Default() { Description = "拥有了一战之力", _tallyMapTep = 12 } }
            };
            LevelRefresh(Level);


            Block block = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == Location);
            block?.Entities?.Add(this);
            Grain.Instance.Entities.Add(this);
        }
        public bool ScriptEvent(int scriptCode, object obj)
        {
            _scriptCode = _scriptCode != -1 ? _scriptCode : scriptCode;
            switch (scriptCode)
            {
                case 200: //移动
                    {
                        _scriptCode = -1;
                        bool result = _script_2_00(obj);
                        return result;
                    }
                default:
                    break;
            }
            _scriptCode = -1;
            return false;
        }

        public void SecondsEventExecute(DateTime time)
        {
        }

        public void MinutesEventExecute(DateTime time)
        {
            if (_tallyMap < _tallyMapTep)
                _tallyMap++;

        }




        private void LevelRefresh(int level)
        {
            if (_levelConfig.ContainsKey(level))
            {
                var levelConfig = _levelConfig[level];
                Description = levelConfig.Description;
                _tallyMapTep = levelConfig._tallyMapTep;
            }
            _tallyMap = _tallyMapTep;
        }


        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="obj">移动指令</param>
        /// <returns></returns>
        bool _script_2_00(object obj)
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
                    if (Location.X + 1 > MainGame.Instance.MapSize.Item1)
                        break;
                    Location = new Point(Location.X + 1, Location.Y);
                    result = true;
                    break;
                case "S":
                    if (Location.Y + 1 > MainGame.Instance.MapSize.Item2)
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

            block_New?.Entities?.Add(this);
            block_Old?.Entities?.Remove(this);

            MainGame.Instance.EntityMove(this, block_Old.Id, block_New.Id);

            _tallyMap--;

            return result;
        }
    }
}
