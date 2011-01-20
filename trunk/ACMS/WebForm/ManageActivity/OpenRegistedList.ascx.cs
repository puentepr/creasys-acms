using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_RegistActivity_OpenRegistedByMeEmpSelector : System.Web.UI.UserControl
{
    public delegate void CancelPersonRegistDelegate(object sender, EventArgs e);
    public event CancelPersonRegistDelegate CancelPersonRegistClick;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
      
        this.InitDataAndShow();  
        GridView1.Visible = true;
        GridView1.DataBind();
    }
    
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        this.mpSearch.Show();   
    }
    protected void ddlC_NAME_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlDEPT_ID.DataBind();
        this.mpSearch.Show();
    }
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        this.mpSearch.Show();
    }
}

public partial class WebForm_RegistActivity_OpenRegistedByMeEmpSelector 
{
    public string activity_id
    {
        get { return (ViewState["activity_id"] == null ? "" : ViewState["activity_id"].ToString()); }
        set { ViewState["activity_id"] = value; }
    }

   
    public string activity_type
    {
        get { return (ViewState["activity_type"] == null ? "" : ViewState["activity_type"].ToString()); }
        set { ViewState["activity_type"] = value; }
    }

    public void InitDataAndShow()
    {
        //if (string.Compare(ddlDEPT_ID.SelectedValue, "") == 0)
        //{
        //    GridView1.Visible = false;
        //}
        //else
        //{
        //    GridView1.Visible = true;
        //}
        GridView1.Visible = false;
       
        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = activity_id;
     
        ObjectDataSource1.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID .SelectedItem.Text  ;
        if (string.Compare(ddlJOB_GRADE_GROUP.SelectedValue, "") == 0)
        {
            ObjectDataSource1.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = "999";
        }
        else
        {
            ObjectDataSource1.SelectParameters["JOB_GRADE_GROUP"].DefaultValue = ddlJOB_GRADE_GROUP.SelectedValue;
        }
        ObjectDataSource1.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID .Text ;
        ObjectDataSource1.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource1.SelectParameters["SEX"].DefaultValue = rblSEX .SelectedValue;
        if (string.Compare(txtEXPERIENCE_START_DATE.Text, "") == 0)
        {
            ObjectDataSource1.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = "1900/1/1";

        }
        else
        {
            ObjectDataSource1.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = txtEXPERIENCE_START_DATE.Text;
        }
        ObjectDataSource1.SelectParameters["C_NAME"].DefaultValue = ddlC_NAME.SelectedValue ;

        ObjectDataSource1.SelectParameters["RegistedType"].DefaultValue = ddlListType.SelectedValue;
        ObjectDataSource1.SelectParameters["UnderDept"].DefaultValue = cbUnderDept.Checked.ToString();


        this.mpSearch.Show();    
    }

}
