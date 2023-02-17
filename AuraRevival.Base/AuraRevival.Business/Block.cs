using AuraRevival.Business.Battle;
using AuraRevival.Business.Construct;
using AuraRevival.Business.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AuraRevival.Business
{
    /// <summary>
    /// 块
    /// </summary>
    public class Block
    {
        /// <summary>
        /// 区块id
        /// </summary>
        //public Tuple<int, int> Id { get;  set; }
        //public (int,int) Id { get; set; }
        public Point Id { get; set; }


        public Block(Point id)
        {
            Id = id;
        }
        public Block(int x, int y)
        {
            Id = new Point(x, y);
            //Id = Tuple.Create(x, y);
        }

        /// <summary>
        /// 建筑
        /// </summary>
        public List<IConstruct> Constructs { get; set; } = new List<IConstruct>();

        /// <summary>
        /// 实体
        /// </summary>
        public List<IEntity> Entities { get; set; } = new List<IEntity>();


        public List<BattleRoom> BattleRooms { get; set; }  = new List<BattleRoom>();

        public void AddEntities(IEntity entity)
        {
            Entities.Add(entity);

            //存在不同的阵营，开战
            if (Entities.Any(x => x.MId != entity.MId))
            {
                var battleRoom = BattleRooms.LastOrDefault(x=>x.State == BattleStateType.InBattle);
                if (battleRoom == null)
                {
                    battleRoom = new BattleRoom(Entities);
                    //battleRoom.AddEntity(Entities);
                    BattleRooms.Add(battleRoom);
                    battleRoom.BattleStart();
                }
                else
                {
                    battleRoom.AddEntity(new List<IEntity>() { entity });
                }
                
            }
        }


        public void AddEnemy()
        {

            //初始化一个敌人
            IEntity entity = new Entity_Default();
            entity.Init(EntityHelper.GetRandomName(),2, Guid.Empty, Id);
        }
    }
}
