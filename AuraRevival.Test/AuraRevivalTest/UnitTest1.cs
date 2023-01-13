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
            game.SecondsEvent += (DateTime time) => { ShowMsg($"ShowMsg�������֣��Ѿ� {time.ToString("yyyy-MM-dd HH:mm:ss")} ��"); };
            game.GameStart();
            Thread.Sleep(200 * 1000);
            Assert.AreEqual(1, index);
        }


        /// <summary>
        /// ����(����)��������
        /// </summary>
        /// <param name="index"></param>
        [TestMethod]
        [DataTestMethod]
        [DataRow("����")]
        public void Construct_UpLevel(string constructBaseName)
        {
            AuraRevival.Business.MainGame game = AuraRevival.Business.MainGame.Instance;
            if (game.GameState == 0)
            {
                game.Init(constructBaseName);
            }
            game.GameStart();
            AuraRevival.Business.Construct.Construct_Base? construct = Grain.Instance.Constructs.FirstOrDefault(x => x.Type == AuraRevival.Business.Construct.ConstructType.Base) as AuraRevival.Business.Construct.Construct_Base;
            
            game.SecondsEvent += (DateTime time) => { ShowMsg($"ShowMsg������ʱ {construct?._tallyMap} "); };

            construct?.ScriptEvent(1, null);


            //AuraRevival.Business.Construct. _Base = new;


            Thread.Sleep(200 * 1000);
        }




        public void ShowMsg(string msg)
        {
            //Console.WriteLine("ShowMsg�������֣��Ѿ� {0} �ˣ�", time.ToString("yyyy-MM-dd HH:mm:ss"));
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}