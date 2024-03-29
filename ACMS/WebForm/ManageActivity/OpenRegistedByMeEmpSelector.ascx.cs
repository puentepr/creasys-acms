﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenRegistedByMeEmpSelectorYY : System.Web.UI.UserControl
{
    public delegate void CancelPersonRegistDelegate(object sender, EventArgs e);
    public event CancelPersonRegistDelegate CancelPersonRegistClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlC_NAME.DataBind();
            ddlC_NAME.Items.Insert(0, new ListItem("請選擇", ""));
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = activity_id;
        ObjectDataSource1.SelectParameters["regist_by"].DefaultValue = regist_by;
        ObjectDataSource1.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedItem.Text ;
        ObjectDataSource1.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = "999";
        //if (string.Compare(ddlJOB_GRADE_GROUP.SelectedValue, "") == 0)
        //{
        //    ObjectDataSource1.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = "999";
        //}
        //else
        //{
        //    ObjectDataSource1.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = ddlJOB_GRADE_GROUP.SelectedValue;
        //}
        ObjectDataSource1.SelectParameters["WINDOWS_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource1.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        //
        ObjectDataSource1.SelectParameters["SEX"].DefaultValue = "";
        ObjectDataSource1.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = "1900/1/1";
        
        ObjectDataSource1.SelectParameters["C_NAME"].DefaultValue = ddlC_NAME.SelectedValue;
        ObjectDataSource1.SelectParameters["UnderDept"].DefaultValue = cbUnderDept.Checked.ToString();
        GridView1.Visible = true;
        GridView1.DataBind();

        //預設勾選第一筆
        if (GridView1.Rows.Count > 0)
        {
            (GridView1.Rows[0].FindControl("CheckBox1") as CheckBox).Checked = true;
        }
        this.mpSearch.Show();

       
        //this.InitDataAndShow();
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

        if (!string.IsNullOrEmpty(emp_id))
        {
           // MySingleton.AlterRegistResult MyResult = MySingleton.GetMySingleton().AlterRegist(null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id, regist_deadline, cancelregist_deadline, ((Button)sender).Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/AMMS/WebForm/RegistActivity/RegistedActivityQuery.aspx" ,path);
            string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');
            MySingleton.AlterRegistResult MyResult = MySingleton.GetMySingleton().AlterRegist(null, null, MySingleton.AlterRegistType.CancelRegist, new Guid(activity_id), emp_id, regist_deadline, cancelregist_deadline, aa + "/Default.aspx", path, "", aa + "/Default.aspx");
            
            GridView1.DataBind();  
 
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


        if (CancelPersonRegistClick != null)
        {
            CancelPersonRegistClick(this, e);
        }




    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }
    protected void ddlC_NAME_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDEPT_ID.DataBind();
        this.mpSearch.Show(); 
    }
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show();
    }
}

public partial class WebForm_RegistActivity_OpenRegistedByMeEmpSelectorYY 
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
        //if (string.Compare(ddlDEPT_ID.SelectedValue, "") == 0)
        //{
        //    GridView1.Visible = false;
        //}
        //else
        //{
        //    GridView1.Visible = true;
        //}


        GridView1.Visible = true;
        btnQuery_Click(null, null);
        this.mpSearch.Show();    
    }

}
