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
        public DataTable RegistActivity_Query(string activity_name, string activity_startdate, string activity_enddate, string activity_type, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.RegistActivity_Query(activity_name, activity_startdate, activity_enddate, activity_type, emp_id);
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

        //3.列出可加入此活動的隊員
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> RegistableTeamMember(string DEPT_ID, string WORK_ID, string NATIVE_NAME, string activity_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistableTeamMember(DEPT_ID, WORK_ID, NATIVE_NAME, activity_id);
        }

        //4.已報名活動查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistedActivityQuery(string activity_name, string activity_startdate, string activity_enddate, string activity_enddate_finish, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable dt1 = mySelectorDAO.RegistedActivityQuery(activity_name, activity_startdate, activity_enddate, activity_enddate_finish, emp_id, "1");
            DataTable dt2 = mySelectorDAO.RegistedActivityQuery(activity_name, activity_startdate, activity_enddate, activity_enddate_finish, emp_id, "2");

            dt1.Merge(dt2);

            return dt1;
        }

        //4.1由我報名的人員選單
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistedByMeEmpSelector(Guid activity_id, string regist_by)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistedByMeEmpSelector(activity_id, regist_by);
        }

        
        //4.2該活動與我同團隊的人員選單
        public DataTable RegistedMyTeamMemberSelector(Guid activity_id, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistedMyTeamMemberSelector(activity_id, emp_id);
        }

        //5.1活動進度查詢-所有活動列表
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable GetAllActivity()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.GetAllActivity();
            return DT;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable GetAllMyActivity(string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.GetAllMyActivity(emp_id);
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
        public DataTable ActivityEditQuery(string activity_name, string activity_startdate, string activity_enddate)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityEditQuery(activity_name, activity_startdate, activity_enddate);
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
            myDDLVO.Value = "";
            myDDLVO.Text = "請選擇";
            myDDLVOList.Insert(0, myDDLVO);
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


        //7.角色 DDL DataSource
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.RoleListVO> SelectRoleList()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.RoleListVO> myRoleListVOList = new List<ACMS.VO.RoleListVO>();

            myRoleListVOList = mySelectorDAO.SelectRoleList();

            VO.RoleListVO myRoleListVO = new ACMS.VO.RoleListVO();

            myRoleListVO.id = null;
            myRoleListVO.role_name = "請選擇";
            myRoleListVOList.Insert(0, myRoleListVO);

            return myRoleListVOList;
        }

        //6-1 新增修改活動 族群限定 選取人員的GridView資料來源
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> EmployeeSelector(string DEPT_ID, string JOB_CNAME, string WORK_ID, string NATIVE_NAME, string SEX, string BIRTHDAY_S, string BIRTHDAY_E, string EXPERIENCE_START_DATE, string C_NAME, Guid activity_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.EmployeeSelector(DEPT_ID, JOB_CNAME, WORK_ID, NATIVE_NAME, SEX, BIRTHDAY_S, BIRTHDAY_E, EXPERIENCE_START_DATE, C_NAME, activity_id);
        }

        //7-1 主辦單位設定 主辦單位 DDL DataSource
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.UnitVO> SelectUnit()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            myUnitVOList = mySelectorDAO.SelectUnit();

            VO.UnitVO myUnitVO = new ACMS.VO.UnitVO();

            myUnitVO.id = null;
            myUnitVO.name = "請選擇";
            myUnitVOList.Insert(0, myUnitVO);

            return myUnitVOList;
        }

        //7-2 角色人員管理 選取所有在職員工
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> GetEmployeeSelector(string DEPT_ID, string WORK_ID, string NATIVE_NAME)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();

            myEmployeeVOList = mySelectorDAO.GetEmployeeSelector(DEPT_ID, WORK_ID, NATIVE_NAME);

            return myEmployeeVOList;
        }


    }
}
