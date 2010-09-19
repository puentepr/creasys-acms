using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Class1 的摘要描述
/// </summary>

[System.ComponentModel.DataObjectAttribute(true)]
public partial class BLL_OpenEmployeeSelector
{
    public BLL_OpenEmployeeSelector()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    //SELECT
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
    public DataTable BLL_OpenEmployeeSelector_Select(string DEPT_ID, string JOB_CNAME, string WORK_ID, string NATIVE_NAME, string SEX, int AGE, string EXPERIENCE_START_DATE, string C_NAME)
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.OpenEmployeeSelector_Select(DEPT_ID, JOB_CNAME, WORK_ID, NATIVE_NAME, SEX, AGE, EXPERIENCE_START_DATE, C_NAME);

    }


    //SELECT
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
    public DataTable BLL_OpenEmployeeSelector_ManageRole_Select(string DEPT_ID, string JOB_CNAME, string WORK_ID, string NATIVE_NAME, string SEX, int AGE, string EXPERIENCE_START_DATE, string C_NAME)
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.OpenEmployeeSelector_ManageRole_Select( DEPT_ID,  JOB_CNAME,  WORK_ID,  NATIVE_NAME,  SEX,  AGE,  EXPERIENCE_START_DATE,  C_NAME);
    }



}



public partial class clsDBUtility
{
    //SELECT
    public DataTable OpenEmployeeSelector_Select(string DEPT_ID, string JOB_CNAME, string WORK_ID, string NATIVE_NAME, string SEX, int AGE, string EXPERIENCE_START_DATE, string C_NAME)
    {
        return null;

    }
    //ManageRole_Select
    public DataTable OpenEmployeeSelector_ManageRole_Select(string DEPT_ID, string JOB_CNAME, string WORK_ID, string NATIVE_NAME, string SEX, int AGE, string EXPERIENCE_START_DATE, string C_NAME)
    {
        SqlParameter[] sqlParams = new SqlParameter[8];

        sqlParams[0] = new SqlParameter("@DEPT_ID", SqlDbType.NVarChar,36);
        sqlParams[0].Value = DEPT_ID.Trim();
        sqlParams[1] = new SqlParameter("@JOB_CNAME", SqlDbType.NVarChar, 200);
        sqlParams[1].Value = JOB_CNAME.Trim();
        sqlParams[2] = new SqlParameter("@WORK_ID", SqlDbType.NVarChar, 36);
        sqlParams[2].Value = WORK_ID.Trim();
        sqlParams[3] = new SqlParameter("@NATIVE_NAME", SqlDbType.NVarChar, 200);
        sqlParams[3].Value = NATIVE_NAME.Trim();
        sqlParams[4] = new SqlParameter("@SEX", SqlDbType.NVarChar, 2);
        sqlParams[4].Value = SEX.Trim();
        sqlParams[5] = new SqlParameter("@AGE", SqlDbType.Int);
        sqlParams[5].Value = AGE;
        sqlParams[6] = new SqlParameter("@EXPERIENCE_START_DATE", SqlDbType.VarChar,10);
        sqlParams[6].Value = EXPERIENCE_START_DATE.Trim();
        sqlParams[7] = new SqlParameter("@C_NAME", SqlDbType.NVarChar, 120);
        sqlParams[7].Value = C_NAME.Trim();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT ID,WORK_ID,NATIVE_NAME,C_DEPT_NAME,C_NAME ");
        sb.AppendLine("FROM V_ACSM_USER A ");
        sb.AppendLine("WHERE 1=1 ");
        sb.AppendLine("AND (@DEPT_ID='' or DEPT_ID=@DEPT_ID) ");
        sb.AppendLine("AND (@JOB_CNAME='' or JOB_CNAME=@JOB_CNAME) ");
        sb.AppendLine("AND (@WORK_ID='' or WORK_ID=@WORK_ID) ");
        sb.AppendLine("AND (@NATIVE_NAME='' or NATIVE_NAME=@NATIVE_NAME) ");
        sb.AppendLine("AND (@SEX='' or SEX=@SEX) ");
        sb.AppendLine("AND (@AGE='' or @AGE=datediff(year,BIRTHDAY,getdate())) ");
        sb.AppendLine("AND (@EXPERIENCE_START_DATE='' or EXPERIENCE_START_DATE=@EXPERIENCE_START_DATE) ");
        sb.AppendLine("AND (@C_NAME='' or C_NAME like '%'+@C_NAME+'%') ");
        sb.AppendLine("ORDER BY WORK_ID,NATIVE_NAME ");

        DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), sqlParams);

        return clsMyObj.GetDataTable(DS);

    }

}

