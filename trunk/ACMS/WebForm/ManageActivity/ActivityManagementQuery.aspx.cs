using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ActivityManagementQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master as MyMasterPage).PanelMainGroupingText = "活動紀錄查詢";
    }

    //新增個人活動
    protected void btnAddActivity_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityEdit.aspx");
    }

    //新增團隊活動
    protected void btnAddActivityTeam_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityEdit.aspx?type=team");
    }

    //編輯活動
    protected void lbtnEditActivaty_Click(object sender, EventArgs e)
    {
        if (((sender as LinkButton).NamingContainer as GridViewRow).RowIndex == 0)
        {
            Response.Redirect("ActivityEdit.aspx");
        }
        else
        {
            Response.Redirect("ActivityEdit.aspx?type=team");
        }
    }
}