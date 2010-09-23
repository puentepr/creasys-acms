using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebForm_ManageActivity_ActivityEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        FormView1.InsertItemTemplate = FormView1.ItemTemplate;
        FormView1.EditItemTemplate = FormView1.ItemTemplate;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"] != null && Request["type"] == "team")
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "新增團隊活動";
                (FormView1.FindControl("lbllimit_count_team") as Label).Visible = true;
                (FormView1.FindControl("lbllimit2_count_team") as Label).Visible = true;
                (FormView1.FindControl("trteam_member_max") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = true;
                (FormView1.FindControl("trteam_member_min") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = true;


                PanelCustomFieldA1.Visible = false;
                PanelCustomFieldA2.Visible = true;
                PanelCustomFieldB2.Visible = true;
                PanelCustomFieldC.GroupingText = "團隊自訂欄位";





            }
            else
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "新增個人活動";
                (FormView1.FindControl("lbllimit_count") as Label).Visible = true;
                (FormView1.FindControl("lbllimit2_count") as Label).Visible = true;
                (FormView1.FindControl("trteam_member_max") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = false;
                (FormView1.FindControl("trteam_member_min") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = false;

                PanelCustomFieldA1.Visible = true;
                PanelCustomFieldA2.Visible = false;
                PanelCustomFieldB2.Visible = false;
                PanelCustomFieldC.GroupingText = "個人自訂欄位";


            }
        }



    }
    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ActivityManagementQuery.aspx");
    //}
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ActivityManagementQuery.aspx");
    //}
    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    OpenEmployeeSelector1.InitDataAndShow();
    //}


    //protected void gvCustomField_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    DataRowView MyDataRowView = (DataRowView)e.Row.DataItem;

    //    if(MyDataRowView["field_control"])
    //}
    protected void lbtnEditItem_Click(object sender, EventArgs e)
    {   
      

        OpenListItem1.InitDataAndShow((sender as LinkButton).CommandArgument, ( ((sender as LinkButton).NamingContainer as GridViewRow).FindControl("ddlfield_control") as DropDownList).SelectedValue);
    
    }
    protected void btnAddGroupLimit_Click(object sender, EventArgs e)
    {
        OpenEmployeeSelector1.InitDataAndShow();
    }
}


public partial class WebForm_ManageActivity_ActivityEdit
{

    public bool IsShowEdit(object objTMP)
    {
        return Convert.ToBoolean(objTMP);   
    
    }
}



