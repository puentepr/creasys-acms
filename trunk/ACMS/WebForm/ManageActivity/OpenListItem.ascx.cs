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
    public void InitDataAndShow(string field_id,string controltype)
    {
        this.mpSearch.Show();


        if (controltype == "textboxlist")
        {
            lblValue.Visible = true;
            txtValue.Visible = true;

            gvCustomField.Columns[1].Visible = true;
        }
        else
        {
            lblValue.Visible = false;
            txtValue.Visible = false;

            gvCustomField.Columns[1].Visible = false;
        }

        SqlDataSource1.SelectParameters["field_id"].DefaultValue = field_id;
    }

}

