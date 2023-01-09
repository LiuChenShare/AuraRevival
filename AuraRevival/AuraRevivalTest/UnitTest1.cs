using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace AuraRevivalTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(1)]
        public void TestMethod1(int index)
        {
            AuraRevival.Business.MainGame game = AuraRevival.Business.MainGame.Instance;
            game.SecondsEvent += ShowMsg;
            game.GameStart();
            Thread.Sleep(200 * 1000);
            Assert.AreEqual(1, index);
        }


        public void ShowMsg(DateTime time)
        {
            //Console.WriteLine("ShowMsg：嘀嘀嘀，已经 {0} 了：", time.ToString("yyyy-MM-dd HH:mm:ss"));
            System.Diagnostics.Debug.WriteLine($"ShowMsg：嘀嘀嘀，已经 {time.ToString("yyyy-MM-dd HH:mm:ss")} 了");
        }
    }
}
