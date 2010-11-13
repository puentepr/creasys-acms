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
public partial class BLL_Pubic
{
    public BLL_Pubic()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    //SELECT
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
    public DataTable BLL_Department_Select()
    {
        clsDBUtility dbUtil = clsDBUtility.GetInstance();
        return dbUtil.BLL_Department_Select();
    }



}



public partial class clsDBUtility
{

    //SELECT
    public DataTable BLL_Department_Select()
    {

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT * ");
        sb.AppendLine("FROM DeptList ");      
        sb.AppendLine("WHERE active='Y' ");

        DataSet DS = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString(), null);

        return clsMyObj.GetDataTable(DS);

    }

}
