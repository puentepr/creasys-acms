﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class SelectorDAO : BaseDAO
    {

        //1.首頁-最新活動顯示
        public DataTable NewActivityList(string activity_type, string emp_id)
        {
            //1.列出登入者可報名的活動(不限族群or我在這個族群)
            //2.報名開始日~報名截止日

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_type", SqlDbType.NChar, 1);
            sqlParams[0].Value = activity_type;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar,100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.sn,A.id,A.activity_name,A.people_type,A.limit_count,A.limit2_count ");
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名人數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine("  SELECT *  ");
            sb.AppendLine("  FROM Activity AA  ");
            sb.AppendLine("  left join (SELECT distinct activity_id FROM ActivityGroupLimit WHERE emp_id=@emp_id) BB on AA.id=BB.activity_id "); //我在這個族群
            sb.AppendLine("  WHERE AA.active='Y' ");
            sb.AppendLine("  and AA.regist_startdate<=getdate() ");//報名已開始
            sb.AppendLine("  and AA.regist_deadline>getdate() ");//報名尚未截止
            sb.AppendLine("  and AA.activity_type=@activity_type ");//活動類型
            sb.AppendLine("  and (AA.is_grouplimit='N' or BB.activity_id is not null) ");//不限族群or我在這個族群
            sb.AppendLine(") A ");
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id ");
            sb.AppendLine("WHERE A.active='Y' ");
            sb.AppendLine("GROUP BY A.sn,A.id,A.activity_name,A.people_type,A.limit_count,A.limit2_count,A.activity_startdate,A.activity_enddate ");
            sb.AppendLine("ORDER BY A.sn ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            if (DT != null)
            {
                clsMyObj.CheckFull(ref DT, true, true);
            }

            return DT;
        }

        //2.可報名活動查詢
        public DataTable RegistActivityQuery(string activity_name, string activity_startdate, string activity_enddate, string activity_type, string emp_id)
        {
            //列出可報名的活動
            //1.族群名單內或全體活動
            //2.報名已開始亦尚未截止
            //3.未額滿

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@activity_name", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = activity_name;
            sqlParams[1] = new SqlParameter("@activity_startdate", SqlDbType.NVarChar, 20);
            sqlParams[1].Value = activity_startdate;
            sqlParams[2] = new SqlParameter("@activity_enddate", SqlDbType.NVarChar, 20);
            sqlParams[2].Value = activity_enddate;
            sqlParams[3] = new SqlParameter("@activity_type", SqlDbType.NChar, 1);
            sqlParams[3].Value = activity_type;
            sqlParams[4] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 50);
            sqlParams[4].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.sn,A.id,A.activity_name,A.people_type,A.limit_count,A.limit2_count ");
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名人數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine("  SELECT *  ");
            sb.AppendLine("  FROM Activity AA  ");
            sb.AppendLine("  left join (SELECT distinct activity_id FROM ActivityGroupLimit WHERE emp_id=@emp_id) BB on AA.id=BB.activity_id "); //我在這個族群
            sb.AppendLine("  WHERE AA.active='Y' ");
            sb.AppendLine("  and AA.regist_startdate<=getdate() ");//報名已開始
            sb.AppendLine("  and AA.regist_deadline>getdate() ");//報名尚未截止
            sb.AppendLine("  and AA.activity_type=@activity_type ");//活動類型
            sb.AppendLine("  and (AA.is_grouplimit='N' or BB.activity_id is not null) ");//可報名的活動
            sb.AppendLine(") A ");
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id ");
            sb.AppendLine("WHERE A.active='Y' ");
            sb.AppendLine("and (A.activity_name like '%'+@activity_name+'%' or @activity_name='') ");
            //sb.AppendLine("and (A.activity_startdate>@activity_startdate or @activity_startdate='') ");
            //sb.AppendLine("and (A.activity_enddate=@activity_enddate or @activity_enddate='') ");
            sb.AppendLine("GROUP BY A.sn,A.id,A.activity_name,A.people_type,A.limit_count,A.limit2_count,A.activity_startdate,A.activity_enddate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("ORDER BY A.sn ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);

        }

        //2.1報名者人事資料(個人活動新增-單一報名者個人資料)
        public DataTable RegisterPersonInfo(string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[0].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM V_ACSM_USER ");
            sb.AppendLine("WHERE ID=@emp_id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);

        }

        //2.2報名者人事資料(個人活動編輯-列出所有由我報名的人的個人資料)
        public DataTable RegisterPeopleInfo(string activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = new Guid(activity_id);
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar,100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT B.* ");
            sb.AppendLine("FROM ActivityRegist A ");
            sb.AppendLine("left join V_ACSM_USER B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE activity_id=@activity_id ");
            sb.AppendLine("and (A.regist_by=@emp_id or A.emp_id=@emp_id) ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);

        }

        //4.已報名活動查詢
        public DataTable ActivityEditQuery(string activity_name, string activity_startdate, string activity_enddate, string activity_enddate_finish, string emp_id)
        {
            //列出regist_by=登入者的活動

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@activity_name", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = activity_name;
            sqlParams[1] = new SqlParameter("@activity_startdate", SqlDbType.NVarChar, 20);
            sqlParams[1].Value = activity_startdate;
            sqlParams[2] = new SqlParameter("@activity_enddate", SqlDbType.NVarChar, 20);
            sqlParams[2].Value = activity_enddate;
            sqlParams[3] = new SqlParameter("@activity_enddate_finish", SqlDbType.NChar, 1);
            sqlParams[3].Value = activity_enddate_finish;
            sqlParams[4] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[4].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.sn,A.id,A.activity_type,A.activity_name,A.people_type,A.limit_count,A.limit2_count ");
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名成功人數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine("  SELECT AA.* ");
            sb.AppendLine("  FROM Activity AA  ");
            sb.AppendLine("  left join ActivityRegist BB on AA.id=BB.activity_id ");
            sb.AppendLine("  WHERE AA.active='Y' ");
            sb.AppendLine("  and ((AA.activity_enddate<=getdate() and @activity_enddate_finish='Y') or @activity_enddate_finish='N' ) ");//是否為活動已結束的歷史資料            
            sb.AppendLine("  and (BB.emp_id=@emp_id or BB.regist_by=@emp_id) ");//報名者是他們本人或報名者是我 注意:已取消的也會顯示
            sb.AppendLine(") A ");
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id ");
            sb.AppendLine("WHERE A.active='Y' ");
            sb.AppendLine("and B.check_status>=0 ");//報名成功人數不含已取消
            sb.AppendLine("and (A.activity_name like '%'+@activity_name+'%' or @activity_name='') ");
            //sb.AppendLine("and (A.activity_startdate>@activity_startdate or @activity_startdate='') ");
            //sb.AppendLine("and (A.activity_enddate=@activity_enddate or @activity_enddate='') ");
            sb.AppendLine("GROUP BY A.sn,A.id,A.activity_type,A.activity_name,A.people_type,A.limit_count,A.limit2_count,A.activity_startdate,A.activity_enddate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("ORDER BY A.sn ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            if (DT != null)
            {
                clsMyObj.CheckFull(ref DT, true, true);
            }

            return DT;

        }

        //4.1由我報名的人員選單
        public DataTable RegistByMeEmpSelector(Guid activity_id, string regist_by)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@regist_by", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = regist_by;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.activity_id,A.emp_id,B.ID,B.C_DEPT_ABBR,B.WORK_ID,B.NATIVE_NAME ");
            sb.AppendLine("FROM ActivityRegist A ");
            sb.AppendLine("left join V_ACSM_USER B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and A.activity_id=@activity_id ");
            sb.AppendLine("and (A.regist_by=@regist_by or @regist_by='') ");
            sb.AppendLine("and A.check_status>=0 ");
            sb.AppendLine("ORDER BY A.id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            return DT; 
        
        }


        //5.1活動進度查詢-所有活動列表
        public DataTable GetAllActivity()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT id,activity_name ");
            sb.AppendLine("FROM Activity ");
            sb.AppendLine("WHERE active='Y' ");
            sb.AppendLine("ORDER BY sn ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), null);

            return clsMyObj.GetDataTable(DS);
        }

        //5.2.活動進度查詢-該活動報到進度情況
        public DataTable ActivityProcessQuery(string activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = new Guid(activity_id);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.emp_id,B.NATIVE_NAME,CASE check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN 4 THEN '已離職' ELSE '' END as check_status ");
            sb.AppendLine("FROM [ActivityRegist] A ");
            sb.AppendLine("left join [V_ACSM_USER] B on A.emp_id = B.id ");
            sb.AppendLine("where 1=1 ");
            sb.AppendLine("and activity_id=@activity_id ");
            //sb.AppendLine("and check_status>=0 ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);
        }


        //6-1新增修改活動查詢
        public DataTable ActivityManagementQuery(string activity_name, string activity_startdate, string activity_enddate)
        {
            //列出活動未結束的活動

            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_name", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = activity_name;
            sqlParams[1] = new SqlParameter("@activity_startdate", SqlDbType.NVarChar, 20);
            sqlParams[1].Value = activity_startdate;
            sqlParams[2] = new SqlParameter("@activity_enddate", SqlDbType.NVarChar, 20);
            sqlParams[2].Value = activity_enddate;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.sn,A.id,A.activity_type,A.activity_name,A.people_type,A.limit_count,A.limit2_count ");
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名人數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate,A.regist_startdate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("FROM Activity A");          
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id ");
            sb.AppendLine("WHERE A.active='Y' ");
            sb.AppendLine("and (A.activity_name like '%'+@activity_name+'%' or @activity_name='') ");
            //sb.AppendLine("and (A.activity_startdate>@activity_startdate or @activity_startdate='') ");
            //sb.AppendLine("and (A.activity_enddate=@activity_enddate or @activity_enddate='') ");
            sb.AppendLine("and A.activity_enddate>getdate() ");//列出活動未結束的活動

            sb.AppendLine("GROUP BY A.sn,A.id,A.activity_type,A.activity_name,A.people_type,A.limit_count,A.limit2_count,A.activity_startdate,A.activity_enddate,A.regist_startdate,A.regist_deadline,A.cancelregist_deadline  ");
            sb.AppendLine("ORDER BY A.sn ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            if (DT != null)
            {
                clsMyObj.CheckFull(ref DT, true, true);
            }

            return DT;

        }


        //6-2報名狀態查詢 + 6-4歷史資料查詢
        public DataTable ActivityQuery(string activity_startdate, string activity_enddate, string org_id, string querytype)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_startdate", SqlDbType.NVarChar, 20);
            sqlParams[0].Value = activity_startdate;
            sqlParams[1] = new SqlParameter("@activity_enddate", SqlDbType.NVarChar, 20);
            sqlParams[1].Value = activity_enddate;
            sqlParams[2] = new SqlParameter("@org_id", SqlDbType.NVarChar, 50);
            sqlParams[2].Value = org_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT id,activity_type,activity_name,people_type,activity_startdate,activity_enddate,regist_deadline,cancelregist_deadline ");
            sb.AppendLine("FROM [Activity] ");
            sb.AppendLine("where 1=1 ");
            //sb.AppendLine("and(  ");
            //sb.AppendLine("(@activity_startdate between activity_startdate and activity_enddate) ");
            //sb.AppendLine("or (@activity_enddate between activity_startdate and activity_enddate) ");
            //sb.AppendLine("or (activity_startdate>@activity_startdate and activity_enddate<@activity_enddate) ");
            //sb.AppendLine(") ");
            sb.AppendLine("and (org_id=@org_id or @org_id='') ");

            if (querytype == "off")
            {
                sb.AppendLine("and activity_enddate<=getdate() ");
            }
            else
            {
                sb.AppendLine("and activity_enddate>getdate() ");
            }

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);
        }

        //6-3報名登錄狀態管理 
        public DataTable ActivityCheckQuery(string activity_id, string emp_id, string emp_name)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = new Guid(activity_id);
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;
            sqlParams[2] = new SqlParameter("@emp_name", SqlDbType.NVarChar, 200);
            sqlParams[2].Value = emp_name;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.emp_id,B.NATIVE_NAME,B.C_DEPT_ABBR,CASE check_status  WHEN 0 THEN '已報名' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN -2 THEN '已離職' ELSE '' END as check_status ");
            sb.AppendLine("FROM [ActivityRegist] A ");
            sb.AppendLine("left join [V_ACSM_USER] B on A.emp_id = B.id ");
            sb.AppendLine("where 1=1 ");
            sb.AppendLine("and A.activity_id=@activity_id ");
            sb.AppendLine("and A.check_status>=0 ");
            sb.AppendLine("and (B.ID=@emp_id or @emp_id='') ");
            sb.AppendLine("and (B.NATIVE_NAME like '%'+@emp_name +'%'or @emp_name='') ");
            sb.AppendLine("order by A.id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);
        }






        public List<VO.EmployeeVO> EmployeeSelector(string DEPT_ID, string JOB_CNAME, string WORK_ID, string NATIVE_NAME, string SEX, string BIRTHDAY_S, string BIRTHDAY_E, string EXPERIENCE_START_DATE, string C_NAME, Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];

            sqlParams[0] = new SqlParameter("@DEPT_ID", SqlDbType.NVarChar,36);
            sqlParams[0].Value = DEPT_ID;
            sqlParams[1] = new SqlParameter("@JOB_CNAME", SqlDbType.NVarChar,200);
            sqlParams[1].Value = JOB_CNAME;
            sqlParams[2] = new SqlParameter("@WORK_ID", SqlDbType.NVarChar, 36);
            sqlParams[2].Value = WORK_ID;
            sqlParams[3] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 200);
            sqlParams[3].Value = NATIVE_NAME;
            sqlParams[4] = new SqlParameter("@SEX", SqlDbType.NVarChar,2);
            sqlParams[4].Value = SEX;
            sqlParams[5] = new SqlParameter("@BIRTHDAY_S", SqlDbType.NVarChar,50);
            sqlParams[5].Value = BIRTHDAY_S;
            sqlParams[6] = new SqlParameter("@BIRTHDAY_E", SqlDbType.NVarChar, 50);
            sqlParams[6].Value = BIRTHDAY_E;
            sqlParams[7] = new SqlParameter("@EXPERIENCE_START_DATE", SqlDbType.NVarChar, 50);
            sqlParams[7].Value = EXPERIENCE_START_DATE;
            sqlParams[8] = new SqlParameter("@C_NAME", SqlDbType.NVarChar, 120);
            sqlParams[8].Value = C_NAME;
            sqlParams[9] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[9].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT CASE WHEN B.emp_id is null THEN 'true' ELSE 'false' END as YN, A.[ID],A.[NATIVE_NAME],A.[ENGLISH_NAME],A.[WORK_ID],A.[OFFICE_MAIL],A.[DEPT_ID],A.[C_DEPT_NAME],A.[C_DEPT_ABBR],A.[OFFICE_PHONE],A.[EXPERIENCE_START_DATE],A.[BIRTHDAY],A.[SEX],A.[JOB_CNAME],A.[STATUS],A.[WORK_END_DATE],A.[COMPANY_CODE],A.[C_NAME] ");
            sb.AppendLine("FROM V_ACSM_USER A ");
            sb.AppendLine("left join (SELECT * FROM ActivityGroupLimit WHERE activity_id=@activity_id) B on A.ID=B.emp_id ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and (A.DEPT_ID =@DEPT_ID or @DEPT_ID='') ");
            sb.AppendLine("and (A.JOB_CNAME = @JOB_CNAME or @JOB_CNAME='') ");
            sb.AppendLine("and (A.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='') ");
            sb.AppendLine("and (A.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='') ");
            sb.AppendLine("and (A.SEX=@SEX or @SEX='') ");
            sb.AppendLine("and (A.BIRTHDAY >= @BIRTHDAY_S or @BIRTHDAY_S='') ");
            sb.AppendLine("and (A.BIRTHDAY <= @BIRTHDAY_E or @BIRTHDAY_E='') ");
            sb.AppendLine("and (A.BIRTHDAY <= @BIRTHDAY_E or @BIRTHDAY_E='') ");
            sb.AppendLine("and (A.EXPERIENCE_START_DATE >=@EXPERIENCE_START_DATE or @EXPERIENCE_START_DATE='') ");
            sb.AppendLine("and (A.C_NAME = @C_NAME or @C_NAME='') ");


            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();
          
            while (MyDataReader.Read())
            {
                VO.EmployeeVO myEmployeeVO = new ACMS.VO.EmployeeVO();

                myEmployeeVO.keyValue = Convert.ToBoolean(  MyDataReader["YN"]);
                myEmployeeVO.ID = (string)MyDataReader["ID"];
                myEmployeeVO.NATIVE_NAME = (string)MyDataReader["NATIVE_NAME"];
                myEmployeeVO.ENGLISH_NAME = (string)MyDataReader["ENGLISH_NAME"];
                myEmployeeVO.WORK_ID = (string)MyDataReader["WORK_ID"];
                myEmployeeVO.OFFICE_MAIL = (string)MyDataReader["OFFICE_MAIL"];
                myEmployeeVO.DEPT_ID = (string)MyDataReader["DEPT_ID"];
                myEmployeeVO.C_DEPT_NAME = (string)MyDataReader["C_DEPT_NAME"];
                myEmployeeVO.C_DEPT_ABBR = (string)MyDataReader["C_DEPT_ABBR"];
                myEmployeeVO.OFFICE_PHONE = (string)MyDataReader["OFFICE_PHONE"];
                myEmployeeVO.EXPERIENCE_START_DATE = (DateTime?)(MyDataReader["EXPERIENCE_START_DATE"] == DBNull.Value ? null : MyDataReader["EXPERIENCE_START_DATE"]);
                myEmployeeVO.BIRTHDAY = (DateTime?)(MyDataReader["BIRTHDAY"] == DBNull.Value ? null : MyDataReader["BIRTHDAY"]);
                myEmployeeVO.SEX = (string)MyDataReader["SEX"];
                myEmployeeVO.JOB_CNAME = (string)MyDataReader["JOB_CNAME"];
                myEmployeeVO.STATUS = (string)MyDataReader["STATUS"];
                myEmployeeVO.WORK_END_DATE = (DateTime?)(MyDataReader["WORK_END_DATE"] == DBNull.Value ? null : MyDataReader["WORK_END_DATE"]);
                myEmployeeVO.COMPANY_CODE = (string)MyDataReader["COMPANY_CODE"];
                myEmployeeVO.C_NAME = (string)MyDataReader["C_NAME"];

                myEmployeeVOList.Add(myEmployeeVO);
              
            }

            return myEmployeeVOList;

        }

        public List<VO.EmployeeVO> SmallEmployeeSelector(string DEPT_ID, string WORK_ID, string NATIVE_NAME, string activity_id)
        {
            if (string.IsNullOrEmpty(activity_id))
            {
                return null;
            }

            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@DEPT_ID", SqlDbType.NVarChar, 36);
            sqlParams[0].Value = DEPT_ID;
            sqlParams[1] = new SqlParameter("@WORK_ID", SqlDbType.NVarChar, 36);
            sqlParams[1].Value = WORK_ID;
            sqlParams[2] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 200);
            sqlParams[2].Value = NATIVE_NAME;
            sqlParams[3] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[3].Value = new Guid(activity_id);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT B.[ID],B.[C_DEPT_ABBR],B.[WORK_ID],B.[NATIVE_NAME] ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine("  SELECT AA.is_grouplimit,BB.emp_id ");
            sb.AppendLine("  FROM Activity AA  ");
            sb.AppendLine("  left join ActivityGroupLimit BB on AA.id=BB.activity_id ");
            sb.AppendLine("  WHERE AA.active='Y' ");
            sb.AppendLine("  and AA.id=@activity_id ");
            sb.AppendLine(") A ");
            sb.AppendLine("left join V_ACSM_USER B on CASE WHEN A.is_grouplimit='Y' THEN A.emp_id ELSE B.ID END = B.ID ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and (B.DEPT_ID=@DEPT_ID or @DEPT_ID='') ");
            sb.AppendLine("and (B.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='') ");
            sb.AppendLine("and (B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='') ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();

            while (MyDataReader.Read())
            {
                VO.EmployeeVO myEmployeeVO = new ACMS.VO.EmployeeVO();
      
                myEmployeeVO.ID = (string)MyDataReader["ID"];
                myEmployeeVO.NATIVE_NAME = (string)MyDataReader["NATIVE_NAME"];
                myEmployeeVO.WORK_ID = (string)MyDataReader["WORK_ID"];          
                myEmployeeVO.C_DEPT_ABBR = (string)MyDataReader["C_DEPT_ABBR"];            

                myEmployeeVOList.Add(myEmployeeVO);

            }

            return myEmployeeVOList;

        }



    
     








       



        




        public List<VO.DDLVO> UnitSelector()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT [id],[name] ");
            sb.AppendLine("FROM Unit  ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.DDLVO> myDDLVOList = new List<ACMS.VO.DDLVO>();

            while (MyDataReader.Read())
            {
                VO.DDLVO myDDLVO = new ACMS.VO.DDLVO();

                myDDLVO.Value = (string)MyDataReader["id"];
                myDDLVO.Text = (string)MyDataReader["name"];

                myDDLVOList.Add(myDDLVO);

            }

            return myDDLVOList;

        }

        public List<VO.DDLVO> DeptSelector()
        { 
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT distinct [DEPT_ID],[C_DEPT_ABBR] ");
            sb.AppendLine("FROM V_ACSM_USER  ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.DDLVO> myDDLVOList = new List<ACMS.VO.DDLVO>();

            while (MyDataReader.Read())
            {
                VO.DDLVO myDDLVO = new ACMS.VO.DDLVO();

                myDDLVO.Value = (string)MyDataReader["DEPT_ID"];
                myDDLVO.Text = (string)MyDataReader["C_DEPT_ABBR"];

                myDDLVOList.Add(myDDLVO);

            }

            return myDDLVOList;

        }

        public List<VO.DDLVO> JOBCNAMESelector()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT distinct [JOB_CNAME] ");
            sb.AppendLine("FROM V_ACSM_USER  ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.DDLVO> myDDLVOList = new List<ACMS.VO.DDLVO>();

            while (MyDataReader.Read())
            {
                VO.DDLVO myDDLVO = new ACMS.VO.DDLVO();

                myDDLVO.Value = (string)MyDataReader["JOB_CNAME"];
                myDDLVO.Text = (string)MyDataReader["JOB_CNAME"];

                myDDLVOList.Add(myDDLVO);

            }

            return myDDLVOList;

        }

        public List<VO.DDLVO> CNAMESelector()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT distinct [C_NAME] ");
            sb.AppendLine("FROM V_ACSM_USER  ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.DDLVO> myDDLVOList = new List<ACMS.VO.DDLVO>();

            while (MyDataReader.Read())
            {
                VO.DDLVO myDDLVO = new ACMS.VO.DDLVO();

                myDDLVO.Value = (string)MyDataReader["C_NAME"];
                myDDLVO.Text = (string)MyDataReader["C_NAME"];

                myDDLVOList.Add(myDDLVO);

            }

            return myDDLVOList;

        }

    }
}
