using AuraRevival.Business;
using AuraRevival.Business.Goods;
using System.Reflection;

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
            game.SecondsEvent += async (DateTime time) => { ShowMsg($"ShowMsg�������֣��Ѿ� {time.ToString("yyyy-MM-dd HH:mm:ss")} ��"); };
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
            
            game.SecondsEvent += async (DateTime time) => { ShowMsg($"ShowMsg������ʱ {construct?._tallyMap} "); };

            construct?.ScriptEvent(1, null);


            //AuraRevival.Business.Construct. _Base = new;


            Thread.Sleep(200 * 1000);
        }

        /// <summary>
        /// ����(����)ÿ����������
        /// </summary>
        /// <param name="index"></param>
        [TestMethod]
        [DataTestMethod]
        [DataRow("����")]
        public void Construct_ProductionSeconds(string constructBaseName)
        {
            AuraRevival.Business.MainGame game = AuraRevival.Business.MainGame.Instance;
            if (game.GameState == 0)
            {
                game.Init(constructBaseName);
            }
            game.GameStart();
            AuraRevival.Business.Construct.Construct_Base? construct = Grain.Instance.Constructs.FirstOrDefault(x => x.Type == AuraRevival.Business.Construct.ConstructType.Base) as AuraRevival.Business.Construct.Construct_Base;


            construct?.ProductionSeconds();


            //AuraRevival.Business.Construct. _Base = new;


            Thread.Sleep(200 * 1000);
        }

        /// <summary>
        /// ��̬������Ʒ
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        [TestMethod]
        [DataTestMethod]
        [DataRow(1,"ľͷ")]
        public void Goods_Init(int code,string name)
        {
            IGoods _mtype = (IGoods)Assembly.Load("AuraRevival.Business").CreateInstance("AuraRevival.Business.Goods.Goods.Goods_Base");

            _mtype.Init(code, name);

            string name2 = _mtype.Name;
        }




        public void ShowMsg(string msg)
        {
            //Console.WriteLine("ShowMsg�������֣��Ѿ� {0} �ˣ�", time.ToString("yyyy-MM-dd HH:mm:ss"));
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}