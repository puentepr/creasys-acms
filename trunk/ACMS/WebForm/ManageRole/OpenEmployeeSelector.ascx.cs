using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenEmployeeSelector : System.Web.UI.UserControl
{
    public delegate void GetEmployeesDelegate(object sender, GetEmployeeEventArgs e);
    public event GetEmployeesDelegate GetEmployeesClick;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.mpSearch.Show();

        ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;

        GridView_Employee.DataBind();
    }

    //選取人員
    protected void lbtnSelect_Click(object sender, EventArgs e)
    {
        string emp_id = GridView_Employee.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

        if (GetEmployeesClick != null)
        {
            GetEmployeesClick(this, new GetEmployeeEventArgs(emp_id));
        }
    }

    //換頁
    protected void GridView_Employee_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }
}

public partial class WebForm_RegistActivity_OpenEmployeeSelector
{
    public void InitDataAndShow()
    {
        ddlDEPT_ID.SelectedIndex = -1;
        txtWORK_ID.Text = "";
        txtNATIVE_NAME.Text = "";

        btnQuery_Click(null, null);
        this.mpSearch.Show(); 
    }

    public string TitleName
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

}
