using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjectDataSource1.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;
            ObjectDataSource2.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;        
        }
    }


    protected void lbtnRegist1_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        Session["form_mode"] = "regist";
        Session["activity_id"] = activity_id;
        Response.Redirect("WebForm/RegistActivity/RegistActivity_Person.aspx");
    }

    protected void lbtnRegist2_Click(object sender, EventArgs e)
    {
        string activity_id = GridView2.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        Session["form_mode"] = "regist";
        Session["activity_id"] = activity_id;
        Response.Redirect("WebForm/RegistActivity/RegistActivity_Team.aspx");
    }
}