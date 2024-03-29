﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenRegistedByMeEmpSelectorX : System.Web.UI.UserControl
{
    public delegate void CancelPersonRegistDelegate(object sender, EventArgs e);
    public event CancelPersonRegistDelegate CancelPersonRegistClick;

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        
        string emp_id = "";
        string path = Server.MapPath("~/UpFiles");
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if ((gvr.FindControl("CheckBox1") as CheckBox).Checked == true)
            {
                emp_id += string.Format("{0},", GridView1.DataKeys[gvr.RowIndex].Value.ToString());
            }

        }

        if (emp_id.EndsWith(","))
        {
            emp_id = emp_id.Substring(0, emp_id.Length - 1);
        }

        MySingleton.AlterRegistResult MyResult = MySingleton.AlterRegistResult.CancelRegistSucess;
        if (!string.IsNullOrEmpty(emp_id))
        {
            // MySingleton.AlterRegistResult MyResult = MySingleton.GetMySingleton().AlterRegist(null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id, regist_deadline, cancelregist_deadline, ((Button)sender).Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx",path);
            string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');
            MyResult = MySingleton.GetMySingleton().AlterRegist(null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id, regist_deadline, cancelregist_deadline, aa + "/Default.aspx", path, "", aa + "/Default.aspx");


            GridView1.DataBind();


        }


        if (CancelPersonRegistClick != null)
        {
            this.Visible = false;
            CancelPersonRegistClick(this, e);
        }

        if (MyResult == MySingleton.AlterRegistResult.CancelRegistSucess)
        {
            clsMyObj.ShowMessage("取消報名完成。");
        }
        else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail_DayOver)
        {
            clsMyObj.ShowMessage("取消報名截止日之後無法取消報名!。");
        }
        else if (MyResult == MySingleton.AlterRegistResult.CancelRegistFail)
        {
            clsMyObj.ShowMessage("取消報名失敗!。");
        }     


    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show(); 
    }
}

public partial class WebForm_RegistActivity_OpenRegistedByMeEmpSelectorX
{
    public string activity_id
    {
        get { return (ViewState["activity_id"] == null ? "" : ViewState["activity_id"].ToString()); }
        set { ViewState["activity_id"] = value; }
    }

    public string regist_by
    {
        get { return (ViewState["regist_by"] == null ? "" : ViewState["regist_by"].ToString()); }
        set { ViewState["regist_by"] = value; }
    }

    public string regist_deadline
    {
        get { return (ViewState["regist_deadline"] == null ? "" : ViewState["regist_deadline"].ToString()); }
        set { ViewState["regist_deadline"] = value; }
    }

    public string cancelregist_deadline
    {
        get { return (ViewState["cancelregist_deadline"] == null ? "" : ViewState["cancelregist_deadline"].ToString()); }
        set { ViewState["cancelregist_deadline"] = value; }
    }

    public void InitDataAndShow()
    {
        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = activity_id;
        ObjectDataSource1.SelectParameters["regist_by"].DefaultValue = regist_by;
        GridView1.DataBind();

        //預設勾選第一筆
        if (GridView1.Rows.Count > 0)
        {
            (GridView1.Rows[0].FindControl("CheckBox1") as CheckBox).Checked = true;
        }

        this.mpSearch.Show();    
    }

}
