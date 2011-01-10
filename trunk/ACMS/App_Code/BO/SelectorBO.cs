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

        //2.個人報名 3.團隊報名 可報名活動查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistActivity_Query(string activity_name, string activity_startdate, string activity_enddate, string activity_type, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.RegistActivity_Query(activity_name, activity_startdate, activity_enddate, activity_type, emp_id);
            return DT;
        }

        //2.1個人報名-(個人活動新增時)被報名者人事資料
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegisterPersonInfo(string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegisterPersonInfo(emp_id);
        }

        //2.2個人報名-(個人活動編輯時)//登入者代理(含自己)的會列出 or 登入者被別人代理報名也會列出
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegisterPeopleInfo(string activity_id, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegisterPeopleInfo(activity_id, emp_id);
        }

        //2-3個人報名-開啟代理報名選單 或 開啟選擇隊員-列出可加入此活動的隊員
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> RegistableMember(string DEPT_ID, string WORK_ID, string NATIVE_NAME, string activity_id, string activity_type,bool UnderDept)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistableMember(DEPT_ID, WORK_ID, NATIVE_NAME, activity_id, activity_type, UnderDept);
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

        //4.1已報名活動查詢-取消個人報名-由登入者代理報名的人員(及本人)選單
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistedByMeEmpSelector(Guid activity_id, string regist_by)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistedByMeEmpSelector(activity_id, regist_by);
        }
        //4.2已報名活動查詢-取消個人報名-由管理者取消選單
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistedByMeEmpSelectorByManage(Guid activity_id, string regist_by, string DEPT_ID, int JOB_GRADE_GROUP,string WINDOWS_ID, string NATIVE_NAME,string SEX,DateTime EXPERIENCE_START_DATE, string C_NAME,Boolean UnderDept)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistedByMeEmpSelectorByManage(activity_id, regist_by, DEPT_ID, JOB_GRADE_GROUP, WINDOWS_ID, NATIVE_NAME, SEX, EXPERIENCE_START_DATE, C_NAME,UnderDept );
        }

        //4.2該活動由管理者的選單
        public DataTable RegistedMyTeamMemberSelectorByManage(Guid activity_id, string DEPT_ID, int JOB_GRADE_GROUP, string WINDOWS_ID, string NATIVE_NAME, string SEX, DateTime EXPERIENCE_START_DATE, string C_NAME,Boolean UnderDept)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistedMyTeamMemberSelectorByManage(activity_id, DEPT_ID, JOB_GRADE_GROUP, WINDOWS_ID, NATIVE_NAME, SEX, EXPERIENCE_START_DATE, C_NAME,UnderDept );
        }

        //4.2該活動與我同團隊的人員選單
        public DataTable RegistedMyTeamMemberSelector(Guid activity_id, string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistedMyTeamMemberSelector(activity_id, emp_id);
        }

        //5.1活動進度查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable GetAllMyActivity(string emp_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.GetAllMyActivity(emp_id);
            return DT;
        }

        //5.2.活動進度查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityProcessQuery(string activity_id)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityProcessQuery(activity_id);
            return DT;
        }

        //6-1活動資料管理-新增修改活動查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityEditQuery(string activity_name, string activity_startdate, string activity_enddate)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityEditQuery(activity_name, activity_startdate, activity_enddate);
            return DT;
        }

        //6-1 主辦單位設定 主辦單位 DDL DataSource
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

        //6-1 新增修改活動 族群限定 選取人員的GridView資料來源
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> EmployeeSelector(string DEPT_ID, string JOB_GRADE_GROUP, string WORK_ID, string NATIVE_NAME, string SEX, string BIRTHDAY_S, string BIRTHDAY_E, string EXPERIENCE_START_DATE, string C_NAME, Guid activity_id,Boolean UnderDept)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.EmployeeSelector(DEPT_ID, JOB_GRADE_GROUP, WORK_ID, NATIVE_NAME, SEX, BIRTHDAY_S, BIRTHDAY_E, EXPERIENCE_START_DATE, C_NAME, activity_id,UnderDept );
        }

        //6-2活動資料管理-報名狀態查詢 + 6-4活動資料管理-歷史資料查詢
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityQuery(string activity_startdate, string activity_enddate, string org_id, string querytype)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityQuery(activity_startdate, activity_enddate, org_id, querytype);
            return DT;
        }

        //6-3活動進度登錄 - 所有活動的DataSource
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable GetAllActivity()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.GetAllActivity();
            return DT;
        }

        //6-3活動進度登錄 
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable ActivityCheckQuery(string activity_id, string DEPT_ID, string emp_id, string emp_name,Boolean  UnderDept)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            DataTable DT = mySelectorDAO.ActivityCheckQuery(activity_id, DEPT_ID, emp_id, emp_name,UnderDept );
            return DT;
        }

        //7-2 主辦單位設定 角色 DDL DataSource
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

        // 7-2 角色人員管理 選取所有在職員工
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> GetEmployeeSelector(string DEPT_ID, string WORK_ID, string NATIVE_NAME ,Boolean UnderDept)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();

            myEmployeeVOList = mySelectorDAO.GetEmployeeSelector(DEPT_ID, WORK_ID, NATIVE_NAME,UnderDept );

            return myEmployeeVOList;
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
        public List<VO.DDLVO> DeptSelectorByCompanyCode(string COMPANYCODE) 
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.DDLVO> myDDLVOList = new List<VO.DDLVO>();
            VO.DDLVO myDDLVO = new VO.DDLVO();

            myDDLVOList = mySelectorDAO.DeptSelectorByCompanyCode(COMPANYCODE );
            myDDLVO.Value = "";
            myDDLVO.Text = "請選擇";
            myDDLVOList.Insert(0, myDDLVO);
            return myDDLVOList;
        }
        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.DDLVO> JOB_GRADE_GROUPSelector()
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();

            List<VO.DDLVO> myDDLVOList = new List<VO.DDLVO>();
            VO.DDLVO myDDLVO = new VO.DDLVO();

            myDDLVOList = mySelectorDAO.JOB_GRADE_GROUPSelector();
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
        //已報名活動查詢-查詢已報名或者未報名清單
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable RegistedList(Guid activity_id,  string DEPT_ID, int JOB_GRADE_GROUP, string WORK_ID, string NATIVE_NAME, string SEX, DateTime EXPERIENCE_START_DATE, string C_NAME,string  RegistedType)
        {
            DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            return mySelectorDAO.RegistedList(activity_id, DEPT_ID, JOB_GRADE_GROUP, WORK_ID, NATIVE_NAME, SEX, EXPERIENCE_START_DATE, C_NAME, RegistedType);
        }

    }
}
