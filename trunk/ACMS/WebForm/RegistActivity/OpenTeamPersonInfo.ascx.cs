using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenTeamPersonInfo : System.Web.UI.UserControl
{
    //public delegate void GetSmallEmployeesDelegate(object sender, GetEmployeeEventArgs e);
    //public event GetSmallEmployeesDelegate GetSmallEmployeesClick;
    public delegate void GetTeamPersonInfoDelegate(object sender, EventArgs e);
    public event GetTeamPersonInfoDelegate GetTeamPersonInfoClick;


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.mpSearch.Show();

        //ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        //ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        //ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;

        //GridView_Employee.DataBind();
    }


    protected void GridView_Employee_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (clsMyObj.IDChk(txtidno.Text) != "0")
        {
            clsMyObj.ShowMessage("身分證字號格式不正確!");
            this.mpSearch.Show();  
            return;
        }

        UC_ActivityTeamMemberVO.WritePersonInfo = "是";
        UC_ActivityTeamMemberVO.idno_type = rblidno_type.SelectedIndex;
        UC_ActivityTeamMemberVO.idno = txtidno.Text;
        UC_ActivityTeamMemberVO.remark = txtremark.Text;

        if (GetTeamPersonInfoClick != null)
        {
            GetTeamPersonInfoClick(this, e);
        }
      
    }
}

public partial class WebForm_RegistActivity_OpenTeamPersonInfo
{
    public void InitDataAndShow()
    {
        rblidno_type.SelectedIndex = UC_ActivityTeamMemberVO.idno_type;
        txtidno.Text = UC_ActivityTeamMemberVO.idno;
        txtremark.Text = UC_ActivityTeamMemberVO.remark;
      
        this.mpSearch.Show(); 
    }


    public ACMS.VO.ActivityTeamMemberVO UC_ActivityTeamMemberVO
    {
        get
        {
            if (ViewState["UC_ActivityTeamMemberVO"] == null)
            {
                ViewState["UC_ActivityTeamMemberVO"] = new ACMS.VO.ActivityTeamMemberVO();
            }

            return (ACMS.VO.ActivityTeamMemberVO)ViewState["UC_ActivityTeamMemberVO"];

        }
        set { ViewState["UC_ActivityTeamMemberVO"] = value; }
    }
   

}
