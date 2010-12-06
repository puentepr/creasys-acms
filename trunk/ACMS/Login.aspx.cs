using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("NoPermission.aspx");
            }
            else
            {
                if (ConfigurationManager.AppSettings["PerformType"] == "TrailRun")
                {
                    return;
                }

                string LoginID = Request.ServerVariables["LOGON_USER"].ToString();
                LoginID = LoginID.Substring(LoginID.IndexOf("\\") + 1);

                UserLogin(LoginID);
            }

        }

    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        UserLogin(txtUserName.Text);
    }


    private void UserLogin(string LoginID)
    {
        // 登入時清空所有 Session 資料
        Session.RemoveAll();

        ACMS.DAO.LoginDAO myLoginDAO = new ACMS.DAO.LoginDAO();

        string UserData;     

        if (myLoginDAO.CheckLogin(LoginID, out UserData) == true)
        {
            // 將管理者登入的 Cookie 設定成 Session Cookie
            bool isPersistent = false;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
             LoginID,
              DateTime.Now,
              DateTime.Now.AddMinutes(30),
              isPersistent,
              UserData,
              FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            Response.Redirect(FormsAuthentication.GetRedirectUrl(LoginID, false));

            //FormsAuthentication.RedirectFromLoginPage(LoginID, false);
        }
        else
        {
            Response.Redirect("NoID.aspx");
        }
    
    }
}