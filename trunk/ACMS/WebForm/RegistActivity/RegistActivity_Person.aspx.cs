﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TServerControl;
using System.IO;
using System.Collections.Generic;

public partial class WebForm_RegistActivity_RegistActivity_Person : System.Web.UI.Page
{
    HiddenField MyHiddenField = new HiddenField();

    Hashtable MyHashtable = new Hashtable();

    protected void Page_Init(object sender, EventArgs e)
    {
        MyHiddenField.ID = "__MyHiddenField";
        this.Form.Controls.Add(MyHiddenField);
        InitQueryBlock(Request.Form[MyHiddenField.UniqueID]);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (this.Master as MyMasterPage).PanelMainGroupingText = "個人報名";
            Wizard1.Visible = false;

            if (Session["form_mode"] != null && Session["activity_id"] != null)
            {
                //以新增方式進來時
                if (Session["form_mode"].ToString() == "regist")
                {
                    RegistGoSecondEventArgs myRegistGoSecondEventArgs = new RegistGoSecondEventArgs(new Guid(Session["activity_id"].ToString()));
                    GoSecondStep_Click(null, myRegistGoSecondEventArgs);
                }

                if (Session["form_mode"].ToString() == "edit")
                {
                    //以編輯方式進來時
                    GoThirdStep_Click(null, null);
                }
            }
            else
            {
                //先查詢,再 GoSecondStep_Click

            }



            //Session["form_mode"] = null;
            //Session["activity_id"] = null;

        }
    }

    //新增報名
    protected void GoSecondStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        Wizard1.MoveTo(Wizard1.WizardSteps[0]);

        RegistActivityQuery1.Visible = false;
        Wizard1.Visible = true;

        //必要屬性
        MyFormMode = FormViewMode.Insert;
        ActivityID = e.activity_id;
        EmpID = clsAuth.ID;//預設是登入者
        RegistBy = clsAuth.ID;//執行是登入者

        PanelRegisterInfoA.Visible = true;
        PanelRegisterInfoB.Visible = false;

        MyHiddenField.Value = ActivityID.ToString();

        //新增一筆報名紀錄    
        //ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
        //ACMS.VO.ActivityRegistVO myActivityRegistVO = new ACMS.VO.ActivityRegistVO();

        //RegistID = Guid.NewGuid();
        ////myActivityRegistVO.id = RegistID;
        //myActivityRegistVO.activity_id = e.activity_id;
        //myActivityRegistVO.emp_id = clsAuth.ID;
        //myActivityRegistVO.regist_by = clsAuth.ID;

        //myActivityRegistDAO.INSERT_NewOne(myActivityRegistVO);

        //載入活動資訊
        GetActivityDefault();

    }


    //編輯
    protected void GoThirdStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        Wizard1.MoveTo(Wizard1.WizardSteps[1]);

        RegistActivityQuery1.Visible = false;
        Wizard1.Visible = true;

        //必要屬性
        MyFormMode = FormViewMode.Insert;
        ActivityID = new Guid(Session["activity_id"].ToString());
        EmpID = clsAuth.ID;//預設是登入者
        RegistBy = clsAuth.ID;//執行是登入者

        MyFormMode = FormViewMode.Edit;

        ObjectDataSource_RegisterPeoplenfo.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();

        PanelRegisterInfoA.Visible = false;
        PanelRegisterInfoB.Visible = true;

        MyHiddenField.Value = ActivityID.ToString();

        //載入活動資訊
        GetActivityDefault();

    }

    private void GetActivityDefault()
    {
        //取得活動資訊
        ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
        ACMS.VO.ActivatyVO myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

        //活動海報訊息
        Literal1.Text = myActivatyVO.activity_info;

        //活動相關訊息
        ObjectDataSource_ActivatyDetails.SelectParameters["id"].DefaultValue = ActivityID.ToString();
        ObjectDataSource_UpFiles.SelectParameters["dirName"].DefaultValue = Server.MapPath(Path.Combine("/ACMS/UpFiles", ActivityID.ToString()));

        //報名者資訊
        ObjectDataSource_RegisterPersonInfo.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;//預設登入者

        //所有報名者資訊
        ObjectDataSource_RegisterPeoplenfo.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
        ObjectDataSource_RegisterPeoplenfo.SelectParameters["emp_id"].DefaultValue = RegistBy;//由登入者所報名(含登入者本人)   

        //注意事項
        Literal_notice.Text = myActivatyVO.notice;

        FormView_fixA.DataBind();
        FormView_fixA.FindControl("tr_person_fix1").Visible = (myActivatyVO.is_showperson_fix1 == "Y");
        FormView_fixA.FindControl("tr_person_fix2").Visible = (myActivatyVO.is_showperson_fix2 == "Y");

        (FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2Start") as Label).Text = myActivatyVO.personextcount_min.ToString();
        (FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2End") as Label).Text = myActivatyVO.personextcount_max.ToString();

        RangeValidator myRangeValidator = (FormView_fixA.FindControl("tr_person_fix2").FindControl("chk_txtperson_fix2_3") as RangeValidator);
        myRangeValidator.MinimumValue = myActivatyVO.personextcount_min.ToString();
        myRangeValidator.MaximumValue = myActivatyVO.personextcount_max.ToString();
    }



    //顯示活動資訊
    protected void FormView_ActivatyDetails_DataBound(object sender, EventArgs e)
    {
        //隱藏每隊人數限制
        (FormView_ActivatyDetails.FindControl("trteam_member_max")).Visible = false;

        //檔案下載是否出現
        DataRowView drv = (DataRowView)FormView_ActivatyDetails.DataItem;

        (FormView_ActivatyDetails.FindControl("GridView_UpFiles") as GridView).Visible = (drv["is_showfile"].ToString() == "Y");

    }

    //下載檔案
    protected void lbtnFileDownload_Click(object sender, EventArgs e)
    {
        GridView GridView_UpFiles = (GridView)FormView_ActivatyDetails.FindControl("GridView_UpFiles");
        FileInfo myFileInfo = new FileInfo(GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());

        if (myFileInfo.Exists)
        {
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", Server.UrlEncode((myFileInfo.Name))));
            // 輸出檔案。
            Response.WriteFile(myFileInfo.FullName);

        }
    }

    //開啟選擇報名者視窗
    protected void btnAgent_Click(object sender, EventArgs e)
    {
        OpenSmallEmployeeSelector1.TitleName = "代理報名";
        //OpenSmallEmployeeSelector1.OkName = "報名";
        OpenSmallEmployeeSelector1.InitDataAndShow(ActivityID.ToString());
    }

    //選取人員之後指定EmpID
    protected void GetSmallEmployees_Click(object sender, GetEmployeeEventArgs e)
    {
        EmpID = e.id;
        ObjectDataSource_RegisterPersonInfo.SelectParameters["emp_id"].DefaultValue = e.id;
        FormView_RegisterPersonInfo.DataBind();

        //個人固定欄位
        ObjectDataSource_fixA.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
        ObjectDataSource_fixA.SelectParameters["emp_id"].DefaultValue = EmpID;

    }

    //人員切換之後指定EmpID
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton RadioButton1 = sender as RadioButton;
        RadioButton1.Checked = true;

        EmpID = GridView_RegisterPeoplinfo.DataKeys[(RadioButton1.NamingContainer as GridViewRow).RowIndex].Value.ToString();

        //載入個人資訊
        //個人固定欄位
        ObjectDataSource_fixA.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
        ObjectDataSource_fixA.SelectParameters["emp_id"].DefaultValue = EmpID;
    }

    protected void GridView_RegisterPeoplinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //给每个RadioButton1绑定setRadio事件
        try
        {
            ((RadioButton)e.Row.FindControl("RadioButton1")).Attributes.Add("onclick", "setRadio(this)");
        }
        catch (Exception)
        { }

    }


    //選擇費用項目時要加總金額
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intSum = 0;

        foreach (ListItem theListItem in (sender as CheckBoxList).Items)
        {
            if (theListItem.Selected == true)
            {
                intSum = intSum + Convert.ToInt32(theListItem.Text.Split(':')[1]);
            }
        }

        string lblSumValueID = string.Format("lblSumValue_{0}", (sender as TCheckBoxList).ID.Substring(3));

        (PlaceHolder1.FindControl(lblSumValueID) as Label).Text = intSum.ToString();
    }

    //檢查欲報名者是否已經報過名
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 1 && MyFormMode == FormViewMode.Insert)
        {
            ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

            if (myActivityRegistDAO.IsRegisted(ActivityID, EmpID) > 0)
            {
                clsMyObj.ShowMessage(@"已存在此員工的報名成功紀錄!\n請選擇其他員工執行代理報名。");
                Wizard1.MoveTo(Wizard1.WizardSteps[0]);
            }


        }
    }

    //取得報名資訊
    private ACMS.VO.ActivityRegistVO GetActivityRegistVO()
    {
        ACMS.VO.ActivityRegistVO myActivityRegistVO = new ACMS.VO.ActivityRegistVO();

        myActivityRegistVO.activity_id = ActivityID;
        myActivityRegistVO.emp_id = EmpID;
        myActivityRegistVO.regist_by = RegistBy;
        myActivityRegistVO.idno = (FormView_fixA.FindControl("tr_person_fix1").FindControl("txtperson_fix1") as TextBox).Text;
        myActivityRegistVO.ext_people = Convert.ToInt32((FormView_fixA.FindControl("tr_person_fix1").FindControl("txtperson_fix2") as TextBox).Text);

        return myActivityRegistVO;

    }

    //取得自訂欄位值
    private List<ACMS.VO.CustomFieldValueVO> GetCustomFieldValueVOList()
    {
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = new List<ACMS.VO.CustomFieldValueVO>();


        if (MyHashtable != null)
        {
            foreach (DictionaryEntry HashtableItem in MyHashtable)
            {
                ACMS.VO.CustomFieldValueVO myCustomFieldValueVO = new ACMS.VO.CustomFieldValueVO();

                myCustomFieldValueVO.id = Guid.NewGuid();
                myCustomFieldValueVO.emp_id = EmpID;

                //HashtableItem(MyControl.ID, myCustomFieldVO.field_id)  
                Control MyControl = PlaceHolder1.FindControl(HashtableItem.Key.ToString());
                myCustomFieldValueVO.field_id = Convert.ToInt32(HashtableItem.Value);

                if (MyControl is TCheckBoxList)
                {
                    myCustomFieldValueVO.field_value = (MyControl as TCheckBoxList).SelectedValueList;
                }
                else if (MyControl is TRadioButtonList)
                {
                    myCustomFieldValueVO.field_value = (MyControl as TRadioButtonList).SelectedValue;
                }
                else if (MyControl is TDropDownList)
                {
                    myCustomFieldValueVO.field_value = (MyControl as TDropDownList).SelectedValue;
                }
                else if (MyControl is TextBox)
                {
                    myCustomFieldValueVO.field_value = (MyControl as TextBox).Text;
                }

                myCustomFieldValueVOList.Add(myCustomFieldValueVO);

            }


        }



        return myCustomFieldValueVOList;



    }

    //完成
    protected void FinishButton_Click(object sender, EventArgs e)
    {
        if (MyFormMode == FormViewMode.Insert)
        {
            //新增報名模式
            ACMS.VO.ActivityRegistVO myActivityRegistVO = GetActivityRegistVO(); //取得報名資訊      
            List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = GetCustomFieldValueVOList();//取得自訂欄位值
            //ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

            //報名
            MySingleton.AlterRegistResult MyResult;

            if (MyFormMode == FormViewMode.Insert)
            {

                MyResult = MySingleton.GetMySingleton().AlterRegist(myActivityRegistVO, myCustomFieldValueVOList, MySingleton.AlterRegistType.RegistInsert, new Guid(), "", "", "");

            }
            else
            {
                MyResult = MySingleton.GetMySingleton().AlterRegist(myActivityRegistVO, myCustomFieldValueVOList, MySingleton.AlterRegistType.RegistUpdate, new Guid(), "", "", "");

            }


            if(MyResult == MySingleton.AlterRegistResult.RegistFail_Already)
            {
                clsMyObj.ShowMessage("已存在報名成功紀錄，無法重複報名!");
            }
            else if (MyResult == MySingleton.AlterRegistResult.RegistFail_Full)
            {
                clsMyObj.ShowMessage(@"抱歉，報名已額滿!\n若錄取名額有增加\n則可再次報名。");
            }


        }


        Response.Redirect("ActivityEditQuery.aspx");

    }

}


//自訂欄位設定
public partial class WebForm_RegistActivity_RegistActivity_Person
{
    protected void InitQueryBlock(string activity_id)
    {
        if (!string.IsNullOrEmpty(activity_id))
        { 

            ACMS.DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
            List<ACMS.VO.CustomFieldVO> myCustomFieldVOList = new List<ACMS.VO.CustomFieldVO>();
            myCustomFieldVOList = myCustomFieldDAO.SelectByActivity_id(new Guid(activity_id));

            if (myCustomFieldVOList != null && myCustomFieldVOList.Count > 0)
            {
                System.Web.UI.WebControls.Table MyTable = new System.Web.UI.WebControls.Table();
                System.Web.UI.WebControls.TableRow MyTableRow;

                foreach (ACMS.VO.CustomFieldVO myCustomFieldVO in myCustomFieldVOList)
                {
                    MyTableRow = new TableRow();
                    System.Web.UI.WebControls.TableCell MyTableCell_Title = new TableCell();
                    System.Web.UI.WebControls.TableCell MyTableCell_Control = new TableCell();
                    MyTableCell_Title.Width = System.Web.UI.WebControls.Unit.Pixel(100);// "40%";
                    MyTableCell_Control.Width = System.Web.UI.WebControls.Unit.Pixel(200);// "40%";

                    //Title
                    Literal lblTitle = new Literal();
                    lblTitle.ID = string.Format("lbl{0}", myCustomFieldVO.field_id);
                    lblTitle.Text = myCustomFieldVO.field_name;

                    MyTableCell_Title.Controls.Add(lblTitle);

                    //Control
                    Control MyControl = new Control();

                    if (myCustomFieldVO.field_control.ToUpper() == "TEXTBOX")
                    {
                        MyControl = new TextBox();
                        MyControl.ID = string.Format("txt{0}", myCustomFieldVO.field_id);
                    }
                    else if (myCustomFieldVO.field_control.ToUpper() == "TEXTBOXLIST")
                    {
                        MyControl = new TCheckBoxList();
                        MyControl.ID = string.Format("plh{0}", myCustomFieldVO.field_id);
                        (MyControl as TCheckBoxList).AutoPostBack = true;
                        (MyControl as TCheckBoxList).SelectedIndexChanged += CheckBoxList1_SelectedIndexChanged;

                        (MyControl as TCheckBoxList).ClearSelection();
                        //(MyControl as TCheckBoxList).EnableViewState = false;

                    }
                    else if (myCustomFieldVO.field_control.ToUpper() == "CHECKBOXLIST")
                    {
                        MyControl = new TCheckBoxList();
                        (MyControl as TCheckBoxList).RepeatDirection = RepeatDirection.Horizontal;
                        (MyControl as TCheckBoxList).RepeatLayout = RepeatLayout.Flow;
                        //(MyControl as TCheckBoxList).RepeatColumns = 3;
                        MyControl.ID = string.Format("cbl{0}", myCustomFieldVO.field_id);
                        (MyControl as TCheckBoxList).ClearSelection();
                        //(MyControl as TCheckBoxList).EnableViewState = false;
                    }
                    else if (myCustomFieldVO.field_control.ToUpper() == "RADIOBUTTONLIST")
                    {
                        MyControl = new TRadioButtonList();
                        (MyControl as TRadioButtonList).RepeatDirection = RepeatDirection.Horizontal;
                        (MyControl as TRadioButtonList).RepeatLayout = RepeatLayout.Flow;
                        //(MyControl as TRadioButtonList).RepeatColumns = 3;
                        MyControl.ID = string.Format("radl{0}", myCustomFieldVO.field_id);
                        (MyControl as TRadioButtonList).ClearSelection();
                        //(MyControl as TRadioButtonList).EnableViewState = false;
                    }

                    //每個 ORG_field_name 長出選項
                    ACMS.DAO.CustomFieldItemDAO myCustomFieldItemDAO = new ACMS.DAO.CustomFieldItemDAO();
                    List<ACMS.VO.CustomFieldItemVO> myCustomFieldItemVOList = new List<ACMS.VO.CustomFieldItemVO>();
                    myCustomFieldItemVOList = myCustomFieldItemDAO.SelectByField_id(myCustomFieldVO.field_id);

                    if (myCustomFieldItemVOList != null && myCustomFieldItemVOList.Count > 0)
                    {
                        int i = 0;
                        string strAll = string.Empty;

                        foreach (ACMS.VO.CustomFieldItemVO myCustomFieldItemVO in myCustomFieldItemVOList)
                        {
                            i++;

                            if (myCustomFieldVO.field_control.ToUpper() == "TEXTBOXLIST")
                            {
                                (MyControl as ListControl).Items.Add(new ListItem(string.Format("{0}:{1}", myCustomFieldItemVO.field_item_name, myCustomFieldItemVO.field_item_text), myCustomFieldItemVO.field_item_id.ToString()));
                            }
                            else
                            {
                                (MyControl as ListControl).Items.Add(new ListItem(myCustomFieldItemVO.field_item_name, myCustomFieldItemVO.field_item_id.ToString()));
                            }

                        }
                    }

                    MyTableCell_Control.Controls.Add(MyControl);

                    if (myCustomFieldVO.field_control.ToUpper() == "TEXTBOXLIST")
                    {
                        Label lblSumText = new Label();
                        Label lblSumValue = new Label();
                        lblSumText.Text = "總計:";
                        lblSumValue.ID = string.Format("lblSumValue_{0}", myCustomFieldVO.field_id);
                        MyTableCell_Control.Controls.Add(lblSumText);
                        MyTableCell_Control.Controls.Add(lblSumValue);
                    }

                    MyTableRow.Cells.Add(MyTableCell_Title);
                    MyTableRow.Cells.Add(MyTableCell_Control);

                    MyTable.Rows.Add(MyTableRow);

                    MyHashtable.Add(MyControl.ID, myCustomFieldVO.field_id);
                }

                PlaceHolder1.Controls.Add(MyTable);

            }

        }
        else
        {
            PlaceHolder1.Controls.Clear();
        }

    }

}

//必要屬性
public partial class WebForm_RegistActivity_RegistActivity_Person
{
    public FormViewMode MyFormMode
    {
        get { return (ViewState["MyFormMode"] == null ? FormViewMode.ReadOnly : (FormViewMode)ViewState["MyFormMode"]); }
        set { ViewState["MyFormMode"] = value; }
    }

    //public Guid RegistID
    //{
    //    get { return new Guid(ViewState["RegistID"].ToString()); }
    //    set { ViewState["RegistID"] = value; }
    //}


    public Guid ActivityID
    {
        get { return new Guid(ViewState["ActivityID"].ToString()); }
        set { ViewState["ActivityID"] = value; }
    }

    //被報名者
    public string EmpID
    {
        get { return ViewState["emp_id"].ToString(); }
        set { ViewState["emp_id"] = value; }
    }

    //實際報名者
    public string RegistBy
    {
        get { return ViewState["regist_by"].ToString(); }
        set { ViewState["regist_by"] = value; }
    }



}



