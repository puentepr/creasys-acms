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
        if (ddlDEPT_ID.SelectedValue == "" && txtNATIVE_NAME.Text == "" && txtWORK_ID.Text == "")
        {
            //if (sender != null)
            //{
            //    clsMyObj.ShowMessage("查詢條件至少要輸入1個條件");
            //}
            //GridView_Employee.Visible = false;
            //return;
            GridView_Employee.Visible = true;
        }


        GridView_Employee.Visible = true;
        ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedItem.Text ;
        ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource_Employee.SelectParameters["UnderDept"].DefaultValue = cbUnderDept.Checked.ToString();
        ObjectDataSource_Employee.SelectParameters["Company_ID"].DefaultValue = ddlC_NAME.SelectedValue;
        if (string.Compare(ddlDEPT_ID.SelectedValue, "") != 0)
        {
            GridView_Employee.DataBind();
        }
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
    protected void ddlC_NAME_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDEPT_ID.DataBind();
        this.mpSearch.Show();   
    }

    protected void GridView_Employee_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView_Employee.Rows)
        {
            if (((LinkButton)gr.FindControl("lbtnSelect")).Enabled==false)
            {
                gr.ToolTip = "已經報名";
            }
        }
        ACMS.BO.ActivatyBO Bo = new ACMS.BO.ActivatyBO();
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        vo = Bo.SelectActivatyByActivatyID(new Guid(ActivityID));
        if (GridView_Employee.Rows.Count == 0)
        {
            if (vo.is_grouplimit == "Y")
            {
                clsMyObj.ShowMessage("『此活動有限定參加人員，您查詢的人員未在名單內』");
            }
        }

    }
}

public partial class WebForm_RegistActivity_OpenAgentSelector
{
    public void InitDataAndShow(string activity_id)
    {

        ActivityID = activity_id;
        ObjectDataSource_Employee.SelectParameters["activity_id"].DefaultValue = activity_id;
        //btnQuery_Click(null, null);
        GridView_Employee.Visible = false;
        this.mpSearch.Show(); 
    }
    public string ActivityID
    {
        get { if (ViewState["activity_id"] == null) { return new Guid ().ToString (); } else return ViewState["activity_id"].ToString(); }
        set { ViewState["activity_id"] = value; }
    }
    public string TitleName
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

}
