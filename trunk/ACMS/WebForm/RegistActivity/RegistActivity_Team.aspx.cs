using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TServerControl.Web;

public partial class WebForm_RegistActivity_RegistActivity_Team : System.Web.UI.Page
{
    HiddenField MyHiddenField = new HiddenField();

    Hashtable MyHashtable = new Hashtable();
    clsDBUtility dbUtil = clsDBUtility.GetInstance();

    protected void Page_Init(object sender, EventArgs e)
    {
        MyHiddenField.ID = "__MyHiddenField";
        this.Form.Controls.Add(MyHiddenField);


        InitQueryBlock(Request.Form[MyHiddenField.UniqueID]);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master as MyMasterPage).PanelMainGroupingText = "團隊報名";
    }

    //報名
    protected void GoSecondStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        Wizard1.MoveTo(Wizard1.WizardSteps[1]);
        Label2.Text = e.activity_id.ToString() + "   --     " + clsAuth.ID;

        FormMode = FormViewMode.Insert;
        emp_id = "1111";
        activity_id = "1";
        //PanelRegisterInfoA.Visible = true;
        //PanelRegisterInfoB.Visible = false;
        //SqlDataSource1.SelectParameters["emp_id"].DefaultValue = emp_id;
        SqlDataSource2.SelectParameters["id"].DefaultValue = activity_id;
        MyHiddenField.Value = activity_id;
    }

    //編輯
    protected void GoThirdStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        Wizard1.MoveTo(Wizard1.WizardSteps[1]);
        Label2.Text = e.activity_id.ToString() + "   --     " + clsAuth.ID;

        FormMode = FormViewMode.Edit;
        emp_id = "1111";
        activity_id = "1";
        //PanelRegisterInfoA.Visible = false;
        //PanelRegisterInfoB.Visible = true;
        //SqlDataSource1.SelectParameters["emp_id"].DefaultValue = emp_id;
        SqlDataSource2.SelectParameters["id"].DefaultValue = activity_id;
        //SqlDataSource3.SelectParameters["activity_id"].DefaultValue = activity_id;
        MyHiddenField.Value = activity_id;
    }







    protected void FinishButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistActivity_Team.aspx");
    }
    protected void lbtnAddPerson_Click(object sender, EventArgs e)
    {
        OpenSmallEmployeeSelector1.InitDataAndShow();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Wizard1.ActiveStepIndex = 3;
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
Wizard1.ActiveStepIndex = 2;
    }

}

public partial class WebForm_RegistActivity_RegistActivity_Team
{
    public FormViewMode FormMode
    {
        get { return (FormViewMode)ViewState["FormMode"]; }
        set { ViewState["FormMode"] = value; }
    }

    public string emp_id
    {
        get { return ViewState["emp_id"].ToString(); }
        set { ViewState["emp_id"] = value; }
    }

    public string activity_id
    {
        get { return ViewState["activity_id"].ToString(); }
        set { ViewState["activity_id"] = value; }
    }



    protected void InitQueryBlock(string activity_id)
    {

        if (!string.IsNullOrEmpty(activity_id))
        {
            using (DataTable DT = dbUtil.GetCustomField(Convert.ToInt32(activity_id)))
            {
                if (DT != null)
                {
                    System.Web.UI.WebControls.Table MyTable = new System.Web.UI.WebControls.Table();
                    System.Web.UI.WebControls.TableRow MyTableRow;

                    foreach (DataRow DR in DT.Rows)
                    {

                        MyTableRow = new TableRow();
                        System.Web.UI.WebControls.TableCell MyTableCell_Title = new TableCell();
                        System.Web.UI.WebControls.TableCell MyTableCell_Control = new TableCell();
                        //MyTableCell_Title.Width = System.Web.UI.WebControls.Unit.Percentage(30);// "40%";
                        //MyTableCell_Title.Width = System.Web.UI.WebControls.Unit.Percentage(60);// "40%";


                        //Title
                        Label lblTitle = new Label();
                        lblTitle.ID = string.Format("lbl{0}", DR["field_id"].ToString());
                        lblTitle.Text = DR["field_name"].ToString();


                        MyTableCell_Title.Controls.Add(lblTitle);



                        //Control
                        Control MyControl = new Control();

                        if (DR["field_control"].ToString().ToUpper() == "TEXTBOX")
                        {
                            MyControl = new TextBox();
                            MyControl.ID = string.Format("txt{0}", DR["field_id"].ToString());

                        }
                        if (DR["field_control"].ToString().ToUpper() == "TEXTBOXLIST")
                        {
                            MyControl = new PlaceHolder();
                            MyControl.ID = string.Format("plh{0}", DR["field_id"].ToString());

                        }
                        else if (DR["field_control"].ToString().ToUpper() == "CHECKBOXLIST")
                        {
                            MyControl = new TCheckBoxList();
                            (MyControl as TCheckBoxList).RepeatDirection = RepeatDirection.Horizontal;
                            (MyControl as TCheckBoxList).RepeatLayout = RepeatLayout.Flow;
                            //(MyControl as TCheckBoxList).RepeatColumns = 3;
                            MyControl.ID = string.Format("cbl{0}", DR["field_id"].ToString());
                            (MyControl as TCheckBoxList).ClearSelection();
                            (MyControl as TCheckBoxList).EnableViewState = false;
                        }
                        else if (DR["field_control"].ToString().ToUpper() == "RADIOBUTTONLIST")
                        {
                            MyControl = new TRadioButtonList();
                            (MyControl as TRadioButtonList).RepeatDirection = RepeatDirection.Horizontal;
                            (MyControl as TRadioButtonList).RepeatLayout = RepeatLayout.Flow;
                            //(MyControl as TRadioButtonList).RepeatColumns = 3;
                            MyControl.ID = string.Format("radl{0}", DR["field_id"].ToString());
                            (MyControl as TRadioButtonList).ClearSelection();
                            (MyControl as TRadioButtonList).EnableViewState = false;
                        }

                        //每個 ORG_field_name 長出選項
                        using (DataTable DT_Items = dbUtil.GetCustomFieldItem(Convert.ToInt32(DR["field_id"])))
                        {
                            if (DT_Items != null)
                            {
                                int i = 0;
                                string strAll = string.Empty;

                                foreach (DataRow DR_Items in DT_Items.Rows)
                                {
                                    i++;


                                    if (DR["field_control"].ToString().ToUpper() == "TEXTBOXLIST")
                                    {
                                        Label lblLabel = new Label();
                                        lblLabel.Text = string.Format("{0}:{1}<br>", DR_Items["field_item_name"].ToString(), DR_Items["field_item_text"].ToString());
                                        (MyControl as PlaceHolder).Controls.Add(lblLabel);

                                    }
                                    else
                                    {

                                        (MyControl as ListControl).Items.Add(new ListItem(DR_Items["field_item_name"].ToString(), DR_Items["field_item_id"].ToString()));

                                    }

                                }




                            }
                        }

                        MyTableCell_Control.Controls.Add(MyControl);

                        MyTableRow.Cells.Add(MyTableCell_Title);
                        MyTableRow.Cells.Add(MyTableCell_Control);


                        MyTable.Rows.Add(MyTableRow);

                        MyHashtable.Add(DR["field_name"].ToString(), MyControl.ID);



                    }

                    (WizardStep4.ContentTemplateContainer.FindControl("PlaceHolder1") as PlaceHolder).Controls.Add(MyTable);
       

                }
            }

        }
        else
        {
            (WizardStep4.ContentTemplateContainer.FindControl("PlaceHolder1") as PlaceHolder).Controls.Clear();
        }

    }

}