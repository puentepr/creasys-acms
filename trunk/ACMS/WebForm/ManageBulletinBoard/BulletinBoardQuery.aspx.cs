using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageBulletinBoard_BulletinBoardQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master as MyMasterPage).PanelMainGroupingText = "公佈欄管理";
    }
    protected void lbtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("BulletinBoardEdit.aspx");
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("BulletinBoardEdit.aspx?id={0}", GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value));
    }
}
