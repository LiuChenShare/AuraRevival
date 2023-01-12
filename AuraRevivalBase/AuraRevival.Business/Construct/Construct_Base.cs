using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuraRevival.Business.Construct
{
    /// <summary>
    /// 基地(建筑)
    /// </summary>
    public class Construct_Base : IConstruct
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ConstructType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Level { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary> 行动值 </summary>
        public int _tally;
        /// <summary> 行动值模板 </summary>
        private int _tallyTep;

        private Construct_Base() { }
        private readonly Dictionary<int, Construct_Base> _levelConfig;

        public Construct_Base(string name)
        {
            Id = Guid.NewGuid();
            Type = ConstructType.Base;
            Name = name;
            Description = "这个是你最后的希望";
            Level = 1;

            _levelConfig = new Dictionary<int, Construct_Base>
            {
                { 1, new Construct_Base() { Description = "这个是你最后的希望", _tallyTep = 60 } },
                { 1, new Construct_Base() { Description = "这个是你最后的希望", _tallyTep = 55 } },
                { 1, new Construct_Base() { Description = "这个是你最后的希望", _tallyTep = 54 } },
                { 1, new Construct_Base() { Description = "这个是你最后的希望", _tallyTep = 53 } }
            };
        }


        public void ConstructSecondsEventExecute(DateTime time)
        {

            throw new NotImplementedException();
        }

        public bool ScriptEvent(int scriptCode, object obj)
        {
            switch (scriptCode)
            {
                case 1:
                    {
                        var levelMax = _levelConfig.Keys.Max();
                        if (Level+1 > levelMax)
                            return false;
                        
                        UpLevel();
                        return true;
                    }
                default: return false;
            }
            //throw new NotImplementedException();
        }



        private void LevelRefresh(int level)
        {
            if (_levelConfig.ContainsKey(level))
            {
                var levelConfig = _levelConfig[level];
                Description = levelConfig.Description;
                _tallyTep = levelConfig._tallyTep;
                _tally = _tallyTep;
            }
        }

        /// <summary>
        /// 升级
        /// </summary>
        /// <returns></returns>
        private Task UpLevel()
        {
            var levelMax = _levelConfig.Keys.Max();
            if (Level + 1 > levelMax)
                return Task.CompletedTask;

            while (_tally > 0)
            {
                Thread.Sleep(1000);
                _tally--;
            }
            LevelRefresh(Level++);

            return Task.CompletedTask;
        }
    }
}
