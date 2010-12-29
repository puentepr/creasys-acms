using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebForm_RegistActivity_OpenRegisedTeammemberSelector : System.Web.UI.UserControl
{
    public delegate void CancelTeamRegistDelegate(object sender, EventArgs e);
    public event CancelTeamRegistDelegate CancelTeamRegistClick;

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.InitDataAndShow();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string emp_id = "";

        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if ((gvr.FindControl("CheckBox1") as CheckBox).Checked == true)
            {
                emp_id += string.Format("{0}", GridView1.DataKeys[gvr.RowIndex].Value.ToString());
                break;
            }
        }
        if (string.Compare(emp_id, "") == 0)
        {
            return;
        }
        Session["ActID"] = activity_id;
        Session["EmpID"] = emp_id;
        Response.Redirect("~/WebForm/ManageActivity/ActivityQuery.aspx");



        //if (emp_id.EndsWith(","))
        //{
        //    emp_id = emp_id.Substring(0, emp_id.Length - 1);
        //}

        //if (!string.IsNullOrEmpty(emp_id))
        //{
        //    MySingleton.AlterRegistResult MyResult = MySingleton.GetMySingleton().AlterRegist_Team(null, null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id, regist_deadline, cancelregist_deadline, ((Button)sender).Page .Request.Url.AbsoluteUri.Substring (0,Request.Url.AbsoluteUri.IndexOf('/', 7))+"/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx");
        //    //.ResolveUrl("~/WebForm/RegistActivity/RegistedActivityQuery.aspx"));

        //    GridView1.DataBind();

        //    if (MyResult == MySingleton.AlterRegistResult.CancelRegistSucess)
        //    {
        //        clsMyObj.ShowMessage("取消報名完成。");
        //    }
        //    else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail_DayOver)
        //    {
        //        clsMyObj.ShowMessage("取消報名截止日之後無法取消報名!。");
        //    }
        //    else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail)
        //    {
        //        clsMyObj.ShowMessage("取消報名失敗!。");
        //    }

        //}

        //if (CancelTeamRegistClick != null)
        //{
        //    CancelTeamRegistClick(this, e);
        //}





    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //RadioButton RadioButton1 = (RadioButton)e.Row.FindControl("RadioButton1");
            //CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");

            ////给每个RadioButton1绑定setRadio事件
            //try
            //{
            //    RadioButton1.Attributes.Add("onclick", "setRadio_GridView1(this)");
            //}
            //catch (Exception)
            //{ }

            //DataRowView drv = (DataRowView)e.Row.DataItem;

            ////隊長
            //if (drv["emp_id"].ToString() == drv["boss_id"].ToString())
            //{
            //    RadioButton1.Checked = true;
            //}

            ////是隊長才能更改誰當新隊長
            //RadioButton1.Enabled = (this.IsTeamBoss == "1");
            
            //if (this.IsTeamBoss == "1")
            //{
            //    //隊長不能取消自己，但可以取消其他任何人
            //    if (drv["emp_id"].ToString() == clsAuth.ID)
            //    {
            //        CheckBox1.Enabled = false;
            //    }
            //    else
            //    {
            //        CheckBox1.Enabled = true;
            //    }
            //}
            //else
            //{
            //    //非隊長只能幫自己取消報名，並且預設勾選自己
            //    CheckBox1.Enabled = (drv["emp_id"].ToString() == clsAuth.ID);
            //    CheckBox1.Checked = (drv["emp_id"].ToString() == clsAuth.ID);
            //}

        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        //this.mpSearch.Show();  

        ////RadioButton RadioButton1 = sender as RadioButton;
        ////RadioButton1.Checked = true;

        ////更改隊長
        //ACMS.DAO.ActivityTeamMemberDAO myActivityTeamMemberDAO = new ACMS.DAO.ActivityTeamMemberDAO();

        //myActivityTeamMemberDAO.ChangeBoss(new Guid(activity_id), GridView1.DataKeys[((sender as RadioButton).NamingContainer as GridViewRow).RowIndex].Value.ToString(), emp_id);

        //if (myActivityTeamMemberDAO.IsTeamBoss(new Guid(activity_id), emp_id))
        //{
        //    IsTeamBoss = "1";
        //}
        //else
        //{
        //    IsTeamBoss = "0";
        //}




        //GridView1.DataBind();

        //GridView_RegisterPeoplinfo.SelectedIndex = (RadioButton1.NamingContainer as GridViewRow).RowIndex;

        //EmpID = GridView_RegisterPeoplinfo.DataKeys[GridView_RegisterPeoplinfo.SelectedIndex].Value.ToString();
    }
}

public partial class WebForm_RegistActivity_OpenRegisedTeammemberSelector
{
    public string activity_id
    {
        get { return (ViewState["activity_id"] == null ? "" : ViewState["activity_id"].ToString()); }
        set { ViewState["activity_id"] = value; }
    }

    public string emp_id
    {
        get { return (ViewState["emp_id"] == null ? "" : ViewState["emp_id"].ToString()); }
        set { ViewState["emp_id"] = value; }
    }

    public string IsTeamBoss
    {
        get { return (ViewState["IsTeamBoss"] == null ? "0" : ViewState["IsTeamBoss"].ToString()); }
        set { ViewState["IsTeamBoss"] = value; }
    }


    public string regist_deadline
    {
        get { return (ViewState["regist_deadline"] == null ? "" : ViewState["regist_deadline"].ToString()); }
        set { ViewState["regist_deadline"] = value; }
    }

    public string cancelregist_deadline
    {
        get { return (ViewState["cancelregist_deadline"] == null ? "" : ViewState["cancelregist_deadline"].ToString()); }
        set { ViewState["cancelregist_deadline"] = value; }
    }

    public void InitDataAndShow()
    {
        if (string.Compare(ddlDEPT_ID.SelectedValue, "") == 0)
        {
            GridView1.Visible = false;
        }
        else
        {
            GridView1.Visible = true;
        }

        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = activity_id;
        
        ObjectDataSource1.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        if (string.Compare(ddlJOB_GRADE_GROUP.SelectedValue, "") == 0)
        {
            ObjectDataSource1.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = "999";
        }
        else
        {
            ObjectDataSource1.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = ddlJOB_GRADE_GROUP.SelectedValue;
        }
        ObjectDataSource1.SelectParameters["WINDOWS_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource1.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource1.SelectParameters["SEX"].DefaultValue = rblSEX.SelectedValue;
        if (string.Compare(txtEXPERIENCE_START_DATE.Text, "") == 0)
        {
            ObjectDataSource1.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = "1900/1/1";

        }
        else
        {
            ObjectDataSource1.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = txtEXPERIENCE_START_DATE.Text;
        }
        ObjectDataSource1.SelectParameters["C_NAME"].DefaultValue = ddlC_NAME.SelectedValue; ;


        ACMS.DAO.ActivityTeamMemberDAO myActivityTeamMemberDAO = new ACMS.DAO.ActivityTeamMemberDAO();

        if (myActivityTeamMemberDAO.IsTeamBoss(new Guid(activity_id), emp_id))
        {
            IsTeamBoss = "1";
        }
        else
        {
            IsTeamBoss = "0";
        }

        GridView1.DataBind();
        this.mpSearch.Show();    
    }

}
