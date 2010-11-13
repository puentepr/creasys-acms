using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenTeamMemberSelector : System.Web.UI.UserControl
{
    //public delegate void GetSmallEmployeesDelegate(object sender, GetEmployeeEventArgs e);
    //public event GetSmallEmployeesDelegate GetSmallEmployeesClick;
    public delegate void GetEmployeesDelegate(object sender, EventArgs e);
    public event GetEmployeesDelegate GetEmployeesClick;


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


    protected void GridView_Employee_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (GetEmployeesClick != null)
        {
            GetEmployeesClick(this, e);
        }
        //try
        //{
        //    GridView_Employee.PageIndex = GridView_Employee.PageIndex + 1;
        //}
        //catch
        //{
        //}


        //this.mpSearch.Show();
    }
}

public partial class WebForm_RegistActivity_OpenTeamMemberSelector
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
