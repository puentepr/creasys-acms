using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_OpenRegistedEmployeeSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show(); 
    }
}

public partial class WebForm_OpenRegistedEmployeeSelector
{
    public void InitDataAndShow()
    {
        this.mpSearch.Show();    

    }

}
