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

public partial class WebForm_RegistActivity_RegistActivity_Agent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master as MyMasterPage).PanelMainGroupingText = "代理報名";
    }

    protected void GoSecondStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        Wizard1.MoveTo(Wizard1.WizardSteps[1]);
        Label2.Text = e.activity_id.ToString() + "   --     " + clsAuth.ID;


    }




    protected void FinishButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistActivity_Agent.aspx");
    }

    protected void lbtnAddPerson_Click(object sender, EventArgs e)
    {
        OpenUnRegistEmployeeSelector1.InitDataAndShow();
    }
}
