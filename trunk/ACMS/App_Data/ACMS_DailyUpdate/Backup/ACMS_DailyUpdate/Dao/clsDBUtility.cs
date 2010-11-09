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
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings[strConnSring].ConnectionString.Trim());
    }


    public DataTable SelectEmployeeSource()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM ORG_TYPE ");

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
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("truncate Table VM2; ");

            SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);

            sb.Length = 0;
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendLine("INSERT ORG_TYPE_B ");
                sb.AppendLine(string.Format("SELECT '{0}' ", dr["id"]));

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);
            }

            sb.Length = 0;
            sb.AppendLine("EXEC sp_rename 'VM1','VM3'; ");
            sb.AppendLine("EXEC sp_rename 'VM2','VM1'; ");
            sb.AppendLine("EXEC sp_rename 'VM3','VM2'; ");

            SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);

            LogMsg.Log("[InsertEmployeeData]-OK ", 1, false);

        }
        catch (Exception ex)
        {
            LogMsg.Log(string.Format("[InsertEmployeeData]-Exception:{0}", ex.Message), 1, false);
        }

    }

    public void UpdateStatus()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Update VVV ");
            sb.AppendLine("set status='2' ");
            sb.AppendLine("WHERE emp_id in (SELECT emp_id FROM AAA WHERE status=2); ");

            sb.AppendLine("Update VVV ");
            sb.AppendLine("set status='3' ");
            sb.AppendLine("WHERE emp_id in (SELECT emp_id FROM AAA WHERE status=2); ");
         
            SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), null);

            LogMsg.Log("[UpdateStatus]-OK ", 1, false);
        }
        catch (Exception ex)
        {
            LogMsg.Log(string.Format("[UpdateStatus]-Exception:{0}", ex.Message), 1, false);
        }

    }



}