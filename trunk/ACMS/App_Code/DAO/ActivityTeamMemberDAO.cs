using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class ActivityTeamMemberDAO : BaseDAO
    {
        //編輯團隊活動時，要帶入該團隊的所有成員
        /// <summary>
        /// 編輯團隊活動時，要帶入該團隊的所有成員
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="RegistBy">報名人</param>
        /// <returns>編輯團隊活動時，要帶入該團隊的所有成員</returns>
        public List<VO.ActivityTeamMemberVO> SelectActivityTeamMember(Guid activity_id, string RegistBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@RegistBy", SqlDbType.NVarChar, 200);
            sqlParams[1].Value = RegistBy;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.*,B.WORK_ID,B.NATIVE_NAME,B.C_DEPT_NAME,B.C_DEPT_ABBR ");
            sb.AppendLine("FROM ActivityTeamMember A ");
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE A.activity_id=@activity_id ");
            sb.AppendLine("and A.boss_id=@RegistBy ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            List<VO.ActivityTeamMemberVO> myActivityTeamMemberVOList = new List<ACMS.VO.ActivityTeamMemberVO>();

            while (MyDataReader.Read())
            {
                VO.ActivityTeamMemberVO myActivityTeamMemberVO = new ACMS.VO.ActivityTeamMemberVO();

                myActivityTeamMemberVO.activity_id = (Guid)MyDataReader["activity_id"];
                myActivityTeamMemberVO.emp_id = (string)MyDataReader["emp_id"];
                myActivityTeamMemberVO.boss_id = (string)MyDataReader["boss_id"];
                myActivityTeamMemberVO.idno_type = Convert.ToInt16(MyDataReader["idno_type"]);
                myActivityTeamMemberVO.idno = (string)MyDataReader["idno"];
                myActivityTeamMemberVO.remark = (string)MyDataReader["remark"];
                myActivityTeamMemberVO.check_status = (int)MyDataReader["check_status"];
                myActivityTeamMemberVO.WritePersonInfo = "是";

                myActivityTeamMemberVO.WORK_ID = (string)MyDataReader["WORK_ID"];
                myActivityTeamMemberVO.NATIVE_NAME = (string)MyDataReader["NATIVE_NAME"];
                myActivityTeamMemberVO.C_DEPT_ABBR = (string)MyDataReader["C_DEPT_ABBR"];
                myActivityTeamMemberVO.C_DEPT_NAME = (string)MyDataReader["C_DEPT_NAME"];
                myActivityTeamMemberVOList.Add(myActivityTeamMemberVO);

            }
            MyDataReader.Close();
            aconn.Close();
            return myActivityTeamMemberVOList;

        }

        //檢查某人員是否為該隊的隊長
        /// <summary>
        /// 檢查某人員是否為該隊的隊長
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <returns>檢查某人員是否為該隊的隊長</returns>
        public bool IsTeamBoss(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();
            SqlConnection aconn=MyConn ();
            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM ActivityTeamMember A ");
            sb.AppendLine("WHERE activity_id=@activity_id and boss_id=@emp_id ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            if (MyDataReader.HasRows)
            {
                MyDataReader.Close();
                aconn.Close();
                return true;
            }
            else
            {
                MyDataReader.Close();
                aconn.Close();
                return false;
            }

        }

        //變更隊長
        /// <summary>
        /// 變更隊長
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="NewBossID">新隊長</param>
        /// <param name="ExBossID">舊隊長</param>
        public void ChangeBoss(Guid activity_id, string NewBossID, string ExBossID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@NewBossID", SqlDbType.NVarChar, 200);
            sqlParams[1].Value = NewBossID;
            sqlParams[2] = new SqlParameter("@ExBossID", SqlDbType.NVarChar, 200);
            sqlParams[2].Value = ExBossID;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("UPDATE ActivityRegist ");
            sb.AppendLine("set emp_id=@NewBossID,regist_by=@NewBossID ");
            sb.AppendLine("WHERE activity_id=@activity_id and emp_id=@ExBossID; ");

            sb.AppendLine("UPDATE ActivityTeamMember ");
            sb.AppendLine("set boss_id=@NewBossID ");
            sb.AppendLine("WHERE activity_id=@activity_id and boss_id=@ExBossID; ");
            sb.AppendLine("UPDATE CustomFieldValue ");

            sb.AppendLine(" set emp_id=@NewBossID  where field_id in (select field_id from CustomField where activity_id=@activity_id)   and emp_id =@ExBossID ");
            SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }
    
    }
}
