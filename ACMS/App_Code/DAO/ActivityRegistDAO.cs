using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class ActivityRegistDAO : BaseDAO
    {
        /// <summary>
        /// 將要刪除的成員資料新增到取消人員清冊中
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">要取消的人員</param>
        /// <param name="activity_type">活動類別</param>
        /// <param name="cancel_by">取消人</param>
        public void InsertActivityRegistCancel(Guid activity_id, string emp_id, string activity_type,string cancel_by)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            string[] emp_ids = emp_id.Split(',');

            foreach (string emp_id1 in emp_ids)
            {
                sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
                sqlParams[0].Value = activity_id;
                sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 50);
                sqlParams[1].Value = emp_id1;
                sqlParams[2] = new SqlParameter("@cancel_by", SqlDbType.NVarChar, 50);
                sqlParams[2].Value = cancel_by;

                StringBuilder sb = new StringBuilder();
                if (activity_type == "1")
                {
                    sb.AppendLine(" insert into ActivityRegistCancel(activity_id,emp_id,boss_id,team_name,regist_by,createat,cancel_by,cancel_date)  select  @activity_id,@emp_id,'','',regist_by,createat,@cancel_by ,getDate()  from ActivityRegist  where activity_id=@activity_id and  emp_id=@emp_id ");

                }
                else
                {
                    sb.AppendLine(" insert into ActivityRegistCancel(activity_id,emp_id,boss_id,team_name,regist_by,createat,cancel_by,cancel_date)  select @activity_id,@emp_id,A.boss_id ,B.team_name ,B.regist_by,B.createat,@cancel_by,getDate()  from ActivityTeamMember A");
                    sb.AppendLine(" left join ActivityRegist B on A.activity_id =B.activity_id and A.boss_id=B.emp_id ");
                    sb.AppendLine("  where A.activity_id=@activity_id and  A.emp_id=@emp_id");
                }

                SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
            }
        }

        /// <summary>
        /// 將要刪除的成員資料新增到取消人員清冊中(找出不在新成員名單中)-團隊專用
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">新成員名單</param>
        /// <param name="cancel_by">取消人員</param>
        public void InsertActivityRegistCancelTeamMember(Guid activity_id, string emp_id, string cancel_by)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 50);
            sqlParams[1].Value = emp_id;
            sqlParams[2] = new SqlParameter("@cancel_by", SqlDbType.NVarChar, 50);
            sqlParams[2].Value = cancel_by;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" insert into ActivityRegistCancel(activity_id,emp_id,boss_id,team_name,regist_by,createat,cancel_by,cancel_date)  select @activity_id,A.emp_id,A.boss_id ,B.team_name ,B.regist_by,B.createat,@cancel_by,getDate()  from ActivityTeamMember A");
            sb.AppendLine(" left join ActivityRegist B on A.activity_id =B.activity_id and A.boss_id=B.emp_id ");
            sb.AppendLine("  where A.activity_id=@activity_id and  not (A.emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')))");


            SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }  
        /// <summary>
        /// 新增一個報名人員的資料
        /// </summary>
        /// <param name="myActivityRegistVO">報名人員型別物件</param>
        /// <returns>ExecuteNonQuery</returns>
        public int INSERT_NewOne(VO.ActivityRegistVO myActivityRegistVO)
        { 
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = myActivityRegistVO.id;
            sqlParams[1] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[1].Value = myActivityRegistVO.activity_id;
            sqlParams[2] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 50);
            sqlParams[2].Value = myActivityRegistVO.emp_id;
            sqlParams[3] = new SqlParameter("@regist_by", SqlDbType.NVarChar, 50);
            sqlParams[3].Value = myActivityRegistVO.regist_by;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT ActivityRegist ");
            sb.AppendLine("([id],[activity_id],[emp_id],[regist_by],[ticket_id],[idno],[ext_people],[createat],[check_status]) ");
            sb.AppendLine("VALUES ");
            sb.AppendLine("(@id,@activity_id,@emp_id,@regist_by,null,'',null,getdate(),0) ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }

    

        //取得該活動所有成功報名者名單
        /// <summary>
        /// 取得該活動所有成功報名者名單
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="activity_type">活動類別</param>
        /// <returns>取得該活動所有成功報名者名單</returns>
        public DataTable SelectEmployeesByID(Guid activity_id, string activity_type)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("SELECT B.WORK_ID,B.NATIVE_NAME,B.C_DEPT_NAME,B.C_DEPT_ABBR,B.OFFICE_MAIL,B.OFFICE_PHONE  ");
            
            //sb.AppendLine(string.Format("FROM ActivityRegist  A ", (activity_type == "1" ? "" : "ActivityTeamMember")));
            //sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            //sb.AppendLine("WHERE 1=1 ");
            //sb.AppendLine("AND A.activity_id=@activity_id ");

            //====================
            sb.AppendLine("SELECT * FROM ");
            sb.AppendLine("( ");
            sb.AppendLine(" SELECT B.idno,'' as boss_id,A.id,A.activity_type,B.emp_id,C.NATIVE_NAME,C.WORK_ID,C.DEPT_ID,C.C_DEPT_NAME,C.C_DEPT_ABBR,CASE B.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN 3 THEN '留職停薪' WHEN 4 THEN '已離職' ELSE '' END as check_status ");
            sb.AppendLine(" ,convert(varchar(16),B.createat,120) as createat,C.OFFICE_MAIL ,C.OFFICE_PHONE,B.team_name,A.limit_count as team_max");
            sb.AppendLine(" FROM Activity A ");
            sb.AppendLine(" inner join [ActivityRegist] B on A.id=B.activity_id and A.id=@activity_id and A.activity_type='1' and B.check_status>=0 ");//已取消的不要出現
            sb.AppendLine(" left join [V_ACSM_USER2] C on B.emp_id = C.id  ");
            sb.AppendLine(" Union ");
            sb.AppendLine(" SELECT  B.idno,B.boss_id, A.id,A.activity_type,B.emp_id,C.NATIVE_NAME,C.WORK_ID,C.DEPT_ID,C.C_DEPT_NAME,C.C_DEPT_ABBR,CASE B.check_status  WHEN 0 THEN '未報到' WHEN 1 THEN '已報到' WHEN 2 THEN '已完成' WHEN -1 THEN '已取消' WHEN 3 THEN '留職停薪' WHEN 4 THEN '已離職' ELSE '' END as check_status ");
            sb.AppendLine(",convert(varchar(16),D.createat,120) as createat ,C.OFFICE_MAIL ,C.OFFICE_PHONE ,D.team_name,A.limit_count as team_max");
            sb.AppendLine(" FROM Activity A ");
            sb.AppendLine(" inner join [ActivityTeamMember] B on A.id=B.activity_id and A.id=@activity_id and A.activity_type='2' and B.check_status>=0 ");//已取消的不要出現
            sb.AppendLine(" left join [V_ACSM_USER2] C on B.emp_id = C.id ");
            sb.AppendLine("left join [ActivityRegist]D on A.id=d.activity_id and D.emp_id =B.boss_id ");
            sb.AppendLine(") AA");
            sb.AppendLine("where 1=1 ");
           // sb.AppendLine("and (WORK_ID like '%'+@emp_id+'%' or @emp_id='') ");
            //sb.AppendLine("and (DEPT_ID=@DEPT_ID or @DEPT_ID='') ");
            //sb.AppendLine("and (NATIVE_NAME like '%'+@emp_name +'%'or @emp_name='') ");
            sb.AppendLine("order by createat ");




            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);

        }

        //取得報名資訊-個人活動   為了組成個人固定欄位
        /// <summary>
        /// 取得報名資訊-個人活動   為了組成個人固定欄位
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <returns>取得報名資訊-個人活動   為了組成個人固定欄位</returns>
        public VO.ActivityRegistVO SelectActivityRegistByPK(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM ActivityRegist ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND activity_id=@activity_id ");
            sb.AppendLine("AND emp_id=@emp_id ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            VO.ActivityRegistVO myActivityRegistVO = new ACMS.VO.ActivityRegistVO();

            while (MyDataReader.Read())
            {
                myActivityRegistVO.id = (int)MyDataReader["id"];
                myActivityRegistVO.activity_id = (Guid)MyDataReader["activity_id"];
                myActivityRegistVO.emp_id = (string)MyDataReader["emp_id"];
                myActivityRegistVO.regist_by = (string)MyDataReader["regist_by"];
                myActivityRegistVO.idno_type = (int)MyDataReader["idno_type"];
                myActivityRegistVO.idno = (string)MyDataReader["idno"];
                myActivityRegistVO.ext_people = (int?)(MyDataReader["ext_people"] == DBNull.Value ? null : MyDataReader["ext_people"]);
                myActivityRegistVO.createat = (DateTime)MyDataReader["createat"];
                myActivityRegistVO.check_status = (int)MyDataReader["check_status"];
            }
            MyDataReader.Close();
            aconn.Close();
            return myActivityRegistVO;

        }


        //取得報名資訊-團隊活動 登入者帳號-團長帳號 找出報名資訊
        /// <summary>
        /// 取得報名資訊-團隊活動 登入者帳號-團長帳號 找出報名資訊
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="member_id">員工</param>
        /// <returns>取得報名資訊-團隊活動 登入者帳號-團長帳號 找出報名資訊</returns>
        public VO.ActivityRegistVO SelectActivityRegistByMemberID(Guid activity_id, string member_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@member_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = member_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM ActivityRegist ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND activity_id=@activity_id ");
            sb.AppendLine("AND emp_id=(SELECT boss_id FROM ActivityTeamMember WHERE emp_id=@member_id and activity_id=@activity_id) ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            VO.ActivityRegistVO myActivityRegistVO = new ACMS.VO.ActivityRegistVO();

            while (MyDataReader.Read())
            {
                myActivityRegistVO.id = (int)MyDataReader["id"];
                myActivityRegistVO.activity_id = (Guid)MyDataReader["activity_id"];
                myActivityRegistVO.emp_id = (string)MyDataReader["emp_id"];
                myActivityRegistVO.regist_by = (string)MyDataReader["regist_by"];
                myActivityRegistVO.idno = (string)MyDataReader["idno"];
                myActivityRegistVO.team_name = (string)MyDataReader["team_name"];
                myActivityRegistVO.ext_people = (int?)(MyDataReader["ext_people"] == DBNull.Value ? null : MyDataReader["ext_people"]);
                myActivityRegistVO.createat = (DateTime)MyDataReader["createat"];
                myActivityRegistVO.check_status = (int)MyDataReader["check_status"];
            }
            MyDataReader.Close();
            aconn.Close();
            return myActivityRegistVO;

        }


        //廢止
        //取得報名資訊-為了組成個人固定欄位-廢止
        /// <summary>
        /// 取得報名資訊-為了組成個人固定欄位-廢止
        /// </summary>
        /// <param name="id">活動代號</param>
        /// <returns>取得報名資訊-為了組成個人固定欄位-廢止</returns>
        public VO.ActivityRegistVO SelectActivityRegistByID(Guid id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM ActivityRegist ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND id=@id ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            VO.ActivityRegistVO myActivityRegistVO = new ACMS.VO.ActivityRegistVO();

            while (MyDataReader.Read())
            {
                myActivityRegistVO.id = (int)MyDataReader["id"];
                myActivityRegistVO.activity_id = (Guid)MyDataReader["activity_id"];
                myActivityRegistVO.emp_id = (string)MyDataReader["emp_id"];
                myActivityRegistVO.regist_by = (string)MyDataReader["regist_by"];
                myActivityRegistVO.idno = (string)MyDataReader["idno"];
                myActivityRegistVO.ext_people = (int?)(MyDataReader["ext_people"] == DBNull.Value ? null : MyDataReader["ext_people"]);
                myActivityRegistVO.createat = (DateTime)MyDataReader["createat"];
                myActivityRegistVO.check_status = (int)MyDataReader["check_status"];

            }
            MyDataReader.Close();
            aconn.Close();
            return myActivityRegistVO;

        }

        //檢查是否重複報名(個人,團隊)
        /// <summary>
        /// 檢查是否重複報名(個人,團隊)
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <param name="boss_id">隊長</param>
        /// <param name="activity_type">活動類別</param>
        /// <returns>0=未報名,0以上有報名</returns>
        public int IsPersonRegisted(Guid activity_id, string emp_id, string boss_id, string activity_type)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar,-1);
            sqlParams[1].Value = emp_id;
            sqlParams[2] = new SqlParameter("@boss_id", SqlDbType.NVarChar, 100);
            sqlParams[2].Value = boss_id;

            StringBuilder sb = new StringBuilder();

            if (activity_type == "1")
            {
                sb.AppendLine("SELECT COUNT(*) ");
                sb.AppendLine("FROM ActivityRegist ");
                sb.AppendLine("WHERE 1=1 ");
                sb.AppendLine("AND activity_id=@activity_id ");
                sb.AppendLine("AND emp_id=@emp_id ");
            }
            else
            {
                sb.AppendLine("SELECT COUNT(*) ");
                sb.AppendLine("FROM ActivityTeamMember A ");
                sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
                sb.AppendLine("WHERE 1=1 ");
                sb.AppendLine("AND A.activity_id=@activity_id ");
                sb.AppendLine("AND A.emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')) ");
                sb.AppendLine("AND A.boss_id<>@boss_id ");
            }

            return (int)SqlHelper.ExecuteScalar(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }

        //檢查是否重複報名(團隊報名時按了下一步時的檢查)
        /// <summary>
        /// 檢查是否重複報名(團隊報名時按了下一步時的檢查)
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <param name="boss_id">隊長</param>
        /// <returns>檢查是否重複報名(團隊報名時按了下一步時的檢查)</returns>
        public string IsTeamRegisted(Guid activity_id, string emp_id, string boss_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, -1);
            sqlParams[1].Value = emp_id;
            sqlParams[2] = new SqlParameter("@boss_id", SqlDbType.NVarChar, 100);
            sqlParams[2].Value = boss_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT top 1 B.NATIVE_NAME ");
            sb.AppendLine("FROM ActivityTeamMember A ");
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND A.activity_id=@activity_id ");
            sb.AppendLine("AND A.emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')) ");
            sb.AppendLine("AND A.boss_id<>@boss_id ");

            return (string)SqlHelper.ExecuteScalar(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }

        //檢查是否已額滿(個人,團隊)
        /// <summary>
        /// 檢查是否已額滿(個人,團隊)
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>檢查是否已額滿(個人,團隊)</returns>
        public int RegistableCount(Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT (ISNULL(A.limit_count,0)+ISNULL(A.limit2_count,0)) - COUNT(B.id) ");
            sb.AppendLine("FROM Activity A ");
            sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("and A.id=@activity_id ");
           // sb.AppendLine("and ISNULL(B.check_status,0)>=0 ");
            sb.AppendLine("group by  (ISNULL(A.limit_count,0)+ISNULL(A.limit2_count,0)) ");

            return (int)SqlHelper.ExecuteScalar(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }

        //新增報名或更新報名資訊
        /// <summary>
        /// 新增報名或更新報名資訊
        /// </summary>
        /// <param name="myActivityRegistVO">報名人員資料型別物件</param>
        /// <param name="myCustomFieldValueVOList">自訂欄位型別物</param>
        /// <param name="myActivityTeamMemberVOList">團隊報名隊員型別物件</param>
        /// <param name="type">insert=新增,else修改</param>
        /// <param name="activity_type">活動類別</param>
        /// <param name="webPath">網址到根目錄</param>
        /// <param name="path">附加檔案目錄</param>
        /// <returns>新增報名或更新報名資訊</returns>
        public int UpdateActivityRegist(VO.ActivityRegistVO myActivityRegistVO, List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList, List<ACMS.VO.ActivityTeamMemberVO> myActivityTeamMemberVOList, string type, string activity_type, string webPath,string path )
        {
            string empidnew="" ;
            SqlParameter[] sqlParams = new SqlParameter[8];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = myActivityRegistVO.id;
            sqlParams[1] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[1].Value = myActivityRegistVO.activity_id;
            sqlParams[2] = new SqlParameter("@emp_id", SqlDbType.NVarChar, -1);
            sqlParams[2].Value = myActivityRegistVO.emp_id;
            sqlParams[3] = new SqlParameter("@regist_by", SqlDbType.NVarChar, 100);
            sqlParams[3].Value = myActivityRegistVO.regist_by;
            sqlParams[4] = new SqlParameter("@idno_type", SqlDbType.Int);
            sqlParams[4].Value = myActivityRegistVO.idno_type;
            sqlParams[5] = new SqlParameter("@idno", SqlDbType.NVarChar, 20);
            sqlParams[5].Value = myActivityRegistVO.idno;
            sqlParams[6] = new SqlParameter("@team_name", SqlDbType.NVarChar, 100);
            sqlParams[6].Value = myActivityRegistVO.team_name;
            sqlParams[7] = new SqlParameter("@ext_people", SqlDbType.Int);
            sqlParams[7].Value = myActivityRegistVO.ext_people;

            StringBuilder sb = new StringBuilder();

            List<string> ListOriginMembers = new List<string>();
            string strNewEmp_idList = "";

            //團隊時報名者是多人
            if (myActivityTeamMemberVOList != null)
            {
                //傳入團長取得團隊所有成員(List<string>)
                ListOriginMembers = AllTeamMemberByBoss(myActivityRegistVO.activity_id, myActivityRegistVO.regist_by);

                foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in myActivityTeamMemberVOList)
                {
                    strNewEmp_idList += string.Format("{0},", myActivityTeamMemberVO.emp_id);
                }

                if (strNewEmp_idList.EndsWith(","))
                {
                    strNewEmp_idList = strNewEmp_idList.Substring(0, strNewEmp_idList.Length - 1);
                }
            }

            if (type == "insert")
            {
                sb.AppendLine("INSERT ActivityRegist ");
                sb.AppendLine("([activity_id],[emp_id],[regist_by],[idno_type],[idno],[team_name],[ext_people],[createat],[check_status]) ");
                sb.AppendLine("SELECT ");
                sb.AppendLine("@activity_id,@emp_id,@regist_by,@idno_type,@idno,@team_name,@ext_people,getdate(),0 ");
                sb.AppendLine("where 1=1 ");

                //沒有重複報名
                if (activity_type == "1")
                {
                    //個人
                    sb.AppendLine("and not exists(SELECt * FROM ActivityRegist where activity_id=@activity_id and emp_id=@emp_id) ");
                }
                else
                {
                    //團隊
                    sb.AppendLine("and not exists( ");
                    sb.AppendLine("SELECT * ");
                    sb.AppendLine("FROM ActivityTeamMember A ");
                    sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
                    sb.AppendLine("WHERE 1=1 ");
                    sb.AppendLine("AND A.activity_id=@activity_id ");
                    sb.AppendLine(string.Format("AND A.emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',',')) ", strNewEmp_idList));
                    sb.AppendLine("AND A.boss_id<>@regist_by ");
                    sb.AppendLine(") ");
                }

                //沒有額滿
                sb.AppendLine("and  exists( ");
                sb.AppendLine("SELECT A.id ");
                sb.AppendLine("FROM Activity A ");
                sb.AppendLine("left join ActivityRegist B on A.id=B.activity_id");
                sb.AppendLine("WHERE 1=1 ");
                sb.AppendLine("and A.id=@activity_id ");
                sb.AppendLine("and ISNULL(B.check_status,0)>=0 ");
                sb.AppendLine("group by A.id,ISNULL(A.limit_count,0),ISNULL(A.limit2_count,0) ");
                sb.AppendLine("having (ISNULL(A.limit_count,0)+ISNULL(A.limit2_count,0)) - COUNT(B.id)>0 ");
                sb.AppendLine(") ");

            }
            else
            {
                //編輯資料

                //團員只異動ActivityTeamMember裡的個人資料
                if (myActivityRegistVO.emp_id != myActivityRegistVO.regist_by && myActivityTeamMemberVOList != null )
                {
                    ACMS.VO.ActivityTeamMemberVO MyObj = myActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO e) { return e.emp_id == myActivityRegistVO.emp_id; });

                    sb.Length = 0;
                    sb.AppendLine("UPDATE ActivityTeamMember ");
                    sb.AppendLine(string.Format("set idno_type={0} ", MyObj.idno_type));
                    sb.AppendLine(string.Format(",idno='{0}' ", MyObj.idno));
                    sb.AppendLine(string.Format(",remark='{0}' ", MyObj.remark));
                    sb.AppendLine(string.Format("WHERE activity_id='{0}' and emp_id='{1}' ", myActivityRegistVO.activity_id, myActivityRegistVO.emp_id));

                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = MyConn();
                    cmd.Connection.Open();
                    cmd.CommandText = sb.ToString();
                    cmd.Parameters.Clear();

                    return cmd.ExecuteNonQuery();

                }

                //以下是團長的異動
                sb.Length = 0;
                sb.AppendLine("UPDATE ActivityRegist ");
                //sb.AppendLine("set activity_id=@activity_id ");
                //sb.AppendLine(",emp_id=@emp_id ");
                //sb.AppendLine(",regist_by=@regist_by ");
                sb.AppendLine("set idno_type=@idno_type ");
                sb.AppendLine(",idno=@idno ");
                sb.AppendLine(",team_name=@team_name ");
                sb.AppendLine(",ext_people=@ext_people ");
                sb.AppendLine("WHERE activity_id=@activity_id and emp_id=@regist_by ");

                //沒有重複報名
                if (activity_type == "2")
                {
                    sb.AppendLine("and not exists( ");
                    sb.AppendLine("SELECT * ");
                    sb.AppendLine("FROM ActivityTeamMember A ");
                    sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
                    sb.AppendLine("WHERE 1=1 ");
                    sb.AppendLine("AND A.activity_id=@activity_id ");
                    sb.AppendLine(string.Format("AND A.emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',',')) ", strNewEmp_idList));
                    sb.AppendLine("AND A.boss_id<>@regist_by ");
                    sb.AppendLine(") ");
                }
            }

            //using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
            //{

            using (SqlConnection myConn = MyConn())
            {
                myConn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = myConn;
                    cmd.CommandText = sb.ToString();
                    cmd.Parameters.AddRange(sqlParams);

                    int intResult = cmd.ExecuteNonQuery();

                    //變更成功就 1.改自訂欄位 2.重製ActivityTeamMember(團隊)
                    if (intResult > 0)
                    {
                        //改自訂欄位:先全刪再新增
                        sb.Length = 0;
                        sb.AppendLine("DELETE A ");
                        sb.AppendLine("FROM CustomFieldValue A ");

                        if (activity_type == "1")
                        {
                            sb.AppendLine(string.Format("inner join CustomField B on A.field_id=B.field_id and A.emp_id='{0}' and B.activity_id='{1}'; ", myActivityRegistVO.emp_id, myActivityRegistVO.activity_id));
                        }
                        else
                        {
                            //團隊報名時自訂欄位用團長的ID當代表
                            sb.AppendLine(string.Format("inner join CustomField B on A.field_id=B.field_id and A.emp_id='{0}' and B.activity_id='{1}'; ", myActivityRegistVO.regist_by, myActivityRegistVO.activity_id));
                        }

                        cmd.CommandText = sb.ToString();
                        cmd.Parameters.Clear();
                        cmd.ExecuteNonQuery();

                        foreach (ACMS.VO.CustomFieldValueVO myCustomFieldValueVO in myCustomFieldValueVOList)
                        {
                            SqlParameter[] sqlParams2 = new SqlParameter[4];

                            sqlParams2[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                            sqlParams2[0].Value = myCustomFieldValueVO.id;
                            sqlParams2[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
                            sqlParams2[1].Value = myCustomFieldValueVO.emp_id;
                            sqlParams2[2] = new SqlParameter("@field_id", SqlDbType.Int);
                            sqlParams2[2].Value = myCustomFieldValueVO.field_id;
                            sqlParams2[3] = new SqlParameter("@field_value", SqlDbType.NVarChar, 200);
                            sqlParams2[3].Value = myCustomFieldValueVO.field_value;

                            StringBuilder sb2 = new StringBuilder();

                            sb2.AppendLine("INSERT CustomFieldValue ");
                            sb2.AppendLine("VALUES ");
                            sb2.AppendLine("(@id,@emp_id,@field_id,@field_value) ");

                            cmd.CommandText = sb2.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(sqlParams2);
                            cmd.ExecuteNonQuery();

                        }

                        //重製ActivityTeamMember
                        if (myActivityTeamMemberVOList != null)
                        {
                            List<string> ListNewEmp_id;

                            ListNewEmp_id = new List<string>(strNewEmp_idList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                            //===========================================
                            //新成員若不在原始成員資料表就要寄報名成功信
                            //===========================================
                            ListNewEmp_id = new List<string>(strNewEmp_idList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                            ListNewEmp_id.RemoveAll(delegate(string e) { return ListOriginMembers.Contains(e); });
                            //andy-報名成功寄信
                            clsMyObj.RegistSuccess_Team(myActivityRegistVO.activity_id.ToString(), string.Join(",", ListNewEmp_id.ToArray()), myActivityRegistVO.regist_by, webPath,path );

                            //===========================================
                            //舊成員若不在原始成員名單就要寄取消報名信
                            //===========================================
                            //ListNewEmp_id有改變，所以要重讀一次
                            ListNewEmp_id = new List<string>(strNewEmp_idList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                            ListOriginMembers.RemoveAll(delegate(string e) { return ListNewEmp_id.Contains(e); });

                            //andy-取消報名寄信
                            //clsMyObj.CancelRegist(myActivityRegistVO.activity_id.ToString(), string.Join(",", ListOriginMembers.ToArray()), myActivityRegistVO.regist_by,webPath);
                            //2011/3/28 日 add 取消人需發信及加到取消人員清冊中
                            foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in myActivityTeamMemberVOList)
                            {
                                empidnew +=myActivityTeamMemberVO.emp_id+  ",";
                            }
                            empidnew = empidnew.Substring(0, empidnew.Length - 1);
                            InsertActivityRegistCancelTeamMember(myActivityTeamMemberVOList[0].activity_id ,empidnew,clsAuth.ID );
                            //=======================================================================================

                            sb.Length = 0;
                            sb.AppendLine("DELETE A ");
                            sb.AppendLine("FROM ActivityTeamMember A ");
                            sb.AppendLine(string.Format("WHERE A.boss_id='{0}' and A.activity_id='{1}'; ", myActivityRegistVO.regist_by, myActivityRegistVO.activity_id));

                            cmd.CommandText = sb.ToString();
                            cmd.Parameters.Clear();
                            cmd.ExecuteNonQuery();

                            foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in myActivityTeamMemberVOList)
                            {
                                SqlParameter[] sqlParams3 = new SqlParameter[7];

                                sqlParams3[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
                                sqlParams3[0].Value = myActivityTeamMemberVO.activity_id;
                                sqlParams3[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
                                sqlParams3[1].Value = myActivityTeamMemberVO.emp_id;
                                sqlParams3[2] = new SqlParameter("@boss_id", SqlDbType.NVarChar, 100);
                                sqlParams3[2].Value = myActivityTeamMemberVO.boss_id;
                                sqlParams3[3] = new SqlParameter("@idno_type", SqlDbType.SmallInt);
                                sqlParams3[3].Value = myActivityTeamMemberVO.idno_type;
                                sqlParams3[4] = new SqlParameter("@idno", SqlDbType.NVarChar, 20);
                                sqlParams3[4].Value = myActivityTeamMemberVO.idno;
                                sqlParams3[5] = new SqlParameter("@remark", SqlDbType.NVarChar, 500);
                                sqlParams3[5].Value = myActivityTeamMemberVO.remark;
                                sqlParams3[6] = new SqlParameter("@check_status", SqlDbType.Int);
                                sqlParams3[6].Value = myActivityTeamMemberVO.check_status;

                                StringBuilder sb3 = new StringBuilder();

                                sb3.AppendLine("INSERT ActivityTeamMember ");
                                sb3.AppendLine("VALUES ");
                                sb3.AppendLine("(@activity_id,@emp_id,@boss_id,@idno_type,@idno,@remark,@check_status) ");

                                cmd.CommandText = sb3.ToString();
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(sqlParams3);
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    return 0;
                }

                //}

                //trans.Complete();
            }

            return 1;

        }

        //取消報名-刪除
        /// <summary>
        /// 取消報名-刪除
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <param name="activity_type">活動類別</param>
        /// <param name="webPath">網址根目錄</param>
        /// <returns>取消報名-刪除</returns>
        public int DeleteRegist(Guid activity_id, string emp_id, string activity_type, string webPath)
        {
            //先取得團隊所
            string OriginMembers = AllTeamMemberByMembers(activity_id, emp_id);

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NText);
            sqlParams[1].Value = emp_id;
          

            StringBuilder sb = new StringBuilder();
            if (activity_type == "2")
            {


                InsertActivityRegistCancel(activity_id, emp_id, "2", clsAuth.ID);
            }
            if (activity_type == "1")
            {
                sb.AppendLine("DELETE ActivityRegist WHERE activity_id=@activity_id and emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')); ");
                sb.AppendLine("DELETE A FROM CustomFieldValue A inner join CustomField B on A.field_id=B.field_id WHERE B.activity_id=@activity_id and A.emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')); ");
            }
            else
            {
                sb.AppendLine("DELETE ActivityTeamMember WHERE activity_id=@activity_id and emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')); ");
                sb.AppendLine("DELETE A FROM CustomFieldValue A inner join CustomField B on A.field_id=B.field_id WHERE B.activity_id=@activity_id and A.emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')); ");
            }

            //using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
            //{
            using (SqlConnection myConn = MyConn())
            {
                myConn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = myConn;
                    cmd.CommandText = sb.ToString();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParams);
                    int delResult = cmd.ExecuteNonQuery();

                    if (activity_type == "2")
                    {
                        //2011/3/28日要先加到取消報名人員
                       

                        //============================2011/3/28 add 
                        sb.Length = 0;

                        //若團隊人數低於門檻則團隊消滅
                        sb.AppendLine(string.Format("Select count(*) from ActivityRegist WHERE activity_id=@activity_id and emp_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',','))) ", OriginMembers));//原本的emp_id已被刪除所以要用OriginMembers
                        //若低於門檻
                        sb.AppendLine("and  exists( ");
                        sb.AppendLine("select A.team_member_min,COUNT(B.emp_id)");
                        sb.AppendLine("from Activity A");
                        sb.AppendLine(string.Format("inner join ActivityTeamMember B on A.id=B.activity_id and A.id=@activity_id and boss_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',','))) and B.check_status>=0", OriginMembers));//原本的emp_id已被刪除所以要用OriginMembers
                        sb.AppendLine("GROUP BY A.team_member_min");
                        sb.AppendLine("having  A.team_member_min>COUNT(B.emp_id)");
                        sb.AppendLine(") ");

                        cmd.CommandText = sb.ToString();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(sqlParams);
                        if (cmd.ExecuteScalar().ToString() != "0")
                        {
                            InsertActivityRegistCancel(activity_id, OriginMembers, "2", clsAuth.ID);
                            //團隊瓦解要寄信給所有人
                            clsMyObj.CancelRegist_Team(activity_id.ToString(), OriginMembers, clsAuth.ID, webPath);

                        }
                        //================================================


                        sb.Length = 0;

                        //若團隊人數低於門檻則團隊消滅
                        sb.AppendLine(string.Format("DELETE ActivityRegist WHERE activity_id=@activity_id and emp_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',','))) ", OriginMembers));//原本的emp_id已被刪除所以要用OriginMembers
                        //若低於門檻
                        sb.AppendLine("and  exists( ");
                        sb.AppendLine("select A.team_member_min,COUNT(B.emp_id)");
                        sb.AppendLine("from Activity A");
                        sb.AppendLine(string.Format("inner join ActivityTeamMember B on A.id=B.activity_id and A.id=@activity_id and boss_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',','))) and B.check_status>=0", OriginMembers));//原本的emp_id已被刪除所以要用OriginMembers
                        sb.AppendLine("GROUP BY A.team_member_min");
                        sb.AppendLine("having  A.team_member_min>COUNT(B.emp_id)");
                        sb.AppendLine(") ");

                        cmd.CommandText = sb.ToString();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(sqlParams);
                        int intDeleteAll = cmd.ExecuteNonQuery();

                        if (intDeleteAll > 0)
                        {
                            sb.Length = 0;
                            //ActivityTeamMember的所有成員也要全部刪除
                            sb.AppendLine(string.Format("DELETE ActivityTeamMember WHERE activity_id=@activity_id and boss_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',','))) ", OriginMembers));//原本的emp_id已被刪除所以要用OriginMembers

                            cmd.CommandText = sb.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(sqlParams);
                            cmd.ExecuteNonQuery();
                            // insert into 取消人員名單
                            //InsertActivityRegistCancel(activity_id, OriginMembers, "2", clsAuth.ID);
                            //團隊瓦解要寄信給所有人
                            clsMyObj.CancelRegist_Team(activity_id.ToString(), OriginMembers, clsAuth.ID, webPath);
                        }
                        else
                        {
                            // insert into 取消人員名單
                           // InsertActivityRegistCancel(activity_id, emp_id, "2", clsAuth.ID);
                            //一般取消報名寄給取消的那些人
                            clsMyObj.CancelRegist_Team(activity_id.ToString(), emp_id, clsAuth.ID, webPath);
                        }

                    }

                }
                catch (Exception ex)
                {
                    return 0;
                }
                //}

                //trans.Complete();
            }
            return 1;
        }

        //取消報名-狀態改取消
        /// <summary>
        /// 取消報名-狀態改取消
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <param name="activity_type">活動代號</param>
        /// <param name="webPath">網址根目錄</param>
        /// <returns>取消報名-狀態改取消</returns>
        public int CancelRegist(Guid activity_id, string emp_id, string activity_type, string webPath)
        {
            //先取得團隊所有成員(用逗號隔開)，因為若團隊會消滅的話要寄給所有成員
            string OriginMembers = AllTeamMemberByMembers(activity_id, emp_id);

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NText);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            if (activity_type == "1")
            {
                sb.AppendLine("UPDATE ActivityRegist SET check_status=-1 WHERE activity_id=@activity_id and emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')); ");
            }
            else
            {
                sb.AppendLine("UPDATE ActivityTeamMember SET check_status=-1 WHERE activity_id=@activity_id and emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')); ");
            }

            using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
            {
                using (SqlConnection myConn = MyConn())
                {
                    myConn.Open();

                    try
                    {
                        SqlCommand cmd = new SqlCommand();

                        cmd.Connection = myConn;
                        cmd.CommandText = sb.ToString();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(sqlParams);
                        cmd.ExecuteNonQuery();

                        if (activity_type == "2")
                        {
                            //2011/3/28日要先加到取消報名人員
                            InsertActivityRegistCancel(activity_id, emp_id, "2", clsAuth.ID);

                            //============================2011/3/28 add 
                            sb.Length = 0;

                            //若團隊人數低於門檻則團隊消滅
                            sb.AppendLine(string.Format("Seclect count(*) from ActivityRegist WHERE activity_id=@activity_id and emp_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',','))) ", OriginMembers));//原本的emp_id已被刪除所以要用OriginMembers
                            //若低於門檻
                            sb.AppendLine("and  exists( ");
                            sb.AppendLine("select A.team_member_min,COUNT(B.emp_id)");
                            sb.AppendLine("from Activity A");
                            sb.AppendLine(string.Format("inner join ActivityTeamMember B on A.id=B.activity_id and A.id=@activity_id and boss_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split('{0}',','))) and B.check_status>=0", OriginMembers));//原本的emp_id已被刪除所以要用OriginMembers
                            sb.AppendLine("GROUP BY A.team_member_min");
                            sb.AppendLine("having  A.team_member_min>COUNT(B.emp_id)");
                            sb.AppendLine(") ");

                            cmd.CommandText = sb.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(sqlParams);
                            if (cmd.ExecuteScalar().ToString() != "0")
                            {
                                InsertActivityRegistCancel(activity_id, OriginMembers, "2", clsAuth.ID);
                            }
                            //================================================

                            sb.Length = 0;

                            //若團隊人數低於門檻則團隊消滅
                            sb.AppendLine("UPDATE ActivityRegist SET check_status=-1 WHERE activity_id=@activity_id and emp_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,','))) ");
                            //若低於門檻
                            sb.AppendLine("and  exists( ");
                            sb.AppendLine("select A.team_member_min,COUNT(B.emp_id)");
                            sb.AppendLine("from Activity A");
                            sb.AppendLine("inner join ActivityTeamMember B on A.id=B.activity_id and A.id=@activity_id and boss_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,','))) and B.check_status>=0");
                            sb.AppendLine("GROUP BY A.team_member_min");
                            sb.AppendLine("having  A.team_member_min>COUNT(B.emp_id)");
                            sb.AppendLine("); ");

                            cmd.CommandText = sb.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(sqlParams);
                            int intCancelAll = cmd.ExecuteNonQuery();

                            if (intCancelAll > 0)
                            {
                                sb.Length = 0;
                                //ActivityTeamMember的所有成員也要全部取消
                                sb.AppendLine("UPDATE ActivityTeamMember SET check_status=-1 WHERE activity_id=@activity_id and boss_id in (SELECT distinct boss_id FROM ActivityTeamMember WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,','))) ");

                                cmd.CommandText = sb.ToString();
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(sqlParams);
                                cmd.ExecuteNonQuery();
                                InsertActivityRegistCancel(activity_id, OriginMembers, "2", clsAuth.ID);
                                //andy-團隊瓦解要寄信給所有人
                                clsMyObj.CancelRegist_Team(activity_id.ToString(), OriginMembers, clsAuth.ID, webPath);
                            }
                            else
                            {
                                //andy-一般取消報名寄給取消的那些人
                                InsertActivityRegistCancel(activity_id, emp_id, "2", clsAuth.ID);
                                clsMyObj.CancelRegist_Team(activity_id.ToString(), emp_id, clsAuth.ID, webPath);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }

                trans.Complete();
            }
            return 1;
        }



        //隊員取得團隊所有成員(用逗號隔開)
        /// <summary>
        /// 隊員取得團隊所有成員(用逗號隔開)
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <returns>隊員取得團隊所有成員(用逗號隔開)</returns>
        public string AllTeamMemberByMembers(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, -1);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT emp_id ");
            sb.AppendLine("FROM ActivityTeamMember ");
            sb.AppendLine("where activity_id=@activity_id and boss_id in ( ");
            sb.AppendLine("SELECT distinct boss_id ");
            sb.AppendLine("FROM ActivityTeamMember ");
            sb.AppendLine("WHERE emp_id in (SELECT * FROM dbo.UTILfn_Split(@emp_id,',')) ");
            sb.AppendLine(") ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            List<string> myList = new List<string>();

            while (MyDataReader.Read())
            {
                myList.Add((string)MyDataReader["emp_id"]);
            }
            MyDataReader.Close();
            aconn.Close();
            return string.Join(",", myList.ToArray());
        }

        //傳入團長取得團隊所有成員(List<string>)
        /// <summary>
        /// 傳入團長取得團隊所有成員(List<string>)
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="boss_id">隊長</param>
        /// <returns>傳入團長取得團隊所有成員(List<string>)</returns>
        public List<string> AllTeamMemberByBoss(Guid activity_id, string boss_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@boss_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = boss_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT emp_id ");
            sb.AppendLine("FROM ActivityTeamMember ");
            sb.AppendLine("where activity_id=@activity_id and boss_id =@boss_id ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            List<string> myList = new List<string>();

            while (MyDataReader.Read())
            {
                myList.Add((string)MyDataReader["emp_id"]);
            }
            MyDataReader.Close();
            aconn.Close();
            return myList;
        }
        /// <summary>
        /// 取得報名後的報名順序
        /// </summary>
        /// <param name="activity">活動代號</param>
        /// <returns>取得報名後的報名順序</returns>
        public string getSNByActivity(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("  Declare @createat datetime,@Count int ,@teamLimit int ");
            sb.AppendLine("  select  @createat=createat from  ActivityRegist  ");
            sb.AppendLine("  where activity_id =@activity_id and emp_id =@emp_id");
            sb.AppendLine("  select  @Count=COUNT(activity_id) from  ActivityRegist ");
            sb.AppendLine("  where   activity_id =@activity_id and createat <=@createat ");
            sb.AppendLine("  select @teamLimit =  isnull(team_member_max,0) +isnull(limit_count ,0) from Activity ");
            sb.AppendLine("  where id =@activity_id ");
           // sb.AppendLine("  select @Count as SumOfRegisted ,@teamLimit as TeamLimit  ");
            sb.AppendLine("  if @Count<=@teamLimit ");
            sb.AppendLine("  select '正取:'+CONVERT(varchar(100),@Count)");
            sb.AppendLine("  else  select '備取:'+CONVERT(varchar(100),@Count-@teamLimit)");
            return SqlHelper.ExecuteScalar(MyConn(),CommandType.Text ,sb.ToString(),sqlParams).ToString();

            
        }
        
        /// <summary>
        /// 取得取消名單清冊
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="name">員工或隊長的中英文</param>
        /// <returns>取得取消名單清冊</returns>
        public DataTable GetCancelRegist(Guid activity_id, string name)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = name;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" Select replace( convert(varchar(20),createat,120),'-','/') as createat,replace(convert(varchar(20),cancel_date,120),'-','/') as cancel_date, A.* from ActivityRegistCancel A left join V_ACSM_USER2 B on A.emp_id =B.ID   left join V_ACSM_USER2 C on A.boss_id =C.ID  where A.activity_id=@activity_id ");
            sb.AppendLine(" and( A.emp_id like '%'+@name+'%' or A.boss_id like '%'+@name +'%'  or B.ENGLISH_NAME like  '%'+@name +'%'  or   C.ENGLISH_NAME like  '%'+@name +'%'   )");
            return SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams).Tables[0];

        }
    }

}
  