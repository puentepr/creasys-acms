using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Globalization;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.IO;
using PDFPlatform;

/// <summary>
/// clsDBUtility 的摘要描述
/// </summary>

public partial class clsDBUtility
{
    private SqlConnection conn;

    public clsDBUtility(string strConnSring)
    {
        conn = new SqlConnection(ConfigurationManager.AppSettings[strConnSring].Trim());
    }


    public DataTable SelectEmployeeSource()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM V_ACSM_USER ");

            DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), null);

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                return DS.Tables[0];
            }
            else
            {
                LogMsg.Log("[SelectEmployeeData]-Rows Count = 0 ", 1, false);
                return null;
            }
        }
        catch(Exception ex)
        {
            LogMsg.Log(string.Format("[SelectEmployeeData]-Exception:{0}",ex.Message), 1, false);
            return null;
        }

    }

    public void InsertEmployeeData(DataTable dt)
    {
        using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("truncate Table V_ACSM_USER_TMP; ");

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);



                foreach (DataRow dr in dt.Rows)
                {

                    sb.Length = 0;
                    sb.AppendLine("INSERT V_ACSM_USER_TMP ");
                    sb.AppendLine(string.Format("SELECT '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',getdate() "
                        , dr["id"]
                        , dr["NATIVE_NAME"]
                        , dr["ENGLISH_NAME"]
                        , dr["WORK_ID"]
                        , dr["OFFICE_MAIL"]
                        , dr["DEPT_ID"]
                        , dr["C_DEPT_NAME"]
                        , dr["C_DEPT_ABBR"]
                        , dr["OFFICE_PHONE"]
                        , (dr["EXPERIENCE_START_DATE"] == DBNull.Value ? null : Convert.ToDateTime(dr["EXPERIENCE_START_DATE"]).ToString("yyyy/MM/dd"))
                        , (dr["BIRTHDAY"] == DBNull.Value ? null : Convert.ToDateTime(dr["BIRTHDAY"]).ToString("yyyy/MM/dd"))
                        , dr["SEX"]
                        , dr["JOB_CNAME"]
                        , dr["STATUS"]
                        , (dr["WORK_END_DATE"] == DBNull.Value ? null : Convert.ToDateTime(dr["WORK_END_DATE"]).ToString("yyyy/MM/dd"))
                        , dr["COMPANY_CODE"], dr["C_NAME"]));

                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);
                }

                sb.Length = 0;
                sb.AppendLine("EXEC sp_rename 'V_ACSM_USER','V_ACSM_USER_3'; ");
                sb.AppendLine("EXEC sp_rename 'V_ACSM_USER_TMP','V_ACSM_USER'; ");
                sb.AppendLine("EXEC sp_rename 'V_ACSM_USER_3','V_ACSM_USER_TMP'; ");

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);

                LogMsg.Log("[InsertEmployeeData]-OK ", 1, false);

                trans.Complete();
            }
            catch (Exception ex)
            {
                LogMsg.Log(string.Format("[InsertEmployeeData]-Exception:{0}", ex.Message), 1, false);
            }
        }
         



    }

    public void UpdateStatus()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Update ActivityRegist ");
            sb.AppendLine("set check_status=2 ");
            sb.AppendLine("WHERE emp_id in (SELECT ID FROM V_ACSM_USER2 WHERE status='2'); ");

            sb.AppendLine("Update ActivityRegist ");
            sb.AppendLine("set check_status=3 ");
            sb.AppendLine("WHERE emp_id in (SELECT ID FROM V_ACSM_USER2 WHERE status='3'); ");
         
            SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);

            LogMsg.Log("[UpdateStatus]-OK ", 1, false);
        }
        catch (Exception ex)
        {
            LogMsg.Log(string.Format("[UpdateStatus]-Exception:{0}", ex.Message), 1, false);
        }

    }



}