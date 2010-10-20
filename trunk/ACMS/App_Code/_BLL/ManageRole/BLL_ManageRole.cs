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
public partial class BLL_ManageRole
{
    public BLL_ManageRole()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    //SELECT
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
    public DataTable BLL_Select()
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.ManageRole_Select();
    }

    //Delete
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
    public int BLL_Delete(int original_role_id, string original_ID)
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.ManageRole_Delete(original_role_id, original_ID);
    }

}



public partial class clsDBUtility
{

    //SELECT
    public DataTable ManageRole_Select()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];

        sqlParams[0] = new SqlParameter("@role_id", SqlDbType.Int);
        sqlParams[0].Value = 0;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT role_id,parent,role_name,role_description,'' as ID,'' as NATIVE_NAME ");
        sb.AppendLine("FROM RoleList A ");
        sb.AppendLine("UNION ");
        sb.AppendLine("SELECT A.role_id,'0','','',B.ID,B.NATIVE_NAME+'('+B.WORK_ID+'-'+B.C_DEPT_ABBR+'-'+CASE B.STATUS WHEN 1 THEN '在職' WHEN '2' THEN '離職' WHEN '3' THEN '留職停薪' ELSE '' END+')' ");
        sb.AppendLine("FROM RoleUserMapping A ");
        sb.AppendLine("inner join dbo.V_ACSM_USER B on A.ID=B.ID ");
        sb.AppendLine("ORDER BY role_id,parent desc ");

        DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), sqlParams);

        return clsMyObj.GetDataTable(DS);

    }

    //Delete
    public Int32 ManageRole_Delete(int original_role_id, string original_ID)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];

        sqlParams[0] = new SqlParameter("@original_role_id", SqlDbType.Int);
        sqlParams[0].Value = original_role_id;
        sqlParams[1] = new SqlParameter("@original_ID", SqlDbType.NVarChar, 100);
        sqlParams[1].Value = original_ID;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("DELETE RoleUserMapping ");
        sb.AppendLine("WHERE role_id=@original_ID and ID=@original_ID ");

        return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), sqlParams);
    }
}
