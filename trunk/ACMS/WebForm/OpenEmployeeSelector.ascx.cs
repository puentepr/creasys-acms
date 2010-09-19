using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_OpenEmployeeSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {     
       
        }

 
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.mpSearch.Show();

        ObjectDataSource2.SelectMethod = this.SelectMethod; 
        ObjectDataSource2.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        ObjectDataSource2.SelectParameters["JOB_CNAME"].DefaultValue = txtJOB_CNAME.Text;
        ObjectDataSource2.SelectParameters["WORK_ID"].DefaultValue = txtWORK_ID.Text;
        ObjectDataSource2.SelectParameters["NATIVE_NAME"].DefaultValue = txtNATIVE_NAME.Text;
        ObjectDataSource2.SelectParameters["SEX"].DefaultValue = rblSEX.SelectedValue;
        ObjectDataSource2.SelectParameters["AGE"].DefaultValue = txtAGE.Text;
        ObjectDataSource2.SelectParameters["EXPERIENCE_START_DATE"].DefaultValue = txtEXPERIENCE_START_DATE.Text;
        ObjectDataSource2.SelectParameters["C_NAME"].DefaultValue = txtC_NAME.Text;


    }
    protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        ObjectDataSource2.SelectMethod = this.SelectMethod; 
    }
}

public partial class WebForm_OpenEmployeeSelector
{
    public void InitDataAndShow()
    {
        btnQuery_Click(null, null);
        this.mpSearch.Show();    
    }


    public string SelectMethod
    {
        get
        {
            return (ViewState["SelectMethod"] == null ? "BLL_OpenEmployeeSelector_Select" : ViewState["SelectMethod"].ToString());
        }

        set
        {
            ViewState["SelectMethod"] = value;
          }
    }

}
