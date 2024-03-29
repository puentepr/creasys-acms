﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.VisualBasic;

public partial class MasterPage1 : System.Web.UI.MasterPage
{
    public string PanelMainGroupingText
    {
        get
        {
            return (ViewState["PanelMainGroupingText"] == null ? "" : ViewState["PanelMainGroupingText"].ToString());
        }
        set
        {
            ViewState["PanelMainGroupingText"] = value;
        }



    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text =string.Format("{0}",clsAuth.NATIVE_NAME.Trim());
        if (!IsPostBack)
        {
            ACMS.DAO.LoginDAO myLoginDAO = new ACMS.DAO.LoginDAO();
            string UserData;



            // Construct a GenericIdentity object based on the current Windows
            // identity name and authentication type.
            string userName = Context.User.Identity.Name;
            userName = userName.Substring(userName.IndexOf("\\") + 1);
            myLoginDAO.CheckLogin(userName, out UserData);
            if (UserData == "")//無群組
            {
                ActivityManagement.Visible = false;
                RightsManagement.Visible = false;
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendLine("");
            //    sb.AppendLine("<script>");
            //    sb.AppendLine("document.getElementById('ActivityManagement').style.display  = 'none';");
            //    sb.AppendLine("document.getElementById('RightsManagement').style.display  = 'none';");
            //    sb.AppendLine("</script>");
            //    ScriptManager.RegisterStartupScript(Panel2, typeof(BasePage), "aaam", sb.ToString(), false);
            }

            if ((UserData.IndexOf("2") == -1 && UserData.IndexOf("1") == -1) || UserData == "")//活動管理人及無群組
            {
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendLine("");
            //    sb.AppendLine("<script>");
            //    sb.AppendLine("document.getElementById('ActivityEditQuery').style.display  = 'none';");
            //    sb.AppendLine("document.getElementById('RightsManagement').style.display  = 'none';");
            //    sb.AppendLine("</script>");
            //    ScriptManager.RegisterStartupScript(Panel2, typeof(BasePage), "aaam", sb.ToString(), false);
                ActivityEditQuery.Visible = false;
                RightsManagement.Visible = false;
            }

           
            if (UserData.IndexOf("1") == -1)//非權限管理者不可進入權限管理
            {
                RightsManagement.Visible = false;
                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("");
                //sb.AppendLine("<script>");             
                //sb.AppendLine("document.getElementById('RightsManagement').style.display  = 'none';");
                //sb.AppendLine("</script>");
                //ScriptManager.RegisterStartupScript(Panel2, typeof(BasePage), "aaam", sb.ToString(), false);  
            }

        }
    }
}

