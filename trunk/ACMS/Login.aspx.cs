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
        clsDBUtility dbUtil = new clsDBUtility();

        if (dbUtil.CheckLogin(txtUserName.Text, txtPassword.Text)==true)
        {
            FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
        }
        else
        {
            clsMyObj.ShowMessage("帳號密碼錯誤!");
        }


    }
}