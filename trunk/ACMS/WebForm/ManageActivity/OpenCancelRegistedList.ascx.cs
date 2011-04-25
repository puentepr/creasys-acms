using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenCancelRegistedList : System.Web.UI.UserControl
{
    public delegate void CancelPersonRegistDelegate(object sender, EventArgs e);
    public event CancelPersonRegistDelegate CancelPersonRegistClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
       
            this.InitDataAndShow();
            GridView1.Visible = true;
            GridView1.DataBind();
        

    }
    
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }
  
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show();
    }
}

public partial class WebForm_RegistActivity_OpenCancelRegistedList
{
    public string activity_id
    {
        get { return (ViewState["activity_id"] == null ? "" : ViewState["activity_id"].ToString()); }
        set { ViewState["activity_id"] = value; }
    }


    public string activity_type
    {
        get { return (ViewState["activity_type"] == null ? "" : ViewState["activity_type"].ToString()); }
        set { ViewState["activity_type"] = value; }
    }

    public void InitDataAndShow()
    {
        GridView1.Visible = true;
        if (activity_type == "1")
        {
            GridView1.Columns[1].Visible = false ;
            GridView1.Columns[2].Visible = false;
        }
            else
        {
            GridView1.Columns[1].Visible = true;
            GridView1.Columns[2].Visible = true;
        }

        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = activity_id;
        ObjectDataSource1.SelectParameters["name"].DefaultValue = txtname.Text ;
        GridView1.DataBind();
        this.mpSearch.Show();
    }

}
