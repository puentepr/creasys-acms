﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_RegistActivityTeam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //OpenUnRegistEmployeeSelector1.InitDataAndShow();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        PanelB.Visible = true; 
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistActivityQuery.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistActivityQuery.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        PanelA.Visible = true;
        PanelB.Visible = true;
        btnBack.Visible = false;
        btnNext.Visible = true;
        PanelMember.Visible = false;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        PanelA.Visible = false;
        PanelB.Visible = false;
        btnBack.Visible = true;
        btnNext.Visible = false;
        PanelMember.Visible = true;
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        PanelA.Visible = false;
        PanelB.Visible = false;
        btnBack.Visible = true;
        btnNext.Visible = false;
        PanelMember.Visible = true;
    }
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        OpenEmployeeSelector1.InitDataAndShow();
    }

}