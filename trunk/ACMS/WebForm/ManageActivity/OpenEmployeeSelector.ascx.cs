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

        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.mpSearch.Show();

        //ObjectDataSource_Employee.SelectMethod = this.SelectMethod;      
        ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["JOB_CNAME"].DefaultValue = ddlJOB_CNAME.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource_Employee.SelectParameters["SEX"].DefaultValue = rblSEX.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["BIRTHDAY_S"].DefaultValue = txtBIRTHDAY_start.Text;
        ObjectDataSource_Employee.SelectParameters["BIRTHDAY_E"].DefaultValue = txtBIRTHDAY_end.Text;
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
}

public partial class WebForm_ManageActivity_OpenEmployeeSelector
{
    public void InitDataAndShow(Guid activity_id)
    {
        ObjectDataSource_Employee.SelectParameters["activity_id"].DefaultValue = activity_id.ToString();

        btnQuery_Click(null, null);
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
