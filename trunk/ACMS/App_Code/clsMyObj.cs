using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;

/// <summary>
/// clsMyObj 的摘要描述
/// </summary>
public class clsMyObj
{
	public clsMyObj()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public static void ShowMessage(string message)
    {
        string js = null;

        js = string.Format("alert('{0}');", message);
        ScriptManager.RegisterClientScriptBlock(HttpContext.Current.Handler as Page, typeof(string),string.Format("alert_{0}",DateTime.Now.ToString("hhmmss")), js, true);
    }

    public static void ShowMessage(string format, params object[] args)
    {
        clsMyObj.ShowMessage(string.Format(format, args));
    }

    public static DataTable GetDataTable(DataSet DS)
    {
        if (DS == null || DS.Tables.Count == 0 || DS.Tables[0] == null)
        {
            return null;
        }
        else
        {
            return DS.Tables[0];
        }
    }

}