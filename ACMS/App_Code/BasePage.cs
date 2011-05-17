//using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

//****************************************************
//功能說明：底層網頁
//建立人員：andy
//建立日期：2011/1/27
//****************************************************
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.Web.UI;
public class BasePage : System.Web.UI.Page
{
    public void RequestLogin()
    {
        
        Response.Redirect(this.ResolveUrl("~/Login.aspx"));
       
    }

    protected override void OnPreInit(System.EventArgs e)
    {
        if (Session["UID"] == null)
        {
            RequestLogin();
        }

        //'以代理人的UID檢查權限.//利於將來可以有代理人的機制//ActualUID是登入者的UID//


        if ((IsPostBack))
        {

        }
        else
        {
            //'ShowMessage(Me.AppRelativeVirtualPath.ToString())
            //'ShowMessage(FullBaseUrl)
            //'ShowMessage(Me.Request.Url.AbsoluteUri)

        }

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



        //if (!(Request.IsAuthenticated))
        //{
        //    Response.Redirect("~/NoPermission.aspx");
        //}


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




        base.OnPreInit(e);
    }



    public  void WriteErrorLog(string FunctionName, string errMsg, string errStatus)
    {
        ACMS.DAO.BaseDAO dao1 = new ACMS.DAO.BaseDAO();
        dao1.ErrorLog(this.Request.Url.AbsolutePath, errMsg, FunctionName, clsAuth.WORK_ID + clsAuth.NATIVE_NAME, errStatus);
        clsMyObj.ShowMessage(errMsg);
    }


   


    /// <summary>
    /// 有UpdatePanel時秀出訊息
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="Msg"></param>
    /// <remarks>有UpdatePanel時秀出訊息</remarks>

    public static void ShowMessageForAjax(Control  obj, string Msg)
    {

        string vbCrLf = "\r\n";
        ScriptManager.RegisterStartupScript (obj, typeof(BasePage), "ShowMessage", "alert(\"" + Msg.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace(vbCrLf, "\\r\\n") + "\");", true);


    }



	/// <summary>
	/// 處理程式沒有攔到錯誤的地方,不要秀出原始檔等相關的頁面出來
	/// </summary>
	/// <param name="e"></param>
	/// <remarks>處理程式沒有攔到錯誤的地方,不要秀出原始檔等相關的頁面出來</remarks>
	protected override void OnError(System.EventArgs e)
	{
        
        string vbCrLf = "\r\n";
        string vbTab = "\t";
		Exception ex = Server.GetLastError();
		string Msg = null;
		//= ex.Message
		Msg = GetInnerException(ex, ".OnError");
		// Prmpt Msg
        if (Msg.IndexOf("MAC failed") > -1)
        {
            Msg = "您已閒置系統超過15分鐘，請您重新登入!";
        }
        WriteErrorLog("OnError", Msg, "0");
        Response.Write("<script language=\"javascript\">alert(\"" + Msg.Replace("\\", "\\\\").Replace(vbCrLf, "\\r\\n").Replace("\"", "'").Replace(vbTab ,"") + "\")</Script>");
        Server.ClearError();
		base.OnError(e);
	}

	/// <summary>
	/// 取出錯誤的相關訊息
	/// </summary>
	/// <param name="ex"></param>
	/// <param name="Source"></param>
	/// <param name="Params"></param>
	/// <returns> 取出錯誤的相關訊息</returns>
	/// <remarks> 取出錯誤的相關訊息</remarks>
	public static string GetInnerException(Exception ex, string Source, params string[] Params)
	{
		StringBuilder strMsg = new StringBuilder();
		StringBuilder strLogMsg = new StringBuilder();
		string strKey = System.DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString("000");
		string strOtherMsg = "";

		if (Params.Length > 0) {
			foreach (string s in Params) {
				strOtherMsg += ", " + s;
			}
		}

		if (strOtherMsg.Length > 2)
			strOtherMsg = strOtherMsg.Substring(2);
		strLogMsg.AppendLine(string.Format("Source:[{0}], Key={1}", Source, strKey));

		if (ex is System.Data.SqlClient.SqlException) {
			foreach (System.Data.SqlClient.SqlError er in ((System.Data.SqlClient.SqlException)ex).Errors) {
				strLogMsg.AppendLine(er.ToString());
				strMsg.AppendLine(er.Message.ToString());
			}
		} else {
			strLogMsg.AppendLine(ex.ToString());
			strMsg.AppendLine(ex.Message.ToString());
		}

		strMsg.AppendLine("(" + strKey + ")");
		if (string.IsNullOrEmpty(strOtherMsg))
			strLogMsg.AppendLine(strOtherMsg);
		//WriteLog(strLogMsg.ToString(), LogType.Error)

		return string.Format("[{0}]：{1}", Source, strMsg.ToString());
	}




}


