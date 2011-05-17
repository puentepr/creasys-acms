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

public partial class WebForm_RegistActivity_RegistActivity_Team : BasePage
{
    HiddenField MyHiddenField = new HiddenField();

    Hashtable MyHashtable = new Hashtable();

    protected void Page_Init(object sender, EventArgs e)
    {
        MyHiddenField.ID = "__MyHiddenField";
        this.Form.Controls.Add(MyHiddenField);
        //  InitQueryBlock(Request.Form[MyHiddenField.UniqueID]);
        //try
        //{
        //    InitQueryBlock(Session["activity_id"].ToString());
        //}
        //catch
        //{
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Wizard1.ActiveStepIndex >= 1)
        try
        {
            InitQueryBlock(ActivityID.ToString());
        }
        catch
        { 
        }

        if (!IsPostBack)
        {
            Session.Remove("form_mode1");
            Session["ShowPanel"] = false;

            Session["Team"] = "Yes";
            (this.Master as MyMasterPage).PanelMainGroupingText = "團隊報名";
            Wizard1.Visible = false;

            if (Session["form_mode"] != null && Session["activity_id"] != null)
            {

                //以新增方式進來時
                if (Session["form_mode"].ToString() == "regist")
                {
                    ActivityID = new Guid(Session["activity_id"].ToString());
                    RegistGoSecondEventArgs myRegistGoSecondEventArgs = new RegistGoSecondEventArgs(new Guid(Session["activity_id"].ToString()));
                    GoSecondStep_Click(null, myRegistGoSecondEventArgs);
                }
                //報名預覽
                if (Session["form_mode"].ToString() == "preview")
                {
                    hiMode1.Value = "preview";
                    ActivityID = new Guid(Session["activity_id"].ToString());
                    Session["form_mode1"] = "preview";
                    RegistGoSecondEventArgs myRegistGoSecondEventArgs = new RegistGoSecondEventArgs(new Guid(Session["activity_id"].ToString()));
                    GoSecondStep_Click(null, myRegistGoSecondEventArgs);
                }

                if (Session["form_mode"].ToString() == "edit")
                {
                    //以編輯方式進來時
                    ActivityID = new Guid(Session["activity_id"].ToString());
                    GoThirdStep_Click(null, null);
                    Wizard1.FindControl("FinishNavigationTemplateContainerID$btnHome").Visible = true;
                    ((Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$FinishButton")).Text = "儲存並發送確認信";
                }
            }
            else
            {
                //先查詢,再 GoSecondStep_Click

            }

            Session["form_mode"] = null;
            // Session["activity_id"] = null;

        }

    }

    //新增報名
    protected void GoSecondStep_Click(object sender, RegistGoSecondEventArgs e)
    {

        try
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
            myActivityTeamMemberVO.emp_id = clsAuth.ID;
            myActivityTeamMemberVO.boss_id = RegistBy;
            myActivityTeamMemberVO.idno = "";
            myActivityTeamMemberVO.remark = "";
            myActivityTeamMemberVO.check_status = 0;

            myActivityTeamMemberVO.WORK_ID = clsAuth.WORK_ID;
            myActivityTeamMemberVO.NATIVE_NAME = clsAuth.NATIVE_NAME;
            myActivityTeamMemberVO.C_DEPT_ABBR = clsAuth.C_DEPT_ABBR;
            myActivityTeamMemberVO.C_DEPT_NAME = clsAuth.C_DEPT_NAME;

            myActivityTeamMemberVO.WritePersonInfo = "否";

            //新增時，預設帶入登入者當團長
            if (!Page_ActivityTeamMemberVOList.Exists(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == myActivityTeamMemberVO.emp_id; }))
            {
                Page_ActivityTeamMemberVOList.Add(myActivityTeamMemberVO);
            }

            GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
            GridView_TemMember.DataBind();

            Wizard1.MoveTo(Wizard1.WizardSteps[0]);
            InitQueryBlock(ActivityID.ToString());

        }
        catch (Exception ex)
        {
            WriteErrorLog("SecondStep", ex.Message, "0");

        }

    }


    //編輯
    protected void GoThirdStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        try
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

            EmpID = clsAuth.ID;
            RegistBy = myActivityRegistVO.regist_by;

            MyHiddenField.Value = ActivityID.ToString();

            txtteam_name.Text = myActivityRegistVO.team_name;
            txtext_people.Text = myActivityRegistVO.ext_people.ToString();


            //不是團長不可編輯
            if (EmpID != RegistBy)
            {
                txtteam_name.Enabled = false;
                txtext_people.Enabled = false;

                btnAddTeamMember.Visible = false;
                //自訂欄位
                PanelCustomFieldA1.Enabled = false;
            }
           

            //載入活動資訊
            GetActivityDefault();


            //編輯時，帶入資料庫資料

            ACMS.DAO.ActivityTeamMemberDAO myActivityTeamMemberDAO = new ACMS.DAO.ActivityTeamMemberDAO();

            Page_ActivityTeamMemberVOList = myActivityTeamMemberDAO.SelectActivityTeamMember(ActivityID, RegistBy);

            GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
            GridView_TemMember.DataBind();


            Wizard1.MoveTo(Wizard1.WizardSteps[0]);
            try
            {
                ((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text = ((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text.Replace("-", "/").Replace("T", " ");
                ((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text = ((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text.Replace("-", "/").Replace("T", " ");
            }
            catch
            { }
            //if (((Label)FormView_ActivatyDetails.FindControl("limit_countLabel")).Text == "999999")
            //{
            //    ((Label)FormView_ActivatyDetails.FindControl("limit_countLabel")).Text = "無上限";
            //}
            //if (((Label)FormView_ActivatyDetails.FindControl("limit2_countLabel")).Text == "0")
            //{
            //    ((Label)FormView_ActivatyDetails.FindControl("limit2_countLabel")).Text = "無";
            //}


            // InitQueryBlock(ActivityID.ToString());

            //編輯時載入動態欄位資料
            GetDynamicValue();
        }
        catch (Exception ex)
        {
            //WriteErrorLog("ThirdStep", ex.Message, "0");

        }

    }

    private void GetActivityDefault()
    {
        try
        {
            //取得活動資訊
            ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
            ACMS.VO.ActivatyVO myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

            //報名截止日後要唯讀
            if (myActivatyVO.regist_deadline < DateTime.Today)
            {
                MyFormMode = FormViewMode.ReadOnly;
                Panel_TeamFix.Enabled = false;
                PanelTeamMember.Enabled = false;
                PanelCustomFieldA1.Enabled = false;
            }

            //活動海報訊息
            Literal1.Text = myActivatyVO.activity_info;

            //活動相關訊息
            ObjectDataSource_ActivatyDetails.SelectParameters["id"].DefaultValue = ActivityID.ToString();
            ObjectDataSource_UpFiles.SelectParameters["dirName"].DefaultValue = Server.MapPath(Path.Combine("~/UpFiles", ActivityID.ToString()));

            //注意事項
            Literal_notice.Text = myActivatyVO.notice.Replace("\r\n", "<br />");


            //團隊固定欄位
            tr_showteam_fix1.Visible = (myActivatyVO.is_showteam_fix1 == "Y");
            tr_showteam_fix2.Visible = (myActivatyVO.is_showteam_fix2 == "Y");

            if (myActivatyVO.is_showteam_fix1 != "Y" && myActivatyVO.is_showteam_fix2 != "Y")
            {
                Panel_TeamFix.Visible = false;
            }
            //if (tr_showteam_fix1.Visible || tr_showteam_fix2.Visible)
            //{
            //    Session["ShowPanel"] = true; 
            //}



            lbltext_peopleStart.Text = myActivatyVO.teamextcount_min.ToString();
            lbltext_peopleEnd.Text = myActivatyVO.teamextcount_max.ToString();

            chk_text_people3.MinimumValue = myActivatyVO.teamextcount_min.ToString();
            chk_text_people3.MaximumValue = myActivatyVO.teamextcount_max.ToString();


            //個人欄位
            //Page_is_showperson_fix1 = myActivatyVO.is_showperson_fix1;
            //Page_is_showperson_fix2 = myActivatyVO.is_showperson_fix2;
            ACMS.BO.CustomFieldBO myCustFieldBo = new ACMS.BO.CustomFieldBO();
            if (myCustFieldBo.SelectByActivity_id(ActivityID).Count > 0)
            {
                Session["ShowPanel"] = true;

            }


            (OpenTeamPersonInfo1.FindControl("tr_idno") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = (myActivatyVO.is_showidno == "Y");
            (OpenTeamPersonInfo1.FindControl("tr_remark") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = (myActivatyVO.is_showremark == "Y");

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
        catch (Exception ex)
        {
            WriteErrorLog("GetDefault", ex.Message, "0");

        }
    }

    //編輯時載入自訂欄位資料
    private void GetDynamicValue()
    {
        ACMS.DAO.CustomFieldValueDAO myCustomFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();

        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustomFieldValueDAO.SelectCustomFieldValue(ActivityID, RegistBy);

        foreach (ACMS.VO.CustomFieldValueVO myCustomFieldValueVO in myCustomFieldValueVOList)
        {
            Session["ShowPanel"] = true;
            if (myCustomFieldValueVO.field_control.ToUpper() == "TEXTBOX")
            {
                TextBox MyControl = new TextBox();
                MyControl.ID = string.Format("txt{0}", myCustomFieldValueVO.field_id);
                try
                {
                    (PlaceHolder1.FindControl(MyControl.ID) as TextBox).Text = myCustomFieldValueVO.field_value;
                }
                catch
                {

                    InitQueryBlock(ActivityID.ToString());
                    (PlaceHolder1.FindControl(MyControl.ID) as TextBox).Text = myCustomFieldValueVO.field_value;

                }
            }
            else if (myCustomFieldValueVO.field_control.ToUpper() == "TEXTBOXLIST")
            {
                TCheckBoxList MyControl = new TCheckBoxList();
                MyControl.ID = string.Format("plh{0}", myCustomFieldValueVO.field_id);
                try
                {
                    (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;
                    CheckBoxList1_SelectedIndexChanged((PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList), null);
                }
                catch
                {

                    InitQueryBlock(ActivityID.ToString());
                    (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;
                    CheckBoxList1_SelectedIndexChanged((PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList), null);

                }


            }
            else if (myCustomFieldValueVO.field_control.ToUpper() == "CHECKBOXLIST")
            {
                TCheckBoxList MyControl = new TCheckBoxList();

                MyControl.ID = string.Format("cbl{0}", myCustomFieldValueVO.field_id);

                try
                {
                    (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;
                }
                catch
                {

                    InitQueryBlock(ActivityID.ToString());
                    (PlaceHolder1.FindControl(MyControl.ID) as TCheckBoxList).SelectedValueList = myCustomFieldValueVO.field_value;

                }



            }
            else if (myCustomFieldValueVO.field_control.ToUpper() == "RADIOBUTTONLIST")
            {
                TRadioButtonList MyControl = new TRadioButtonList();
                MyControl.ID = string.Format("radl{0}", myCustomFieldValueVO.field_id);
                try
                {
                    (MyControl as TRadioButtonList).ClearSelection();
                    (PlaceHolder1.FindControl(MyControl.ID) as TRadioButtonList).SelectedValue = myCustomFieldValueVO.field_value;
                }
                catch
                {

                    InitQueryBlock(ActivityID.ToString());
                    (MyControl as TRadioButtonList).ClearSelection();
                    (PlaceHolder1.FindControl(MyControl.ID) as TRadioButtonList).SelectedValue = myCustomFieldValueVO.field_value;

                }



            }

        }
    }

    //顯示活動資訊
    protected void FormView_ActivatyDetails_DataBound(object sender, EventArgs e)
    {
        //檔案下載是否出現
        DataRowView drv = (DataRowView)FormView_ActivatyDetails.DataItem;

        (FormView_ActivatyDetails.FindControl("GridView_UpFiles") as GridView).Visible = (drv["is_showfile"].ToString() == "Y");
        ((Label)FormView_ActivatyDetails.FindControl("people_typeLabel")).Text = ((Label)FormView_ActivatyDetails.FindControl("people_typeLabel")).Text.Replace("\r\n", "<br/>");
        ((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text = DateTime.Parse(((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text).ToString("yyyy/MM/dd HH:mm");
        ((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text = DateTime.Parse(((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text).ToString("yyyy/MM/dd HH:mm");
        ((Label)FormView_ActivatyDetails.FindControl("regist_startdateLabel")).Text = DateTime.Parse(((Label)FormView_ActivatyDetails.FindControl("regist_startdateLabel")).Text).ToString("yyyy/MM/dd");
        ((Label)FormView_ActivatyDetails.FindControl("regist_deadlineLabel")).Text = DateTime.Parse(((Label)FormView_ActivatyDetails.FindControl("regist_deadlineLabel")).Text).ToString("yyyy/MM/dd");
        ((Label)FormView_ActivatyDetails.FindControl("cancelregist_deadlineLabel")).Text = DateTime.Parse(((Label)FormView_ActivatyDetails.FindControl("cancelregist_deadlineLabel")).Text).ToString("yyyy/MM/dd");
        if (((Label)FormView_ActivatyDetails.FindControl("limit_countLabel")).Text == "999999")
        {
            ((Label)FormView_ActivatyDetails.FindControl("limit_countLabel")).Text = "無上限";
        }
        if (((Label)FormView_ActivatyDetails.FindControl("limit2_countLabel")).Text == "0")
        {
            ((Label)FormView_ActivatyDetails.FindControl("limit2_countLabel")).Text = "無";
        }

    }

    //下載檔案
    protected void lbtnFileDownload_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GridView_UpFiles = (GridView)FormView_ActivatyDetails.FindControl("GridView_UpFiles");
            FileInfo myFileInfo = new FileInfo(GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());
            string fileName = GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
            fileName = this.ResolveUrl("~/Upfiles/" + fileName.Substring(fileName.IndexOf(ActivityID.ToString())));

            if (myFileInfo.Exists)
            {
                //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", Server.UrlEncode((myFileInfo.Name))));
                //// 輸出檔案。
                //Response.WriteFile(myFileInfo.FullName);
                Response.Write("<script type=\"text/javascript\"> window.open('" + fileName + "')</script>");
            }
        }
        catch (Exception ex)
        {
            WriteErrorLog("DownLoadFile", ex.Message, "0");

        }
    }

    //開啟選擇隊員視窗
    protected void btnAgent_Click(object sender, EventArgs e)
    {
        try
        {
            OpenTeamMemberSelector1.TitleName = "選擇隊員";
            //OpenAgentSelector1.OkName = "報名";
            OpenTeamMemberSelector1.Visible = true;
            OpenTeamMemberSelector1.Page_ActivityTeamMemberVOList = Page_ActivityTeamMemberVOList;
            OpenTeamMemberSelector1.InitDataAndShow(ActivityID.ToString());
        }
        catch (Exception ex)
        {
            WriteErrorLog("Agent", ex.Message, "0");

        }
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
                if (((CheckBox)GridView_Employee.Rows[i].FindControl("chkRJRA")).Checked)
                {
                    ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = new ACMS.VO.ActivityTeamMemberVO();

                    myActivityTeamMemberVO.activity_id = ActivityID;
                    myActivityTeamMemberVO.emp_id = GridView_Employee.DataKeys[i].Value.ToString();
                    myActivityTeamMemberVO.boss_id = RegistBy;
                    myActivityTeamMemberVO.idno_type = 0;
                    myActivityTeamMemberVO.idno = "";
                    myActivityTeamMemberVO.remark = "";
                    myActivityTeamMemberVO.check_status = 0;
                    myActivityTeamMemberVO.WritePersonInfo = "否";

                    myActivityTeamMemberVO.WORK_ID = GridView_Employee.Rows[i].Cells[0].Text.ToString();
                    myActivityTeamMemberVO.NATIVE_NAME = GridView_Employee.Rows[i].Cells[1].Text.ToString();
                    myActivityTeamMemberVO.C_DEPT_NAME = GridView_Employee.Rows[i].Cells[2].Text.ToString();

                    if (!Page_ActivityTeamMemberVOList.Exists(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == myActivityTeamMemberVO.emp_id; }))
                    {
                        Page_ActivityTeamMemberVOList.Add(myActivityTeamMemberVO);
                    }

                }
                else
                {

                    ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == GridView_Employee.DataKeys[i].Value.ToString(); });
                    if (myActivityTeamMemberVO != null)
                    {
                        if (myActivityTeamMemberVO.emp_id != myActivityTeamMemberVO.boss_id)
                        {
                            Page_ActivityTeamMemberVOList.Remove(myActivityTeamMemberVO);
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        {
            ShowMessageForAjax(this, "加入隊員失敗!");
            WriteErrorLog("GetEmployee_click", ex.Message, "0");
        }

        GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
        GridView_TemMember.DataBind();
        OpenTeamMemberSelector1.Page_ActivityTeamMemberVOList = Page_ActivityTeamMemberVOList;



    }

    //隊員RowDataBound
    protected void GridView_TemMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header && (MyFormMode == FormViewMode.Edit || MyFormMode == FormViewMode.ReadOnly ))
        {
          //  e.Row.FindControl("chkDelHead").Visible = false;
          //  e.Row.FindControl("lbtnVOdeleteHeader").Visible = false;

        }
        ACMS.DAO.ActivityRegistDAO regDao = new ACMS.DAO.ActivityRegistDAO();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //團長不可被刪除
            ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = (ACMS.VO.ActivityTeamMemberVO)(e.Row.DataItem);

            if (myActivityTeamMemberVO.emp_id == myActivityTeamMemberVO.boss_id)
            {
                (e.Row.FindControl("lbtnVOdelete") as LinkButton).Visible = false;
                (e.Row.FindControl("chkDel")).Visible = false;
            }

            if (regDao.IsPersonRegisted(ActivityID,myActivityTeamMemberVO.emp_id ,"","2")>0)
            {
            
             (e.Row.FindControl("lbtnVOdelete") as LinkButton).Visible = false;
             (e.Row.FindControl("chkDel")).Visible = false;
            
            }

            //團長能改大家的資料，但是團員只能改自己的資料
            if (EmpID != RegistBy)
            {
                if (GridView_TemMember.DataKeys[e.Row.RowIndex].Value.ToString() != EmpID)
                {
                    (e.Row.FindControl("lbtnVOedit") as LinkButton).Visible = false;
                }
                (e.Row.FindControl("lbtnVOdelete") as LinkButton).Visible = false;
               // (e.Row.FindControl("chkDel")).Visible = false;
            }

            //==如果是Edit則不可刪除人員
            if (MyFormMode == FormViewMode.Edit)
            {
                (e.Row.FindControl("lbtnVOdelete") as LinkButton).Visible = false;
               // (e.Row.FindControl("chkDel")).Visible = false;
            }


        }





    }


    //編輯隊員個人欄位
    protected void lbtnVOedit_Click(object sender, EventArgs e)
    {
        try
        {
            string emp_id = GridView_TemMember.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

            ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == emp_id; });

            OpenTeamPersonInfo1.UC_ActivityTeamMemberVO = myActivityTeamMemberVO;
            OpenTeamPersonInfo1.Visible = true;
            OpenTeamPersonInfo1.InitDataAndShow();
        }
        catch (Exception ex)
        {
            WriteErrorLog("VOEdit", ex.Message, "0");

        }
    }

    //編輯隊員個人欄位之後
    protected void GetTeamPersonInfo_Click(object sender, EventArgs e)
    {
        try
        {
            ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.emp_id; });

            myActivityTeamMemberVO.WritePersonInfo = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.WritePersonInfo;
            myActivityTeamMemberVO.idno_type = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.idno_type;
            myActivityTeamMemberVO.idno = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.idno;
            myActivityTeamMemberVO.remark = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.remark;
            //myActivityTeamMemberVO.C_DEPT_NAME = OpenTeamPersonInfo1.UC_ActivityTeamMemberVO.C_DEPT_NAME;


            GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
            GridView_TemMember.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("GetTemPersonInfo_click", ex.Message, "0");

        }

    }







    //刪除隊員
    protected void lbtnVOdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string emp_id = GridView_TemMember.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

            ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == emp_id; });
            Page_ActivityTeamMemberVOList.Remove(myActivityTeamMemberVO);

            GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
            GridView_TemMember.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("VoDelete", ex.Message, "0");

        }

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
                intSum = intSum + Convert.ToInt32(theListItem.Text.Replace("元", "").Split(':')[1]);
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
            OpenTeamMemberSelector1.Visible = false;
            OpenTeamPersonInfo1.Visible = false;
            if (IsPersonInfoRequired)
            {
                //個人資料都有填,才能按下一步
                foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in Page_ActivityTeamMemberVOList)
                {
                    if (myActivityTeamMemberVO.WritePersonInfo != "是")
                    {
                        ShowMessageForAjax(this, string.Format(@"{0}尚未填寫個人相關欄位!無法繼續報名程序!", myActivityTeamMemberVO.NATIVE_NAME));
                        Wizard1.MoveTo(Wizard1.WizardSteps[0]);
                        return;
                    }
                }
            }

            //當團長在操作此畫面時
            if (EmpID == RegistBy)
            {
                //因為有可能更改團員，團隊成員人數符合限制,才能按下一步
                if (!(GridView_TemMember.Rows.Count >= Page_team_member_min && GridView_TemMember.Rows.Count <= Page_team_member_max))
                {
                    ShowMessageForAjax(this, string.Format(@"團隊成員人數必須介於{0}~{1}人!", Page_team_member_min, Page_team_member_max));
                    Wizard1.MoveTo(Wizard1.WizardSteps[0]);
                    return;
                }


                //因為有可能更改團員，所以要檢查欲報名者是否已經報過名

                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                string strEmp_id = "";
                foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in Page_ActivityTeamMemberVOList)
                {
                    strEmp_id += string.Format("{0},", myActivityTeamMemberVO.emp_id);
                }

                if (strEmp_id.EndsWith(","))
                {
                    strEmp_id = strEmp_id.Substring(0, strEmp_id.Length - 1);
                }

                string strDouble = myActivityRegistDAO.IsTeamRegisted(ActivityID, strEmp_id, RegistBy);

                if (!string.IsNullOrEmpty(strDouble))
                {
                    ShowMessageForAjax(this, string.Format(@"{0}已經是別的團隊的成員，請選擇其他成員!", strDouble));
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
        myActivityRegistVO.emp_id = EmpID;//若是團員近來修改個人欄位時，此值會是團員ID,所以不能存ActivityRegist 只能存ActivityTeamMember
        myActivityRegistVO.regist_by = RegistBy;
        myActivityRegistVO.idno = "";
        myActivityRegistVO.team_name = txtteam_name.Text;
        myActivityRegistVO.ext_people = (txtext_people.Text == "" ? 0 : Convert.ToInt32(txtext_people.Text));

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
        //預覽時
        if (Session["form_mode1"] != null)
        {
            if (Session["form_mode1"].ToString() == "preview")
            {
                Session.Remove("form_mode1");
                Response.Redirect("~/WebForm/ManageActivity/ActivityEditQuery.aspx");
            }
        }

        if (hiMode1.Value == "preview")
        {
            Response.Redirect("~/WebForm/ManageActivity/ActivityEditQuery.aspx");
        }

        if (MyFormMode == FormViewMode.ReadOnly)
        {
            Response.Redirect("RegistedActivityQuery.aspx?type=2");
        }
         ACMS.DAO.ActivityGroupLimitDAO  limDAO=new ACMS.DAO.ActivityGroupLimitDAO ();
        try
        {

            ACMS.VO.ActivityRegistVO myActivityRegistVO = GetActivityRegistVO(); //取得報名資訊      
            List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = GetCustomFieldValueVOList();//取得自訂欄位值
            //ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            ACMS.DAO.ActivityRegistDAO dao = new ACMS.DAO.ActivityRegistDAO();
            //報名
            MySingleton.AlterRegistResult MyResult;
            string path = Server.MapPath("~/UpFiles");
            string errMsg = "";
            foreach (ACMS.VO.ActivityTeamMemberVO vo in Page_ActivityTeamMemberVOList)
            {
                if (limDAO.GroupLimitIsExist(ActivityID.ToString(), vo.emp_id) == false)
                {
                    errMsg += vo.emp_id + ",";
                }
            }
            if (errMsg != "")
            {

                clsMyObj.ShowMessage("以下人員不在可報名名單中,所以無法報名:"+errMsg.TrimEnd (','));
                return;
            }
            if (MyFormMode == FormViewMode.Insert)
            {


                if (dao.RegistableCount(ActivityID) < 0)
                {

                    clsMyObj.ShowMessage("已額滿,無法報名");
                    return;

                }

                //MyResult = MySingleton.GetMySingleton().AlterRegist_Team(myActivityRegistVO, myCustomFieldValueVOList, Page_ActivityTeamMemberVOList, MySingleton.AlterRegistType.RegistInsert, new Guid(), "", "", "", ((Button)sender).Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx",path);
                string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');

                MyResult = MySingleton.GetMySingleton().AlterRegist_Team(myActivityRegistVO, myCustomFieldValueVOList, Page_ActivityTeamMemberVOList, MySingleton.AlterRegistType.RegistInsert, new Guid(), "", "", "", aa + "/Default.aspx", path, "", aa + "/Default.aspx");

            }
            else
            {
                string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');

                //   MyResult = MySingleton.GetMySingleton().AlterRegist_Team(myActivityRegistVO, myCustomFieldValueVOList, Page_ActivityTeamMemberVOList, MySingleton.AlterRegistType.RegistUpdate, new Guid(), "", "", "", ((Button)sender).Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx",path);
                MyResult = MySingleton.GetMySingleton().AlterRegist_Team(myActivityRegistVO, myCustomFieldValueVOList, Page_ActivityTeamMemberVOList, MySingleton.AlterRegistType.RegistUpdate, new Guid(), "", "", "", aa + "/Default.aspx", path, "", aa + "/Default.aspx");

            }


            if (MyResult == MySingleton.AlterRegistResult.RegistFail_Already)
            {
                ShowMessageForAjax(this, "已存在報名成功紀錄，無法重複報名!");
                return;
            }
            else if (MyResult == MySingleton.AlterRegistResult.RegistFail_Full)
            {
                ShowMessageForAjax(this, @"抱歉，報名已額滿!若錄取名額有增加則可再次報名。");
                return;
            }
            else if (MyResult == MySingleton.AlterRegistResult.RegistFail)
            {
                ShowMessageForAjax(this, @"資料存檔發生錯誤，無法完成報名。");
                return;
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            WriteErrorLog("SaveData", ex.Message, "0");
        }
        Response.Redirect("RegistedActivityQuery.aspx?type=2");
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
            // InitQueryBlock(ActivityID.ToString());
            //RadioButton1_CheckedChanged(RadioButton1, null);

        }


    }




    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 1)
        {
            try
            {
                ((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text = ((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text.Replace("-", "/").Replace("T", " ");
                ((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text = ((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text.Replace("-", "/").Replace("T", " ");

                //if (((Label)FormView_ActivatyDetails.FindControl("limit_countLabel")).Text == "999999")
                //{
                //    ((Label)FormView_ActivatyDetails.FindControl("limit_countLabel")).Text = "無上限";
                //}
                //if (((Label)FormView_ActivatyDetails.FindControl("limit2_countLabel")).Text == "0")
                //{
                //    ((Label)FormView_ActivatyDetails.FindControl("limit2_countLabel")).Text = "無";
                //}

            }
            catch
            {
            }

            GridView gvUpfiles = ((GridView)FormView_ActivatyDetails.FindControl("GridView_UpFiles"));
            if (gvUpfiles != null)
            {
                foreach (GridViewRow gr in gvUpfiles.Rows)
                {

                    (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(gr.FindControl("lbtnFileDownload"));
                }

            }
        }
    }
    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 1)
        {
            if (((Boolean)Session["ShowPanel"]) == false)
            {
                Wizard1.MoveTo(Wizard1.WizardSteps[3]);
            }
        }
    }
    protected void GridView_TemMember_DataBound(object sender, EventArgs e)
    {

    }

    protected void chkDelHead_CheckedChanged(object sender, EventArgs e)
    {
        bool chk = ((CheckBox)sender).Checked;
        foreach (GridViewRow gr in GridView_TemMember.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkDel"))).Visible)
            {
                ((CheckBox)(gr.FindControl("chkDel"))).Checked = chk;
            }
        }

    }
    protected void lbtnVOdeleteHeader_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow gr in GridView_TemMember.Rows)
        {
            try
            {
                if (((CheckBox)gr.FindControl("chkDel")).Checked)
                {
                    string emp_id = GridView_TemMember.DataKeys[gr.RowIndex].Value.ToString();

                    ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO = Page_ActivityTeamMemberVOList.Find(delegate(ACMS.VO.ActivityTeamMemberVO p) { return p.emp_id == emp_id; });
                    if (myActivityTeamMemberVO.emp_id != myActivityTeamMemberVO.boss_id)
                    {
                        Page_ActivityTeamMemberVOList.Remove(myActivityTeamMemberVO);
                    }

                }
            }
            catch (Exception ex)
            {
                WriteErrorLog("HeaderVoDelete", ex.Message, "0");

            }
        }
        GridView_TemMember.DataSource = Page_ActivityTeamMemberVOList;
        GridView_TemMember.DataBind();
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

                Session["ShowPanel"] = true;
                foreach (ACMS.VO.CustomFieldVO myCustomFieldVO in myCustomFieldVOList)
                {
                    MyTableRow = new TableRow();
                    System.Web.UI.WebControls.TableCell MyTableCell_Title = new TableCell();
                    System.Web.UI.WebControls.TableCell MyTableCell_Control = new TableCell();
                    MyTableCell_Title.Width = System.Web.UI.WebControls.Unit.Pixel(200);// "40%";
                    MyTableCell_Control.Width = System.Web.UI.WebControls.Unit.Pixel(200);// "40%";

                    //Title
                    Literal lblTitle = new Literal();
                    lblTitle.ID = string.Format("lbl{0}", myCustomFieldVO.field_id);
                    lblTitle.Text = myCustomFieldVO.field_name;
                    MyTableCell_Title.HorizontalAlign = HorizontalAlign.Right ;
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
                                (MyControl as ListControl).Items.Add(new ListItem(string.Format("{0}:{1}", myCustomFieldItemVO.field_item_name, myCustomFieldItemVO.field_item_text+"元"), myCustomFieldItemVO.field_item_id.ToString()));
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
                    MyTableCell_Title.BorderWidth = Unit.Parse("1px");
                    MyTableRow.Cells.Add(MyTableCell_Title);
                    MyTableCell_Control.BorderWidth = Unit.Parse("1px");
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


    protected void Home_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.ResolveUrl("~/Default.aspx"));
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
        get { if (ViewState["ActivityID"] == null) { return new Guid(); } else return new Guid(ViewState["ActivityID"].ToString()); }
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



