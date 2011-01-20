using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class WebForm_RegistActivity_RegistActivity_Query : System.Web.UI.UserControl
{
    public delegate void GoSecondStepDelegate(object sender, RegistGoSecondEventArgs e);
    public event GoSecondStepDelegate GoSecondStep_Click;

    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Page.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(GridView1);

        if (!IsPostBack)
        {
            if (Session["Team"]!=null)
            {
                GridView1 .Columns [7].Visible =false ;
            }
            ObjectDataSource1.SelectParameters["activity_type"].DefaultValue = ActivityType;
            ObjectDataSource1.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;

            btnQuery_Click(null,null);
        }
    }

    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["activity_name"].DefaultValue = txtactivity_name.Text;
        ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = txtactivity_startdate.Text;
        ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = txtactivity_enddate.Text;

        GridView1.DataBind();
    }

    //報名
    protected void lbtnRegist_Click(object sender, EventArgs e)
    {
        if (GoSecondStep_Click != null)
        {
            Session.Remove("Agent");
            Session.Remove("Team");
            Guid activity_id = new Guid(GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());
            GoSecondStep_Click(this, new RegistGoSecondEventArgs(activity_id));
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            ((Label)e.Row.FindControl("Label1")).Text = ((Label)e.Row.FindControl("Label1")).Text.Replace("\r\n", "<br/>");
        }
    }
    protected void lbtnRegistAgent_Click(object sender, EventArgs e)
    {
      
        if (GoSecondStep_Click != null)
        {
            Session["Agent"] = "Yes";
            Session.Remove("Team");
            Guid activity_id = new Guid(GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());
            GoSecondStep_Click(this, new RegistGoSecondEventArgs(activity_id));
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
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
}

public partial class WebForm_RegistActivity_RegistActivity_Query
{
    public string ActivityType
    {
        get { return (ViewState["ActivityType"] == null ? "" : ViewState["ActivityType"].ToString()); }
        set { ViewState["ActivityType"] = value; }
    }  

}

