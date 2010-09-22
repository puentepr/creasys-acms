using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivityQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"] == null)
            {
                Page.Visible = false;
                return;
            }
            else
            {


                switch (Request["type"].ToString())
                {
                    case "A":
                        Panel1.GroupingText = "個人報名";

                        break;
                    case "B":
                        Panel1.GroupingText = "代理報名";

                        break;
                    case "C":
                        Panel1.GroupingText = "團隊報名";

                        break;
                    default:
                        Page.Visible = false;
                        break;
                }




            
            }
       
        
        }
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

    protected void lbtnRegist_Click(object sender, EventArgs e)
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
    protected void lbtnRegist_PreRender(object sender, EventArgs e)
    {
        switch (Request["type"].ToString())
        {
            case "A":
                (sender as LinkButton).Text = "個人報名";

                break;
            case "B":
                (sender as LinkButton).Text = "代理報名";
                break;
            case "C":
                (sender as LinkButton).Text = "團隊報名";
                break;
            default:
                Page.Visible = false;
                break;
        }

    }
}