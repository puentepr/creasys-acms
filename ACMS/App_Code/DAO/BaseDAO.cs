using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class BaseDAO
    {
        public SqlConnection MyConn()
        {
            SqlConnection myConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);

            return myConn;


        }
        public void ErrorLog(string ProgamName, string ErrMsg, string FunctionName, string byWho, string ErrStatus)
        {

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@ProgamName", SqlDbType.NVarChar,ProgamName .Length  );
            sqlParams[0].Value = ProgamName;
            sqlParams[1] = new SqlParameter("@ErrMsg", SqlDbType.NVarChar, ErrMsg.Length);
            sqlParams[1].Value = ErrMsg;
            sqlParams[2] = new SqlParameter("@FunctionName", SqlDbType.NVarChar, FunctionName.Length);
            sqlParams[2].Value = FunctionName;
            sqlParams[3] = new SqlParameter("@byWho", SqlDbType.NVarChar, byWho.Length );
            sqlParams[3].Value = byWho;
            sqlParams[4] = new SqlParameter("@ErrStatus", SqlDbType.NVarChar, ErrStatus.Length );
            sqlParams[4].Value = ErrStatus;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" insert into  ACMSErrorLog (Program ,ErrMsg ,FunctionName ,ByWho ,ErrStatus ) values(@ProgamName,@ErrMsg,@FunctionName,@byWho,@ErrStatus) ");


            SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);


        }

       

    }
}
