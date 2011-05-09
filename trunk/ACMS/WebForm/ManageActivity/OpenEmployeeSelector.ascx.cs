using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageActivity_OpenEmployeeSelector : System.Web.UI.UserControl
{
    ACMS.DAO.ActivityRegistDAO regDAO = new ACMS.DAO.ActivityRegistDAO();
    public delegate void GetEmployeesDelegate(object sender, EventArgs e);
    public event GetEmployeesDelegate GetEmployeesClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (int i = DateTime.Now.Year; i >= 1930; i--)
            {
                ddlBIRTHDAY_start_year.Items.Insert(0, new ListItem(i.ToString()));
                ddlBIRTHDAY_end_year.Items.Insert(0, new ListItem(i.ToString()));
            }
            ddlBIRTHDAY_start_year.SelectedValue = "1930";
            ddlBIRTHDAY_end_year.SelectedValue = DateTime.Now.Year.ToString();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridView_Employee.Visible = true;
        this.mpSearch.Show();

        //ObjectDataSource_Employee.SelectMethod = this.SelectMethod;      
        ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedItem.Text;
        ObjectDataSource_Employee.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = ddlJOB_GRADE_GROUP.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource_Employee.SelectParameters["SEX"].DefaultValue = rblSEX.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["BIRTHDAY_S"].DefaultValue = ddlBIRTHDAY_start_year.SelectedValue + "/" + ddlBIRTHDAY_start_month.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["BIRTHDAY_E"].DefaultValue = ddlBIRTHDAY_end_year.SelectedValue + "/" + ddlBIRTHDAY_end_month.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = txtEXPERIENCE_START_DATE.Text;
        ObjectDataSource_Employee.SelectParameters["C_NAME"].DefaultValue = ddlC_NAME.SelectedItem.Text;
        ObjectDataSource_Employee.SelectParameters["UnderDept"].DefaultValue = cbUnderDept.Checked.ToString();
        ObjectDataSource_Employee.SelectParameters["COMPANY_CODE"].DefaultValue = ddlC_NAME.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["activity_id"].DefaultValue = ViewState["activity_id"].ToString();

        GridView_Employee.DataBind();

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //andy 先檢查是否已經報名了.若已經報名且打算取消限制人員名單則秀訊息
        string erremp_id = "";
        string emp_id = "";

        foreach (GridViewRow gr in GridView_Employee.Rows)
        {
            if (((TServerControl.TCheckBoxYN)gr.FindControl("chkRJRA")).Checked == false)
            {
                emp_id = ((HiddenField)gr.FindControl("hiemp_id")).Value;
                if (regDAO.IsPersonRegisted(myactivity_id, emp_id, "", myactivity_type) > 0)
                { erremp_id += gr.Cells[1].Text + ","; }
            }
        }
        if (erremp_id != "")
        {
            clsMyObj.ShowMessage("目前已有下列人員已經報名:" + erremp_id.TrimEnd(',') + "所以無法完成此項作業");
            return;
        }

        if (GetEmployeesClick != null)
        {
            GetEmployeesClick(this.GridView_Employee, e);
        }
        try
        {
            GridView_Employee.PageIndex = GridView_Employee.PageIndex + 1;
        }
        catch
        {
        }

        clsMyObj.ShowMessage("確定已完成");
        this.mpSearch.Show();




    }
    protected void GridView_Employee_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();
    }


    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow gr in GridView_Employee.Rows)
    //    {
    //        if (((CheckBox)(gr.FindControl("chkRJRA"))).Enabled)
    //        {
    //            ((CheckBox)(gr.FindControl("chkRJRA"))).Checked = true;
    //        }

    //    }
    //    this.mpSearch.Show();   
    //}
    protected void ddlC_NAME_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDEPT_ID.DataBind();
        this.mpSearch.Show();
    }
    protected void GridView_Employee_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show();
    }
    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        bool chk1 = (GridView_Employee.HeaderRow.FindControl("chk1") as CheckBox).Checked;
        foreach (GridViewRow gr in GridView_Employee.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkRJRA"))).Enabled)
            {
                ((CheckBox)(gr.FindControl("chkRJRA"))).Checked = chk1;
            }

        }
        this.mpSearch.Show();
    }
    protected void GridView_Employee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (myMode == FormViewMode.ReadOnly)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (((TServerControl.TCheckBoxYN)e.Row.FindControl("chkRJRA")).Checked)
        //        {
        //            //  ((TServerControl.TCheckBoxYN)e.Row.FindControl("chkRJRA")).Enabled = false;
        //        }
        //    }
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string emp_id = ((HiddenField)e.Row.FindControl("hiemp_id")).Value;

            if (regDAO.IsPersonRegisted(myactivity_id, emp_id, "", myactivity_type) > 0)
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
                ((TServerControl.TCheckBoxYN)e.Row.FindControl("chkRJRA")).Checked = true;

                ((TServerControl.TCheckBoxYN)e.Row.FindControl("chkRJRA")).Enabled = false;
            }

        }
    }

}

public partial class WebForm_ManageActivity_OpenEmployeeSelector
{
   

    public FormViewMode myMode
    { 
        get{return (FormViewMode ) ViewState ["myMode"];}
        set { ViewState["myMode"] = value; }

    }

    public Guid myactivity_id
    {
        get { return (Guid)ViewState["activity_id"]; }
        set { ViewState["activity_id"] = value; }

    }

    public string  myactivity_type
    {
        get { return ViewState["activity_type"].ToString (); }
        set { ViewState["activity_type"] = value; }

    }

    public void InitDataAndShow(Guid activity_id,FormViewMode  fMode,string activity_type)
    {
        myactivity_id = activity_id;
        ViewState["activity_id"] = activity_id;
        myMode = fMode;
        ObjectDataSource_Employee.SelectParameters["activity_id"].DefaultValue = activity_id.ToString();
        myactivity_type = activity_type;
        GridView_Employee.Visible = false;
        this.mpSearch.Show();    
    }


    //public string SelectMethod
    //{
    //    get
    //    {
    //        return (ViewState["SelectMethod"] == null ? "BLL_OpenEmployeeSelector_Select" : ViewState["SelectMethod"].ToString());
    //    }

    //    set
    //    {
    //        ViewState["SelectMethod"] = value;
    //      }
    //}

}
