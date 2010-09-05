using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageBulletinBoard_BulletinBoardEdit : System.Web.UI.Page
{
    protected virtual void Page_Init(object sender, System.EventArgs e)
    {
            FormView1.InsertItemTemplate = FormView1.ItemTemplate;
            FormView1.EditItemTemplate = FormView1.ItemTemplate;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["id"] == null)
        {
            (this.Master as MyMasterPage).PanelMainGroupingText = "新增公佈欄項目";
            MyFormViewMode = FormViewMode.Insert;

            
        }
        else
        {
            (this.Master as MyMasterPage).PanelMainGroupingText = "編輯公佈欄項目";
            MyFormViewMode = FormViewMode.Edit;

            SqlDataSource1.SelectParameters["id"].DefaultValue = Request["id"];

        }


        FormView1.ChangeMode(MyFormViewMode);


    }
    protected void FormView1_ModeChanged(object sender, EventArgs e)
    {
        if (Request["id"] == null)
        {
            (FormView1.FindControl("btnSave") as Button).CommandName = "Insert";

        }
        else
        {
            (FormView1.FindControl("btnSave") as Button).CommandName = "Update";
        }

    }
}

public partial class WebForm_ManageBulletinBoard_BulletinBoardEdit
{

    public FormViewMode MyFormViewMode
    {
        get
        {
            return (FormViewMode)(this.ViewState["MyFormViewMode"] == null ? FormViewMode.ReadOnly : ViewState["MyFormViewMode"]);
        }
        set
        {
            this.ViewState["MyFormViewMode"] = value;
        }
    }




}



