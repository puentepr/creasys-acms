using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using PDFPlatform;
using System.Configuration;
using Quartz;
using System.Configuration.Install;
using Quartz.Impl;

namespace ACMS_DailyUpdate
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void MyStart(string[] args)
        {
            this.OnStart(args);
        }

        public void MyStop()
        {
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            LogMsg.Log("===================================================================", 5, false);
            LogMsg.Log("Service Start", 5, false);
            LogMsg.Log("===================================================================", 5, false);

            //啟動排程
            //StartupSchedule();         


            //InsertData
            try
            {
                clsDBUtility sourceDBUtility = new clsDBUtility("source_connectionstring");
                clsDBUtility destinationDBUtility = new clsDBUtility("destination_connectionstring");

                //更新資料表
                destinationDBUtility.InsertEmployeeData(sourceDBUtility.SelectEmployeeSource());

                LogMsg.Log("[InsertData]-OK", 5, false);
            }
            catch (Exception ex)
            {
                LogMsg.Log("[InsertData]-NG", 1, false);
                LogMsg.Log(ex.Message, 5, false);
            }

            //UpdateStatus
            try
            {
                clsDBUtility destinationDBUtility = new clsDBUtility("destination_connectionstring");

                //更改狀態
                destinationDBUtility.UpdateStatus();

                LogMsg.Log("[UpdateStatus]-OK", 5, false);
            }
            catch (Exception ex)
            {
                LogMsg.Log("[UpdateStatus]-NG", 1, false);
                LogMsg.Log(ex.Message, 5, false);
            }




        }

        protected override void OnStop()
        {
            LogMsg.Log("===================================================================", 5, false);
            LogMsg.Log("Service Stop", 5, false);
            LogMsg.Log("===================================================================", 5, false);
        }

        //啟動排程
        private void StartupSchedule()
        {
            LogMsg.Log("Start to [StartupSchedule]", 5, false);

            int UpdateHour;
            int.TryParse(ConfigurationManager.AppSettings["UpdateHour"], out UpdateHour);
            UpdateHour = (UpdateHour == 0 ? 2 : UpdateHour);

            TimeSpan TimeSpan1 = new TimeSpan(UpdateHour, 0, 0);
            DateTime DateTime1 = DateTime.Today.Add(TimeSpan1);

            //初始化調度器工廠 
            ISchedulerFactory sf = new StdSchedulerFactory();
            //獲取默認調度器 
            IScheduler scheduler = sf.GetScheduler();

            //作業 
            JobDetail job1 = new JobDetail("DailyJob_JobName", "DailyJob_JobGroup", typeof(DailyJob));

            //觸發器 
            SimpleTrigger trigger1 = new SimpleTrigger("DailyJob_TriggerName", "DailyJob_TriggerGroup", DateTime1.ToUniversalTime(), null, (int.MaxValue), new TimeSpan(24, 0, 0));

            //關聯任務和觸發器 
            scheduler.ScheduleJob(job1, trigger1);

            //開始任務 
            scheduler.Start();

            LogMsg.Log("Start to [StartupSchedule]-OK", 5, false);

        }

        public class DailyJob : IJob
        {
            public void Execute(JobExecutionContext context)
            {
                ////InsertData
                //try
                //{
                //    clsDBUtility sourceDBUtility = new clsDBUtility("source_connectionstring");
                //    clsDBUtility destinationDBUtility = new clsDBUtility("destination_connectionstring");
                    
                //    //更新資料表
                //    destinationDBUtility.InsertEmployeeData(sourceDBUtility.SelectEmployeeSource());

                //    LogMsg.Log("[InsertData]-OK", 5, false);
                //}
                //catch (Exception ex)
                //{
                //    LogMsg.Log("[InsertData]-NG", 1, false);
                //    LogMsg.Log(ex.Message, 5, false);
                //}

                ////UpdateStatus
                //try
                //{
                //    clsDBUtility destinationDBUtility = new clsDBUtility("destination_connectionstring");
                    
                //    //更改狀態
                //    destinationDBUtility.UpdateStatus();

                //    LogMsg.Log("[UpdateStatus]-OK", 5, false);
                //}
                //catch (Exception ex)
                //{
                //    LogMsg.Log("[UpdateStatus]-NG", 1, false);
                //    LogMsg.Log(ex.Message, 5, false);
                //}
            }
        }

    }


    //[RunInstallerAttribute(true)]
    //public class ProjectInstaller : System.ServiceProcess.ServiceInstaller
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller1;
        private ServiceInstaller serviceInstaller1;

        public ProjectInstaller()
        {
            // define and create the service installer
            serviceInstaller1 = new ServiceInstaller();
            serviceInstaller1.StartType = ServiceStartMode.Automatic;
            serviceInstaller1.ServiceName = "ACMS_DailyUpdate";
            serviceInstaller1.DisplayName = "ACMS_DailyUpdate";

            //serviceInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(AfterInstallEventHandler);

            Installers.Add(serviceInstaller1);

            // define and create the process installer
            serviceProcessInstaller1 = new ServiceProcessInstaller();
            serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
            Installers.Add(serviceProcessInstaller1);

        }

        public void AfterInstallEventHandler(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            ServiceController control = new ServiceController("ACMS_DailyUpdate");
            control.Start();
        }

    }	




}
