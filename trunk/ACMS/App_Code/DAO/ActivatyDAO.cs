using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class ActivatyDAO : BaseDAO
    {
        /// <summary>
        /// 新增一筆活動資料
        /// </summary>
        /// <param name="myActivatyVO">活動資料型別物件</param>
        /// <returns>新增一筆活動資料</returns>
        public int INSERT_NewOne(VO.ActivatyVO myActivatyVO)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = myActivatyVO.id;
            sqlParams[1] = new SqlParameter("@activity_type", SqlDbType.NChar, 1);
            sqlParams[1].Value = myActivatyVO.activity_type;
            sqlParams[2] = new SqlParameter("@activity_info", SqlDbType.NText);
            sqlParams[2].Value = myActivatyVO.activity_info;

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("INSERT Activity ");
            //sb.AppendLine("([id],[activity_info],[activity_name],[activity_type],[people_type],[activity_startdate],[activity_enddate],[limit_count],[limit2_count],[team_member_max],[team_member_min],[regist_deadline],[cancelregist_deadline],[is_showfile],[is_showprogress],[is_showextpeoplecount],[is_showremark],[is_grouplimit]) ");
            //sb.AppendLine("SELECT ");
            //sb.AppendLine("(@id,@activity_info,@activity_name,@activity_type,@people_type,@activity_startdate,@activity_enddate,@limit_count,@limit2_count,@team_member_max,@team_member_min,@regist_deadline,@cancelregist_deadline,@is_showfile,@is_showprogress,@is_showextpeoplecount,@is_showremark,@is_grouplimit) ");

            sb.AppendLine("INSERT Activity ");
            sb.AppendLine("([id],[activity_type],[activity_info],[org_id],[activity_name],[people_type],[activity_startdate],[activity_enddate],[limit_count],[limit2_count],[team_member_max],[team_member_min],[regist_startdate],[regist_deadline],[cancelregist_deadline],[is_showfile],[is_showprogress],[is_showperson_fix1],[is_showperson_fix2],[personextcount_max],[personextcount_min],[is_showidno],[is_showremark],[remark_name],[is_showteam_fix1],[is_showteam_fix2],[teamextcount_max],[teamextcount_min],[is_grouplimit],[notice],[active]) ");
            sb.AppendLine("VALUES ");
            sb.AppendLine("(@id,@activity_type,'','','','',null,null,null,null,null,null,null,null,null,'N','N','N','N',null,null,'N','N','','N','N',null,null,'N','',null) ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }
        /// <summary>
        /// 取得活動資料
        /// </summary>
        /// <param name="id">活動代號</param>
        /// <returns>取得活動資料</returns>
        public DataTable SelectActivatyDTByID(Guid id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.*,B.name as UnitName ");
            sb.AppendLine("FROM Activity A ");
            sb.AppendLine("left join Unit B on A.org_id=B.id ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND A.id=@id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);      

        }
        /// <summary>
        /// 取得活動資料
        /// </summary>
        /// <param name="id">活動代號</param>
        /// <returns>取得活動資料</returns>
        public VO.ActivatyVO SelectActivatyByID(Guid id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT *");
            sb.AppendLine("FROM Activity ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND id=@id ");
            SqlConnection aconn=MyConn ();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            VO.ActivatyVO myActivatyVO = new ACMS.VO.ActivatyVO();

            while (MyDataReader.Read())
            {
                myActivatyVO.id = (Guid)MyDataReader["id"];
                myActivatyVO.activity_type = (string)MyDataReader["activity_type"];
                myActivatyVO.activity_info = (string)MyDataReader["activity_info"];
                myActivatyVO.org_id = (string)MyDataReader["org_id"];
                myActivatyVO.activity_name = (string)MyDataReader["activity_name"];
                myActivatyVO.people_type = (string)MyDataReader["people_type"];
                myActivatyVO.activity_startdate = (DateTime?)(MyDataReader["activity_startdate"] == DBNull.Value ? null : MyDataReader["activity_startdate"]);
                myActivatyVO.activity_enddate = (DateTime?)(MyDataReader["activity_enddate"] == DBNull.Value ? null : MyDataReader["activity_enddate"]);
                myActivatyVO.limit_count = (int?)(MyDataReader["limit_count"] == DBNull.Value ? null : MyDataReader["limit_count"]);
                myActivatyVO.limit2_count = (int?)(MyDataReader["limit2_count"] == DBNull.Value ? null : MyDataReader["limit2_count"]);
                myActivatyVO.team_member_max = (int?)(MyDataReader["team_member_max"] == DBNull.Value ? null : MyDataReader["team_member_max"]);
                myActivatyVO.team_member_min = (int?)(MyDataReader["team_member_min"] == DBNull.Value ? null : MyDataReader["team_member_min"]);
                myActivatyVO.regist_startdate = (DateTime?)(MyDataReader["regist_startdate"] == DBNull.Value ? null : MyDataReader["regist_startdate"]);
                myActivatyVO.regist_deadline = (DateTime?)(MyDataReader["regist_deadline"] == DBNull.Value ? null : MyDataReader["regist_deadline"]);
                myActivatyVO.cancelregist_deadline = (DateTime?)(MyDataReader["cancelregist_deadline"] == DBNull.Value ? null : MyDataReader["cancelregist_deadline"]);
                myActivatyVO.is_showfile = (string)MyDataReader["is_showfile"];
                myActivatyVO.is_showprogress = (string)MyDataReader["is_showprogress"];
                myActivatyVO.is_showperson_fix1 = (string)MyDataReader["is_showperson_fix1"];
                myActivatyVO.is_showperson_fix2 = (string)MyDataReader["is_showperson_fix2"];
                myActivatyVO.personextcount_max = (int?)(MyDataReader["personextcount_max"] == DBNull.Value ? null : MyDataReader["personextcount_max"]);
                myActivatyVO.personextcount_min = (int?)(MyDataReader["personextcount_min"] == DBNull.Value ? null : MyDataReader["personextcount_min"]);
                myActivatyVO.is_showidno = (string)MyDataReader["is_showidno"];
                myActivatyVO.is_showremark = (string)MyDataReader["is_showremark"];
                myActivatyVO.remark_name = (string)MyDataReader["remark_name"];  
                myActivatyVO.is_showteam_fix1 = (string)MyDataReader["is_showteam_fix1"];
                myActivatyVO.is_showteam_fix2 = (string)MyDataReader["is_showteam_fix2"];
                myActivatyVO.teamextcount_max = (int?)(MyDataReader["teamextcount_max"] == DBNull.Value ? null : MyDataReader["teamextcount_max"]);
                myActivatyVO.teamextcount_min = (int?)(MyDataReader["teamextcount_min"] == DBNull.Value ? null : MyDataReader["teamextcount_min"]);
                myActivatyVO.is_grouplimit = (string)MyDataReader["is_grouplimit"];
                myActivatyVO.notice = (string)MyDataReader["notice"];
             
            }
            
            MyDataReader.Close();
            aconn.Close();
            return myActivatyVO;

        }
        /// <summary>
        /// 修改一筆活動資料
        /// </summary>
        /// <param name="myActivatyVO">活動資料型別物件</param>
        /// <returns>修改一筆活動資料</returns>
        public int UpdateActivaty(VO.ActivatyVO myActivatyVO)
        {
            SqlParameter[] sqlParams = new SqlParameter[31];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = myActivatyVO.id;
            sqlParams[1] = new SqlParameter("@activity_type", SqlDbType.NChar, 1);
            sqlParams[1].Value = myActivatyVO.activity_type;
            sqlParams[2] = new SqlParameter("@activity_info", SqlDbType.NText);
            sqlParams[2].Value = myActivatyVO.activity_info;
            sqlParams[3] = new SqlParameter("@org_id", SqlDbType.NVarChar, 50);
            sqlParams[3].Value = myActivatyVO.org_id;
            sqlParams[4] = new SqlParameter("@activity_name", SqlDbType.NVarChar, 50);
            sqlParams[4].Value = myActivatyVO.activity_name;
            sqlParams[5] = new SqlParameter("@people_type", SqlDbType.NVarChar, 50);
            sqlParams[5].Value = myActivatyVO.people_type;
            sqlParams[6] = new SqlParameter("@activity_startdate", SqlDbType.DateTime);
            sqlParams[6].Value = myActivatyVO.activity_startdate;
            sqlParams[7] = new SqlParameter("@activity_enddate", SqlDbType.DateTime);
            sqlParams[7].Value = myActivatyVO.activity_enddate;
            sqlParams[8] = new SqlParameter("@limit_count", SqlDbType.Int);
            sqlParams[8].Value = myActivatyVO.limit_count;
            sqlParams[9] = new SqlParameter("@limit2_count", SqlDbType.Int);
            sqlParams[9].Value = myActivatyVO.limit2_count;
            sqlParams[10] = new SqlParameter("@team_member_max", SqlDbType.Int);
            sqlParams[10].Value = myActivatyVO.team_member_max;
            sqlParams[11] = new SqlParameter("@team_member_min", SqlDbType.Int);
            sqlParams[11].Value = myActivatyVO.team_member_min;
            sqlParams[12] = new SqlParameter("@regist_startdate", SqlDbType.DateTime);
            sqlParams[12].Value = myActivatyVO.regist_startdate;
            sqlParams[13] = new SqlParameter("@regist_deadline", SqlDbType.DateTime);
            sqlParams[13].Value = myActivatyVO.regist_deadline;
            sqlParams[14] = new SqlParameter("@cancelregist_deadline", SqlDbType.DateTime);
            sqlParams[14].Value = myActivatyVO.cancelregist_deadline;
            sqlParams[15] = new SqlParameter("@is_showfile", SqlDbType.NChar, 1);
            sqlParams[15].Value = myActivatyVO.is_showfile;
            sqlParams[16] = new SqlParameter("@is_showprogress", SqlDbType.NChar, 1);
            sqlParams[16].Value = myActivatyVO.is_showprogress;
            sqlParams[17] = new SqlParameter("@is_showperson_fix1", SqlDbType.NChar, 1);
            sqlParams[17].Value = myActivatyVO.is_showperson_fix1;
            sqlParams[18] = new SqlParameter("@is_showperson_fix2", SqlDbType.NChar, 1);
            sqlParams[18].Value = myActivatyVO.is_showperson_fix2;
            sqlParams[19] = new SqlParameter("@personextcount_max", SqlDbType.Int);
            sqlParams[19].Value = myActivatyVO.personextcount_max;
            sqlParams[20] = new SqlParameter("@personextcount_min", SqlDbType.Int);
            sqlParams[20].Value = myActivatyVO.personextcount_min;
            sqlParams[21] = new SqlParameter("@is_showidno", SqlDbType.NChar, 1);
            sqlParams[21].Value = myActivatyVO.is_showidno;
            sqlParams[22] = new SqlParameter("@is_showremark", SqlDbType.NChar, 1);
            sqlParams[22].Value = myActivatyVO.is_showremark;
            sqlParams[23] = new SqlParameter("@remark_name", SqlDbType.NVarChar,50);
            sqlParams[23].Value = myActivatyVO.remark_name;
            sqlParams[24] = new SqlParameter("@is_showteam_fix1", SqlDbType.NChar, 1);
            sqlParams[24].Value = myActivatyVO.is_showteam_fix1;
            sqlParams[25] = new SqlParameter("@is_showteam_fix2", SqlDbType.NChar, 1);
            sqlParams[25].Value = myActivatyVO.is_showteam_fix2;
            sqlParams[26] = new SqlParameter("@teamextcount_max", SqlDbType.Int);
            sqlParams[26].Value = myActivatyVO.teamextcount_max;
            sqlParams[27] = new SqlParameter("@teamextcount_min", SqlDbType.Int);
            sqlParams[27].Value = myActivatyVO.teamextcount_min;
            sqlParams[28] = new SqlParameter("@is_grouplimit", SqlDbType.NChar, 1);
            sqlParams[28].Value = myActivatyVO.is_grouplimit;
            sqlParams[29] = new SqlParameter("@notice", SqlDbType.NText);
            sqlParams[29].Value = myActivatyVO.notice;
            sqlParams[30] = new SqlParameter("@active", SqlDbType.NChar, 1);
            sqlParams[30].Value = myActivatyVO.active;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("UPDATE Activity ");
            sb.AppendLine("SET id=@id ");
            sb.AppendLine(",activity_type=@activity_type ");
            sb.AppendLine(",activity_info=@activity_info ");
            sb.AppendLine(",org_id=@org_id ");
            sb.AppendLine(",activity_name=@activity_name ");
            sb.AppendLine(",people_type=@people_type ");
            sb.AppendLine(",activity_startdate=@activity_startdate ");
            sb.AppendLine(",activity_enddate=@activity_enddate ");
            sb.AppendLine(",limit_count=@limit_count ");
            sb.AppendLine(",limit2_count=@limit2_count ");
            sb.AppendLine(",team_member_max=@team_member_max ");
            sb.AppendLine(",team_member_min=@team_member_min ");
            sb.AppendLine(",regist_startdate=@regist_startdate ");
            sb.AppendLine(",regist_deadline=@regist_deadline ");
            sb.AppendLine(",cancelregist_deadline=@cancelregist_deadline ");
            sb.AppendLine(",is_showfile=@is_showfile ");
            sb.AppendLine(",is_showprogress=@is_showprogress ");
            sb.AppendLine(",is_showperson_fix1=@is_showperson_fix1 ");
            sb.AppendLine(",is_showperson_fix2=@is_showperson_fix2 ");
            sb.AppendLine(",personextcount_max=@personextcount_max ");
            sb.AppendLine(",personextcount_min=@personextcount_min ");
            sb.AppendLine(",is_showidno=@is_showidno ");
            sb.AppendLine(",is_showremark=@is_showremark ");
            sb.AppendLine(",remark_name=@remark_name ");
            sb.AppendLine(",is_showteam_fix1=@is_showteam_fix1 ");
            sb.AppendLine(",is_showteam_fix2=@is_showteam_fix2 ");
            sb.AppendLine(",teamextcount_max=@teamextcount_max ");
            sb.AppendLine(",teamextcount_min=@teamextcount_min ");
            sb.AppendLine(",is_grouplimit=@is_grouplimit ");
            sb.AppendLine(",notice=@notice ");
            sb.AppendLine(",active=@active ");

            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND id=@id ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }


        /// <summary>
        /// 刪除一筆活動資料
        /// </summary>
        /// <param name="id">活動代號</param>
        /// <returns>刪除一筆活動資料</returns>
        public int DeleteActivatyByID(string id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value =new Guid(id);     

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE Activity WHERE id=@id; ");
            sb.AppendLine("DELETE ActivityGroupLimit WHERE activity_id=@id; ");
            sb.AppendLine("DELETE ActivityRegist WHERE activity_id=@id; ");
            //sb.AppendLine("DELETE A FROM ActivityTeamMember A inner join ActivityRegist B on A.b_id=B.id WHERE B.activity_id=@id; ");
           // sb.AppendLine("DELETE ActivityTeam WHERE activity_id=@id; ");
            sb.AppendLine("DELETE A FROM CustomFieldItem A inner join CustomField B on A.field_id=B.field_id WHERE B.activity_id=@id; ");
            sb.AppendLine("DELETE A FROM CustomFieldValue A inner join CustomField B on A.field_id=B.field_id WHERE B.activity_id=@id; ");
            sb.AppendLine("DELETE CustomField WHERE activity_id=@id; ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }

    }
}
