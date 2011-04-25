using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class LoginDAO : BaseDAO
    {
        //人員登入並取得登入者資訊
        /// <summary>
        /// 人員登入並取得登入者資訊
        /// </summary>
        /// <param name="LoginID">登入帳號</param>
        /// <param name="UserData">out 群組資料</param>
        /// <returns>人員登入並取得登入者資訊</returns>
        public bool CheckLogin(String LoginID, out string UserData)
        {
            
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = LoginID;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.* ");
            sb.AppendLine("FROM V_ACSM_USER2 A ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND A.STATUS='1' ");
            sb.AppendLine("AND A.WINDOWS_ID=@LoginID ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
            try
            {
                if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    //取得登入者資訊
                    DataRow DR = DS.Tables[0].Rows[0];
                    clsAuth.ID = DR["ID"].ToString();
                    clsAuth.NATIVE_NAME = DR["NATIVE_NAME"].ToString();
                    clsAuth.ENGLISH_NAME = DR["ENGLISH_NAME"].ToString();
                    clsAuth.WORK_ID = DR["WORK_ID"].ToString();
                    clsAuth.DEPT_ID = DR["DEPT_ID"].ToString();
                    clsAuth.C_DEPT_ABBR = DR["C_DEPT_ABBR"].ToString();
                    clsAuth.C_DEPT_NAME = DR["C_DEPT_NAME"].ToString();
                    clsAuth.OFFICE_PHONE = DR["OFFICE_PHONE"].ToString();
                    clsAuth.EXPERIENCE_START_DATE = DR["EXPERIENCE_START_DATE"].ToString();
                    clsAuth.BIRTHDAY = DR["BIRTHDAY"].ToString();
                    clsAuth.SEX = DR["SEX"].ToString();
                    clsAuth.JOB_CNAME = DR["JOB_CNAME"].ToString();
                    clsAuth.STATUS = DR["STATUS"].ToString();
                    clsAuth.WORK_END_DATE = DR["WORK_END_DATE"].ToString();
                    clsAuth.COMPANY_CODE = DR["COMPANY_CODE"].ToString();
                    clsAuth.C_NAME = DR["C_NAME"].ToString();

                    //取得角色ID與角色名稱
                    UserData = CheckRole(DR["ID"].ToString());

                    return true;
                }
                else
                {
                    UserData = "";
                    return false;
                }

            }
            finally
            {
                if (DS != null) DS.Dispose();
            }
        }

        //取得role_ids與role_names
        /// <summary>
        /// 取得群組代號
        /// </summary>
        /// <param name="emp_id">員工</param>
        /// <returns>取得群組代號</returns>
        public string CheckRole(string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT B.id,B.role_name ");
            sb.AppendLine("FROM RoleUserMapping A ");
            sb.AppendLine("inner join RoleList B on A.role_id=B.id");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND A.emp_id=@emp_id");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            try
            {
                if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    //取得角色ID與角色名稱
                    List<string> _list_role_ids = new List<string>();
                    List<string> _list_role_names = new List<string>();

                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        _list_role_ids.Add(DR["id"].ToString());
                        _list_role_names.Add(DR["role_name"].ToString());

                    }

                    clsAuth.role_ids = String.Join(",", _list_role_ids.ToArray());
                    clsAuth.role_names = String.Join(",", _list_role_names.ToArray());

                    return String.Join(",", _list_role_ids.ToArray());

                }
                else
                {
                    clsAuth.role_ids = "";
                    clsAuth.role_names = "";

                    return "";
                }

            }
            finally
            {
                if (DS != null) DS.Dispose();
            }

        }


    }
}