using System;
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
            //3-1.若是個人活動可以一直報名，第一次是自己第二次之後是幫別人報名
            //3-2.若是團隊活動並且報過名就不顯示了，只會顯示在"已報名活動查詢"然後使用編輯模式

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_type", SqlDbType.NChar, 1);
            sqlParams[0].Value = activity_type;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar,100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.sn,A.id,A.activity_name,A.people_type,A.limit_count,A.limit2_count ");
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名人(隊)數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine("  SELECT *  ");
            sb.AppendLine("  FROM Activity AA  ");
            sb.AppendLine("  left join (SELECT distinct activity_id FROM ActivityGroupLimit WHERE emp_id=@emp_id) BB on AA.id=BB.activity_id "); //我在這個族群
            sb.AppendLine("  WHERE AA.active='Y' ");
            sb.AppendLine("  and AA.regist_startdate<=getdate() ");//報名已開始
            sb.AppendLine("  and dateadd(day,0,AA.regist_deadline)>=convert(datetime,convert(varchar(10),getdate(),111)) ");//報名尚未截止
            sb.AppendLine("  and AA.activity_type=@activity_type ");//活動類型
            sb.AppendLine("  and (AA.is_grouplimit='N' or BB.activity_id is not null) ");//不限族群or我在這個族群
            sb.AppendLine(") A ");
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id ");
            sb.AppendLine("WHERE 1=1 ");

            if (activity_type == "2")//若是團隊活動並且報過名，則不出現
            {
                sb.AppendLine("and A.id not in (SELECT distinct activity_id FROM ActivityTeamMember WHERE emp_id=@emp_id )");
            }
            //sb.AppendLine(" and A.regist_deadline>=convert(datetime,convert(varchar(10),getdate(),111))");
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

        //2.個人報名 3.團隊報名 可報名活動查詢
        public DataTable RegistActivity_Query(string activity_name, string activity_startdate, string activity_enddate, string activity_type, string emp_id)
        {
            //列出可報名的活動
            //1.列出登入者可報名的活動(不限族群or我在這個族群)
            //2.報名開始日~報名截止日
            //3-1.若是個人活動可以一直報名，第一次是自己第二次之後是幫別人報名
            //3-2.若是團隊活動並且報過名就不顯示了，只會顯示在"已報名活動查詢"然後使用編輯模式
            //4.未額滿

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
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名人(隊)數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine("  SELECT *  ");
            sb.AppendLine("  FROM Activity AA  ");
            sb.AppendLine("  left join (SELECT distinct activity_id FROM ActivityGroupLimit WHERE emp_id=@emp_id) BB on AA.id=BB.activity_id "); //我在這個族群
            sb.AppendLine("  WHERE AA.active='Y' ");
            sb.AppendLine("  and AA.regist_startdate<=getdate() ");//報名已開始
            sb.AppendLine("  and dateadd(day,1,AA.regist_deadline)>getdate() ");//報名尚未截止
            sb.AppendLine("  and AA.activity_type=@activity_type ");//活動類型
            sb.AppendLine("  and (AA.is_grouplimit='N' or BB.activity_id is not null) ");//可報名的活動
            sb.AppendLine(") A ");
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and (A.activity_name like '%'+@activity_name+'%' or @activity_name='') ");
            sb.AppendLine("and ( ");
            sb.AppendLine("      (@activity_startdate='' and @activity_enddate='') ");
            sb.AppendLine("       or (@activity_startdate <= cast(convert(varchar, A.activity_startdate, 102) as datetime) and @activity_enddate >= cast(convert(varchar, A.activity_enddate, 102) as datetime) and @activity_startdate<>'' and @activity_enddate<>'') ");
            sb.AppendLine("       or ( ");
            sb.AppendLine("           ((@activity_startdate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_startdate<>'' ) ");
            sb.AppendLine("           or ");
            sb.AppendLine("           ((@activity_enddate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_enddate<>'' ) ");
            sb.AppendLine("         ) ");
            sb.AppendLine("    ) ");

            if (activity_type == "2")//若是團隊活動並且報過名，則不出現
            {
                sb.AppendLine("and A.id not in (SELECT distinct activity_id FROM ActivityTeamMember WHERE emp_id=@emp_id )");
            }

            sb.AppendLine("GROUP BY A.sn,A.id,A.activity_name,A.people_type,A.limit_count,A.limit2_count,A.activity_startdate,A.activity_enddate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("HAVING COUNT(B.emp_id)<(A.limit_count+A.limit2_count)");//未額滿
            sb.AppendLine("ORDER BY A.sn ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            if (DT != null)
            {
                clsMyObj.CheckFull(ref DT, true, true);
            }

            return DT;

        }

        //2.1個人報名-(個人活動新增時)被報名者人事資料
        public DataTable RegisterPersonInfo(string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[0].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM V_ACSM_USER2 ");
            sb.AppendLine("WHERE ID=@emp_id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);

        }

        //2.2個人報名-(個人活動編輯時)//登入者代理(含自己)的會列出 or 登入者被別人代理報名也會列出
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
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE activity_id=@activity_id ");
            sb.AppendLine("and (A.regist_by=@emp_id or A.emp_id=@emp_id) ");//登入者代理(含自己)的會列出 or 登入者被別人代理報名也會列出

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);

        }

        //2-3個人報名-開啟代理報名選單 或 開啟選擇隊員-列出可加入此活動的隊員
        public List<VO.EmployeeVO> RegistableMember(string DEPT_ID, string WORK_ID, string NATIVE_NAME, string activity_id,string activity_type,Boolean UnderDept)
        {
            if (string.IsNullOrEmpty(activity_id))
            {
                return null;
            }

            string tablename = (activity_type == "1" ? "ActivityRegist" : "ActivityTeamMember");

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

            sb.AppendLine("SELECT A.[ID],A.[C_DEPT_NAME],A.[C_DEPT_ABBR],A.[WORK_ID],A.[NATIVE_NAME],CASE WHEN B.emp_id is null THEN 'true' ELSE 'false' END as theEnable  ");
            sb.AppendLine("FROM V_ACSM_USER2 A ");
            sb.AppendLine(string.Format("left join (SELECT emp_id FROM {0} WHERE activity_id=@activity_id) B on A.ID = B.emp_id ",tablename));//已報名過不可再選
            sb.AppendLine("WHERE A.ID in ");
            sb.AppendLine("( ");
            sb.AppendLine("SELECT CASE WHEN AA.is_grouplimit='Y' THEN BB.emp_id ELSE A.ID END ");
            sb.AppendLine("FROM Activity AA ");
            sb.AppendLine("left join ActivityGroupLimit BB on AA.id=BB.activity_id ");//區分是否有族群限制
            sb.AppendLine("WHERE AA.active='Y' ");
            sb.AppendLine("and AA.id=@activity_id ");
            sb.AppendLine(") ");
            sb.AppendLine("and A.status <2 ");//不為離職或留職停薪
           // sb.AppendLine("and (A.DEPT_ID=@DEPT_ID or @DEPT_ID='') ");
            if (UnderDept)
            {
                sb.AppendLine("and (A.DEPT_ID=@DEPT_ID or A.C_DEPT_NAME like '%'+@DEPT_ID +'%' or @DEPT_ID='') ");
            }
            else
            {
                sb.AppendLine("and (A.DEPT_ID=@DEPT_ID or A.C_DEPT_NAME=@DEPT_ID  or @DEPT_ID='') ");
            }
            sb.AppendLine("and (A.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='') ");
            sb.AppendLine("and (A.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or A.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='') ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();

            while (MyDataReader.Read())
            {
                VO.EmployeeVO myEmployeeVO = new ACMS.VO.EmployeeVO();

                myEmployeeVO.ID = (string)MyDataReader["ID"];
                myEmployeeVO.NATIVE_NAME = (string)MyDataReader["NATIVE_NAME"];
                myEmployeeVO.WORK_ID = (string)MyDataReader["WORK_ID"];
                myEmployeeVO.C_DEPT_ABBR = (string)MyDataReader["C_DEPT_ABBR"];
                myEmployeeVO.C_DEPT_NAME = (string)MyDataReader["C_DEPT_NAME"];
                myEmployeeVO.keyValue = Convert.ToBoolean(MyDataReader["theEnable"]);

                myEmployeeVOList.Add(myEmployeeVO);

            }

            return myEmployeeVOList;
        
        }
        
        //4.已報名活動查詢
        public DataTable RegistedActivityQuery(string activity_name, string activity_startdate, string activity_enddate, string activity_enddate_finish, string emp_id, string activity_type)
        {
            //1.列出(被報名者=登入者)的活動
            //2.但不含已取消的活動

            SqlParameter[] sqlParams = new SqlParameter[6];

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
            sqlParams[5] = new SqlParameter("@activity_type", SqlDbType.NChar, 1);
            sqlParams[5].Value = activity_type;

            StringBuilder sb = new StringBuilder();

            string strTableName = (activity_type == "1" ? "ActivityRegist" : "ActivityTeamMember");

            sb.AppendLine("SELECT A.sn,A.id,A.activity_type,A.activity_name,A.people_type,A.limit_count,A.limit2_count ");
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名人(隊)數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine("  SELECT AA.* ");
            sb.AppendLine("  FROM Activity AA  ");
            sb.AppendLine(string.Format("  inner join {0} BB on AA.id=BB.activity_id and BB.check_status>=0 ", strTableName));//不含已取消的活動
            sb.AppendLine("  WHERE AA.active='Y' ");
            sb.AppendLine("  and (AA.activity_type=@activity_type) ");
            sb.AppendLine("  and ((AA.activity_enddate>getdate() and @activity_enddate_finish='N') or (AA.activity_enddate<=getdate() and @activity_enddate_finish='Y') ) ");//執行中活動(N) 或 歷史資料查詢(Y)            
            sb.AppendLine("  and BB.emp_id=@emp_id ");//(被報名者=登入者)的活動
            sb.AppendLine(") A ");
            sb.AppendLine(string.Format("left join {0} B on A.id=B.activity_id ", strTableName));
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and (A.activity_name like '%'+@activity_name+'%' or @activity_name='') ");
            sb.AppendLine("and ( ");
            sb.AppendLine("      (@activity_startdate='' and @activity_enddate='') ");
            sb.AppendLine("       or (@activity_startdate <= cast(convert(varchar, A.activity_startdate, 102) as datetime) and @activity_enddate >= cast(convert(varchar, A.activity_enddate, 102) as datetime) and @activity_startdate<>'' and @activity_enddate<>'') ");
            sb.AppendLine("       or ( ");
            sb.AppendLine("           ((@activity_startdate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_startdate<>'' ) ");
            sb.AppendLine("           or ");
            sb.AppendLine("           ((@activity_enddate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_enddate<>'' ) ");
            sb.AppendLine("         ) ");
            sb.AppendLine("    ) ");
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

        //4.2已報名活動查詢-取消個人報名-由管理者取消選單
        public DataTable RegistedByMeEmpSelectorByManage(Guid activity_id, string emp_id, string DEPT_ID, int JOB_GRADE_GROUP, string WINDOWS_ID, string NATIVE_NAME, string SEX, DateTime EXPERIENCE_START_DATE, string C_NAME,Boolean UnderDept)
        {

            if (DEPT_ID == "請選擇")
            {
                DEPT_ID = "";
            }

            SqlParameter[] sqlParams = new SqlParameter[9];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;
            sqlParams[2] = new SqlParameter("@Dept_ID", SqlDbType.NVarChar, 1000);
            sqlParams[2].Value = DEPT_ID;
            sqlParams[3] = new SqlParameter("@JOB_GRADE_GROUP", SqlDbType.Int);
            sqlParams[3].Value = JOB_GRADE_GROUP;
            sqlParams[4] = new SqlParameter("@WINDOWS_ID", SqlDbType.NVarChar, 100);
            sqlParams[4].Value = WINDOWS_ID;
            sqlParams[5] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 100);
            sqlParams[5].Value = NATIVE_NAME;
            sqlParams[6] = new SqlParameter("@SEX", SqlDbType.NVarChar, 100);
            sqlParams[6].Value = SEX;
            sqlParams[7] = new SqlParameter("@EXPERIENCE_START_DATE", SqlDbType.DateTime);
            sqlParams[7].Value = EXPERIENCE_START_DATE;
            sqlParams[8] = new SqlParameter("@C_NAME", SqlDbType.NVarChar,100);
            sqlParams[8].Value = C_NAME;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.activity_id,A.emp_id,B.ID,B.C_DEPT_NAME,B.C_DEPT_ABBR,B.WORK_ID,B.NATIVE_NAME ");
            sb.AppendLine("FROM ActivityRegist A ");
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and A.activity_id=@activity_id ");
            sb.AppendLine("and ((A.regist_by=@emp_id or A.emp_id=@emp_id) or @emp_id='') ");//由登入者代理報名的人員(及本人)選單 管理員執行時 @regist_by=''
            if (UnderDept)
            {
                sb.AppendLine(" and (B.C_DEPT_NAME like '%'+@Dept_ID+'%' or B.DEPT_ID=@Dept_ID or @DEPT_ID ='')");
            }
            else
            {
                sb.AppendLine(" and (B.C_DEPT_NAME =@Dept_ID or B.DEPT_ID=@Dept_ID or @DEPT_ID ='')");
            }
           
            sb.AppendLine(" and (B.JOB_GRADE_GROUP=@JOB_GRADE_GROUP or @JOB_GRADE_GROUP=999)");
            sb.AppendLine(" and (B.WORK_ID like '%'+@WINDOWS_ID+'%' or @WINDOWS_ID='')");
            sb.AppendLine(" and (B.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='')");
            if (SEX == "0")
            {
                sb.AppendLine(" and (B.SEX= @SEX or B.SEX='女' or  @SEX='')");
            }
            else
            {
                sb.AppendLine(" and (B.SEX= @SEX or B.SEX='男' or @SEX='')");
            }
            sb.AppendLine(" and B.EXPERIENCE_START_DATE>= @EXPERIENCE_START_DATE");
            sb.AppendLine(" and (B.C_NAME like '%'+@C_NAME+'%' or @C_NAME='')");            
            sb.AppendLine("and A.check_status>=0 ");//已取消就不要再出現了
            sb.AppendLine("ORDER BY A.id ");//按照報名順序

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            return DT;

        }

        //4.1已報名活動查詢-取消個人報名-由登入者代理報名的人員(及本人)選單
        public DataTable RegistedByMeEmpSelector(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.activity_id,A.emp_id,B.ID,B.C_DEPT_NAME,B.C_DEPT_ABBR,B.WORK_ID,B.NATIVE_NAME ");
            sb.AppendLine("FROM ActivityRegist A ");
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and A.activity_id=@activity_id ");
            sb.AppendLine("and ((A.regist_by=@emp_id or A.emp_id=@emp_id) or @emp_id='') ");//由登入者代理報名的人員(及本人)選單 管理員執行時 @regist_by=''
            sb.AppendLine("and A.check_status>=0 ");//已取消就不要再出現了
            sb.AppendLine("ORDER BY A.id ");//按照報名順序

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            return DT; 
        
        }
        //4.2該活動由管理者的選單
        public DataTable RegistedMyTeamMemberSelectorByManage(Guid activity_id, string DEPT_ID, int JOB_GRADE_GROUP, string WINDOWS_ID, string NATIVE_NAME, string SEX, DateTime EXPERIENCE_START_DATE, string C_NAME,Boolean UnderDept)
        {
            if( DEPT_ID =="請選擇")
            {
                DEPT_ID="";
            }
           
            SqlParameter[] sqlParams = new SqlParameter[8];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;          
            sqlParams[1] = new SqlParameter("@Dept_ID", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = DEPT_ID;
            sqlParams[2] = new SqlParameter("@JOB_GRADE_GROUP", SqlDbType.Int);
            sqlParams[2].Value = JOB_GRADE_GROUP;
            sqlParams[3] = new SqlParameter("@WINDOWS_ID", SqlDbType.NVarChar, 100);
            sqlParams[3].Value = WINDOWS_ID;
            sqlParams[4] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 100);
            sqlParams[4].Value = NATIVE_NAME;
            sqlParams[5] = new SqlParameter("@SEX", SqlDbType.NVarChar, 100);
            sqlParams[5].Value = SEX;
            sqlParams[6] = new SqlParameter("@EXPERIENCE_START_DATE", SqlDbType.DateTime);
            sqlParams[6].Value = EXPERIENCE_START_DATE;
            sqlParams[7] = new SqlParameter("@C_NAME", SqlDbType.NVarChar, 100);
            sqlParams[7].Value = C_NAME;
           

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.activity_id,A.emp_id,A.boss_id,B.ID,B.C_DEPT_NAME,B.C_DEPT_ABBR,B.WORK_ID,B.NATIVE_NAME ");
            sb.AppendLine("FROM ActivityTeamMember A ");
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and A.activity_id=@activity_id ");
            sb.AppendLine("and (A.boss_id =B.ID)");
            if (UnderDept)
            {
                sb.AppendLine(" and (B.DEPT_ID=@Dept_ID or B.C_DEPT_NAME like '%'+@Dept_ID+'%'  or @DEPT_ID='')");
            }
            else
            {
                sb.AppendLine(" and (B.DEPT_ID=@Dept_ID or B.C_DEPT_NAME=@Dept_ID  or @DEPT_ID='')");
            }
            sb.AppendLine(" and (B.JOB_GRADE_GROUP=@JOB_GRADE_GROUP or @JOB_GRADE_GROUP=999)");
            sb.AppendLine(" and (B.WORK_ID like '%'+@WINDOWS_ID+'%' or @WINDOWS_ID='')");
            sb.AppendLine(" and (B.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='')");
            if (SEX == "0")
            {
                sb.AppendLine(" and (B.SEX= @SEX or B.SEX='女' or  @SEX='')");
            }
            else
            {
                sb.AppendLine(" and (B.SEX= @SEX or B.SEX='男' or @SEX='')");
            }
            sb.AppendLine(" and B.EXPERIENCE_START_DATE>= @EXPERIENCE_START_DATE");
            sb.AppendLine(" and (B.C_NAME like '%'+@C_NAME+'%' or @C_NAME='')");     
            sb.AppendLine("and A.check_status>=0 ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            return DT;

        }

        //4.2該活動與我同團隊的人員選單
        public DataTable RegistedMyTeamMemberSelector(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.activity_id,A.emp_id,A.boss_id,B.ID,B.C_DEPT_NAME,B.C_DEPT_ABBR,B.WORK_ID,B.NATIVE_NAME ");
            sb.AppendLine("FROM ActivityTeamMember A ");
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and A.activity_id=@activity_id ");
            sb.AppendLine("and (A.boss_id=(SELECT boss_id FROM ActivityTeamMember WHERE activity_id=@activity_id and emp_id=@emp_id )) ");
            sb.AppendLine("and A.check_status>=0 ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            return DT;

        }

        //5.1活動進度查詢
        public DataTable GetAllMyActivity(string emp_id)
        {
            //登入者所參加的所有活動列表
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[0].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.id,activity_name ");
            sb.AppendLine("FROM Activity A ");
            sb.AppendLine("WHERE A.active='Y' ");
            sb.AppendLine("and ");
            sb.AppendLine("( ");
            sb.AppendLine("id in (SELECT distinct activity_id FROM ActivityRegist WHERE emp_id=@emp_id) ");
            sb.AppendLine(" or id in (SELECT distinct activity_id FROM ActivityTeamMember WHERE emp_id=@emp_id) ");
            sb.AppendLine(") ");
            sb.AppendLine(" and A.activity_startdate<= convert(datetime,convert(varchar(10),GETDATE(),111))  and  activity_enddate>=convert(datetime,convert(varchar(10),GETDATE(),111))");
           
            sb.AppendLine("ORDER BY sn ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);
        }

        //5.2.活動進度查詢
        public DataTable ActivityProcessQuery(string activity_id)
        {
            if (string.IsNullOrEmpty(activity_id))
            {
                return null;
            }
            //該活動報到進度情況
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = new Guid(activity_id);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT B.emp_id,C.NATIVE_NAME,C.WORK_ID,CASE B.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN -2 THEN '已離職' WHEN -3 THEN '留職停薪' ELSE '' END as check_status ");
            sb.AppendLine("FROM Activity A ");
            sb.AppendLine("inner join [ActivityRegist] B on A.id=B.activity_id and A.id=@activity_id and A.activity_type='1' ");
            sb.AppendLine("left join [V_ACSM_USER2] C on B.emp_id = C.id ");
            sb.AppendLine("Union ");
            sb.AppendLine("SELECT B.emp_id,C.NATIVE_NAME,C.WORK_ID,CASE B.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN -2 THEN '已離職' WHEN -3 THEN '留職停薪' ELSE '' END as check_status ");
            sb.AppendLine("FROM Activity A ");
            sb.AppendLine("inner join [ActivityTeamMember] B on A.id=B.activity_id and A.id=@activity_id and A.activity_type='2' ");
            sb.AppendLine("left join [V_ACSM_USER2] C on B.emp_id = C.id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);
        }
        
        //6-1活動資料管理-新增修改活動查詢
        public DataTable ActivityEditQuery(string activity_name, string activity_startdate, string activity_enddate)
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
            sb.AppendLine(",COUNT(B.emp_id) as register_count ");//報名人(隊)數
            sb.AppendLine(",A.activity_startdate,A.activity_enddate,A.regist_startdate,A.regist_deadline,A.cancelregist_deadline ");
            sb.AppendLine("FROM Activity A");          
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id ");
            sb.AppendLine("WHERE A.active='Y' ");
            sb.AppendLine("and (A.activity_name like '%'+@activity_name+'%' or @activity_name='') ");
            sb.AppendLine("and ( ");
            sb.AppendLine("      (@activity_startdate='' and @activity_enddate='') ");
            sb.AppendLine("       or (@activity_startdate <= cast(convert(varchar, A.activity_startdate, 102) as datetime) and @activity_enddate >= cast(convert(varchar, A.activity_enddate, 102) as datetime) and @activity_startdate<>'' and @activity_enddate<>'') ");
            sb.AppendLine("       or ( ");
            sb.AppendLine("           ((@activity_startdate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_startdate<>'' ) ");
            sb.AppendLine("           or ");
            sb.AppendLine("           ((@activity_enddate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_enddate<>'' ) ");
            sb.AppendLine("         ) ");
            sb.AppendLine("    ) ");
            //andy add 因為有權限管制
            sb.AppendLine("and (A.org_id in (select unit_id from RoleUserMapping where emp_id='" + clsAuth.ID + "')");
            sb.AppendLine("     or  0 in  (select unit_id from RoleUserMapping where emp_id='" + clsAuth.ID + "'))");


            sb.AppendLine("and A.activity_enddate>=  Convert(Datetime,Convert(varchar(10),getDate(),111)) and A.regist_startdate>Convert(Datetime,Convert(varchar(10),getDate(),111))");//列出活動未結束的活動
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

        //6-1 主辦單位設定 主辦單位 DDL DataSource
        public List<VO.UnitVO> SelectUnit()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT *");
            sb.AppendLine("FROM Unit A ");
            sb.AppendLine("WHERE active='Y' ");
            sb.AppendLine("and( ");
            sb.AppendLine(string.Format("id in (select unit_id from RoleUserMapping where emp_id='{0}') ", clsAuth.ID));
            sb.AppendLine(string.Format("or 0 in (select unit_id from RoleUserMapping where emp_id='{0}') ", clsAuth.ID));
            sb.AppendLine("); ");

            IDataReader myIDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            while (myIDataReader.Read())
            {
                VO.UnitVO myUnitVO = new ACMS.VO.UnitVO();
                myUnitVO.id = (int)myIDataReader["id"];
                myUnitVO.name = (string)myIDataReader["name"];
                myUnitVOList.Add(myUnitVO);
            }

            return myUnitVOList;
        }

        //6-1 新增修改活動 族群限定 選取人員的GridView資料來源
        public List<VO.EmployeeVO> EmployeeSelector(string DEPT_ID, string JOB_GRADE_GROUP, string WORK_ID, string NATIVE_NAME, string SEX, string BIRTHDAY_S, string BIRTHDAY_E, string EXPERIENCE_START_DATE, string C_NAME, Guid activity_id, Boolean UnderDept)
        {
            SqlParameter[] sqlParams = new SqlParameter[10];
            if (DEPT_ID == "請選擇")
            {
                DEPT_ID = "";
            }
            sqlParams[0] = new SqlParameter("@DEPT_ID", SqlDbType.NVarChar, 36);
            sqlParams[0].Value = DEPT_ID;
            sqlParams[1] = new SqlParameter("@JOB_GRADE_GROUP", SqlDbType.NVarChar, 36);
            sqlParams[1].Value = JOB_GRADE_GROUP;
            sqlParams[2] = new SqlParameter("@WORK_ID", SqlDbType.NVarChar, 36);
            sqlParams[2].Value = WORK_ID;
            sqlParams[3] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 200);
            sqlParams[3].Value = NATIVE_NAME;
            sqlParams[4] = new SqlParameter("@SEX", SqlDbType.NVarChar, 2);
            sqlParams[4].Value = SEX;
            sqlParams[5] = new SqlParameter("@BIRTHDAY_S", SqlDbType.NVarChar, 50);
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
            sb.AppendLine("FROM V_ACSM_USER2 A ");
            sb.AppendLine("left join (SELECT * FROM ActivityGroupLimit WHERE activity_id=@activity_id) B on A.ID=B.emp_id ");
            sb.AppendLine("WHERE A.STATUS<2 ");
            //sb.AppendLine("and (A.DEPT_ID =@DEPT_ID or @DEPT_ID='') ");
            if (UnderDept)
            {
                sb.AppendLine("and (A.C_DEPT_NAME like '%'+ @DEPT_ID+'%' or @DEPT_ID='' ) ");
            }
            else
            {
                sb.AppendLine("and (A.C_DEPT_NAME = @DEPT_ID or @DEPT_ID='' ) ");
            }
            sb.AppendLine("and (A.JOB_GRADE_GROUP = @JOB_GRADE_GROUP or @JOB_GRADE_GROUP='') ");
            sb.AppendLine("and (A.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='') ");
            sb.AppendLine("and (A.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or A.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='') ");
            if (SEX == "0")
            {
                sb.AppendLine(" and (A.SEX= @SEX or A.SEX='女' or  @SEX='')");
            }
            else
            {
                sb.AppendLine(" and (A.SEX= @SEX or A.SEX='男' or @SEX='')");
            }
            sb.AppendLine("and (A.BIRTHDAY >= @BIRTHDAY_S or @BIRTHDAY_S='') ");
            sb.AppendLine("and (A.BIRTHDAY <= @BIRTHDAY_E or @BIRTHDAY_E='') ");
            sb.AppendLine("and (A.EXPERIENCE_START_DATE >=@EXPERIENCE_START_DATE or @EXPERIENCE_START_DATE='') ");
            sb.AppendLine("and (A.C_NAME = @C_NAME or @C_NAME='') ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();

            while (MyDataReader.Read())
            {
                VO.EmployeeVO myEmployeeVO = new ACMS.VO.EmployeeVO();

                myEmployeeVO.keyValue = Convert.ToBoolean(MyDataReader["YN"]);
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
                myEmployeeVO.BIRTHDAY = (string)MyDataReader["BIRTHDAY"];
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
                
        //6-2活動資料管理-報名狀態查詢 + 6-4活動資料管理-歷史資料查詢
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
            sb.AppendLine("FROM [Activity] A ");
            sb.AppendLine("WHERE A.active='Y' ");
            sb.AppendLine("and ( ");
            sb.AppendLine("      (@activity_startdate='' and @activity_enddate='') ");
            sb.AppendLine("       or (@activity_startdate <= cast(convert(varchar, A.activity_startdate, 102) as datetime) and @activity_enddate >= cast(convert(varchar, A.activity_enddate, 102) as datetime) and @activity_startdate<>'' and @activity_enddate<>'') ");
            sb.AppendLine("       or ( ");
            sb.AppendLine("           ((@activity_startdate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_startdate<>'' ) ");
            sb.AppendLine("           or ");
            sb.AppendLine("           ((@activity_enddate between cast(convert(varchar, A.activity_startdate, 102) as datetime) and cast(convert(varchar, A.activity_enddate, 102) as datetime)) and @activity_enddate<>'' ) ");
            sb.AppendLine("         ) ");
            sb.AppendLine("    ) ");
            if (org_id != "")//andy modi 因為報名查詢會有權限的管制
            {
                sb.AppendLine("and (org_id=@org_id or @org_id='') ");
            }
            else
            {
                sb.AppendLine("and (org_id in (select unit_id from RoleUserMapping where emp_id='"+clsAuth.ID+"')");
                sb.AppendLine("     or  0 in  (select unit_id from RoleUserMapping where emp_id='"+clsAuth.ID+"'))");

                   
            }

            if (querytype != "off")
            {
                sb.AppendLine("and A.activity_enddate>getdate() ");//報名狀態查詢(活動未結束)
            }
            else
            {
                sb.AppendLine("and A.activity_enddate<=getdate() ");//歷史資料查詢(活動已結束)
            }

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);
        }

        //6-3活動進度登錄 - 所有活動的DataSource
        public DataTable GetAllActivity()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.id,A.activity_name ");
            sb.AppendLine("FROM Activity A ");
            sb.AppendLine("inner join Unit B on A.org_id=B.id ");
            sb.AppendLine("WHERE A.active='Y' and B.active='Y' ");
            sb.AppendLine("and( ");
            sb.AppendLine(string.Format("B.id in (select unit_id from RoleUserMapping where emp_id='{0}') ", clsAuth.ID));
            sb.AppendLine(string.Format("or 0 in (select unit_id from RoleUserMapping where emp_id='{0}') ", clsAuth.ID));
            sb.AppendLine(") ");
            sb.AppendLine(" and A.activity_startdate<= convert(datetime,convert(varchar(10),GETDATE(),111))  and  activity_enddate>=convert(datetime,convert(varchar(10),GETDATE(),111))");
            sb.AppendLine("ORDER BY A.sn; ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), null);

            return clsMyObj.GetDataTable(DS);
        }

        //6-3活動進度登錄 
        public DataTable ActivityCheckQuery(string activity_id,string DEPT_ID, string emp_id, string emp_name,Boolean UnderDept)
        {

            if (DEPT_ID == "請選擇")
            {
                DEPT_ID = "";
            }
            if (string.IsNullOrEmpty(activity_id))
            {
                return null;
            }

            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = new Guid(activity_id);
            sqlParams[1] = new SqlParameter("@DEPT_ID", SqlDbType.NVarChar, 36);
            sqlParams[1].Value = DEPT_ID;
            sqlParams[2] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[2].Value = emp_id;
            sqlParams[3] = new SqlParameter("@emp_name", SqlDbType.NVarChar, 200);
            sqlParams[3].Value = emp_name;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM ");
            sb.AppendLine("( ");
            sb.AppendLine(" SELECT C.ENGLISH_NAME, A.id,A.activity_type,B.emp_id,C.NATIVE_NAME,C.WORK_ID,C.DEPT_ID,C.C_DEPT_NAME,C.C_DEPT_ABBR,CASE B.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN 3 THEN '留職停薪' WHEN 4 THEN '已離職' ELSE '' END as check_status ");
            sb.AppendLine(" ,convert(varchar(30),B.createat,120) as createat,C.OFFICE_MAIL ,C.OFFICE_PHONE,B.team_name,A.limit_count as team_max");
            sb.AppendLine(" FROM Activity A ");
            sb.AppendLine(" inner join [ActivityRegist] B on A.id=B.activity_id and A.id=@activity_id and A.activity_type='1' and B.check_status>=0 ");//已取消的不要出現
            sb.AppendLine(" left join [V_ACSM_USER2] C on B.emp_id = C.id  ");
            sb.AppendLine(" Union ");
            sb.AppendLine(" SELECT C.ENGLISH_NAME,A.id,A.activity_type,B.emp_id,C.NATIVE_NAME,C.WORK_ID,C.DEPT_ID,C.C_DEPT_NAME,C.C_DEPT_ABBR,CASE B.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN 3 THEN '留職停薪' WHEN 4 THEN '已離職' ELSE '' END as check_status ");
            sb.AppendLine(",convert(varchar(30),D.createat,120) as createat ,C.OFFICE_MAIL ,C.OFFICE_PHONE ,D.team_name,A.limit_count as team_max");
            sb.AppendLine(" FROM Activity A ");
            sb.AppendLine(" inner join [ActivityTeamMember] B on A.id=B.activity_id and A.id=@activity_id and A.activity_type='2' and B.check_status>=0 ");//已取消的不要出現
            sb.AppendLine(" left join [V_ACSM_USER2] C on B.emp_id = C.id ");
            sb.AppendLine("left join [ActivityRegist]D on A.id=d.activity_id and D.emp_id =B.boss_id ");
            sb.AppendLine(") AA");
            sb.AppendLine("where 1=1 ");
            sb.AppendLine("and (WORK_ID like '%'+@emp_id+'%' or @emp_id='') ");
            if (UnderDept)
            {
                sb.AppendLine("and (C_DEPT_NAME like '%'+@DEPT_ID+'%' or DEPT_ID=@DEPT_ID or @DEPT_ID='') ");
            }
            else
            {
                sb.AppendLine("and (C_DEPT_NAME = @DEPT_ID or  DEPT_ID=@DEPT_ID or @DEPT_ID='') ");
            }
           
            
            sb.AppendLine("and (ENGLISH_NAME like '%'+@emp_name+'%' or NATIVE_NAME like '%'+@emp_name +'%'or @emp_name='') ");
            sb.AppendLine("order by createat ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);
        }         
                
        //7-2 主辦單位設定 角色 DDL DataSource
        public List<VO.RoleListVO> SelectRoleList()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT *");
            sb.AppendLine("FROM RoleList A ");

            IDataReader myIDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.RoleListVO> myRoleListVOList = new List<ACMS.VO.RoleListVO>();

            while (myIDataReader.Read())
            {
                VO.RoleListVO myRoleListVO = new ACMS.VO.RoleListVO();
                myRoleListVO.id = (int)myIDataReader["id"];
                myRoleListVO.role_name = (string)myIDataReader["role_name"];
                myRoleListVOList.Add(myRoleListVO);
            }

            return myRoleListVOList;
        }

       // 7-2 角色人員管理 選取所有在職員工
        public List<VO.EmployeeVO> GetEmployeeSelector(string DEPT_ID, string WORK_ID, string NATIVE_NAME,Boolean UnderDept)
        {
            if (DEPT_ID == "請選擇") 
            {
                DEPT_ID = ""; 
            }
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@DEPT_ID", SqlDbType.NVarChar, 36);
            sqlParams[0].Value = DEPT_ID;
            sqlParams[1] = new SqlParameter("@WORK_ID", SqlDbType.NVarChar, 36);
            sqlParams[1].Value = WORK_ID;
            sqlParams[2] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 200);
            sqlParams[2].Value = NATIVE_NAME;

            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("SELECT B.WINDOWS_ID, B.[ID],B.C_DEPT_NAME,B.[C_DEPT_ABBR],B.[WORK_ID],B.[NATIVE_NAME] ");
            sb.AppendLine("FROM V_ACSM_USER2 B ");
            sb.AppendLine("WHERE status<2 ");//在職員工
            if(UnderDept)
            {
                sb.AppendLine("and ((B.DEPT_ID=@DEPT_ID or B.C_DEPT_NAME like '%'+@DEPT_ID+'%') or @DEPT_ID='') ");
            }
            else
            {
               sb.AppendLine("and ((B.DEPT_ID=@DEPT_ID or B.C_DEPT_NAME =@DEPT_ID )or @DEPT_ID='') ");
            }
            //sb.AppendLine("and (B.DEPT_ID=@DEPT_ID ) ");
            sb.AppendLine("and (B.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='') ");
            sb.AppendLine("and (B.ENGLISH_NAME like '%'  +@NATIVE_NAME+  '%'or B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='') ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();

            while (MyDataReader.Read())
            {
                VO.EmployeeVO myEmployeeVO = new ACMS.VO.EmployeeVO();

                myEmployeeVO.ID = (string)MyDataReader["WINDOWS_ID"] + "(" + (string)MyDataReader["NATIVE_NAME"] + ")";
                myEmployeeVO.NATIVE_NAME = (string)MyDataReader["NATIVE_NAME"];
                myEmployeeVO.WORK_ID = (string)MyDataReader["WORK_ID"];
                myEmployeeVO.C_DEPT_NAME = (string)MyDataReader["C_DEPT_NAME"];
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

                myDDLVO.Value = MyDataReader["id"].ToString();
                myDDLVO.Text = MyDataReader["name"].ToString();

                myDDLVOList.Add(myDDLVO);
            }

            return myDDLVOList;

        }

        public List<VO.DDLVO> DeptSelector()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT distinct [DEPT_ID],C_DEPT_NAME,[C_DEPT_ABBR] ");
            sb.AppendLine("FROM V_ACSM_USER2  ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.DDLVO> myDDLVOList = new List<ACMS.VO.DDLVO>();

            while (MyDataReader.Read())
            {
                VO.DDLVO myDDLVO = new ACMS.VO.DDLVO();

                myDDLVO.Value = (string)MyDataReader["DEPT_ID"];
                myDDLVO.Text = (string)MyDataReader["C_DEPT_NAME"];

                myDDLVOList.Add(myDDLVO);

            }

            return myDDLVOList;

        }
        public List<VO.DDLVO> DeptSelectorByCompanyCode(string COMPANYCODE)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@COMPANYCODE", SqlDbType.NVarChar,300);
            sqlParams[0].Value = COMPANYCODE;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT distinct [DEPT_ID],C_DEPT_NAME,[C_DEPT_ABBR] ");
            sb.AppendLine("FROM V_ACSM_USER2   WHERE C_NAME=@COMPANYCODE");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.DDLVO> myDDLVOList = new List<ACMS.VO.DDLVO>();

            while (MyDataReader.Read())
            {
                VO.DDLVO myDDLVO = new ACMS.VO.DDLVO();

                myDDLVO.Value = (string)MyDataReader["DEPT_ID"];
                myDDLVO.Text = (string)MyDataReader["C_DEPT_NAME"];

                myDDLVOList.Add(myDDLVO);

            }

            return myDDLVOList;

        }


        public List<VO.DDLVO> JOB_GRADE_GROUPSelector()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM JOB_GRADE_GROUP ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.DDLVO> myDDLVOList = new List<ACMS.VO.DDLVO>();

            while (MyDataReader.Read())
            {
                VO.DDLVO myDDLVO = new ACMS.VO.DDLVO();

                myDDLVO.Value = MyDataReader["GROUP_CODE"].ToString();
                myDDLVO.Text = MyDataReader["GROUP_DESCRIPTION"].ToString();

                myDDLVOList.Add(myDDLVO);

            }

            return myDDLVOList;

        }

        public List<VO.DDLVO> CNAMESelector()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT distinct [C_NAME] ");
            sb.AppendLine("FROM V_ACSM_USER2  ");

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


        //已報名活動查詢-查詢已報名或者未報名清單
        public DataTable RegistedList(Guid activity_id, string DEPT_ID, int JOB_GRADE_GROUP, string WORK_ID, string NATIVE_NAME, string SEX, DateTime EXPERIENCE_START_DATE, string C_NAME,string List_Type)
        {
            if (DEPT_ID == null)
            {
                return new DataTable();
            }
            SqlParameter[] sqlParams = new SqlParameter[8];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@Dept_ID", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = DEPT_ID;
            sqlParams[2] = new SqlParameter("@JOB_GRADE_GROUP", SqlDbType.Int);
            sqlParams[2].Value = JOB_GRADE_GROUP;
            sqlParams[3] = new SqlParameter("@WORK_ID", SqlDbType.NVarChar, 100);
            sqlParams[3].Value = WORK_ID;
            sqlParams[4] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 100);
            sqlParams[4].Value = NATIVE_NAME;
            sqlParams[5] = new SqlParameter("@SEX", SqlDbType.NVarChar, 100);
            sqlParams[5].Value = SEX;
            sqlParams[6] = new SqlParameter("@EXPERIENCE_START_DATE", SqlDbType.DateTime);
            sqlParams[6].Value = EXPERIENCE_START_DATE;
            sqlParams[7] = new SqlParameter("@C_NAME", SqlDbType.NVarChar, 100);
            sqlParams[7].Value = C_NAME;
            StringBuilder sb = new StringBuilder();
            if (List_Type == "0")//已報名清冊
            {
                sb.AppendLine(" SELECT A.team_name,A.emp_id,B.ID,B.C_DEPT_NAME,B.C_DEPT_ABBR,B.WORK_ID,B.NATIVE_NAME ");
                sb.AppendLine("  ,case  A.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN -2 THEN '已離職' WHEN -3 THEN '留職停薪' ELSE '' END as check_status");
                sb.AppendLine("  FROM ActivityRegist A");
                sb.AppendLine(" left join V_ACSM_USER2 B on A.emp_id=B.ID ");
                sb.AppendLine(" left join ActivityTeamMember  C on A.activity_id =c.activity_id ");
                sb.AppendLine(" left join Activity D on A.activity_id =D.id");
                sb.AppendLine("WHERE 1=1 and D.activity_type ='1' ");
                sb.AppendLine("and A.activity_id=@activity_id ");
                sb.AppendLine(" and (B.DEPT_ID=@Dept_ID or @DEPT_ID ='')");
                sb.AppendLine(" and (B.JOB_GRADE_GROUP=@JOB_GRADE_GROUP or @JOB_GRADE_GROUP=999)");
                sb.AppendLine(" and (B.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='')");
                sb.AppendLine(" and (B.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='')");
                if (SEX == "0")
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='女' or  @SEX='')");
                }
                else
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='男' or @SEX='')");
                }
                sb.AppendLine(" and B.EXPERIENCE_START_DATE>= @EXPERIENCE_START_DATE");
                sb.AppendLine(" and (B.C_NAME like '%'+@C_NAME+'%' or @C_NAME='')");

                sb.AppendLine(" union SELECT A.team_name,A.emp_id,B.ID,B.C_DEPT_NAME,B.C_DEPT_ABBR,B.WORK_ID,B.NATIVE_NAME ");
                sb.AppendLine("  ,case  A.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN -2 THEN '已離職' WHEN -3 THEN '留職停薪' ELSE '' END as check_status");
                sb.AppendLine("  FROM ActivityRegist A");

                sb.AppendLine(" left join ActivityTeamMember  C on A.activity_id =c.activity_id ");
                sb.AppendLine(" left join V_ACSM_USER2 B on C.emp_id=B.ID ");
                sb.AppendLine(" left join Activity D on A.activity_id =D.id");
                sb.AppendLine("WHERE 1=1 and D.activity_type ='2' ");
                sb.AppendLine("and A.activity_id=@activity_id ");
                sb.AppendLine(" and (B.DEPT_ID=@Dept_ID or @DEPT_ID ='')");
                sb.AppendLine(" and (B.JOB_GRADE_GROUP=@JOB_GRADE_GROUP or @JOB_GRADE_GROUP=999)");
                sb.AppendLine(" and (B.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='')");
                sb.AppendLine(" and (B.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='')");
                if (SEX == "0")
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='女' or  @SEX='')");
                }
                else
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='男' or @SEX='')");
                }
                sb.AppendLine(" and B.EXPERIENCE_START_DATE>= @EXPERIENCE_START_DATE");
                sb.AppendLine(" and (B.C_NAME like '%'+@C_NAME+'%' or @C_NAME='')");



                sb.AppendLine("ORDER BY C_DEPT_ABBR,NATIVE_NAME ");//
            }
            else
            {
                sb.AppendLine(" SELECT '' as team_name,B.WORK_ID ,B.NATIVE_NAME ,B.C_DEPT_NAME,B.C_DEPT_ABBR ,'未報名' as check_status");
                sb.AppendLine(" from ActivityGroupLimit E");
                sb.AppendLine(" left join V_ACSM_USER2 B on E.emp_id=B.ID ");
                sb.AppendLine(" left join Activity C on E.activity_id =C.ID ");
                sb.AppendLine(" WHERE 1=1  and C.activity_type ='1'");
                sb.AppendLine(" and E.activity_id=@activity_id");
                sb.AppendLine(" and  not  E.emp_id in (select emp_id  from ActivityRegist  where activity_id =@activity_id )");
                sb.AppendLine(" and (B.DEPT_ID=@Dept_ID or @DEPT_ID ='')");
                sb.AppendLine(" and (B.JOB_GRADE_GROUP=@JOB_GRADE_GROUP or @JOB_GRADE_GROUP=999)");
                sb.AppendLine(" and (B.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='')");
                sb.AppendLine(" and (B.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='')");
                if (SEX == "0")
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='女' or  @SEX='')");
                }
                else
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='男' or @SEX='')");
                }
                sb.AppendLine(" and B.EXPERIENCE_START_DATE>= @EXPERIENCE_START_DATE");
                sb.AppendLine(" and (B.C_NAME like '%'+@C_NAME+'%' or @C_NAME='')");


                sb.AppendLine("    union ");
                sb.AppendLine("   SELECT '' as team_name,B.WORK_ID ,B.NATIVE_NAME,B.C_DEPT_NAME ,B.C_DEPT_ABBR ,'未報名' as check_status");
                sb.AppendLine(" from ActivityGroupLimit E ");
                sb.AppendLine(" left join V_ACSM_USER2 B on E.emp_id=B.ID ");
                sb.AppendLine(" left join Activity C on E.activity_id =C.ID ");
                sb.AppendLine(" WHERE 1=1  and C.activity_type ='2'");
                sb.AppendLine(" and E.activity_id=@activity_id");
                sb.AppendLine(" and  not  E.emp_id in (select emp_id  from ActivityTeamMember  where activity_id =@activity_id)");
                sb.AppendLine(" and (B.DEPT_ID=@Dept_ID or @DEPT_ID ='')");
                sb.AppendLine(" and (B.JOB_GRADE_GROUP=@JOB_GRADE_GROUP or @JOB_GRADE_GROUP=999)");
                sb.AppendLine(" and (B.WORK_ID like '%'+@WORK_ID+'%' or @WORK_ID='')");
                sb.AppendLine(" and ( B.ENGLISH_NAME like '%'+@NATIVE_NAME+'%' or B.NATIVE_NAME like '%'+@NATIVE_NAME+'%' or @NATIVE_NAME='')");
                if (SEX == "0")
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='女' or  @SEX='')");
                }
                else
                {
                    sb.AppendLine(" and (B.SEX= @SEX or B.SEX='男' or @SEX='')");
                }
                sb.AppendLine(" and B.EXPERIENCE_START_DATE>= @EXPERIENCE_START_DATE");
                sb.AppendLine(" and (B.C_NAME like '%'+@C_NAME+'%' or @C_NAME='')");
                sb.AppendLine(" order by C_DEPT_ABBR,NATIVE_NAME");


            }

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            DataTable DT = clsMyObj.GetDataTable(DS);

            return DT;

        }


    }
}
