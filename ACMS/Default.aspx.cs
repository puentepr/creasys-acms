using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : BasePage
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
        Session.Remove("Agent");
        Response.Redirect("WebForm/RegistActivity/RegistActivity_Person.aspx");
    }

    protected void lbtnRegist2_Click(object sender, EventArgs e)
    {
        string activity_id = GridView2.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        Session["form_mode"] = "regist";
        Session["activity_id"] = activity_id;
        Response.Redirect("WebForm/RegistActivity/RegistActivity_Team.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)(e.Row.DataItem);

            if (drv["registable_count"].ToString() == "額滿")
            {
                e.Row.FindControl("lbtnRegist1").Visible = false;
            }
            ((Label)e.Row.FindControl("Label1")).Text = ((Label)e.Row.FindControl("Label1")).Text.Replace("\r\n", "<br/>");
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)(e.Row.DataItem);

            if (drv["registable_count"].ToString() == "額滿")
            {
                e.Row.FindControl("lbtnRegist2").Visible = false;
            }
            ((Label)e.Row.FindControl("Label1")).Text = ((Label)e.Row.FindControl("Label1")).Text.Replace("\r\n", "<br/>");
        }
    }
    protected void lbtnRegist2Agent_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        Session["form_mode"] = "regist";
        Session["activity_id"] = activity_id;
        Session["Agent"] = "Yes";
        Response.Redirect("WebForm/RegistActivity/RegistActivity_Person.aspx");
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0 )
        {
            GridView1.Visible = false;
            lblGrideView1.Visible = true;

        }
        else
        {
            GridView1.Visible = true;
            lblGrideView1.Visible = false;
        }

    }
    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count == 0)
        {
            GridView2.Visible = false;
            lblGrideView2.Visible = true;

        }
        else
        {
            GridView2.Visible = true;
            lblGrideView2.Visible = false;
        }
    }
}