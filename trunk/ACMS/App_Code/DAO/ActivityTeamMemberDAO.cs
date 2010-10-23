using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class ActivityTeamMemberDAO : BaseDAO
    {

        public List<VO.ActivityTeamMemberVO> SelectActivityTeamMember(Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM ActivityTeamMember ");
            sb.AppendLine("WHERE activity_id=@activity_id ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.ActivityTeamMemberVO> myActivityTeamMemberVOList = new List<ACMS.VO.ActivityTeamMemberVO>();

            while (MyDataReader.Read())
            {
                VO.ActivityTeamMemberVO myActivityTeamMemberVO = new ACMS.VO.ActivityTeamMemberVO();

                myActivityTeamMemberVO.activity_id = (Guid)MyDataReader["activity_id"];
                myActivityTeamMemberVO.emp_id = (string)MyDataReader["emp_id"];
                myActivityTeamMemberVO.boss_id = (string)MyDataReader["boss_id"];
                myActivityTeamMemberVO.idno = (string)MyDataReader["idno"];
                myActivityTeamMemberVO.remark = (string)MyDataReader["remark"];
                myActivityTeamMemberVO.check_status = (int)MyDataReader["check_status"];

                myActivityTeamMemberVOList.Add(myActivityTeamMemberVO);

            }

            return myActivityTeamMemberVOList;

        }

   

    }
}
