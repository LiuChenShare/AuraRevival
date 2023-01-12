﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using AuraRevival.Business.Construct;
using AuraRevival.Business;

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
            game.SecondsEvent += (DateTime time) => { ShowMsg($"ShowMsg：嘀嘀嘀，已经 {time.ToString("yyyy-MM-dd HH:mm:ss")} 了"); };
            game.GameStart();
            Thread.Sleep(200 * 1000);
            Assert.AreEqual(1, index);
        }


        [TestMethod]
        [DataTestMethod]
        [DataRow(1)]
        public void Construct_UpLevel(int index)
        {
            //AuraRevival.Business.Class1

            //AuraRevival.Business.Construct. _Base = new;


        }




        public void ShowMsg(string msg)
        {
            //Console.WriteLine("ShowMsg：嘀嘀嘀，已经 {0} 了：", time.ToString("yyyy-MM-dd HH:mm:ss"));
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
