using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_OpenRegistedTeamSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == 0)
        { 
                (e.Row.FindControl("RadioButton1") as RadioButton).Checked = true;
        (e.Row.FindControl("CheckBox1") as CheckBox).Visible = false;
        }

    }
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show(); 
    }
}

public partial class WebForm_OpenRegistedTeamSelector
{
    public void InitDataAndShow()
    {
        this.mpSearch.Show();    

    }

}
