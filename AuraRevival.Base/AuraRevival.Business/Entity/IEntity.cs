using System;
using System.Collections.Generic;
using System.Text;

namespace AuraRevival.Business.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        string Description { get; }

        int Level { get; }

        int Type { get; }

    }
}
