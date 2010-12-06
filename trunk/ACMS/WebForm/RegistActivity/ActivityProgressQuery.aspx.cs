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

public partial class WebForm_RegistActivity_ActivityProgressQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjectDataSource_Activity.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;
            (this.Master as MyMasterPage).PanelMainGroupingText = "活動進度查詢";
        }

    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (DataList1.DataKeys[e.Item.ItemIndex].ToString() == clsAuth.ID)
        {
            e.Item.BackColor = System.Drawing.Color.Yellow;
        }
        else
        {
            Label NATIVE_NAMELabel = e.Item.FindControl("NATIVE_NAMELabel") as Label;
            Label emp_idLabel = e.Item.FindControl("emp_idLabel") as Label;

            NATIVE_NAMELabel.Text = NATIVE_NAMELabel.Text.Substring(0,1) + "XX";
            emp_idLabel.Text = "";
        }
    }

}
