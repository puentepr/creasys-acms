﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class WebForm_RegistActivity_RegistedActivityQuery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //======2011/3/30 分為個人及團隊
                if (Request.QueryString["Type"] != null)
                {
                    Session["Type"] = Request.QueryString["Type"];
                }
                if (Session["Type"] == null)
                {
                    Session["Type"] = "1";
                }
                else
                {
                    if (Session["Type"].ToString() == "2")
                    {
                        //  GridView1.Columns[5].HeaderText = "已報名隊數";
                        GridView1.Columns[4].HeaderText = "可報名隊數";
                        GridView1.Columns[5].HeaderText = "已報名隊數";
                    }
                }


                if (Session["EmpID"] != null)
                {
                    btnQuery.Visible = false;
                    OpenRegisedTeammemberSelector1.Visible = true;
                    OpenRegisedTeammemberSelector1.activity_id = Session["ActID"].ToString();
                    OpenRegisedTeammemberSelector1.emp_id = Session["EmpID"].ToString();
                    ACMS.VO.ActivatyVO vo1 = new ACMS.VO.ActivatyVO();
                    ACMS.BO.ActivatyBO bo1 = new ACMS.BO.ActivatyBO();
                    Guid id1 = new Guid(Session["ActID"].ToString());
                    vo1 = bo1.SelectActivatyByActivatyID(id1);

                    OpenRegisedTeammemberSelector1.regist_deadline = vo1.regist_deadline.ToString();
                    OpenRegisedTeammemberSelector1.cancelregist_deadline = vo1.cancelregist_deadline.ToString();
                    OpenRegisedTeammemberSelector1.InitDataAndShow();
                    Session.Remove("ActID");
                    Session.Remove("EmpID");
                    return;
                }



                //andy 
                if (!(string.IsNullOrEmpty(Request.QueryString["ActID"])))
                {

                    ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
                    ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
                    Guid id = new Guid(Request.QueryString["ActID"]);
                    vo = bo.SelectActivatyByActivatyID(id);


                    txtactivity_name.Text = vo.activity_name;
                }
                if (Session["ActID"] != null)
                {
                    if (!(string.IsNullOrEmpty(Session["ActID"].ToString())))
                    {

                        ACMS.VO.ActivatyVO vo1 = new ACMS.VO.ActivatyVO();
                        ACMS.BO.ActivatyBO bo1 = new ACMS.BO.ActivatyBO();
                        Guid id1 = new Guid(Session["ActID"].ToString());
                        vo1 = bo1.SelectActivatyByActivatyID(id1);


                        txtactivity_name.Text = vo1.activity_name;
                    }
                    Session.Remove("ActID");
                }
                //===========================================
                (this.Master as MyMasterPage).PanelMainGroupingText = "已報名活動查詢";
                ObjectDataSource1.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;
                btnQuery_Click(null, null);


            }
        }
        catch (Exception ex)
        {
            WriteErrorLog("PageLoad", ex.Message, "0");

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType== DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            //因為是已報名活動查詢，所以這裡一定是"編輯"已報名活動
            
            ////報名截止後，不可再編輯(幫別人代理報名)，這裡不控制等店進去之後變唯讀
            //if (Convert.ToDateTime(drv["regist_deadline"]) <= DateTime.Today)
            //{
            //    (e.Row.FindControl("lbtnRegistEdit") as LinkButton).Visible = false;            
            //}

            //取消報名截止日後，誰都不能再取消報名
            if (Convert.ToDateTime(drv["cancelregist_deadline"]) < DateTime.Today)
            {
                (e.Row.FindControl("lbtnRegistCancel") as LinkButton).Visible = false;

            }

            if (rblFinish.SelectedValue == "Y")//歷史資料
            {
                (e.Row.FindControl("lbtnRegistEdit") as LinkButton).Text = "檢視";

            }
            else
            {
                if (Convert.ToDateTime(drv["regist_deadline"]) < DateTime.Today)
                {
                    (e.Row.FindControl("lbtnRegistEdit") as LinkButton).Text = "檢視";

                }
            }
            ((Label)e.Row.FindControl("Label1")).Text = ((Label)e.Row.FindControl("Label1")).Text.Replace("\r\n", "<br/>");
         
        }


    }







    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            ObjectDataSource1.SelectParameters["activity_name"].DefaultValue = txtactivity_name.Text;
            ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = txtactivity_startdate.Text;
            ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = txtactivity_enddate.Text;
            ObjectDataSource1.SelectParameters["activity_enddate_finish"].DefaultValue = rblFinish.SelectedValue;
            ObjectDataSource1.SelectParameters["activity_type"].DefaultValue = Session["Type"].ToString();

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("Query", ex.Message, "0");

        }
    }

    //編輯報名資料
    protected void lbtnRegistEdit_Click(object sender, EventArgs e)
    {
       
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[0].ToString();
        string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();
        Session["form_mode"] = "edit";
        Session["activity_id"] = activity_id;

        if (activity_type == "1")
        {
            Response.Redirect("~/WebForm/RegistActivity/RegistActivity_Person.aspx");
        }
        else
        {
            Response.Redirect("~/WebForm/RegistActivity/RegistActivity_Team.aspx");
        }       

    }


    //取消報名
    protected void lbtnRegistCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[0].ToString();
            string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

            string regist_deadline = (sender as LinkButton).CommandArgument;
            string cancelregist_deadline = (sender as LinkButton).CommandName;

            if (activity_type == "1")
            {
                OpenRegistedByMeEmpSelector1.Visible = true;
                OpenRegistedByMeEmpSelector1.activity_id = activity_id;
                OpenRegistedByMeEmpSelector1.regist_by = clsAuth.ID;
                OpenRegistedByMeEmpSelector1.regist_deadline = regist_deadline;
                OpenRegistedByMeEmpSelector1.cancelregist_deadline = cancelregist_deadline;
                OpenRegistedByMeEmpSelector1.InitDataAndShow();
            }
            else
            {
                OpenRegisedTeammemberSelector1.Visible = true;
                OpenRegisedTeammemberSelector1.activity_id = activity_id;
                OpenRegisedTeammemberSelector1.emp_id = clsAuth.ID;
                OpenRegisedTeammemberSelector1.regist_deadline = regist_deadline;
                OpenRegisedTeammemberSelector1.cancelregist_deadline = cancelregist_deadline;
                OpenRegisedTeammemberSelector1.InitDataAndShow();
            }


        }
        catch (Exception ex)
        {
            WriteErrorLog("CancelRegister", ex.Message, "0");

        }

    }


    //取消個人報名後
    public void CancelPersonRegist_Click(object sender, EventArgs e)
    {
        try
        {
            ObjectDataSource1.SelectParameters["activity_name"].DefaultValue = txtactivity_name.Text;
            ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = txtactivity_startdate.Text;
            ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = txtactivity_enddate.Text;
            ObjectDataSource1.SelectParameters["activity_enddate_finish"].DefaultValue = rblFinish.SelectedValue;
            ObjectDataSource1.SelectParameters["activity_type"].DefaultValue = Session["Type"].ToString();

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("CancelPersonRegist", ex.Message, "0");

        }
    }

    //取消團隊報名後
    public void CancelTeamRegist_Click(object sender, EventArgs e)
    {
        try
        {
            ObjectDataSource1.SelectParameters["activity_name"].DefaultValue = txtactivity_name.Text;
            ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = txtactivity_startdate.Text;
            ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = txtactivity_enddate.Text;
            ObjectDataSource1.SelectParameters["activity_enddate_finish"].DefaultValue = rblFinish.SelectedValue;
            ObjectDataSource1.SelectParameters["activity_type"].DefaultValue = Session["Type"].ToString();

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("CancelTempRegist", ex.Message, "0");

        }
    }



    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            GridView1.Visible = false;
            lblGrideView1.Visible = true;

        }
        else
        {
            GridView1.Visible = true;
            lblGrideView1.Visible = false;
        }
    }
}