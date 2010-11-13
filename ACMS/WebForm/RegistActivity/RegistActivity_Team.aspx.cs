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
using TServerControl;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class WebForm_RegistActivity_RegistActivity_Team : System.Web.UI.Page
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
            (this.Master as MyMasterPage).PanelMainGroupingText = "團隊報名";
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

            Session["form_mode"] = null;
            Session["activity_id"] = null;

        }

    }

    //新增報名
    protected void GoSecondStep_Click(object sender, RegistGoSecondEventArgs e)
    {


        RegistActivity_Query1.Visible = false;
        Wizard1.Visible = true;

        //必要屬性
        MyFormMode = FormViewMode.Insert;
        ActivityID = e.activity_id;
        EmpID = clsAuth.ID;//預設是登入者
        RegistBy = clsAuth.ID;//執行是登入者

        MyHiddenField.Value = ActivityID.ToString();

        //載入活動資訊
        GetActivityDefault();

        //登入者為第一個團員
        ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = new ACMS.VO.ActivityTeamMemberVO();

        myActivityTeamMemberVO.activity_id = ActivityID;
        myActivityTeamMemberVO.emp_id =clsAuth.ID;
        myActivityTeamMemberVO.boss_id = RegistBy;
        myActivityTeamMemberVO.idno = "";
        myActivityTeamMemberVO.remark = "";
        myActivityTeamMemberVO.check_status = 0;

        myActivityTeamMemberVO.WORK_ID =clsAuth.WORK_ID;
        myActivityTeamMemberVO.NATIVE_NAME = clsAuth.NATIVE_NAME;
        myActivityTeamMemberVO.C_DEPT_ABBR = clsAuth.C_DEPT_ABBR;
        myActivityTeamMemberVO.WritePersonInfo = "否";

        //新增時，預設帶入登入者當團長
        if (!Page_ActivityTeamMemberVOList.Exists(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == myActivityTeamMemberVO.emp_id; }))
        {
            Page_ActivityTeamMemberVOList.Add(myActivityTeamMemberVO);
        }

        GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
        GridView_TemMember.DataBind();

        Wizard1.MoveTo(Wizard1.WizardSteps[0]);

    }


    //編輯
    protected void GoThirdStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        RegistActivity_Query1.Visible = false;
        Wizard1.Visible = true;

        //必要屬性
        MyFormMode = FormViewMode.Edit;
        ActivityID = new Guid(Session["activity_id"].ToString());

        //載入報名資訊
        ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
        ACMS.VO.ActivityRegistVO myActivityRegistVO = new ACMS.VO.ActivityRegistVO();

        myActivityRegistVO = myActivityRegistDAO.SelectActivityRegistByMemberID(ActivityID, clsAuth.ID);

        EmpID = myActivityRegistVO.emp_id;
        RegistBy = myActivityRegistVO.regist_by;

        MyHiddenField.Value = ActivityID.ToString();

        txtteam_name.Text = myActivityRegistVO.team_name;
        txtext_people.Text = myActivityRegistVO.ext_people.ToString();


        //不是團長不可編輯
        if (EmpID != clsAuth.ID)
        {
            txtteam_name.Enabled = false;
            txtext_people.Enabled = false;
        }

        //載入活動資訊
        GetActivityDefault();


        //編輯時，帶入資料庫資料

        ACMS.DAO.ActivityTeamMemberDAO myActivityTeamMemberDAO = new ACMS.DAO.ActivityTeamMemberDAO();

        Page_ActivityTeamMemberVOList =  myActivityTeamMemberDAO.SelectActivityTeamMember(ActivityID);

        GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
        GridView_TemMember.DataBind();


        Wizard1.MoveTo(Wizard1.WizardSteps[1]);

        InitQueryBlock(ActivityID.ToString());

        //編輯時載入動態欄位資料
        GetDynamicValue();

    }

    private void GetActivityDefault()
    {
        //取得活動資訊
        ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
        ACMS.VO.ActivatyVO myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

        //報名截止日後要唯讀
        //if (myActivatyVO.regist_deadline <= DateTime.Today)
        //{
        //    MyFormMode = FormViewMode.ReadOnly;
        //}

        //活動海報訊息
        Literal1.Text = myActivatyVO.activity_info;

        //活動相關訊息
        ObjectDataSource_ActivatyDetails.SelectParameters["id"].DefaultValue = ActivityID.ToString();
        ObjectDataSource_UpFiles.SelectParameters["dirName"].DefaultValue = Server.MapPath(Path.Combine("/ACMS/UpFiles", ActivityID.ToString()));

        //注意事項
        Literal_notice.Text = myActivatyVO.notice;


        //團隊固定欄位
        tr_showteam_fix1.Visible=( myActivatyVO.is_showteam_fix1=="Y");
        tr_showteam_fix2.Visible = (myActivatyVO.is_showteam_fix2 == "Y");

        if (tr_showteam_fix1.Visible == false && tr_showteam_fix2.Visible==false)
        {
            Panel_TeamFix.Visible = false;
        }

        lbltext_peopleStart.Text = myActivatyVO.teamextcount_min.ToString();
        lbltext_peopleEnd.Text = myActivatyVO.teamextcount_max.ToString();

        chk_text_people3.MinimumValue = myActivatyVO.teamextcount_min.ToString();
        chk_text_people3.MaximumValue = myActivatyVO.teamextcount_max.ToString();


        //個人欄位
        //Page_is_showperson_fix1 = myActivatyVO.is_showperson_fix1;
        //Page_is_showperson_fix2 = myActivatyVO.is_showperson_fix2;

         (OpenTeamPersonInfo1.FindControl("tr_idno") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = ( myActivatyVO.is_showidno=="Y");
         (OpenTeamPersonInfo1.FindControl("tr_remark") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = ( myActivatyVO.is_showremark=="Y");

        (OpenTeamPersonInfo1.FindControl("lblRemark") as Label).Text = myActivatyVO.remark_name;
        (OpenTeamPersonInfo1.FindControl("chk_txtremark") as RequiredFieldValidator).ErrorMessage = string.Format("{0}必填", myActivatyVO.remark_name);

        if (myActivatyVO.is_showidno == "N" && myActivatyVO.is_showremark == "N")
        {
            IsPersonInfoRequired = false;
            GridView_TemMember.Columns[3].Visible = false;
            GridView_TemMember.Columns[4].Visible = false;
        }
        else
        {
            IsPersonInfoRequired = true;
        }



        Page_team_member_min = myActivatyVO.team_member_min;
        Page_team_member_max = myActivatyVO.team_member_max;



        //FormView_fixA.DataBind();
        //FormView_fixA.FindControl("tr_person_fix1").Visible = (myActivatyVO.is_showperson_fix1 == "Y");
        //FormView_fixA.FindControl("tr_person_fix2").Visible = (myActivatyVO.is_showperson_fix2 == "Y");

        //(FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2Start") as Label).Text = myActivatyVO.personextcount_min.ToString();
        //(FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2End") as Label).Text = myActivatyVO.personextcount_max.ToString();

        //RangeValidator myRangeValidator = (FormView_fixA.FindControl("tr_person_fix2").FindControl("chk_txtperson_fix2_3") as RangeValidator);
        //myRangeValidator.MinimumValue = myActivatyVO.personextcount_min.ToString();
        //myRangeValidator.MaximumValue = myActivatyVO.personextcount_max.ToString();
    }
    
        //編輯時載入動態欄位資料
    private void GetDynamicValue()
    {
        ACMS.DAO.CustomFieldValueDAO myCustomFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();

        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustomFieldValueDAO.SelectCustomFieldValue(ActivityID, EmpID);

        foreach (ACMS.VO.CustomFieldValueVO myCustomFieldValueVO in myCustomFieldValueVOList)
        {

            if (myCustomFieldValueVO.field_control.ToUpper() == "TEXTBOX")
            {
                TextBox MyControl = new TextBox();
                MyControl.ID = string.Format("txt{0}", myCustomFieldValueVO.field_id);
                (PlaceHolder1.FindControl(MyControl.ID) as TextBox).Text = myCustomFieldValueVO.field_value;
            }
            else if (myCustomFieldValueVO.field_control.ToUpper() == "TEXTBOXLIST")
            {
                TCheckBoxList MyControl = new TCheckBoxList();
                MyControl.ID = string.Format("plh{0}", myCustomFieldValueVO.field_id);
                (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;

                CheckBoxList1_SelectedIndexChanged((PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList), null);

            }
            else if (myCustomFieldValueVO.field_control.ToUpper() == "CHECKBOXLIST")
            {
                TCheckBoxList MyControl = new TCheckBoxList();

                MyControl.ID = string.Format("cbl{0}", myCustomFieldValueVO.field_id);
                (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;


            }
            else if (myCustomFieldValueVO.field_control.ToUpper() == "RADIOBUTTONLIST")
            {
                TRadioButtonList MyControl = new TRadioButtonList();
                MyControl.ID = string.Format("radl{0}", myCustomFieldValueVO.field_id);
                (MyControl as TRadioButtonList).ClearSelection();
                (PlaceHolder1.FindControl(MyControl.ID) as TRadioButtonList).SelectedValue = myCustomFieldValueVO.field_value;
            }

        }
    }

    //顯示活動資訊
    protected void FormView_ActivatyDetails_DataBound(object sender, EventArgs e)
    {
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

    //開啟選擇隊員視窗
    protected void btnAgent_Click(object sender, EventArgs e)
    {
        OpenTeamMemberSelector1.TitleName = "選擇隊員";
        //OpenAgentSelector1.OkName = "報名";
        OpenTeamMemberSelector1.InitDataAndShow(ActivityID.ToString());
    }

    //選取隊員之後
    protected void GetEmployees_Click(object sender, EventArgs e)
    {
        GridView GridView_Employee = (GridView)OpenTeamMemberSelector1.FindControl("GridView_Employee");
        int i;

        try
        {
            for (i = 0; i < GridView_Employee.Rows.Count; i++)
            {
                if (((CheckBox)GridView_Employee.Rows[i].FindControl("CheckBox1")).Checked)
                {
                    ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = new ACMS.VO.ActivityTeamMemberVO();

                    myActivityTeamMemberVO.activity_id = ActivityID;
                    myActivityTeamMemberVO.emp_id = GridView_Employee.DataKeys[i].Value.ToString();
                    myActivityTeamMemberVO.boss_id = RegistBy;
                    myActivityTeamMemberVO.idno_type = 0;
                    myActivityTeamMemberVO.idno = "";
                    myActivityTeamMemberVO.remark = "";
                    myActivityTeamMemberVO.check_status = 0;
                    myActivityTeamMemberVO.WritePersonInfo="否";

                    myActivityTeamMemberVO.WORK_ID = GridView_Employee.Rows[i].Cells[0].Text.ToString();
                    myActivityTeamMemberVO.NATIVE_NAME = GridView_Employee.Rows[i].Cells[1].Text.ToString();
                    myActivityTeamMemberVO.C_DEPT_ABBR = GridView_Employee.Rows[i].Cells[2].Text.ToString();

                    if (!Page_ActivityTeamMemberVOList.Exists(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == myActivityTeamMemberVO.emp_id; }))
                    {
                        Page_ActivityTeamMemberVOList.Add(myActivityTeamMemberVO);
                    }                  

                }
            }

        }
        catch (Exception ex)
        {
            clsMyObj.ShowMessage("加入隊員失敗!");
        }

        GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
        GridView_TemMember.DataBind();

       

    }

    //隊員RowDataBound
    protected void GridView_TemMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //團長不可被刪除
            ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = (ACMS.VO.ActivityTeamMemberVO)(e.Row.DataItem);

            if (myActivityTeamMemberVO.emp_id == myActivityTeamMemberVO.boss_id)
            {
                (e.Row.FindControl("lbtnVOdelete") as LinkButton).Visible = false;
            }



            //團長能改大家的資料，但是團員只能改自己的資料
            if (EmpID != clsAuth.ID)
            {
                if (GridView_TemMember.DataKeys[e.Row.RowIndex].Value.ToString() != clsAuth.ID)
                {
                    (e.Row.FindControl("lbtnVOedit") as LinkButton).Visible = false;

                }
                (e.Row.FindControl("lbtnVOdelete") as LinkButton).Visible = false;
            }




        }





    }


    //編輯隊員個人欄位
    protected void lbtnVOedit_Click(object sender, EventArgs e)
    {
        string emp_id = GridView_TemMember.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

        ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == emp_id; });

        OpenTeamPersonInfo1.UC_ActivityTeamMemberVO = myActivityTeamMemberVO;

        OpenTeamPersonInfo1.InitDataAndShow();
    }

    //編輯隊員個人欄位之後
    protected void GetTeamPersonInfo_Click(object sender, EventArgs e)
    {
        ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO =Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.emp_id; });

        myActivityTeamMemberVO.WritePersonInfo = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.WritePersonInfo;
        myActivityTeamMemberVO.idno_type = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.idno_type;
        myActivityTeamMemberVO.idno = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.idno;
        myActivityTeamMemberVO.remark = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.remark;


        GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
        GridView_TemMember.DataBind();

    }

    





    //刪除隊員
    protected void lbtnVOdelete_Click(object sender, EventArgs e)
    {
        string emp_id =  GridView_TemMember.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

        ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == emp_id; });
        Page_ActivityTeamMemberVOList.Remove(myActivityTeamMemberVO);

        GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
        GridView_TemMember.DataBind();

    }


    //選取人員之後指定EmpID
    protected void GetSmallEmployees_Click(object sender, GetEmployeeEventArgs e)
    {
        //EmpID = e.id;
        //ObjectDataSource_RegisterPersonInfo.SelectParameters["emp_id"].DefaultValue = e.id;
        //FormView_RegisterPersonInfo.DataBind();

        ////個人固定欄位
        //ObjectDataSource_fixA.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
        //ObjectDataSource_fixA.SelectParameters["emp_id"].DefaultValue = EmpID;

    }

    //人員切換之後指定EmpID
    //protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    //{
    //    RadioButton RadioButton1 = sender as RadioButton;
    //    RadioButton1.Checked = true;

    //    GridView_TemMember.SelectedIndex = (RadioButton1.NamingContainer as GridViewRow).RowIndex;

    //    EmpID = GridView_TemMember.DataKeys[GridView_TemMember.SelectedIndex].Value.ToString();

    //    //載入個人資訊
    //    //個人固定欄位
    //    ObjectDataSource_fixA.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
    //    ObjectDataSource_fixA.SelectParameters["emp_id"].DefaultValue = EmpID;

    //    FormView_fixA.DataBind();

    //    //載入動態欄位資料

    //    ACMS.DAO.CustomFieldValueDAO myCustomFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();

    //    List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustomFieldValueDAO.SelectCustomFieldValue(ActivityID, EmpID);

    //    foreach (ACMS.VO.CustomFieldValueVO myCustomFieldValueVO in myCustomFieldValueVOList)
    //    {

    //        if (myCustomFieldValueVO.field_control.ToUpper() == "TEXTBOX")
    //        {
    //            TextBox MyControl = new TextBox();
    //            MyControl.ID = string.Format("txt{0}", myCustomFieldValueVO.field_id);
    //            (PlaceHolder1.FindControl(MyControl.ID) as TextBox).Text = myCustomFieldValueVO.field_value;
    //        }
    //        else if (myCustomFieldValueVO.field_control.ToUpper() == "TEXTBOXLIST")
    //        {
    //            TCheckBoxList MyControl = new TCheckBoxList();
    //            MyControl.ID = string.Format("plh{0}", myCustomFieldValueVO.field_id);
    //            (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;

    //            CheckBoxList1_SelectedIndexChanged((PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList), null);

    //        }
    //        else if (myCustomFieldValueVO.field_control.ToUpper() == "CHECKBOXLIST")
    //        {
    //            TCheckBoxList MyControl = new TCheckBoxList();

    //            MyControl.ID = string.Format("cbl{0}", myCustomFieldValueVO.field_id);
    //            (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;


    //        }
    //        else if (myCustomFieldValueVO.field_control.ToUpper() == "RADIOBUTTONLIST")
    //        {
    //            TRadioButtonList MyControl = new TRadioButtonList();
    //            MyControl.ID = string.Format("radl{0}", myCustomFieldValueVO.field_id);
    //            (MyControl as TRadioButtonList).ClearSelection();
    //            (PlaceHolder1.FindControl(MyControl.ID) as TRadioButtonList).SelectedValue = myCustomFieldValueVO.field_value;
    //        }
        
    //    }

    


    //}



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

   //下一步
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 1)
        {
            if (IsPersonInfoRequired)
            {
                //個人資料都有填,才能按下一步
                foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in Page_ActivityTeamMemberVOList)
                {
                    if (myActivityTeamMemberVO.WritePersonInfo != "是")
                    {
                        clsMyObj.ShowMessage(string.Format(@"{0}尚未填寫個人相關欄位!\n無法繼續報名程序!", myActivityTeamMemberVO.NATIVE_NAME));
                        Wizard1.MoveTo(Wizard1.WizardSteps[0]);
                        return;
                    }
                }
            }

            

            //團隊成員人數符合限制,才能按下一步
            if (!(GridView_TemMember.Rows.Count >= Page_team_member_min && GridView_TemMember.Rows.Count <= Page_team_member_max))
            {

                clsMyObj.ShowMessage(string.Format(@"團隊成員人數必須介於{0}~{1}人!", Page_team_member_min, Page_team_member_max));
                Wizard1.MoveTo(Wizard1.WizardSteps[0]);
                return;
            }

            //檢查欲報名者是否已經報過名
            if (MyFormMode == FormViewMode.Insert)
            {
                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                if (myActivityRegistDAO.IsPersonRegisted(ActivityID, EmpID) > 0)
                {
                    clsMyObj.ShowMessage(@"您已經以團長的身分報名報名過該活動!\n");
                    Wizard1.MoveTo(Wizard1.WizardSteps[0]);
                    return;
                }


                string strEmp_id = "";
                foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in Page_ActivityTeamMemberVOList)
                {
                    strEmp_id += string.Format("{0},", myActivityTeamMemberVO.emp_id);
                }

                if (strEmp_id.EndsWith(","))
                {
                    strEmp_id = strEmp_id.Substring(0, strEmp_id.Length - 1);
                }



                string strDouble = myActivityRegistDAO.IsTeamRegisted(ActivityID, strEmp_id);

                if (!string.IsNullOrEmpty(strDouble))
                {
                    clsMyObj.ShowMessage(string.Format(@"{0}已經是別的團隊的成員，請選擇其他成員!", strDouble));
                    Wizard1.MoveTo(Wizard1.WizardSteps[0]);
                    return;
                }

            }

        }







        //else if (Wizard1.ActiveStepIndex == 1 && MyFormMode != FormViewMode.Insert)
        //{

        //    if (GridView_TemMember.SelectedIndex == -1)
        //    {
        //        clsMyObj.ShowMessage(@"請選擇要編輯的人員。");
        //        Wizard1.MoveTo(Wizard1.WizardSteps[0]);
        //    }
        //}

    }

    //取得報名資訊
    private ACMS.VO.ActivityRegistVO GetActivityRegistVO()
    {
        ACMS.VO.ActivityRegistVO myActivityRegistVO = new ACMS.VO.ActivityRegistVO();

        myActivityRegistVO.activity_id = ActivityID;
        myActivityRegistVO.emp_id = EmpID;
        myActivityRegistVO.regist_by = RegistBy;
        myActivityRegistVO.idno = "";
        myActivityRegistVO.team_name = txtteam_name.Text;
        myActivityRegistVO.ext_people =(txtext_people.Text=="" ? 0: Convert.ToInt32(txtext_people.Text));

        return myActivityRegistVO;

    }

    //存檔時取得自訂欄位值
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
        ACMS.VO.ActivityRegistVO myActivityRegistVO = GetActivityRegistVO(); //取得報名資訊      
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = GetCustomFieldValueVOList();//取得自訂欄位值
        //ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

        //報名
        MySingleton.AlterRegistResult MyResult;

        if (MyFormMode == FormViewMode.Insert)
        {
            MyResult = MySingleton.GetMySingleton().AlterRegist_Team(myActivityRegistVO, myCustomFieldValueVOList, Page_ActivityTeamMemberVOList, MySingleton.AlterRegistType.RegistInsert, new Guid(), "", "", "");

        }
        else
        {
            MyResult = MySingleton.GetMySingleton().AlterRegist_Team(myActivityRegistVO, myCustomFieldValueVOList, Page_ActivityTeamMemberVOList, MySingleton.AlterRegistType.RegistUpdate, new Guid(), "", "", "");

        }


        if (MyResult == MySingleton.AlterRegistResult.RegistFail_Already)
        {
            clsMyObj.ShowMessage("已存在報名成功紀錄，無法重複報名!");
        }
        else if (MyResult == MySingleton.AlterRegistResult.RegistFail_Full)
        {
            clsMyObj.ShowMessage(@"抱歉，報名已額滿!\n若錄取名額有增加\n則可再次報名。");
        }
        else if (MyResult == MySingleton.AlterRegistResult.RegistFail)
        {
            clsMyObj.ShowMessage(@"資料存檔發生錯誤，無法完成報名。");
        }


        Response.Redirect("RegistedActivityQuery.aspx");

    }

    //protected void FormView_fixA_DataBound(object sender, EventArgs e)
    //{
    //    ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
    //    ACMS.VO.ActivatyVO myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

    //    FormView_fixA.FindControl("tr_person_fix1").Visible = (myActivatyVO.is_showperson_fix1 == "Y");
    //    FormView_fixA.FindControl("tr_person_fix2").Visible = (myActivatyVO.is_showperson_fix2 == "Y");

    //    (FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2Start") as Label).Text = myActivatyVO.personextcount_min.ToString();
    //    (FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2End") as Label).Text = myActivatyVO.personextcount_max.ToString();

    //    RangeValidator myRangeValidator = (FormView_fixA.FindControl("tr_person_fix2").FindControl("chk_txtperson_fix2_3") as RangeValidator);
    //    myRangeValidator.MinimumValue = myActivatyVO.personextcount_min.ToString();
    //    myRangeValidator.MaximumValue = myActivatyVO.personextcount_max.ToString();
    //}


    //protected void GridView_RegisterPeoplinfo_DataBound(object sender, EventArgs e)
    //{
    //    if()
    //}

    protected void GridView_RegisterPeoplinfo_DataBound(object sender, EventArgs e)
    {
        if (GridView_TemMember.Rows.Count > 0)
        {  
            //系統預設會勾選第一筆資料
           // RadioButton RadioButton1 = (RadioButton)GridView_TemMember.Rows[0].FindControl("RadioButton1");         
           //InitQueryBlock(ActivityID.ToString());
            //RadioButton1_CheckedChanged(RadioButton1, null);
 
        }
    }




}


//產生"自訂欄位"控制項
public partial class WebForm_RegistActivity_RegistActivity_Team
{
    protected void InitQueryBlock(string activity_id)
    {
        MyHashtable.Clear();

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
                    MyTableCell_Title.Width = System.Web.UI.WebControls.Unit.Pixel(150);// "40%";
                    MyTableCell_Control.Width = System.Web.UI.WebControls.Unit.Pixel(200);// "40%";

                    //Title
                    Literal lblTitle = new Literal();
                    lblTitle.ID = string.Format("lbl{0}", myCustomFieldVO.field_id);
                    lblTitle.Text = myCustomFieldVO.field_name;

                    MyTableCell_Title.Controls.Add(lblTitle);

                    //Control
                    Control MyControl = new Control();
                    Control MyControl_chk = new Control();

                    if (myCustomFieldVO.field_control.ToUpper() == "TEXTBOX")
                    {
                        MyControl = new TextBox();
                        MyControl.ID = string.Format("txt{0}", myCustomFieldVO.field_id);

                        MyControl_chk = new RequiredFieldValidator();
                        MyControl_chk.ID = string.Format("chk_txt{0}", myCustomFieldVO.field_id);
                        (MyControl_chk as RequiredFieldValidator).ControlToValidate = MyControl.ID;
                        (MyControl_chk as RequiredFieldValidator).Display = ValidatorDisplay.Dynamic;
                        (MyControl_chk as RequiredFieldValidator).ErrorMessage = string.Format("{0}必填!", myCustomFieldVO.field_name);
                        (MyControl_chk as RequiredFieldValidator).Text = "*";
                        (MyControl_chk as RequiredFieldValidator).ValidationGroup = "WizardNext";
                    }
                    else if (myCustomFieldVO.field_control.ToUpper() == "TEXTBOXLIST")
                    {
                        MyControl = new TCheckBoxList();
                        MyControl.ID = string.Format("plh{0}", myCustomFieldVO.field_id);
                        (MyControl as TCheckBoxList).AutoPostBack = true;
                        (MyControl as TCheckBoxList).SelectedIndexChanged += CheckBoxList1_SelectedIndexChanged;

                        (MyControl as TCheckBoxList).ClearSelection();
                        //(MyControl as TCheckBoxList).EnableViewState = false;

                        MyControl_chk = new TCheckBoxListRequiredValidator();
                        MyControl_chk.ID = string.Format("chk_plh{0}", myCustomFieldVO.field_id);
                        (MyControl_chk as TCheckBoxListRequiredValidator).ControlToValidate = MyControl.ID;
                        (MyControl_chk as TCheckBoxListRequiredValidator).Display = ValidatorDisplay.Dynamic;
                        (MyControl_chk as TCheckBoxListRequiredValidator).ErrorMessage = string.Format("{0}必填!", myCustomFieldVO.field_name);
                        (MyControl_chk as TCheckBoxListRequiredValidator).Text = "*";
                        (MyControl_chk as TCheckBoxListRequiredValidator).ValidationGroup = "WizardNext";

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

                        MyControl_chk = new TCheckBoxListRequiredValidator();
                        MyControl_chk.ID = string.Format("chk_cbl{0}", myCustomFieldVO.field_id);
                        (MyControl_chk as TCheckBoxListRequiredValidator).ControlToValidate = MyControl.ID;
                        (MyControl_chk as TCheckBoxListRequiredValidator).Display = ValidatorDisplay.Dynamic;
                        (MyControl_chk as TCheckBoxListRequiredValidator).ErrorMessage = string.Format("{0}必填!", myCustomFieldVO.field_name);
                        (MyControl_chk as TCheckBoxListRequiredValidator).Text = "*";
                        (MyControl_chk as TCheckBoxListRequiredValidator).ValidationGroup = "WizardNext";
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

                        MyControl_chk = new RequiredFieldValidator();
                        MyControl_chk.ID = string.Format("chk_radl{0}", myCustomFieldVO.field_id);
                        (MyControl_chk as RequiredFieldValidator).ControlToValidate = MyControl.ID;
                        (MyControl_chk as RequiredFieldValidator).Display = ValidatorDisplay.Dynamic;
                        (MyControl_chk as RequiredFieldValidator).ErrorMessage = string.Format("{0}必填!", myCustomFieldVO.field_name);
                        (MyControl_chk as RequiredFieldValidator).Text = "*";
                        (MyControl_chk as RequiredFieldValidator).ValidationGroup = "WizardNext";
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
                    MyTableCell_Control.Controls.Add(MyControl_chk);

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
public partial class WebForm_RegistActivity_RegistActivity_Team
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


    public int? Page_team_member_min
    {
        get { return (int)ViewState["Page_team_member_min"]; }
        set { ViewState["Page_team_member_min"] = value; }
    }

    public int? Page_team_member_max
    {
        get { return (int)ViewState["Page_team_member_max"]; }
        set { ViewState["Page_team_member_max"] = value; }
    }

    //是否需填寫個人資訊
    public bool IsPersonInfoRequired
    {
        get { return (bool)ViewState["IsPersonInfoRequired"]; }
        set { ViewState["IsPersonInfoRequired"] = value; }
    }

}



