using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class SelectorBO : BaseBO
    {
        //1.首頁-最新活動顯示
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable NewActivityList(string activity_type, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.NewActivityList(activity_type, emp_id);
        }

        //2.可報名活動查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistActivityQuery(string activity_name, string activity_startdate, string activity_enddate, string activity_type, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.RegistActivityQuery(activity_name, activity_startdate, activity_enddate, activity_type, emp_id);
            clsMyObj.CheckFull(ref DT, false, false);
            return DT;
        }

        //2.1報名者人事資料(個人活動新增-單一報名者個人資料)
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegisterPersonInfo(string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegisterPersonInfo(emp_id);
        }

        //2.2報名者人事資料(個人活動編輯-列出所有由我報名的人的個人資料)
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegisterPeopleInfo(string activity_id, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegisterPeopleInfo(activity_id, emp_id);
        }


        //4.已報名活動查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityEditQuery(string activity_name, string activity_startdate, string activity_enddate, string activity_enddate_finish, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.ActivityEditQuery(activity_name, activity_startdate, activity_enddate, activity_enddate_finish, emp_id);
        }

        //4.1由我報名的人員選單
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistByMeEmpSelector(Guid activity_id, string regist_by)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistByMeEmpSelector(activity_id, regist_by);
        }

        //5.1活動進度查詢-所有活動列表
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable GetAllActivity()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.GetAllActivity();
            return DT;
        }

        //5.2.活動進度查詢-該活動報到進度情況
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityProcessQuery(string activity_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityProcessQuery(activity_id);
            return DT;
        }

        //6-1新增修改活動查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityManagementQuery(string activity_name, string activity_startdate, string activity_enddate)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityManagementQuery(activity_name, activity_startdate, activity_enddate);
            return DT;
        }

        //6-2報名狀態查詢 + 6-4歷史資料查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityQuery(string activity_startdate, string activity_enddate, string org_id, string querytype)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityQuery(activity_startdate, activity_enddate, org_id, querytype);
            return DT;
        }

        //6-3報名登錄狀態管理 
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityCheckQuery(string activity_id, string DEPT_ID, string emp_id, string emp_name)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityCheckQuery(activity_id, DEPT_ID, emp_id, emp_name);
            return DT;
        }


        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> EmployeeSelector(string DEPT_ID, string JOB_CNAME, string WORK_ID, string NATIVE_NAME, string SEX, string BIRTHDAY_S, string BIRTHDAY_E, string EXPERIENCE_START_DATE, string C_NAME, Guid activity_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.EmployeeSelector(DEPT_ID, JOB_CNAME, WORK_ID, NATIVE_NAME, SEX, BIRTHDAY_S, BIRTHDAY_E, EXPERIENCE_START_DATE, C_NAME, activity_id);
        }

        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> SmallEmployeeSelector(string DEPT_ID, string WORK_ID, string NATIVE_NAME, string activity_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.SmallEmployeeSelector(DEPT_ID, WORK_ID, NATIVE_NAME, activity_id);
        }

          //SELECT
          [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
          public List<VO.DDLVO> UnitSelector()
          {
              DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

              List<VO.DDLVO> myDDLVOList = new List<VO.DDLVO>();
              VO.DDLVO myDDLVO = new VO.DDLVO();

              myDDLVOList = mySelectorDAO.UnitSelector();
              myDDLVO.Value = "";
              myDDLVO.Text = "請選擇";
              myDDLVOList.Insert(0, myDDLVO);
              return myDDLVOList;
          }


        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.DDLVO> DeptSelector()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.DDLVO> myDDLVOList = new List<VO.DDLVO>();
            VO.DDLVO myDDLVO = new VO.DDLVO();

            myDDLVOList = mySelectorDAO.DeptSelector();
            myDDLVO.Value ="";
            myDDLVO.Text = "請選擇";
            myDDLVOList.Insert(0,myDDLVO);
            return myDDLVOList;
        }

        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.DDLVO> JOBCNAMESelector()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.DDLVO> myDDLVOList = new List<VO.DDLVO>();
            VO.DDLVO myDDLVO = new VO.DDLVO();

            myDDLVOList = mySelectorDAO.JOBCNAMESelector();
            myDDLVO.Value = "";
            myDDLVO.Text = "請選擇";
            myDDLVOList.Insert(0, myDDLVO);
            return myDDLVOList;
        }

        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.DDLVO> CNAMESelector()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.DDLVO> myDDLVOList = new List<VO.DDLVO>();
            VO.DDLVO myDDLVO = new VO.DDLVO();

            myDDLVOList = mySelectorDAO.CNAMESelector();
            myDDLVO.Value = "";
            myDDLVO.Text = "請選擇";
            myDDLVOList.Insert(0, myDDLVO);
            return myDDLVOList;
        }






    }
}
