﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebForm_RegistActivity_OpenRegisedTeammemberSelectorX : System.Web.UI.UserControl
{
    public delegate void CancelTeamRegistDelegate(object sender, EventArgs e);
    public event CancelTeamRegistDelegate CancelTeamRegistClick;

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {



        string emp_id1 = "";
        string emp_id2 = "";
        string path = Server.MapPath("~/UpFiles");

        //  先檢查是否低於下限

        int membersInt = 0;

        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if ((gvr.FindControl("CheckBox1") as CheckBox).Checked == false)
            {
                membersInt += 1;
            }

        }

        ACMS.BO.ActivatyBO aBO = new ACMS.BO.ActivatyBO();
        ACMS.VO.ActivatyVO aVO = aBO.SelectActivatyByActivatyID(new Guid(activity_id));

        if (membersInt < aVO.team_member_min)
        {

           // string sdoPostScript = "  __doPostBack('" + btnCancelAll1.ClientID + "','1');   ";
           // string ScriptAll = " if (confirm('取消報名則團隊人數將低於下限,是否要全隊取消報名?')==true) { alert ('1232456'); window.open('http://www.google.com.tw');" + sdoPostScript + "} ";
          //  string ScriptAll = " if (confirm('取消報名則團隊人數將低於下限,是否要全隊取消報名?')==true) { alert ('1232456')} ";

            //BasePage.RunClientScript (this.Page , ScriptAll);

            //clsMyObj.ShowMessage("若您取消報名則團隊人數將低於下限，因此系統將取消整個團隊的報名資格，若確定要取消報名，請點選「確定」按鈕後於下個視窗點選「確定取消報名」按鈕!");
            btnOK.Visible = false;
            btnCancelAll.Visible = false;
            btnOK0.Visible = true;
            lblMessage.Visible = true;
            GridView1.Visible = false;
            mpSearch.Show();
            return;

        }
        
        
        
        
        //已換隊長

        if (newBoss != "")
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                if ((gvr.FindControl("CheckBox1") as CheckBox).Checked == true)
                {
                    if (GridView1.DataKeys[gvr.RowIndex].Value.ToString() == newBoss)
                    {
                        clsMyObj.ShowMessage("您為此隊隊長，請更換隊長，再進行取消!");
                        this.mpSearch.Show();
                        return;
                    }

                }
            }
            ACMS.DAO.ActivityTeamMemberDAO myActivityTeamMemberDAO = new ACMS.DAO.ActivityTeamMemberDAO();
            myActivityTeamMemberDAO.ChangeBoss(new Guid(activity_id), newBoss, emp_id);

        }

        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if ((gvr.FindControl("CheckBox1") as CheckBox).Checked == true && (gvr.FindControl("RadioButton1") as RadioButton).Checked == true)
            {
                clsMyObj.ShowMessage("您為此隊隊長，請更換隊長，再進行取消!");
                    this.mpSearch.Show();
                    return;
               
            }
        }

       


        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if ((gvr.FindControl("CheckBox1") as CheckBox).Checked == true)
            {
                emp_id1 += string.Format("{0},", GridView1.DataKeys[gvr.RowIndex].Value.ToString());
            }
            emp_id2 += string.Format("{0},", GridView1.DataKeys[gvr.RowIndex].Value.ToString());
        }

        if (emp_id1.EndsWith(","))
        {
            emp_id1 = emp_id1.Substring(0, emp_id1.Length - 1);
        }

        if (emp_id1 == "" && newBoss == "")
        {
            clsMyObj.ShowMessage("您未取消報名也未更換隊長,程式沒有修改任資料!");

            return;
        }

        MySingleton.AlterRegistResult MyResult = MySingleton.AlterRegistResult.CancelRegistSucess;
        if (!string.IsNullOrEmpty(emp_id1))
        {
            string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');

            //MySingleton.AlterRegistResult MyResult = MySingleton.GetMySingleton().AlterRegist_Team(null, null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id1, regist_deadline, cancelregist_deadline, ((Button)sender).Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx", path);
            MyResult = MySingleton.GetMySingleton().AlterRegist_Team(null, null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id1, regist_deadline, cancelregist_deadline, aa + "/Default.aspx", path, "", aa + "/Default.aspx");
            //.ResolveUrl("~/WebForm/RegistActivity/RegistedActivityQuery.aspx"));

            GridView1.DataBind();

           
        }

        if (CancelTeamRegistClick != null)
        {
            this.Visible = false;
            CancelTeamRegistClick(this, e);
        }

        ACMS.DAO.ActivityRegistDAO regDao = new ACMS.DAO.ActivityRegistDAO ();
        emp_id2 = emp_id2.TrimEnd(',');
        string members = regDao.AllTeamMemberByMembers(new Guid(activity_id), emp_id2);

        if (members == "")
        {
            clsMyObj.ShowMessage("已達人數下限,目前已取消該隊的報名資格");
            return;
        }
        if (MyResult == MySingleton.AlterRegistResult.CancelRegistSucess)
        {
            if (newBoss == "" && emp_id1 != "")
            {
                clsMyObj.ShowMessage("取消報名完成。");

            }
            if (newBoss != "" && emp_id1 != "")
            {
                clsMyObj.ShowMessage("更換隊長及取消報名完成。");

            }
            if (newBoss != "" && emp_id1== "")
            {
                clsMyObj.ShowMessage("更換隊長完成。");

            }
        }
        else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail_DayOver)
        {
            clsMyObj.ShowMessage("取消報名截止日之後無法取消報名!。");
        }
        else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail)
        {
            clsMyObj.ShowMessage("取消報名失敗!。");
        }

    }

    //=======全隊取消
    protected void btnCancelAll_Click(object sender, EventArgs e)
    {



        string emp_id1 = "";
        string path = Server.MapPath("~/UpFiles");
        foreach (GridViewRow gvr in GridView1.Rows)
        {

            emp_id1 += string.Format("{0},", GridView1.DataKeys[gvr.RowIndex].Value.ToString());

        }

        if (emp_id1.EndsWith(","))
        {
            emp_id1 = emp_id1.Substring(0, emp_id1.Length - 1);
        }
        MySingleton.AlterRegistResult MyResult = MySingleton.AlterRegistResult.CancelRegistSucess;
        if (!string.IsNullOrEmpty(emp_id1))
        {
            string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');

            //MySingleton.AlterRegistResult MyResult = MySingleton.GetMySingleton().AlterRegist_Team(null, null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id1, regist_deadline, cancelregist_deadline, ((Button)sender).Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx", path);
            MyResult = MySingleton.GetMySingleton().AlterRegist_Team(null, null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id1, regist_deadline, cancelregist_deadline, aa + "/Default.aspx", path, "All", aa + "/Default.aspx");
            //.ResolveUrl("~/WebForm/RegistActivity/RegistedActivityQuery.aspx"));

            GridView1.DataBind();



        }

        if (CancelTeamRegistClick != null)
        {
            this.Visible = false;
            CancelTeamRegistClick(this, e);
        }



        if (MyResult == MySingleton.AlterRegistResult.CancelRegistSucess)
        {
            clsMyObj.ShowMessage("取消報名完成。");
        }
        else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail_DayOver)
        {
            clsMyObj.ShowMessage("取消報名截止日之後無法取消報名!。");
        }
        else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail)
        {
            clsMyObj.ShowMessage("取消報名失敗!。");
        }

    }




    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButton RadioButton1 = (RadioButton)e.Row.FindControl("RadioButton1");
            CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");

            //给每个RadioButton1绑定setRadio事件
            try
            {
                RadioButton1.Attributes.Add("onclick", "setRadio_GridView1(this)");
            }
            catch (Exception)
            { }

            DataRowView drv = (DataRowView)e.Row.DataItem;

            //隊長
            if (drv["emp_id"].ToString() == drv["boss_id"].ToString())
            {
                RadioButton1.Checked = true;
            }

            //是隊長才能更改誰當新隊長
            RadioButton1.Enabled = (this.IsTeamBoss == "1");

            
            if (this.IsManager == "0")
            {

                if (this.IsTeamBoss == "1")
                {
                    //隊長不能取消自己，但可以取消其他任何人//2011/2/17日修改成可以取消所有人
                    if (drv["emp_id"].ToString() == drv["boss_id"].ToString())
                    {
                        CheckBox1.Enabled = true;
                    }
                    else
                    {
                        CheckBox1.Enabled = true;
                    }
                }
                else
                {
                    //非隊長只能幫自己取消報名，並且預設勾選自己
                    CheckBox1.Enabled = (drv["emp_id"].ToString() == clsAuth.ID);
                    CheckBox1.Checked = (drv["emp_id"].ToString() == clsAuth.ID);
                }
            }
            else//從管理來的取消可以修改任何人
            {
                CheckBox1.Enabled = true;

            }


        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();  

        //RadioButton RadioButton1 = sender as RadioButton;
        //RadioButton1.Checked = true;

        //更改隊長//2011/2/17日修改為按確定後才可以修改DB
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

        newBoss = GridView1.DataKeys[((sender as RadioButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();


        //GridView1.DataBind();

        //GridView_RegisterPeoplinfo.SelectedIndex = (RadioButton1.NamingContainer as GridViewRow).RowIndex;

        //EmpID = GridView_RegisterPeoplinfo.DataKeys[GridView_RegisterPeoplinfo.SelectedIndex].Value.ToString();
    }
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show(); 
    }
    protected void btnOK0_Click(object sender, EventArgs e)
    {
        btnCancelAll_Click(sender, e);
    }
  
}

public partial class WebForm_RegistActivity_OpenRegisedTeammemberSelectorX
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

    public string newBoss
    {
        get { return (ViewState["newBoss"] == null ? "" : ViewState["newBoss"].ToString()); }
        set { ViewState["newBoss"] = value; }
    }
    public string oldBoss
    {
        get { return (ViewState["oldBoss"] == null ? "" : ViewState["oldBoss"].ToString()); }
        set { ViewState["oldBoss"] = value; }
    }
    public string IsTeamBoss
    {
        get { return (ViewState["IsTeamBoss"] == null ? "0" : ViewState["IsTeamBoss"].ToString()); }
        set { ViewState["IsTeamBoss"] = value; }
    }

    public string IsManager
    {
        get { return (ViewState["IsManager"] == null ? "0" : ViewState["IsManager"].ToString()); }
        set { ViewState["IsManager"] = value; }
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
        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = activity_id;
        ObjectDataSource1.SelectParameters["emp_id"].DefaultValue = emp_id;

        ACMS.DAO.ActivityTeamMemberDAO myActivityTeamMemberDAO = new ACMS.DAO.ActivityTeamMemberDAO();
        ACMS.DAO.ActivatyDAO myADAO = new ACMS.DAO.ActivatyDAO();
        if (myActivityTeamMemberDAO.IsTeamBoss(new Guid(activity_id), emp_id))
        {
            IsTeamBoss = "1";
        }
        else
        {
            IsTeamBoss = "0";
        }
        if (IsTeamBoss == "0" && myADAO.chkAdmin(new Guid(activity_id), emp_id) == false)
        {
            btnCancelAll.Visible = false;
        }
        else
        {
            btnCancelAll.Visible = true;
        
        }
        GridView1.Visible = true;
        GridView1.DataBind();
        this.mpSearch.Show();
        btnOK.Visible = true;
        btnOK0.Visible = false;
        lblMessage.Visible = false;
       
    }

}
