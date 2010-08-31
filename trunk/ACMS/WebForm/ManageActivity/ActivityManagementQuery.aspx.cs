﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ActivityManagementQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //新增個人活動
    protected void btnAddActivity_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityEdit.aspx");
    }

    //新增團隊活動
    protected void btnAddActivityTeam_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityTeamEdit.aspx");
    }

    //編輯活動
    protected void lbtnEditActivaty_Click(object sender, EventArgs e)
    {
        if (((sender as LinkButton).NamingContainer as GridViewRow).RowIndex == 0)
        {
            Response.Redirect("ActivityEdit.aspx");
        }
        else
        {
            Response.Redirect("ActivityTeamEdit.aspx");
        }
    }
}