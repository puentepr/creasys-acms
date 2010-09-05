using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ActivityHistoryQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master as MyMasterPage).PanelMainGroupingText = "歷史資料查詢";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityEdit.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityEdit.aspx");
    }
}