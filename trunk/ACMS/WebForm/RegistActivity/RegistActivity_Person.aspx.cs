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

public partial class WebForm_RegistActivity_RegistActivity_Person : BasePage
{

   
    HiddenField MyHiddenField = new HiddenField();

    Hashtable MyHashtable = new Hashtable();

    protected void Page_Init(object sender, EventArgs e)
    {
        MyHiddenField.ID = "__MyHiddenField";
        this.Form.Controls.Add(MyHiddenField);
        
        //InitQueryBlock(Request.Form[MyHiddenField.UniqueID]);
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Wizard1.ActiveStepIndex >= 1)
            {
                InitQueryBlock(ActivityID.ToString());
            }
            if (!IsPostBack)
            {
                Session.Remove("form_mode1");
                Session["ShowPanel"] = false;
                Session.Remove("Team");
                ((MyMasterPage)(this.Master)).PanelMainGroupingText = "個人報名";
                Wizard1.Visible = false;

                if (Session["form_mode"] != null && Session["activity_id"] != null)
                {
                    //以新增方式進來時
                    if (Session["form_mode"].ToString() == "regist")
                    {
                        RegistGoSecondEventArgs myRegistGoSecondEventArgs = new RegistGoSecondEventArgs(new Guid(Session["activity_id"].ToString()));

                        GoSecondStep_Click(null, myRegistGoSecondEventArgs);
                    }
                    //以預覽方式進來時
                    if (Session["form_mode"].ToString() == "preview")
                    {
                        hiMode1.Value = "preview";
                        Session["form_mode1"] = "preview";
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
                //Session["activity_id"] = null;

            }
        }
        catch (Exception ex)
        {
            WriteErrorLog("PageLoad", ex.Message, "0");

        }
    }

    //新增報名
    protected void GoSecondStep_Click(object sender, RegistGoSecondEventArgs e)
    {
        try
        {
            Wizard1.MoveTo(Wizard1.WizardSteps[0]);

            RegistActivity_Query1.Visible = false;
            Wizard1.Visible = true;

            //必要屬性
            MyFormMode = FormViewMode.Insert;


            ActivityID = e.activity_id;

            EmpID = clsAuth.ID;//預設是登入者
            RegistBy = clsAuth.ID;//執行是登入者

            PanelRegisterInfoA.Visible = true;
            PanelRegisterInfoB.Visible = false;



            MyHiddenField.Value = ActivityID.ToString();

            //載入活動資訊
            GetActivityDefault();

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
            Wizard1.MoveTo(Wizard1.WizardSteps[1]);

            RegistActivity_Query1.Visible = false;
            Wizard1.Visible = true;

            //必要屬性
            MyFormMode = FormViewMode.Insert;
            ActivityID = new Guid(Session["activity_id"].ToString());

            EmpID = clsAuth.ID;//預設是登入者 為了讓FormView顯示
            RegistBy = clsAuth.ID;//執行是登入者

            MyFormMode = FormViewMode.Edit;

            PanelRegisterInfoA.Visible = false;
            PanelRegisterInfoB.Visible = true;



            MyHiddenField.Value = ActivityID.ToString();

            //載入活動資訊
            GetActivityDefault();



            ((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text = ((Label)FormView_ActivatyDetails.FindControl("activity_startdateLabel")).Text.Replace("-", "/").Replace("T", " ");
            ((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text = ((Label)FormView_ActivatyDetails.FindControl("activity_enddateLabel")).Text.Replace("-", "/").Replace("T", " ");


        }
        catch (Exception ex)
        {
           // WriteErrorLog("ThirdStep", ex.Message, "0");

        }
    }

    private void GetActivityDefault()
    {
        try
        {
            InitQueryBlock(ActivityID.ToString());
            //取得活動資訊
            ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
            ACMS.VO.ActivatyVO myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

            //報名截止日後要唯讀
            if (myActivatyVO.regist_deadline < DateTime.Today)
            {
                MyFormMode = FormViewMode.ReadOnly;
                GridView_RegisterPeoplinfo.Enabled = false;
                PanelCustomFieldA1.Enabled = false;
            }
            
            //活動海報訊息
            Literal1.Text = myActivatyVO.activity_info;

            //活動相關訊息
            ObjectDataSource_ActivatyDetails.SelectParameters["id"].DefaultValue = ActivityID.ToString();
            ObjectDataSource_UpFiles.SelectParameters["dirName"].DefaultValue = Server.MapPath(Path.Combine("~/UpFiles", ActivityID.ToString()));

            //報名者資訊
            ObjectDataSource_RegisterPersonInfo.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;//預設登入者

            //所有報名者資訊
            ObjectDataSource_RegisterPeoplenfo.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
            ObjectDataSource_RegisterPeoplenfo.SelectParameters["emp_id"].DefaultValue = RegistBy;//由登入者所報名(含登入者本人)   

            //注意事項
            Literal_notice.Text = myActivatyVO.notice.Replace("\r\n", "<br />");

            FormView_fixA.DataBind();
            ACMS.BO.CustomFieldBO myCustFieldBo = new ACMS.BO.CustomFieldBO();
            if (myCustFieldBo.SelectByActivity_id(ActivityID).Count > 0)
            {
                Session["ShowPanel"] = true;

            }
        }
        catch (Exception ex)
        {
            WriteErrorLog("GetDefault", ex.Message, "0");

        }
        //FormView_fixA.FindControl("tr_person_fix1").Visible = (myActivatyVO.is_showperson_fix1 == "Y");
        //FormView_fixA.FindControl("tr_person_fix2").Visible = (myActivatyVO.is_showperson_fix2 == "Y");

        //(FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2Start") as Label).Text = myActivatyVO.personextcount_min.ToString();
        //(FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2End") as Label).Text = myActivatyVO.personextcount_max.ToString();

        //RangeValidator myRangeValidator = (FormView_fixA.FindControl("tr_person_fix2").FindControl("chk_txtperson_fix2_3") as RangeValidator);
        //myRangeValidator.MinimumValue = myActivatyVO.personextcount_min.ToString();
        //myRangeValidator.MaximumValue = myActivatyVO.personextcount_max.ToString();
    }



    //顯示活動資訊
    protected void FormView_ActivatyDetails_DataBound(object sender, EventArgs e)
    {
        ////隱藏每隊人數限制
        //(FormView_ActivatyDetails.FindControl("trteam_member_max")).Visible = false;

        //檔案下載是否出現
        DataRowView drv = (DataRowView)FormView_ActivatyDetails.DataItem;

        try
        { 
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
        catch (Exception ex)
        {
          //  WriteErrorLog("ActivityDetailDataBind", ex.Message, "0");

        }
    }

    //下載檔案
    protected void lbtnFileDownload_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GridView_UpFiles = (GridView)FormView_ActivatyDetails.FindControl("GridView_UpFiles");
            FileInfo myFileInfo = new FileInfo(GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());

            //ScriptManager.RegisterClientScriptBlock(HttpContext.Current.Handler as Page, typeof(string), string.Format("alert_{0}", DateTime.Now.ToString("hhmmss")), js, true);
            string fileName = GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
            fileName = this.ResolveUrl("~/Upfiles/" + fileName.Substring(fileName.IndexOf(ActivityID.ToString())));


            if (myFileInfo.Exists)
            {
                //    Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", Server.UrlEncode((myFileInfo.Name))));
                //    // 輸出檔案。
                //    Response.WriteFile(myFileInfo.FullName);
                // Response.Write("<script type=\"text/javascript\"> window.open('" + fileName + "')</script>");
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), string.Format("alert_{0}", DateTime.Now.ToString("hhmmss")), " window.open('" + fileName + "')", true);
            }
        }
        catch (Exception ex)
        {
            WriteErrorLog("DownLoadFile", ex.Message, "0");

        }
       
    }

    //開啟選擇報名者視窗
    protected void btnAgent_Click(object sender, EventArgs e)
    {
        try
        {
            OpenAgentSelector1.TitleName = "代理報名";
            //OpenAgentSelector1.OkName = "報名";
            OpenAgentSelector1.InitDataAndShow(ActivityID.ToString());
        }
        catch (Exception ex)
        {
            WriteErrorLog("Agent", ex.Message, "0");

        }
    }

    //選取人員之後指定EmpID
    protected void GetSmallEmployees_Click(object sender, GetEmployeeEventArgs e)
    {
        try
        {
            EmpID = e.id;
            ObjectDataSource_RegisterPersonInfo.SelectParameters["emp_id"].DefaultValue = e.id;
            FormView_RegisterPersonInfo.DataBind();

            //個人固定欄位
            ObjectDataSource_fixA.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
            ObjectDataSource_fixA.SelectParameters["emp_id"].DefaultValue = EmpID;
        }
        catch (Exception ex)
        {
            WriteErrorLog("GetSmallEmployees", ex.Message, "0");

        }

    }

    //人員切換之後指定EmpID
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton RadioButton1 = sender as RadioButton;
        RadioButton1.Checked = true;

        GridView_RegisterPeoplinfo.SelectedIndex = (RadioButton1.NamingContainer as GridViewRow).RowIndex;

        EmpID = GridView_RegisterPeoplinfo.DataKeys[GridView_RegisterPeoplinfo.SelectedIndex].Value.ToString();

        //載入個人資訊
        //個人固定欄位
        ObjectDataSource_fixA.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();
        ObjectDataSource_fixA.SelectParameters["emp_id"].DefaultValue = EmpID;

        FormView_fixA.DataBind();

        //載入動態欄位資料

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

    //[報名人事資料」區塊
    protected void GridView_RegisterPeoplinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            RadioButton RadioButton1 = (RadioButton)e.Row.FindControl("RadioButton1");
            //给每个RadioButton1绑定setRadio事件
            try
            {
                RadioButton1.Attributes.Add("onclick", "setRadio(this)");
            }
            catch (Exception)
            { }

        }




    }

    //選擇費用項目時要加總金額
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intSum = 0;

        foreach (ListItem theListItem in (sender as CheckBoxList).Items)
        {
            if (theListItem.Selected == true)
            {
                intSum = intSum + Convert.ToInt32(theListItem.Text.Replace ("元","").Split(':')[1]);
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

            if (myActivityRegistDAO.IsPersonRegisted(ActivityID, EmpID, "", "1") > 0)
            {
                clsMyObj.ShowMessage(@"已存在此員工的報名成功紀錄!請選擇其他員工執行代理報名。");
                Wizard1.MoveTo(Wizard1.WizardSteps[0]);
            }
        }
        else if (Wizard1.ActiveStepIndex == 1 && MyFormMode != FormViewMode.Insert)
        {

            if (GridView_RegisterPeoplinfo.SelectedIndex == -1)
            {
                clsMyObj.ShowMessage(@"請選擇要編輯的人員。");
                Wizard1.MoveTo(Wizard1.WizardSteps[0]);
            }
        }
        
        if (Wizard1.ActiveStepIndex == 2)
        {
            RadioButtonList rblidno_type = (RadioButtonList)FormView_fixA.FindControl("tr_person_fix1").FindControl("rblidno_type");
            TextBox txtperson_fix1 = (TextBox)FormView_fixA.FindControl("tr_person_fix1").FindControl("txtperson_fix1");

            if (rblidno_type.SelectedIndex == 0 && rblidno_type.Visible )
            {
                if (clsMyObj.IDChk(txtperson_fix1.Text) != "0")
                {
                    clsMyObj.ShowMessage("身分證字號格式不正確!");
                    Wizard1.MoveTo(Wizard1.WizardSteps[1]);
                }
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
        myActivityRegistVO.idno_type = (FormView_fixA.FindControl("tr_person_fix1").FindControl("rblidno_type") as RadioButtonList).SelectedIndex;
        myActivityRegistVO.idno = (FormView_fixA.FindControl("tr_person_fix1").FindControl("txtperson_fix1") as TextBox).Text;
       // myActivityRegistVO.idno_ext = (FormView_fixA.FindControl("tr_person_fix1").FindControl("txtidno_ext") as TextBox).Text;

        myActivityRegistVO.team_name = "";
        try
        {
            myActivityRegistVO.ext_people = Convert.ToInt32((FormView_fixA.FindControl("tr_person_fix1").FindControl("txtperson_fix2") as TextBox).Text);
        }
        catch
        {
            myActivityRegistVO.ext_people = 0;
        }


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
        ACMS.DAO.ActivityGroupLimitDAO  limDAO=new ACMS.DAO.ActivityGroupLimitDAO ();
        
        //預覽時
        if (Session["form_mode1"] != null)
        {
            if (Session["form_mode1"].ToString() == "preview")
            {
                Session.Remove("form_mode1");
                Response.Redirect("~/WebForm/ManageActivity/ActivityEditQuery.aspx");
            }
        }



        if (MyFormMode == FormViewMode.ReadOnly)
        {
            Response.Redirect("RegistedActivityQuery.aspx?type=1");
        }
        try
        {
            //以新增方式進來時
            ACMS.VO.ActivityRegistVO myActivityRegistVO = GetActivityRegistVO(); //取得報名資訊      
            List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = GetCustomFieldValueVOList();//取得自訂欄位值
            //ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            string path = Server.MapPath("~/UpFiles");
            //報名
            MySingleton.AlterRegistResult MyResult;
            if (limDAO.GroupLimitIsExist (ActivityID.ToString () ,myActivityRegistVO.emp_id )==false  )
            {

                clsMyObj.ShowMessage(myActivityRegistVO.emp_id+"不在可報名的名單中!");
                return;
            
            }
            if (MyFormMode == FormViewMode.Insert)
            {

                //if (dao.IsPersonRegisted (ActivityID ,myActivityRegistVO.emp_id ,"","1")>0)
                //{
                //    clsMyObj.ShowMessage ("已報名,無法重覆報名");
                //    return ;
                //}
                //MyResult = MySingleton.GetMySingleton().AlterRegist(myActivityRegistVO, myCustomFieldValueVOList, MySingleton.AlterRegistType.RegistInsert, new Guid(), "", "", "", this.Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx",path);
                string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');
                MyResult = MySingleton.GetMySingleton().AlterRegist(myActivityRegistVO, myCustomFieldValueVOList, MySingleton.AlterRegistType.RegistInsert, new Guid(), "", "", "", aa + "/Default.aspx", path, "", aa + "/Default.aspx");

            }
            else
            {
                // MyResult = MySingleton.GetMySingleton().AlterRegist(myActivityRegistVO, myCustomFieldValueVOList, MySingleton.AlterRegistType.RegistUpdate, new Guid(), "", "", "", this.Page.Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 7)) + "/ACMS/WebForm/RegistActivity/RegistedActivityQuery.aspx",path);
                string aa = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath).TrimEnd('/');
                MyResult = MySingleton.GetMySingleton().AlterRegist(myActivityRegistVO, myCustomFieldValueVOList, MySingleton.AlterRegistType.RegistUpdate, new Guid(), "", "", "", aa + "/Default.aspx", path, "", aa + "/Default.aspx");

            }


            if (MyResult == MySingleton.AlterRegistResult.RegistFail_Already)
            {
                clsMyObj.ShowMessage("已存在報名成功紀錄，無法重複報名!");
                return ;
            }
            else if (MyResult == MySingleton.AlterRegistResult.RegistFail_Full)
            {
                clsMyObj.ShowMessage(@"抱歉，報名已額滿!若錄取名額有增加則可再次報名。");
                return ;
            }
            else if (MyResult == MySingleton.AlterRegistResult.RegistFail)
            {
                clsMyObj.ShowMessage(@"資料存檔發生錯誤，無法完成報名。");
                return ;
            }
            else
            {


            }


        }
        catch (Exception ex)
        {
            WriteErrorLog("SaveData", ex.Message, "0");
            return;
        }
        Response.Redirect("RegistedActivityQuery.aspx?type=1");
    }

    protected void FormView_fixA_DataBound(object sender, EventArgs e)
    {
        ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
        ACMS.VO.ActivatyVO myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

        FormView_fixA.FindControl("tr_person_fix1").Visible = (myActivatyVO.is_showperson_fix1 == "Y");
        FormView_fixA.FindControl("lblID").Visible = (myActivatyVO.is_showperson_fix1 == "Y");
       
        FormView_fixA.FindControl("tr_person_fix2").Visible = (myActivatyVO.is_showperson_fix2 == "Y");
        FormView_fixA.FindControl("tr_person_fix21").Visible = (myActivatyVO.is_showperson_fix2 == "Y");

        (FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2Start") as Label).Text = myActivatyVO.personextcount_min.ToString();
        (FormView_fixA.FindControl("tr_person_fix2").FindControl("lblAf2End") as Label).Text = myActivatyVO.personextcount_max.ToString();

        RangeValidator myRangeValidator = (FormView_fixA.FindControl("tr_person_fix2").FindControl("chk_txtperson_fix2_3") as RangeValidator);
        myRangeValidator.MinimumValue = myActivatyVO.personextcount_min.ToString();
        myRangeValidator.MaximumValue = myActivatyVO.personextcount_max.ToString();

        if (myActivatyVO.is_showperson_fix1 == "Y" || myActivatyVO.is_showperson_fix2 == "Y")
        {
            Session["ShowPanel"] = true;
        }
    }


    //protected void GridView_RegisterPeoplinfo_DataBound(object sender, EventArgs e)
    //{
    //    if()
    //}

    protected void GridView_RegisterPeoplinfo_DataBound(object sender, EventArgs e)
    {
        if (GridView_RegisterPeoplinfo.Rows.Count > 0)
        {
            //系統預設會勾選第一筆資料
            RadioButton RadioButton1 = (RadioButton)GridView_RegisterPeoplinfo.Rows[0].FindControl("RadioButton1");
            //InitQueryBlock(ActivityID.ToString());
            RadioButton1_CheckedChanged(RadioButton1, null);

        }
    }
    protected void rblidno_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rblidno_type = (RadioButtonList)FormView_fixA.FindControl("tr_person_fix1").FindControl("rblidno_type");
        RequiredFieldValidator chk_txtperson_fix1 = (RequiredFieldValidator)FormView_fixA.FindControl("tr_person_fix1").FindControl("chk_txtperson_fix1");

        if (rblidno_type.SelectedIndex == 0)
        {
            chk_txtperson_fix1.ErrorMessage = "身分證字號必填";
        }
        else
        {
            chk_txtperson_fix1.ErrorMessage = "護照號碼必填";
        }

        //(FindControl("UpdatePanel_CustomField") as UpdatePanel).Update();

    }

    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 0)
        {
            if (Session["Agent"] != null)
            {
                OpenAgentSelector1.TitleName = "代理報名";
                //OpenAgentSelector1.OkName = "報名";
                OpenAgentSelector1.InitDataAndShow(ActivityID.ToString());
                btnAgent.Visible = true;
            }
        }
        if (Wizard1.ActiveStepIndex == 1)
        {
            if (((Boolean)Session["ShowPanel"]) == false)
            {
                Wizard1.MoveTo(Wizard1.WizardSteps[3]);
            }
        }
       
    }
    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 1)
        {
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
}

//自訂欄位設定
public partial class WebForm_RegistActivity_RegistActivity_Person
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
                Session["ShowPanel"] = true;
                System.Web.UI.WebControls.Table MyTable = new System.Web.UI.WebControls.Table();
                System.Web.UI.WebControls.TableRow MyTableRow;

                foreach (ACMS.VO.CustomFieldVO myCustomFieldVO in myCustomFieldVOList)
                {
                   
                    //=======================================================================
                    System.Web.UI.WebControls.TableCell MyTableCell_Title = new TableCell();
                    System.Web.UI.WebControls.TableCell MyTableCell_Control = new TableCell();
                 
                    MyTableRow = new TableRow();
                    MyTableRow.Font.Size = 9;
                    MyTableCell_Title.HorizontalAlign = HorizontalAlign.Right;
                    MyTableCell_Title.Width = System.Web.UI.WebControls.Unit.Pixel(200);// "40%";
                    MyTableCell_Control.Width = System.Web.UI.WebControls.Unit.Pixel(200);// "40%";

                    //Title
                    Literal lblTitle = new Literal();
                    lblTitle.ID = string.Format("lbl{0}", myCustomFieldVO.field_id);
                    lblTitle.Text = myCustomFieldVO.field_name ;
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

    //public string idno_ext
    //{
    //    get { return ViewState["idno_ext"].ToString(); }
    //    set { ViewState["idno_ext"] = value; }
    //}


}



