using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRevival.Business
{
    public class MainGame
    {
        int temperature;                        //水温
        delegate void BoilHandler(int temp);//声明委托
        event BoilHandler BoilEvent;//声明事件
        //实例化Timer类，设置间隔时间为1秒；
        System.Timers.Timer GameTimer = new System.Timers.Timer(1000);

        public void GameStart()
        {
            Alarm alarm = new Alarm();

            BoilEvent += alarm.MakeAlert;            //注册方法
            //BoilEvent += (new Alarm()).MakeAlert;    //给匿名对象注册方法
            BoilEvent += Display.ShowMsg;            //注册静态方法

            GameTimer.Elapsed += new System.Timers.ElapsedEventHandler(Execute);//到达时间的时候执行事件；
            GameTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            GameTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            GameTimer.Start(); //启动定时器
        }

        public void Execute(object source, System.Timers.ElapsedEventArgs e)
        {
            //GameTimer.Stop(); //先关闭定时器
            ////MessageBox.Show("OK!");
            //GameTimer.Start(); //执行完毕后再开启器

            temperature++;
            //    if (BoilEvent != null)
            //    { //如果有对象注册
            //        BoilEvent(temperature);  //调用所有注册对象的方法
            //    }
            BoilEvent?.Invoke(temperature);  //调用所有注册对象的方法

            if (temperature > 99)
            {
                GameTimer.Stop();
            }
        }
    }

    // 警报器
    public class Alarm
    {
        public void MakeAlert(int param)
        {
            if (param > 95)
            {
                Console.WriteLine("Alarm：嘀嘀嘀，水已经 {0} 度了：", param);
                System.Diagnostics.Debug.WriteLine("Alarm：嘀嘀嘀，水已经 {0} 度了：", param);
            }
        }

        public void Display(object state)
        {
        }

    }
    // 显示器
    public class Display
    {
        public static void ShowMsg(int param)
        { //静态方法
            Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", param);
            System.Diagnostics.Debug.WriteLine("Display：水快烧开了，当前温度：{0}度。", param);
        }
    }

}
