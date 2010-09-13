using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ActivityQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"] != null && Request["type"] == "off")
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "歷史資料查詢";
                gvActivityList.Columns[7].Visible = false;
            }
            else
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "報名狀態查詢";

            }
        }


    }

    protected void lbtnShowPerson_Click(object sender, EventArgs e)
    {
        this.OpenNameList1.InitDataAndShow(); 
    }
}