using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // 登入時清空所有 Session 資料
        Session.RemoveAll();



        clsDBUtility dbUtil = new clsDBUtility();

        string UserData;

        if (dbUtil.CheckLogin(txtUserName.Text, txtPassword.Text, out UserData) == true)
        {

            // 將管理者登入的 Cookie 設定成 Session Cookie
            bool isPersistent = false;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
             txtUserName.Text,
              DateTime.Now,
              DateTime.Now.AddMinutes(30),
              isPersistent,
              UserData,
              FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Text,false));

            //FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
        }
        else
        {
            clsMyObj.ShowMessage("帳號密碼錯誤!");
        }


    }
}