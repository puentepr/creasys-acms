using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

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

    public static void CheckFull(ref DataTable myDataTable)
    {
        if (myDataTable != null)
        {
            myDataTable.Columns.Add("is_full");

            foreach (DataRow DR in myDataTable.Rows)
            {
                int intTMP = Convert.ToInt32(DR["limit_count"]) - Convert.ToInt32(DR["register_count"]);

                if (intTMP > 0)
                {
                    //未額滿顯示"可報名人數"
                    DR["is_full"] = intTMP.ToString();
                }
                else
                {
                    //已額滿顯示"額滿"
                    DR["is_full"] = "額滿";
                }
            }

            myDataTable.AcceptChanges();
        }
    }    

}

public class RegistGoSecondEventArgs : EventArgs
{
    public RegistGoSecondEventArgs(int activity_id)
    {
        this._id = activity_id;
    }

    private int _id;

    public int activity_id
    {
        get { return _id; }
    }

}
