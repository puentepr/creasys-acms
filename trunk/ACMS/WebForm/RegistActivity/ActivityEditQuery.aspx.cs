﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class WebForm_RegistActivity_ActivityEditQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (this.Master as MyMasterPage).PanelMainGroupingText = "已報名活動查詢";
            ObjectDataSource1.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;
            btnQuery_Click(null, null);
        }
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType== DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            //報名截止後，不可再編輯(幫別人代理報名)
            if (Convert.ToDateTime(drv["regist_deadline"]) <= DateTime.Today)
            {
                (e.Row.FindControl("lbtnRegistEdit") as LinkButton).Visible = false;            
            }

            //取消報名截止日後，誰都不能再取消報名
            if (Convert.ToDateTime(drv["cancelregist_deadline"]) <= DateTime.Today)
            {
                (e.Row.FindControl("lbtnRegistCancel") as LinkButton).Visible = false;
            }
        
        }


    }







    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["activity_name"].DefaultValue = txtactivity_name.Text;
        ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = txtactivity_startdate.Text;
        ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = txtactivity_enddate.Text;
        ObjectDataSource1.SelectParameters["activity_enddate_finish"].DefaultValue = rblFinish.SelectedValue;

        GridView1.DataBind();
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
            Response.Redirect("RegistActivity_Person.aspx");
        }
        else
        {
            Response.Redirect("RegistActivity_Team.aspx");
        }       

    }


    //取消報名
    protected void lbtnRegistCancel_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[0].ToString();
        string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

        string regist_deadline = (sender as LinkButton).CommandArgument;
        string cancelregist_deadline = (sender as LinkButton).CommandName;

        if (activity_type == "1")
        {
            OpenRegistByMeEmpSelector1.activity_id = activity_id;
            OpenRegistByMeEmpSelector1.regist_by = clsAuth.ID;
            OpenRegistByMeEmpSelector1.regist_deadline =regist_deadline;
            OpenRegistByMeEmpSelector1.cancelregist_deadline = cancelregist_deadline;
            OpenRegistByMeEmpSelector1.InitDataAndShow();
        }
        else
        {
            Response.Redirect("RegistActivity_Team.aspx");
        }




    }




  
}