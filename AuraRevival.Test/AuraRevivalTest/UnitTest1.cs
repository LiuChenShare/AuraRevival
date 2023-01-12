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
            AuraRevival.Business.MainGame game = AuraRevival.Business.MainGame.Instance;
            AuraRevival.Business.Construct.Construct_Base construct = new AuraRevival.Business.Construct.Construct_Base("基地");
            game.SecondsEvent += (DateTime time) => { ShowMsg($"ShowMsg：倒计时 {construct._tally} "); };

            construct.ScriptEvent(1, null);

            game.GameStart();

            //AuraRevival.Business.Construct. _Base = new;


            Thread.Sleep(200 * 1000);
        }




        public void ShowMsg(string msg)
        {
            //Console.WriteLine("ShowMsg：嘀嘀嘀，已经 {0} 了：", time.ToString("yyyy-MM-dd HH:mm:ss"));
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}