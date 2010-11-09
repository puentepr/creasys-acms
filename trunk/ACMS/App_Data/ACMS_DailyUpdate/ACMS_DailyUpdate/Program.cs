using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace ACMS_DailyUpdate
{
    static class Program
    {
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                Service1 ServicesToRun = new Service1();

                ServicesToRun.MyStart(null);

                Console.WriteLine("服務已啟動，請按下 Enter 鍵關閉服務...");
                Console.ReadLine();
                ServicesToRun.MyStop();
                Console.WriteLine("服務已關閉");
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new Service1() };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
