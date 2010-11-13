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

    //SELECT 新增角色作業的繼承角色(下拉式選單)資料來源
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
    public DataTable BLL_SelectAllRoles()
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.SelectAllRoles();
    }

    //SELECT
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
    public DataTable BLL_Select(int role_id)
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.ManageRole_Select(role_id);
    }

    //Insert
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
    public int BLL_Insert(int parent, string role_name, string role_description)
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.ManageRole_Insert(parent, role_name, role_description);
    }

    //Update
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
    public int BLL_Update(int original_role_id, string role_name, string role_description, string active)
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.ManageRole_Update(original_role_id, role_name, role_description, active);
    }

    //Delete
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
    public int BLL_Delete(int original_role_id)
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.ManageRole_Delete(original_role_id);
    }

}



public partial class clsDBUtility
{
    //SELECT
    public DataTable SelectAllRoles()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT A.role_id,(B.BarText+A.role_name) as role_name ");
        sb.AppendLine("FROM RoleList A ");
        sb.AppendLine("inner join (SELECT * FROM dbo.fn_GetRecursiveChildRoleIDByRoleID('',0)) B on A.role_id=B.[Value] ");
        sb.AppendLine("ORDER BY B.SN; ");

        DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), null);

        return clsMyObj.GetDataTable(DS);

    }

    //SELECT
    public DataTable ManageRole_Select(int role_id)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];

        sqlParams[0] = new SqlParameter("@role_id", SqlDbType.Int);
        sqlParams[0].Value = role_id;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT A.role_id,A.role_name,A.parent,A.role_description,A.active,A.editable,C.role_name as ParentName ");
        sb.AppendLine("FROM RoleList A ");
        sb.AppendLine("inner join (SELECT * FROM dbo.fn_GetRecursiveChildRoleIDByRoleID('',@role_id)) B on A.role_id=B.[Value] ");
        sb.AppendLine("inner join RoleList C on A.parent=C.role_id ");
        sb.AppendLine("WHERE A.active='Y' ");
        sb.AppendLine("ORDER BY B.SN; ");

        DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), sqlParams);

        return clsMyObj.GetDataTable(DS);

    }

    //INSERT
    public Int32 ManageRole_Insert(int parent, string role_name, string role_description)
    {
        SqlParameter[] sqlParams = new SqlParameter[3];

        sqlParams[0] = new SqlParameter("@parent", SqlDbType.Int);
        sqlParams[0].Value = parent;
        sqlParams[1] = new SqlParameter("@role_name", SqlDbType.NVarChar, 50);
        sqlParams[1].Value = role_name;
        sqlParams[2] = new SqlParameter("@role_description", SqlDbType.NVarChar, 50);
        sqlParams[2].Value = role_description;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("INSERT RoleList ([role_name],[role_description],[parent],[active],[editable]) ");
        sb.AppendLine("SELECT  ");
        sb.AppendLine("@role_name,@role_description,@parent,'Y','Y' ");

        return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), sqlParams);

    }

    //UPDATE
    public Int32 ManageRole_Update(int original_role_id, string role_name, string role_description, string active)
    {
        SqlParameter[] sqlParams = new SqlParameter[4];

        sqlParams[0] = new SqlParameter("@original_role_id", SqlDbType.Int);
        sqlParams[0].Value = original_role_id;
        sqlParams[1] = new SqlParameter("@role_name", SqlDbType.NVarChar, 50);
        sqlParams[1].Value = role_name;
        sqlParams[2] = new SqlParameter("@role_description", SqlDbType.NVarChar, 50);
        sqlParams[2].Value = role_description;
        sqlParams[3] = new SqlParameter("@active", SqlDbType.NChar, 1);
        sqlParams[3].Value = active;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("UPDATE RoleList ");
        sb.AppendLine("SET [role_name]=@role_name ");
        sb.AppendLine(",[role_description]=@role_description ");
        sb.AppendLine(",[active]=@active ");
        sb.AppendLine("WHERE role_id=@original_role_id ");

        return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), sqlParams);
    }

    //Delete
    public Int32 ManageRole_Delete(int original_role_id)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];

        sqlParams[0] = new SqlParameter("@original_role_id", SqlDbType.Int);
        sqlParams[0].Value = original_role_id;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("DELETE RoleList ");
        sb.AppendLine("WHERE role_id=@original_role_id ");

        return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sb.ToString(), sqlParams);
    }
}
