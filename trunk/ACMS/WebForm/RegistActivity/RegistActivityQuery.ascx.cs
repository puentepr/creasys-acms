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

public partial class WebForm_RegistActivity_RegistActivityQuery : System.Web.UI.UserControl
{

    public delegate void GoSecondStepDelegate(object sender, RegistGoSecondEventArgs e);
    public event GoSecondStepDelegate GoSecondStep_Click;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Panel1.GroupingText = TypeName;
        }
    }


    protected void lbtnRegist_Click(object sender, EventArgs e)
    {
        if (GoSecondStep_Click != null)
        {
            int activity_id = (int)gv_Activity.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;
            GoSecondStep_Click(this, new RegistGoSecondEventArgs(activity_id));
        }    

        //if (((sender as LinkButton).NamingContainer as GridViewRow).RowIndex == 0)
        //{
        //    Response.Redirect("RegistActivity.aspx");
        //}
        //else
        //{
        //    Response.Redirect("RegistActivityTeam.aspx");
        //}
    }
    protected void lbtnRegist_PreRender(object sender, EventArgs e)
    {
        (sender as LinkButton).Text = TypeName;
    }
    protected void lbtnCancelRegist_Click(object sender, EventArgs e)
    {

        if (TypeName == "個人報名")
        {
            clsMyObj.ShowMessage("個人報名將取消!"); 
        }
        else if (TypeName == "代理報名")
        {
            OpenRegistedEmployeeSelector1.Visible = true;
            OpenRegistedEmployeeSelector1.InitDataAndShow();

        }
        else if (TypeName == "團隊報名")
        {
            clsMyObj.ShowMessage("該團隊將解散!該團隊成員將全部取消報名。"); 
        }


        //if (((sender as LinkButton).NamingContainer as GridViewRow).RowIndex == 0)
        //{
        //    OpenRegistedEmployeeSelector1.InitDataAndShow();
        //}
        //else
        //{
        //    OpenRegistedTeamSelector1.InitDataAndShow();
        //}
    }
}

public partial class WebForm_RegistActivity_RegistActivityQuery
{
    public string TypeName
    {
        get { return ViewState["TypeName"].ToString(); }
        set { ViewState["TypeName"] = value; }
    }

    public string NextURL
    {
        get { return ViewState["NextURL"].ToString(); }
        set { ViewState["NextURL"] = value; }
    }



}

