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
        mpSearch.Show();
        //try
        //{
        //    GridView_Employee.PageIndex = GridView_Employee.PageIndex + 1;
        //}
        //catch
        //{
        //}


        //this.mpSearch.Show();
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
    protected void GridView_Employee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (ACMS.VO.ActivityTeamMemberVO mem in Page_ActivityTeamMemberVOList)
            {
                if (((HiddenField)e.Row.FindControl("hiID")).Value == mem.emp_id)
                {
                    (((TServerControl.TCheckBoxYN)(e.Row.FindControl("chkRJRA")))).Checked = true;
                   
                }

                if (((HiddenField)e.Row.FindControl("hiID")).Value == mem.boss_id)
                {
                    (((TServerControl.TCheckBoxYN)(e.Row.FindControl("chkRJRA")))).Checked = true;
                    e.Row.Visible = false;
                   
                }
            }
        }
    }
    protected void chkRJRA_CheckedChanged(object sender, EventArgs e)
    {
        bool chk = ((TServerControl.TCheckBoxYN )sender).Checked ;

        foreach (GridViewRow gr in GridView_Employee.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkRJRA"))).Enabled)
            {
                ((CheckBox)(gr.FindControl("chkRJRA"))).Checked = chk;
            }

        }
        this.mpSearch.Show();  
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

    //團隊成員
    public List<ACMS.VO.ActivityTeamMemberVO> Page_ActivityTeamMemberVOList
    {
        get
        {
            if (ViewState["Page_ActivityTeamMemberVOList"] == null)
            {
                ViewState["Page_ActivityTeamMemberVOList"] = new List<ACMS.VO.ActivityTeamMemberVO>();
            }

            return (List<ACMS.VO.ActivityTeamMemberVO>)ViewState["Page_ActivityTeamMemberVOList"];

        }
        set { ViewState["Page_ActivityTeamMemberVOList"] = value; }
    }



}
