using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class ActivityRegistDAO : BaseDAO
    {

        public int INSERT_NewOne(VO.ActivityRegistVO myActivityRegistVO)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = myActivityRegistVO.id;
            sqlParams[1] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[1].Value = myActivityRegistVO.activity_id;
            sqlParams[2] = new SqlParameter("@emp_id", SqlDbType.NVarChar,50);
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
        public DataTable SelectEmployeesByID(Guid activity_id, string activity_type)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT B.WORK_ID,B.NATIVE_NAME,B.C_DEPT_ABBR ");
            sb.AppendLine(string.Format("FROM {0} A ", (activity_type == "1" ? "ActivityRegist" : "ActivityTeamMember")));
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");  
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND A.activity_id=@activity_id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            return clsMyObj.GetDataTable(DS);

        }

        //取得報名資訊-個人活動   為了組成個人固定欄位
        public VO.ActivityRegistVO SelectActivityRegistByPK(Guid activity_id,string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar,100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM ActivityRegist ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND activity_id=@activity_id ");
            sb.AppendLine("AND emp_id=@emp_id ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

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

            return myActivityRegistVO;

        }


        //取得報名資訊-團隊活動 登入者帳號-團長帳號 找出報名資訊
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

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

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

            return myActivityRegistVO;

        }


        //廢止
        //取得報名資訊-為了組成個人固定欄位
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

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

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

            return myActivityRegistVO;

        }

        //檢查是否重複報名(個人,團隊)
        public int IsPersonRegisted(Guid activity_id, string emp_id, string boss_id, string activity_type)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, -1);
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
            sb.AppendLine("and ISNULL(B.check_status,0)>=0 ");
            sb.AppendLine("group by  (ISNULL(A.limit_count,0)+ISNULL(A.limit2_count,0)) ");

            return (int)SqlHelper.ExecuteScalar(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }

        //新增報名或更新報名資訊
        public int UpdateActivityRegist(VO.ActivityRegistVO myActivityRegistVO, List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList, List<ACMS.VO.ActivityTeamMemberVO> myActivityTeamMemberVOList, string type, string activity_type)
        {
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

            string strNewEmp_idList = "";

            //團隊時報名者是多人
            if (myActivityTeamMemberVOList != null)
            {
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
                if (myActivityRegistVO.emp_id != myActivityRegistVO.regist_by)
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
                                //舊成員若不在新成員名單就要寄取消報名信
                                //新成員若不在舊成員資料表就要寄報名成功信
                                //ACMS.DAO.ActivityTeamMemberDAO myActivityTeamMemberDAO = new ActivityTeamMemberDAO();
                                //myActivityTeamMemberDAO.SendMailWhenTeamMemberChanged(myActivityRegistVO.activity_id, strNewEmp_idList);

                                sb.Length = 0;
                                sb.AppendLine("DELETE A ");
                                sb.AppendLine("FROM ActivityTeamMember A ");
                                sb.AppendLine(string.Format("inner join Activity B on A.activity_id=B.id and A.boss_id=A.emp_id and A.activity_id='{0}'; ", myActivityRegistVO.activity_id));

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

                }

                trans.Complete();
            }

            return 1;

        }


        //取消報名-刪除
        public int DeleteRegist(Guid activity_id, string emp_id,string activity_type)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NText);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

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

            using (SqlConnection myConn = MyConn())
            {
                myConn.Open();

                using (SqlTransaction trans = myConn.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();

                        cmd.Connection = myConn;
                        cmd.Transaction = trans;
                        cmd.CommandText = sb.ToString();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(sqlParams);
                        cmd.ExecuteNonQuery();

                        if (activity_type == "2")
                        {
                            //若團隊人數低於門檻則團隊消滅

                            cmd.CommandText = sb.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(sqlParams);
                            cmd.ExecuteNonQuery();


                        }    

                        trans.Commit();

                        return 1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();

                        return 0;
                    }
                }
            }
        
        }

        //取消報名-狀態改取消
        public int CancelRegist(Guid activity_id, string emp_id, string activity_type)
        {
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

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }

      




    }
}
