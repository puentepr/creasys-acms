using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenAgentSelector : System.Web.UI.UserControl
{
    public delegate void GetSmallEmployeesDelegate(object sender, GetEmployeeEventArgs e);
    public event GetSmallEmployeesDelegate GetSmallEmployeesClick;



    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.mpSearch.Show();

        ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;

        GridView_Employee.DataBind();
    }
    protected void lbtnSelect_Click(object sender, EventArgs e)
    {
        string emp_id = GridView_Employee.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

        if (GetSmallEmployeesClick != null)
        {
            GetSmallEmployeesClick(this, new GetEmployeeEventArgs(emp_id));
        }
    }
    protected void GridView_Employee_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }
}

public partial class WebForm_RegistActivity_OpenAgentSelector
{
    public void InitDataAndShow(string activity_id)
    {
        ObjectDataSource_Employee.SelectParameters["activity_id"].DefaultValue = activity_id;
        btnQuery_Click(null, null);
        this.mpSearch.Show(); 
    }

    public string TitleName
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

}
