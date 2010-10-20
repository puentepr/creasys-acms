using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ACMS.DAO
{
    public class BaseDAO
    {
        public SqlConnection MyConn()
        {
            SqlConnection myConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
            return myConn;
        }

    }
}
