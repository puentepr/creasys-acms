using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivityQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        if (((sender as LinkButton).NamingContainer as GridViewRow).RowIndex == 0)
        {
            OpenRegistedEmployeeSelector1.InitDataAndShow();
        }
        else
        {
            OpenRegistedTeamSelector1.InitDataAndShow();
        }

    }
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        if (((sender as LinkButton).NamingContainer as GridViewRow).RowIndex == 0)
        {
            Response.Redirect("RegistActivity.aspx");
        }
        else
        {
            Response.Redirect("RegistActivityTeam.aspx");
        }


    }
}