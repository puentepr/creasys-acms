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
        GridView_Employee.Visible = true;

        ObjectDataSource_Employee.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedItem.Text ;
        ObjectDataSource_Employee.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource_Employee.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource_Employee.SelectParameters["UnderDept"].DefaultValue = cbUnderDept.Checked.ToString();
        ObjectDataSource_Employee.SelectParameters["Company_ID"].DefaultValue = ddlC_NAME .SelectedValue;
        
        GridView_Employee.DataBind();
        
        this.mpSearch.Show();
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
    protected void ddlC_NAME_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDEPT_ID.DataBind();
        this.mpSearch.Show();
    }
    protected void GridView_Employee_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show(); 
    }



    protected void GridView_Employee_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView_Employee.Rows)
        {
            if (((TServerControl.TCheckBoxYN)gr.FindControl("chkRJRA")).Enabled == false)
            {
                gr.ToolTip = "已經報名";
            }
        }

        ACMS.BO.ActivatyBO Bo= new ACMS.BO.ActivatyBO();
        ACMS.VO.ActivatyVO vo=new ACMS.VO.ActivatyVO ();
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

public partial class WebForm_RegistActivity_OpenTeamMemberSelector
{
    public void InitDataAndShow(string activity_id)
    {
        ActivityID = activity_id;
        GridView_Employee.Visible = false;
        ObjectDataSource_Employee.SelectParameters["activity_id"].DefaultValue = activity_id;
       // btnQuery_Click(null, null);
        this.mpSearch.Show(); 
    }
    public string ActivityID
    {
        get { if (ViewState["activity_id"] == null) { return new Guid().ToString(); } else return ViewState["activity_id"].ToString(); }
        set { ViewState["activity_id"] = value; }
    }

    public string TitleName
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

   

}
