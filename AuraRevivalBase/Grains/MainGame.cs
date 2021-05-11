using AuraRevival.Core;
using IGrains;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Grains
{
    public class MainGame : Orleans.Grain, IMainGame
    {
        IDisposable registerTimer;
        DateTime gameData;

        public Task<string> SayHello()
        {
            //设置一个定时器,5s后开始，每1s一次
            registerTimer = RegisterTimer(_hourglass, "参数", new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 1));
            gameData = new DateTime(2317, 1, 1, 8, 0, 0);
            throw new System.NotImplementedException();
        }

        private Task _hourglass(object obj)
        {
            gameData.AddSeconds(24);//现实的1秒对应24秒
            ConsoleHelper.WriteInfoLine($"GameData ：{gameData:yyyy/MM/dd HH:mm:ss}");
            return Task.CompletedTask;
        }

        private Task<DateTime> GameOver()
        {
            registerTimer.Dispose();
            ConsoleHelper.WriteInfoLine($"GameData ：{gameData:yyyy/MM/dd HH:mm:ss}");
            return Task.FromResult(gameData);
        }
    }
}
