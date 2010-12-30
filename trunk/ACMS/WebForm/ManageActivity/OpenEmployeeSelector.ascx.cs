using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageActivity_OpenEmployeeSelector : System.Web.UI.UserControl
{
    public delegate void GetEmployeesDelegate(object sender, EventArgs e);
    public event GetEmployeesDelegate GetEmployeesClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (int i = DateTime.Now.Year; i >= 1930; i--)
            {
                ddlBIRTHDAY_start_year.Items.Insert(0,new ListItem(i.ToString()));
                ddlBIRTHDAY_end_year.Items.Insert(0, new ListItem(i.ToString()));
            }
            ddlBIRTHDAY_start_year.SelectedValue = "1930";
            ddlBIRTHDAY_end_year.SelectedValue = DateTime.Now.Year.ToString();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.mpSearch.Show();

        //ObjectDataSource_Employee.SelectMethod = this.SelectMethod;      
        ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = ddlJOB_GRADE_GROUP.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource_Employee.SelectParameters["SEX"].DefaultValue = rblSEX.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["BIRTHDAY_S"].DefaultValue = ddlBIRTHDAY_start_year.SelectedValue + "/" + ddlBIRTHDAY_start_month.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["BIRTHDAY_E"].DefaultValue = ddlBIRTHDAY_end_year.SelectedValue + "/" + ddlBIRTHDAY_end_month.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = txtEXPERIENCE_START_DATE.Text;
        ObjectDataSource_Employee.SelectParameters["C_NAME"].DefaultValue = ddlC_NAME.SelectedValue;
        
        GridView_Employee.DataBind();

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (GetEmployeesClick != null)
        {
            GetEmployeesClick(this, e);
        }
        try
        {
            GridView_Employee.PageIndex = GridView_Employee.PageIndex + 1;
        }
        catch
        {
        }


        this.mpSearch.Show();




    }
    protected void GridView_Employee_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }

   
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView_Employee.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkRJRA"))).Enabled)
            {
                ((CheckBox)(gr.FindControl("chkRJRA"))).Checked = true;
            }

        }
        this.mpSearch.Show();   
    }
}

public partial class WebForm_ManageActivity_OpenEmployeeSelector
{
    public void InitDataAndShow(Guid activity_id)
    {
        ObjectDataSource_Employee.SelectParameters["activity_id"].DefaultValue = activity_id.ToString();
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
