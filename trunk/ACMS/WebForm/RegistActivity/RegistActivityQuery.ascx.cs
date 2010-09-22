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

    public delegate void GoThirdStepDelegate(object sender, RegistGoSecondEventArgs e);
    public event GoThirdStepDelegate GoThirdStep_Click;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Panel1.GroupingText = TypeName;
        }
    }


    //報名
    protected void lbtnRegist_Click(object sender, EventArgs e)
    {
        if (GoSecondStep_Click != null)
        {
            int activity_id = (int)gv_Activity.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;
            GoSecondStep_Click(this, new RegistGoSecondEventArgs(activity_id));
        }
    }

    //報名資料修改
    protected void lbtnRegistEdit_Click(object sender, EventArgs e)
    {
        if (GoThirdStep_Click != null)
        {
            int activity_id = (int)gv_Activity.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;
            GoThirdStep_Click(this, new RegistGoSecondEventArgs(activity_id));
        }
    }

    //取消報名
    protected void lbtnCancelRegist_Click(object sender, EventArgs e)
    {

        //OpenRegistedEmployeeSelector1.Visible = true;
        //OpenRegistedEmployeeSelector1.InitDataAndShow();

        //if (TypeName == "個人報名")
        //{
        //    clsMyObj.ShowMessage("個人報名將取消!"); 
        //}
        //else if (TypeName == "代理報名")
        //{


        //}
        //else if (TypeName == "團隊報名")
        //{
        //    clsMyObj.ShowMessage("該團隊將解散!該團隊成員將全部取消報名。"); 
        //}


        if (((sender as LinkButton).NamingContainer as GridViewRow).RowIndex == 0)
        {
            OpenRegistedEmployeeSelector1.Visible = true;
            OpenRegistedEmployeeSelector1.InitDataAndShow();
        }
        else
        {
            OpenRegistedTeamSelector1.Visible = true;
            OpenRegistedTeamSelector1.InitDataAndShow();
        }
    }


    protected void gv_Activity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView MyDataRowView = (e.Row.DataItem as DataRowView);
            int limit_count = (int)MyDataRowView["limit_count"];
            int RegistCount = (int)MyDataRowView["RegistCount"];

            if (limit_count - RegistCount > 0)
            {
                //未額滿顯示"可報名人數"
                (e.Row.FindControl("lblisfull") as Label).Text = (limit_count - RegistCount).ToString();

                //LinkButton lbtnRegist = (e.Row.FindControl("lbtnRegist") as LinkButton);
                //if (Convert.ToDateTime(MyDataRowView["activity_startdate"]) > DateTime.Now && MyDataRowView["emp_id"] == DBNull.Value)
                //{
                //    //若未過期且本人沒報名則可以報名
                //    lbtnRegistEdit.Text = "報名";
                //    lbtnRegistEdit.CommandName = "Regist";
                //}
                //else
                //{
                //    //若已過期則不可以報名
                //    lbtnRegistEdit.Visible = false;
                //}



            }
            else
            {
                //已額滿顯示"額滿"
                (e.Row.FindControl("lblisfull") as Label).Text = "額滿";
            }










        }
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

