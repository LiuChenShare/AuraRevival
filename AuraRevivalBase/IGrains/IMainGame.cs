using System;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IMainGame : Orleans.IGrainWithIntegerKey
    {
        /// <summary>
        /// 打招呼
        /// </summary>
        /// <returns></returns>
        Task<string> SayHello();
    }
}
