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
            Console.WriteLine("Alarm：嘀嘀嘀，水已经 {0} 度了：");
            System.Diagnostics.Debug.WriteLine("Alarm：嘀嘀嘀，水已经 {0} 度了：");
            AuraRevival.Business.MainGame game = new AuraRevival.Business.MainGame();
            game.GameStart();
            Thread.Sleep(200 * 1000);
            Assert.AreEqual(1, index);
        }
    }
}
