using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.IO;
using System.Collections.Generic;

    /// <summary>
    /// clsDBUtility 的摘要描述
    /// </summary>
    public partial class clsDBUtility
    {
        private static clsDBUtility dbUtil;
        private SqlConnection conn;

        public static clsDBUtility GetInstance()
        {
            if (dbUtil == null)
            {
                dbUtil = new clsDBUtility();
            }

            return dbUtil;
        }

        public clsDBUtility()
        {
            conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        }

        #region "Login相關"

        //人員登入並取得登入者資訊
        public bool CheckLogin(String LoginID, String LoginPwd)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = LoginID;
            sqlParams[1] = new SqlParameter("@LoginPwd", SqlDbType.NVarChar, 50);
            sqlParams[1].Value = LoginPwd;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.*,B.dept_id,B.dept_name ");
            sb.AppendLine("FROM UserList A ");
            sb.AppendLine("inner join DeptList B on A.dept_id=B.dept_id");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND A.active='Y' ");
            sb.AppendLine("AND B.active='Y' ");
            sb.AppendLine("AND A.emp_id=@LoginID ");
            sb.AppendLine("AND A.password=@LoginPwd ");

            DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), sqlParams);

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                //取得登入者資訊
                DataRow DR = DS.Tables[0].Rows[0];
                clsAuth.emp_cname = DR["emp_cname"].ToString();
                clsAuth.email = DR["email"].ToString();
                clsAuth.dept_id = DR["dept_id"].ToString();
                clsAuth.dept_name = DR["dept_name"].ToString();

                //取得角色ID與角色名稱
                CheckRole(LoginID);

                return true;
            }
            else
            {
                return false;
            }


        }

        //取得role_ids與role_names
        public void  CheckRole(string UserID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = UserID;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT B.role_id,B.role_name ");
            sb.AppendLine("FROM RoleUserMapping A ");
            sb.AppendLine("inner join RoleList B on A.role_id=B.role_id");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND B.active='Y' ");
            sb.AppendLine("AND A.emp_id=@UserID ");

            DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), sqlParams);

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                //取得角色ID與角色名稱



                List<string> _list_role_ids = new List<string>();
                List<string> _list_role_names = new List<string>();

                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    _list_role_ids.Add(DR["role_id"].ToString());
                    _list_role_names.Add(DR["role_name"].ToString());

                }

                clsAuth.role_ids = String.Join(",", _list_role_ids.ToArray());
                clsAuth.role_names = String.Join(",", _list_role_names.ToArray());

            }



        }





        //檢查帳號是否存在
        //public Boolean IsIDExist(String LoginID)
        //{
        //    SqlParameter[] sqlParams = new SqlParameter[1];

        //    sqlParams[0] = new SqlParameter("@LoginID", SqlDbType.NVarChar, 20);
        //    sqlParams[0].Value = LoginID;

        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendLine("SELECT COUNT(*) ");
        //    sb.AppendLine("FROM UserList ");
        //    sb.AppendLine("WHERE emp_id=@LoginID ");

        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sb.ToString(), sqlParams)) > 0 ? true : false;

        //}




        #endregion



        public DataTable GetCustomField(int activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.Int);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM CustomField A ");
            sb.AppendLine("WHERE activity_id=@activity_id ");

            DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), sqlParams);

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                return DS.Tables[0];

            }
            else
            {
                return null;
            }



        }


        public DataTable GetCustomFieldItem(int key_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@key_id", SqlDbType.Int);
            sqlParams[0].Value = key_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * ");
            sb.AppendLine("FROM CustomFieldItem A ");
            sb.AppendLine("WHERE key_id=@key_id ");

            DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), sqlParams);

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                return DS.Tables[0];

            }
            else
            {
                return null;
            }



        }






}