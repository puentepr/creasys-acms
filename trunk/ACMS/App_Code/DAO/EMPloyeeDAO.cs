using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
/// <summary>
/// EMPloyeeDAO 的摘要描述
/// </summary>
public class EMPloyeeDAO : ACMS.DAO.BaseDAO
{
	public EMPloyeeDAO()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}


    public ACMS.VO.EmployeeVO getEmployee(string empID)
    {
        

        SqlParameter[] sqlParams = new SqlParameter[1];

        sqlParams[0] = new SqlParameter("@ID", SqlDbType.NVarChar  );
        sqlParams[0].Value = empID;
       
        StringBuilder sb = new StringBuilder();


        sb.AppendLine("select * from V_ACSM_USER2  where ID=@ID ");

        DataSet ds = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        ACMS.VO.EmployeeVO EMPloyee = new ACMS.VO.EmployeeVO();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow DR = ds.Tables[0].Rows[0];
            EMPloyee.ID = DR["ID"].ToString();
            EMPloyee.NATIVE_NAME = DR["NATIVE_NAME"].ToString();
            EMPloyee.ENGLISH_NAME = DR["ENGLISH_NAME"].ToString();
            EMPloyee.WORK_ID = DR["WORK_ID"].ToString();
            EMPloyee.DEPT_ID = DR["DEPT_ID"].ToString();
            EMPloyee.C_DEPT_ABBR = DR["C_DEPT_ABBR"].ToString();
            EMPloyee.C_DEPT_ABBR = DR["C_DEPT_ABBR"].ToString();
            EMPloyee.OFFICE_PHONE = DR["OFFICE_PHONE"].ToString();
            
            EMPloyee.BIRTHDAY = DR["BIRTHDAY"].ToString();
            EMPloyee.SEX = DR["SEX"].ToString();
            EMPloyee.JOB_CNAME = DR["JOB_CNAME"].ToString();
            EMPloyee.STATUS = DR["STATUS"].ToString();
           
            EMPloyee.COMPANY_CODE = DR["COMPANY_CODE"].ToString();
            EMPloyee.C_NAME = DR["C_NAME"].ToString();
            EMPloyee.OFFICE_MAIL = DR["OFFICE_MAIL"].ToString();

        }
            

        return EMPloyee ;

    }

}
