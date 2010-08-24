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

            ObjectDataSource_ManageRole.SelectParameters["role_id"].DefaultValue = ((DropDownList)FormView1.FindControl("ddlParent")).SelectedValue;
        }

    }

    protected void ddlRolePapas_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1_DataBind();
    }

    //新增
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DropDownList ddlParent = (DropDownList)this.FormView1.FindControl("ddlParent");
        TextBox txtRoleName = (TextBox)this.FormView1.FindControl("txtRoleName");
        TextBox txtRoleDescription = (TextBox)this.FormView1.FindControl("txtRoleDescription");

        ObjectDataSource_ManageRole.InsertParameters["parent"].DefaultValue = ddlParent.SelectedValue;
        ObjectDataSource_ManageRole.InsertParameters["role_name"].DefaultValue = txtRoleName.Text;
        ObjectDataSource_ManageRole.InsertParameters["role_description"].DefaultValue = txtRoleDescription.Text;

        try
        {
            ObjectDataSource_ManageRole.Insert();
 
            txtRoleName.Text = string.Empty;
            txtRoleDescription.Text = string.Empty;

            GridView1_DataBind();

        }
        catch (Exception ex)
        {

            if (((System.Data.SqlClient.SqlException)ex.InnerException).Number == 2627)
            {
                clsMyObj.ShowMessage("相同階層下)該角色名稱已存在!請使用別的名稱。");
            }
            else
            {
                clsMyObj.ShowMessage(ex.InnerException.Message.Replace("'", "\\'"));
            }

        }

        ddlParent.SelectedIndex = -1;
        ddlParent.DataBind();

    }

    //編輯-RowUpdating
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        e.NewValues["active"] = ((CheckBox)GridView1.Rows[e.RowIndex].FindControl("chkActive")).Checked == true ? "Y" : "N";
    }


    //編輯-ObjectDataSource_Role_Updated
    protected void ObjectDataSource_Role_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null && e.Exception.InnerException != null)
        {
            if (((System.Data.SqlClient.SqlException)e.Exception.InnerException).Number == 2627)
            {
                clsMyObj.ShowMessage("相同階層下)該角色名稱已存在!請使用別的名稱。");
            }
            else
            {
                clsMyObj.ShowMessage(e.Exception.InnerException.Message.Replace("'", "\\'"));
            }

        }

    }

    //GridView1 RowDataBound
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)(e.Row.DataItem);

            if (drv["parent"].ToString() == ((DropDownList)FormView1.FindControl("ddlParent")).SelectedValue)
            {
                e.Row.Cells[2].Font.Bold = true;
            }
        }
    }



    //GridView1 執行DataBind
    protected void GridView1_DataBind()
    {
        ObjectDataSource_ManageRole.SelectParameters["role_id"].DefaultValue = ((DropDownList)this.FormView1.FindControl("ddlParent")).SelectedValue;
        GridView1.DataBind();
    }

    //GridView1 RowUpdated後要更新ddlParent
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        ((DropDownList)FormView1.FindControl("ddlParent")).DataBind();
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
    protected bool BooleanConverter(object strTMP)
    {
        return (strTMP.ToString() == "Y" ? true : false);
    }
}