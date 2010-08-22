using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TServerControl.Web;

public partial class WebForm_ManageRole_ManageRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //((Literal)this.Page.Master.FindControl("Literal1")).Text = this.GetLocalResourceObject("ManageRole").ToString();

            ObjectDataSource_ManageRole.SelectParameters["role_id"].DefaultValue = ((TDropDownList)FormView1.FindControl("ddlParent")).SelectedValue;
        }

    }

    protected void ddlRolePapas_SelectedIndexChanged(object sender, EventArgs e)
    {
        TGridView1_DataBind();
    }

    //新增
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        TDropDownList ddlParent = (TDropDownList)this.FormView1.FindControl("ddlParent");
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

            TGridView1_DataBind();

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
    protected void TGridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        e.NewValues["active"] = ((CheckBox)TGridView1.Rows[e.RowIndex].FindControl("chkActive")).Checked == true ? "Y" : "N";
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

    //TGridView1 RowDataBound
    protected void TGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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



    //TGridView1 執行DataBind
    protected void TGridView1_DataBind()
    {
        ObjectDataSource_ManageRole.SelectParameters["role_id"].DefaultValue = ((DropDownList)this.FormView1.FindControl("ddlParent")).SelectedValue;
        TGridView1.DataBind();
    }

    //TGridView1 RowUpdated後要更新ddlParent
    protected void TGridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        ((TDropDownList)FormView1.FindControl("ddlParent")).DataBind();
    }


    //開窗設定權限
    protected void lbtnSetup_Click(object sender, EventArgs e)
    {
        TGridView1.EditIndex = -1;
        //OpenManageRoleProgramMapping1.RoleID = (int)TGridView1.DataKeys[((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex][0];
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