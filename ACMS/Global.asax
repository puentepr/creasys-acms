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
        if (Request.IsAuthenticated)
        {
            // 先取得該使用者的 FormsIdentity
            FormsIdentity id = (FormsIdentity)User.Identity;
            // 再取出使用者的 FormsAuthenticationTicket
            FormsAuthenticationTicket ticket = id.Ticket;
            // 將儲存在 FormsAuthenticationTicket 中的角色定義取出，並轉成字串陣列
            string[] roles = ticket.UserData.Split(new char[] { ',' });
            // 指派角色到目前這個 HttpContext 的 User 物件去

            Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, roles);
            //System.Security.Principal.WindowsIdentity windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //string[] roles = new string[1];
            //ACMS.DAO.LoginDAO myLoginDAO = new ACMS.DAO.LoginDAO();
            //string UserData;



            //// Construct a GenericIdentity object based on the current Windows
            //// identity name and authentication type.
            //string authenticationType = windowsIdentity.AuthenticationType;
            //string userName = windowsIdentity.Name;
            //userName = userName.Substring(userName.IndexOf("\\") + 1);
            //myLoginDAO.CheckLogin(userName, out UserData);
            //if (windowsIdentity.IsAuthenticated)
            //{
            //    // Add custom NetworkUser role.
            //    roles[0] = UserData;
            //}
            //System.Security.Principal.GenericIdentity genericIdentity =
            //    new System.Security.Principal.GenericIdentity(userName, authenticationType);

            //// Construct a GenericPrincipal object based on the generic identity
            //// and custom roles for the user.
            //System.Security.Principal.GenericPrincipal genericPrincipal =
            //    new System.Security.Principal.GenericPrincipal(genericIdentity, roles);

            //Context.User = genericPrincipal;

        }
    }
       
</script>
