using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

/// <summary>
/// clsAuth 的摘要描述
/// </summary>
public partial class clsAuth
{
    public clsAuth()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }
}

public partial class clsAuth
{
    #region Property

    public static string UserID
    {
        get
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }

    public static string emp_cname
    {
        get
        {
            return GetCookie("emp_cname");
        }
        set
        {
            SetCookie(value, "emp_cname");
        }
    }

    public static string email
    {
        get
        {
            return GetCookie("email");
        }
        set
        {
            SetCookie(value, "email");
        }
    }

    public static string dept_id
    {
        get
        {
            return GetCookie("dept_id");
        }
        set
        {
            SetCookie(value, "dept_id");
        }
    }

    public static string dept_name
    {
        get
        {
            return GetCookie("dept_name");
        }
        set
        {
            SetCookie(value, "dept_name");
        }
    }

    public static string role_ids
    {
        get
        {
            return GetCookie("role_ids");
        }
        set
        {
            SetCookie(value, "role_ids");
        }
    }

    public static string role_names
    {
        get
        {
            return GetCookie("role_names");
        }
        set
        {
            SetCookie(value, "role_names");
        }
    }


    private static string GetCookie(string strParam)
    {
        return (System.Web.HttpContext.Current.Request.Cookies[strParam] == null ? string.Empty : HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[strParam].Value));
    }

    private static void SetCookie(string strValue, string strParam)
    {
        System.Web.HttpContext.Current.Response.Cookies[strParam].Value = HttpContext.Current.Server.UrlEncode(strValue);
        System.Web.HttpContext.Current.Response.Cookies[strParam].Expires = DateTime.Now.AddMonths(1);
    }











    #endregion












    


}


public partial class clsMailInfo
{ 

    private string _Subject;

	public string Subject
	{
		get { return _Subject;}
		set { _Subject = value;}
	}


    private string _EmailTo;

    public string EmailTo
    {
        get { return _EmailTo; }
        set { _EmailTo = value; }
    }

    private string _Content;

    public string Content
    {
        get { return _Content; }
        set { _Content = value; }
    }


}