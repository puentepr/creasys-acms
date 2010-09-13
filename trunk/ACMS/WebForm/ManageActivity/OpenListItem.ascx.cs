using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageActivity_OpenListItem : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

public partial class WebForm_ManageActivity_OpenListItem
{
    public void InitDataAndShow(string key_id)
    {
        this.mpSearch.Show();

    }

}

