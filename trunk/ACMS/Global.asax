<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 應用程式啟動時執行的程式碼
       
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  應用程式關閉時執行的程式碼

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 發生未處理錯誤時執行的程式碼

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 啟動新工作階段時執行的程式碼

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 工作階段結束時執行的程式碼。 
        // 注意: 只有在 Web.config 檔將 sessionstate 模式設定為 InProc 時，
        // 才會引發 Session_End 事件。如果將工作階段模式設定為 StateServer 
        // 或 SQLServer，就不會引發這個事件。

    }

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("NoPermission.aspx".ToLower()) >= 0)
        {
            return;
        }
        if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("NoID.aspx".ToLower()) >= 0)
        {
            return;
        }
     
        if (Request.IsAuthenticated)
        {
            //// 先取得該使用者的 FormsIdentity
            //FormsIdentity id = (FormsIdentity)User.Identity;
            //// 再取出使用者的 FormsAuthenticationTicket
            //FormsAuthenticationTicket ticket = id.Ticket;
            //// 將儲存在 FormsAuthenticationTicket 中的角色定義取出，並轉成字串陣列
            //string[] roles = ticket.UserData.Split(new char[] { ',' });
            //// 指派角色到目前這個 HttpContext 的 User 物件去

            //Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, roles);



           // System.Security.Principal.WindowsIdentity windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
           // string[] roles = new string[1];
           // ACMS.DAO.LoginDAO myLoginDAO = new ACMS.DAO.LoginDAO();
           // string UserData;



           // // Construct a GenericIdentity object based on the current Windows
           // // identity name and authentication type.
           // string authenticationType = windowsIdentity.AuthenticationType;
           // string userName = windowsIdentity.Name;
           // userName = userName.Substring(userName.IndexOf("\\") + 1);
           // if (!myLoginDAO.CheckLogin(userName, out UserData))
           // {
           //     Response.Redirect("~/NoID.aspx");
           // }
           // if (windowsIdentity.IsAuthenticated)
           // {
           //     // Add custom NetworkUser role.
           //     roles[0] = UserData;
           // }
           // System.Security.Principal.GenericIdentity genericIdentity =
           //     new System.Security.Principal.GenericIdentity(userName, authenticationType);

           // // Construct a GenericPrincipal object based on the generic identity
           // // and custom roles for the user.
           // System.Security.Principal.GenericPrincipal genericPrincipal =
           //     new System.Security.Principal.GenericPrincipal(genericIdentity, roles);

           //// Context.User = genericPrincipal;

        }
    }

    void Application_AuthorizeRequest(object sender, EventArgs e)
    {
        return;
        if (HttpContext.Current.Session != null)
        {

            if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("aspx".ToLower()) == -1)
            {
                return;
            }

            //if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("Login.aspx".ToLower()) >=0)
            //{
            //    return;
            //}
            //if (Session["UID"] == null)
            //{
            //    Response.Redirect("~/Login.aspx");
            //}



            ACMS.DAO.LoginDAO myLoginDAO = new ACMS.DAO.LoginDAO();
            string UserData;



            // Construct a GenericIdentity object based on the current Windows
            // identity name and authentication type.
            if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("NoPermission.aspx".ToLower()) >= 0)
            {
                return;
            }
            if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("NoID.aspx".ToLower()) >= 0)
            {
                return;
            }
            //string userName = Context.User.Identity.Name;
            //userName = userName.Substring(userName.IndexOf("\\") + 1);
            string userName = "";
            try
            {
                userName = Session["UID"].ToString();
            }
            catch
            {
                return;
            }
            if (userName == "")
            {
                return;
            }
            if (myLoginDAO.CheckLogin(userName, out UserData) == false)
            {
                Response.Redirect("~/NoID.aspx");
            }



            if (!(Request.IsAuthenticated))
            {
                Response.Redirect("~/NoPermission.aspx");
            }


            //if (Context.User.IsInRole(""))
            //{
            //    Response.Redirect("~/NoPermission.aspx");
            //}

            if ((UserData.IndexOf("2") == -1 && UserData.IndexOf("1") == -1) || UserData == "")//活動管理人及無群組
            {
                if (string.Compare(Context.Request.AppRelativeCurrentExecutionFilePath.ToLower(), "~/WebForm/ManageActivity/ActivityEditQuery.aspx".ToLower()) == 0)//新增修改活動
                {
                    Response.Redirect("~/NoPermission.aspx");
                }
                //else if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("WebForm/ManageActivity/HistoryActivityQuery.aspx".ToLower()) >= 0)//歷史查詢
                //{
                //    Response.Redirect("~/NoPermission.aspx");
                //}
            }

            if (UserData == "")//無群組
            {

                if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("WebForm/ManageActivity/ActivityCheck.aspx".ToLower()) >= 0)//活動進度紀錄
                {
                    Response.Redirect("~/NoPermission.aspx");
                }
                else if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("WebForm/ManageActivity/ActivityEditQuery.aspx".ToLower()) >= 0)//新增修改活動
                {
                    Response.Redirect("~/NoPermission.aspx");
                }
                else if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("WebForm/ManageActivity/ActivityQuery.aspx".ToLower()) >= 0)//報名狀況查詢
                {
                    Response.Redirect("~/NoPermission.aspx");
                }
                else if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("WebForm/ManageActivity/HistoryActivityQuery.aspx".ToLower()) >= 0)//歷史查詢
                {
                    Response.Redirect("~/NoPermission.aspx");
                }

            }

            if (UserData.IndexOf("1") == -1)//非權限管理者不可進入權限管理
            {
                if (Context.Request.AppRelativeCurrentExecutionFilePath.ToLower().IndexOf("WebForm/ManageRole".ToLower()) >= 0)
                {
                    Response.Redirect("~/NoPermission.aspx");
                }
            }
        }

    }
</script>
