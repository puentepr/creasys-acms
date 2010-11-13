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

    public static string ID
    {
        get
        {
            return GetCookie("ID");
        }
        set
        {
            SetCookie(value, "ID");
        }

    }

    public static string NATIVE_NAME
    {
        get
        {
            return GetCookie("NATIVE_NAME");
        }
        set
        {
            SetCookie(value, "NATIVE_NAME");
        }
    }

    public static string ENGLISH_NAME
    {
        get
        {
            return GetCookie("ENGLISH_NAME");
        }
        set
        {
            SetCookie(value, "ENGLISH_NAME");
        }
    }

    public static string WORK_ID
    {
        get
        {
            return GetCookie("WORK_ID");
        }
        set
        {
            SetCookie(value, "WORK_ID");
        }
    }

    public static string OFFICE_MAIL
    {
        get
        {
            return GetCookie("OFFICE_MAIL");
        }
        set
        {
            SetCookie(value, "OFFICE_MAIL");
        }
    }

    public static string DEPT_ID
    {
        get
        {
            return GetCookie("DEPT_ID");
        }
        set
        {
            SetCookie(value, "DEPT_ID");
        }
    }

    public static string C_DEPT_NAME
    {
        get
        {
            return GetCookie("C_DEPT_NAME");
        }
        set
        {
            SetCookie(value, "C_DEPT_NAME");
        }
    }

    public static string C_DEPT_ABBR
    {
        get
        {
            return GetCookie("C_DEPT_ABBR");
        }
        set
        {
            SetCookie(value, "C_DEPT_ABBR");
        }
    }


    public static string OFFICE_PHONE
    {
        get
        {
            return GetCookie("OFFICE_PHONE");
        }
        set
        {
            SetCookie(value, "OFFICE_PHONE");
        }
    }


    public static string EXPERIENCE_START_DATE
    {
        get
        {
            return GetCookie("EXPERIENCE_START_DATE");
        }
        set
        {
            SetCookie(value, "EXPERIENCE_START_DATE");
        }
    }


    public static string BIRTHDAY
    {
        get
        {
            return GetCookie("BIRTHDAY");
        }
        set
        {
            SetCookie(value, "BIRTHDAY");
        }
    }


    public static string SEX
    {
        get
        {
            return GetCookie("SEX");
        }
        set
        {
            SetCookie(value, "SEX");
        }
    }


    public static string JOB_CNAME
    {
        get
        {
            return GetCookie("JOB_CNAME");
        }
        set
        {
            SetCookie(value, "JOB_CNAME");
        }
    }


    public static string STATUS
    {
        get
        {
            return GetCookie("STATUS");
        }
        set
        {
            SetCookie(value, "STATUS");
        }
    }


    public static string WORK_END_DATE
    {
        get
        {
            return GetCookie("WORK_END_DATE");
        }
        set
        {
            SetCookie(value, "WORK_END_DATE");
        }
    }


    public static string COMPANY_CODE
    {
        get
        {
            return GetCookie("COMPANY_CODE");
        }
        set
        {
            SetCookie(value, "COMPANY_CODE");
        }
    }

    public static string C_NAME
    {
        get
        {
            return GetCookie("C_NAME");
        }
        set
        {
            SetCookie(value, "C_NAME");
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