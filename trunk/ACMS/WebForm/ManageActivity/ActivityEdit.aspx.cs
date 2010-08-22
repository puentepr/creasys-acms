using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageActivity_ActivityEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        FormView1.InsertItemTemplate = FormView1.ItemTemplate;
        FormView1.EditItemTemplate = FormView1.ItemTemplate;
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityManagementQuery.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityManagementQuery.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        OpenEmployeeSelector1.InitDataAndShow();
    }

}