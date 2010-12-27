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
           
                //Response.Redirect("NoPermission.aspx");
                System.Security.Principal.WindowsIdentity windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();


                string userName = windowsIdentity.Name;
                userName = userName.Substring(userName.IndexOf("\\") + 1); ;
                UserLogin(userName);
                Response.Redirect("Default.aspx");
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
            //// 將管理者登入的 Cookie 設定成 Session Cookie
            //bool isPersistent = false;

            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
            // LoginID,             
            //  DateTime.Now,
            //  DateTime.Now.AddMinutes(30),
            //  isPersistent,
            //  UserData,
            //  FormsAuthentication.FormsCookiePath);

            //string encTicket = FormsAuthentication.Encrypt(ticket);

            //// Create the cookie.
            //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            //Response.Redirect(FormsAuthentication.GetRedirectUrl(LoginID, false));
        

            ////FormsAuthentication.RedirectFromLoginPage(LoginID, false);




            System.Security.Principal.WindowsIdentity windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
            string[] roles = new string[1];
            



            // Construct a GenericIdentity object based on the current Windows
            // identity name and authentication type.
            string authenticationType = windowsIdentity.AuthenticationType;
            string userName = windowsIdentity.Name;
            userName = userName.Substring(userName.IndexOf("\\") + 1);
            myLoginDAO.CheckLogin(userName, out UserData);
            if (windowsIdentity.IsAuthenticated)
            {
                // Add custom NetworkUser role.
                roles[0] = UserData;
            }
            System.Security.Principal.GenericIdentity genericIdentity =
                new System.Security.Principal.GenericIdentity(userName, authenticationType);

            // Construct a GenericPrincipal object based on the generic identity
            // and custom roles for the user.
            System.Security.Principal.GenericPrincipal genericPrincipal =
                new System.Security.Principal.GenericPrincipal(genericIdentity, roles);

            Context.User = genericPrincipal;
        }
        else
        {
            Response.Redirect("NoID.aspx");
        }
    
    }
}