using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PanelMain.GroupingText = PanelMainGroupingText;
    }
}
public partial class MyMasterPage 
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Page.Header.DataBind();
    }


    public string PanelMainGroupingText
    {
        get
        {
            return (ViewState["PanelMainGroupingText"] == null ? "" : ViewState["PanelMainGroupingText"].ToString());
        }
        set
        {
            ViewState["PanelMainGroupingText"] = value;
        }



    }


}