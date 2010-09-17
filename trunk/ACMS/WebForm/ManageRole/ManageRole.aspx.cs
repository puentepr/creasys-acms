using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageRole_ManageRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //((Literal)this.Page.Master.FindControl("Literal1")).Text = this.GetLocalResourceObject("ManageRole").ToString();

            //ObjectDataSource_ManageRole.SelectParameters["role_id"].DefaultValue = ((DropDownList)FormView1.FindControl("ddlParent")).SelectedValue;
        }

    }



    //GridView1 RowDataBound
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)(e.Row.DataItem);

            if (string.IsNullOrEmpty(drv["NATIVE_NAME"].ToString().Trim()))
            {
                (e.Row.FindControl("ibtnAdd") as ImageButton).Visible = true;
                (e.Row.FindControl("ibtnDel") as ImageButton).Visible = false;
            }
            else
            {
                (e.Row.FindControl("ibtnAdd") as ImageButton).Visible = false;
                (e.Row.FindControl("ibtnDel") as ImageButton).Visible = true;
            }
        }
    }



   
    //開窗設定權限
    protected void lbtnSetup_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        //OpenManageRoleProgramMapping1.RoleID = (int)GridView1.DataKeys[((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex][0];
        //OpenManageRoleProgramMapping1.InitDataAndShow();
    }





}

public partial class WebForm_ManageRole_ManageRole
{
    //protected bool BooleanConverter(object strTMP)
    //{
    //    return (strTMP.ToString() == "Y" ? true : false);
    //}
}