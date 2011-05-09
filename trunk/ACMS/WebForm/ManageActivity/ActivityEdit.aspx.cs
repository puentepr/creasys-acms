using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ACMS;
using NPOI.HSSF.UserModel;
using System.Transactions;
using System.Data.SqlClient;
using TServerControl;
using System.Configuration;

public partial class WebForm_ManageActivity_ActivityEdit : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //FormView1.EditItemTemplate = FormView1.ItemTemplate;
        //FormView2.EditItemTemplate = FormView2.ItemTemplate;
    }
    protected void rblgrouplimit_Change(object sender, EventArgs e)
    {

        if (rblgrouplimit.SelectedValue == "Y")
        {
            FileUpload_GroupLimit.Enabled = true;
            HyperLink1.Enabled = true;
            btnAdd_GroupLimit.Enabled = true;
            btnExport_GroupLimit.Enabled = true;
            btnUpload_GroupLimit.Enabled = true;
            Panel_GroupLimit.Visible = true;
        }
        else
        {
            btnUpload_GroupLimit.Enabled = false ;
            HyperLink1.Enabled = false;
            btnAdd_GroupLimit.Enabled = false;
            btnExport_GroupLimit.Enabled = false;
            FileUpload_GroupLimit.Enabled = false;
            Panel_GroupLimit.Visible = false;
          
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //族群限定的上傳與匯出
        (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(btnUpload_GroupLimit);
        (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(btnExport_GroupLimit);

        if (!IsPostBack)
        {
            Wizard1.ActiveStepIndex = 0;
            //取得必要的Session
            if (Session["form_mode"] == null)
            {
                Response.Redirect("ActivityEditQuery.aspx");
            }

            //編輯時須帶入activity_id
            if (Session["form_mode"].ToString() != "new" && Session["activity_id"] == null)
            {
                Response.Redirect("ActivityEditQuery.aspx");
            }

            //取得FormView外的欄位初始值
            ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
            ACMS.VO.ActivatyVO myActivatyVO = new ACMS.VO.ActivatyVO();
         
            if (Session["form_mode"].ToString() == "new")
            {
                //新增模式
                MyFormMode = FormViewMode.Insert;
                ActivityType= Session["activity_type"].ToString();

                //新增一筆活動              
                myActivatyVO.id = ActivityID;
                myActivatyVO.emp_id = clsAuth.WORK_ID + clsAuth.NATIVE_NAME;
                myActivatyVO.activity_type = ActivityType;
                myActivatyDAO.INSERT_NewOne(myActivatyVO);

                myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

            }
            else
            {
                ActivityID= new Guid(Session["activity_id"].ToString());
                myActivatyVO = myActivatyDAO.SelectActivatyByID(ActivityID);

                ActivityType = myActivatyVO.activity_type;

                if (Session["form_mode"].ToString() == "edit")
                {
                    //編輯模式
                    MyFormMode = FormViewMode.Edit;
   
                }

                if (Session["form_mode"].ToString() == "readonly" ||  myActivatyVO.activity_enddate< DateTime.Now)
                {
                    //唯讀模式:活動已結束也要是唯讀
                    MyFormMode = FormViewMode.ReadOnly;

                    //FCKeditor1.Visible = false;
                    FCKeditor1.Visible = false;
                    liactivity_info.Visible = true;
                    
                    rblgrouplimit.Enabled = false;
                    Panel_GroupLimit.Enabled = false;
                    FormView2.Enabled = false;
                    FormView1.Enabled = false;
                    txtnotice.Enabled = false;
                    PanelCustomFieldC.Enabled = false;
                }
                

            }
            if (Session["History"] != null)
            {
                FCKeditor1.Visible = false;
                liactivity_info.Visible = true;
            }
            
            Session["form_mode"] = null;
            Session["activity_type"] = null;
            Session["activity_id"] = null;

            

            //取得FormView外的欄位初始值
            FCKeditor1.Value = myActivatyVO.activity_info;
            liactivity_info.Text = myActivatyVO.activity_info;
            
            rblgrouplimit.SelectedValue = myActivatyVO.is_grouplimit;
            //if (rblgrouplimit.SelectedValue == "Y")//檢查是否可以運作
            //{
            //    FileUpload_GroupLimit.Enabled = true;
            //    btnUpload_GroupLimit.Enabled = true;
            //    HyperLink1.Enabled = true;
            //    btnAdd_GroupLimit.Enabled = true;
            //    btnExport_GroupLimit.Enabled = true;
            //}
            //else
            //{
            //    FileUpload_GroupLimit.Enabled = false;
            //    btnUpload_GroupLimit.Enabled = false;
            //    HyperLink1.Enabled = false;
            //    btnAdd_GroupLimit.Enabled = false;
            //    btnExport_GroupLimit.Enabled = false;
            //}

            if (rblgrouplimit.SelectedValue == "Y")
            {


                FileUpload_GroupLimit.Enabled = true;
                HyperLink1.Enabled = true;
                btnAdd_GroupLimit.Enabled = true;
                btnExport_GroupLimit.Enabled = true;
                btnUpload_GroupLimit.Enabled = true;
                Panel_GroupLimit.Visible = true;
            }

          
           
            //如果已經過了活動開始報名日，則某些功能需唯讀
            if (myActivatyVO.regist_startdate <= DateTime.Now)
            {
                FormView1.Enabled = false;
                FormView2.Enabled = false;
                PanelCustomFieldC.Enabled = false;
                rblgrouplimit.Enabled = false;
                Panel_GroupLimit.Enabled = false;
            }
            else
            {
                if (rblgrouplimit.SelectedValue == "Y")//檢查是否可以運作
                {
                    FileUpload_GroupLimit.Enabled = true;
                    HyperLink1.Enabled = true;
                    btnAdd_GroupLimit.Enabled = true;
                    btnExport_GroupLimit.Enabled = true;
                    btnUpload_GroupLimit.Enabled = true;
                    Panel_GroupLimit.Visible = true;
                }
                else
                {
                    FileUpload_GroupLimit.Enabled = false;
                    HyperLink1.Enabled = false;
                    btnAdd_GroupLimit.Enabled = false;
                    btnExport_GroupLimit.Enabled = false;
                    btnUpload_GroupLimit.Enabled = false;
                    Panel_GroupLimit.Visible = false;
                }

            }

            txtnotice.Text = myActivatyVO.notice;

            //活動資訊-活動內容
            ObjectDataSource_Activaty.SelectParameters["id"].DefaultValue = ActivityID.ToString();
            //活動資訊-上傳檔
            ObjectDataSource_UpFiles.SelectParameters["dirName"].DefaultValue = Server.MapPath(Path.Combine("~/UpFiles", ActivityID.ToString()));
           
            //個人固定欄位
            ObjectDataSource_Activaty2.SelectParameters["id"].DefaultValue = ActivityID.ToString();

            //自訂欄位
            ObjectDataSource_CustomField.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();

            //族群限定
            ObjectDataSource_GroupLimit.SelectParameters["activity_id"].DefaultValue = ActivityID.ToString();

            if (ActivityType == "1")
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "新增個人活動";
                (FormView1.FindControl("lbllimit_count") as Literal).Visible = true;
                (FormView1.FindControl("lbllimit2_count") as Literal).Visible = true;
                //(FormView1.FindControl("chk_txtlimit_count") as RequiredFieldValidator).ErrorMessage = "活動人數上限必填";
               // (FormView1.FindControl("chk_txtlimit_count2") as CompareValidator).ErrorMessage = "活動人數上限必填數字";
                //(FormView1.FindControl("chk_txtlimit2_count") as RequiredFieldValidator).ErrorMessage = "活動備取人數必填";
               // (FormView1.FindControl("chk_txtlimit2_count2") as CompareValidator).ErrorMessage = "活動備取人數必填數字";
                (FormView1.FindControl("trteam_member_max") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = false;
                (FormView1.FindControl("trteam_member_min") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = false;

                (FormView2.FindControl("PanelCustomFieldA1") as Panel).Visible = true;
                (FormView2.FindControl("PanelCustomFieldB1") as Panel).Visible = false;
                (FormView2.FindControl("PanelCustomFieldB2") as Panel).Visible = false;

                //PanelCustomFieldC.GroupingText = "個人自訂欄位";
            }
            else
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "新增團隊活動";
                (FormView1.FindControl("lbllimit_count_team") as Literal).Visible = true;
                (FormView1.FindControl("lbllimit2_count_team") as Literal).Visible = true;
                //(FormView1.FindControl("chk_txtlimit_count") as RequiredFieldValidator).ErrorMessage = "活動隊數上限必填";
               // (FormView1.FindControl("chk_txtlimit_count2") as CompareValidator).ErrorMessage = "活動隊數上限必填數字";
                //(FormView1.FindControl("chk_txtlimit2_count") as RequiredFieldValidator).ErrorMessage = "活動備取隊數必填";
               // (FormView1.FindControl("chk_txtlimit2_count2") as CompareValidator).ErrorMessage = "活動備取隊數必填數字";

                (FormView1.FindControl("trteam_member_max") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = true;
                (FormView1.FindControl("trteam_member_min") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = true;

                (FormView2.FindControl("PanelCustomFieldA1") as Panel).Visible = false;
                (FormView2.FindControl("PanelCustomFieldB1") as Panel).Visible = true;
                (FormView2.FindControl("PanelCustomFieldB2") as Panel).Visible = true;

                //PanelCustomFieldC.GroupingText = "團隊自訂欄位";

            }

            //andy 從報名狀況查詢進來的即使已經開始報名也要新增限制人員群組//原來活動是要有限制族群的才需要打開限制人群的管制
            if (MyFormMode == FormViewMode.ReadOnly &&  (myActivatyVO.activity_enddate >DateTime.Now) && myActivatyVO .is_grouplimit =="Y")
            {



                rblgrouplimit.Enabled = true;
                Panel_GroupLimit.Enabled = true;
                GridView_GroupLimit.Columns[3].Visible = false;
                GridView_GroupLimit.DataBind();

            }
        }
    }
   
    //上傳檔與下載
    protected void FormView1_PreRender(object sender, EventArgs e)
    {
        (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(FormView1.FindControl("btnUpload"));
        (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(FormView1.FindControl("GridView_UpFiles"));
     
    }

    //上傳附件檔案
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //先檢查5M的總容量限制
        long ttlByte = 0;
       
        ACMS.BO.UpFileBO upBO = new ACMS.BO.UpFileBO();
        List <ACMS.VO.UpFileVO> upFiles = upBO .SELECT (Server.MapPath ("~/UPFiles/"+ActivityID .ToString()));
        StreamReader  sr ;
        foreach (ACMS.VO.UpFileVO upFile in upFiles)
        {
            sr = new StreamReader(upFile.path);
            ttlByte += sr.ReadToEnd ().Length;
            sr.Close();
        }



        FileUpload myFileUpload = (FileUpload)FormView1.FindControl("FileUpload1");
        if (myFileUpload.FileBytes.LongLength+ttlByte   >= 5100000)
        {
            WriteErrorLog("UploadFile", "已超過總容量5M的管制", "0");
            return;
        }

        try
        {

           

            if (myFileUpload.HasFile)
            {
                //    //andy 2011/1/6 日sugar說要拿掉檢查.
                //if (ConfigurationManager.AppSettings["ValidExtention"].ToLower().IndexOf(Path.GetExtension(myFileUpload.FileName).Replace(".","").ToLower()) ==-1)
                //{
                //    clsMyObj.ShowMessage(string.Format("副檔名只能是[{0}]!", ConfigurationManager.AppSettings["ValidExtention"]));
                //    return;
                //}


                try
                {
                    try
                    {
                        DirectoryInfo myDirectoryInfo = new DirectoryInfo(Server.MapPath(Path.Combine("~/UpFiles", ActivityID.ToString())));

                        if (!myDirectoryInfo.Exists)
                        {
                            myDirectoryInfo.Create();
                        }

                        FileStream myFileStream = new FileStream(Path.Combine(myDirectoryInfo.FullName, myFileUpload.FileName), FileMode.Create);
                        myFileStream.Write(myFileUpload.FileBytes, 0, myFileUpload.FileBytes.Length);
                        myFileStream.Close();
                    }
                    catch (Exception ex1)
                    {
                        clsMyObj.ShowMessage("目錄權限不足.無法寫入檔案!");
                        WriteErrorLog("UploadFile", ex1.Message, "0");

                    }
                }
                catch (Exception ex)
                {
                    clsMyObj.ShowMessage("檔案上傳時發生錯誤!:" + ex.Message);
                    WriteErrorLog("UploadFile", ex.Message, "0");


                }


                (FormView1.FindControl("Image1") as Image).CssClass = "pldisVisible";
                GridView GridView_UpFiles = (GridView)FormView1.FindControl("GridView_UpFiles");

                GridView_UpFiles.DataBind();
            }
        }
        catch (Exception ex2)
        {
            WriteErrorLog("UploadFile", ex2.Message, "0");
        }
    }

    //下載附件檔
    protected void lbtnFileDownload_Click(object sender, EventArgs e)
    {
        GridView GridView_UpFiles = (GridView)FormView1.FindControl("GridView_UpFiles");
        FileInfo myFileInfo = new FileInfo(GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());
        string fileName = GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        fileName=this.ResolveUrl ("~/Upfiles/"+fileName.Substring (fileName.IndexOf (ActivityID.ToString() )));

       
        if (myFileInfo.Exists)
        {
        //    Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", Server.UrlEncode((myFileInfo.Name))));
        //    // 輸出檔案。
        //    Response.WriteFile(myFileInfo.FullName);
            Response.Write ("<script type=\"text/javascript\"> window.open('"+fileName +"')</script>");
        }
    }

    //刪除附件檔
    protected void lbtnFileDelete_Click(object sender, EventArgs e)
    {
        GridView GridView_UpFiles = (GridView)FormView1.FindControl("GridView_UpFiles");
        FileInfo myFileInfo = new FileInfo(GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());
        try
        {
            if (myFileInfo.Exists)
            {
                myFileInfo.Delete();
            }

            GridView_UpFiles.DataBind();
        }
        catch (Exception ex)

        { 
        WriteErrorLog ("DeleteFile",ex.Message,"0");
        }
    }

    //勾選改變時要必填
    protected void chkis_showperson_fix2_CheckedChanged(object sender, EventArgs e)
    {
        //CheckBox chkis_showperson_fix2 = (CheckBox)sender;
        //chk_txtpersonextcount_min.Visible = chkis_showperson_fix2.Checked;
        //chk_txtpersonextcount_min2.Visible = chkis_showperson_fix2.Checked;

        //chk_txtpersonextcount_max.Visible = chkis_showperson_fix2.Checked;
        //chk_txtpersonextcount_max2.Visible = chkis_showperson_fix2.Checked;

        if ((FormView2.FindControl("chkis_showperson_fix2") as TCheckBoxYN).YesNo == "Y")
        {

            FormView2.FindControl("txtpersonextcount_max").Visible = true;
            FormView2.FindControl("txtpersonextcount_min").Visible = true;
        }
        else
        {
            FormView2.FindControl("txtpersonextcount_max").Visible = false ;
            FormView2.FindControl("txtpersonextcount_min").Visible = false;

           ((TextBox ) FormView2.FindControl("txtpersonextcount_max")).Text  = "0";
            ((TextBox ) FormView2.FindControl("txtpersonextcount_min")).Text = "0";
        }





    }
    //勾選改變時要必填
    protected void chkis_showremark_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkis_showremark = (CheckBox)sender;
        chk_txtremark_name.Visible = chkis_showremark.Checked;



    }
    //勾選改變時要必填
    protected void chkis_showteam_fix2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkis_showteam_fix2 = (CheckBox)sender;
        chk_txtteamextcount_min.Visible = chkis_showteam_fix2.Checked;
        chk_txtteamextcount_min2.Visible = chkis_showteam_fix2.Checked;

        chk_txtteamextcount_max.Visible = chkis_showteam_fix2.Checked;
        chk_txtteamextcount_max2.Visible = chkis_showteam_fix2.Checked;
    }
    //步驟存檔

    private void saveStep()
    {
        if (MyFormMode == FormViewMode.ReadOnly)
        {
            return;
        }

        ACMS.VO.ActivatyVO myActivatyVO = new ACMS.VO.ActivatyVO();

        myActivatyVO.id = ActivityID;
        myActivatyVO.activity_type = ActivityType;
        myActivatyVO.activity_info = FCKeditor1.Value;
        myActivatyVO.org_id = ((DropDownList)FormView1.FindControl("ddlorg_id")).SelectedValue;
        myActivatyVO.activity_name = ((TextBox)FormView1.FindControl("txtactivity_name")).Text;
        myActivatyVO.people_type = ((TextBox)FormView1.FindControl("txtpeople_type")).Text;
        myActivatyVO.activity_startdate = ((WebForm_DatetimePicker)FormView1.FindControl("txtactivity_startdate")).DateTimeValue;
        myActivatyVO.activity_enddate = ((WebForm_DatetimePicker)FormView1.FindControl("txtactivity_enddate")).DateTimeValue;
        if (((TextBox)FormView1.FindControl("txtlimit_count")).Text == "" || ((TextBox)FormView1.FindControl("txtlimit_count")).Text == "無上限")
        {
            myActivatyVO.limit_count =999999;
            myActivatyVO.limit2_count = 0;
        }
        if (((TextBox)FormView1.FindControl("txtlimit2_count")).Text == "" || ((TextBox)FormView1.FindControl("txtlimit2_count")).Text == "無")
        {
            myActivatyVO.limit2_count = 0;
        }
        try
        {
        myActivatyVO.limit_count = Convert.ToInt32(((TextBox)FormView1.FindControl("txtlimit_count")).Text);
        myActivatyVO.limit2_count = Convert.ToInt32(((TextBox)FormView1.FindControl("txtlimit2_count")).Text);
        }
        catch 
        {}

        if (((TextBox)FormView1.FindControl("txtteam_member_max")).Text == "")
        {
            ((TextBox)FormView1.FindControl("txtteam_member_max")).Text = "0";
        }
        if (((TextBox)FormView1.FindControl("txtteam_member_min")).Text == "")
        {
            ((TextBox)FormView1.FindControl("txtteam_member_min")).Text = "0";
        }

        if (ActivityType == "2")
        {
            myActivatyVO.team_member_max = Convert.ToInt32(((TextBox)FormView1.FindControl("txtteam_member_max")).Text);
            myActivatyVO.team_member_min = Convert.ToInt32(((TextBox)FormView1.FindControl("txtteam_member_min")).Text);
        }
        try
        {
            myActivatyVO.regist_startdate = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_startdate")).Text);    
            myActivatyVO.regist_deadline = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_deadline")).Text);
            myActivatyVO.cancelregist_deadline = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtcancelregist_deadline")).Text);
        }
        catch
        { }
  
        myActivatyVO.is_showfile = "Y";
        myActivatyVO.is_showprogress = ((CheckBox)FormView1.FindControl("chkis_showprogres")).Checked == true ? "Y" : "N";


        myActivatyVO.is_showperson_fix1 = ((CheckBox)FormView2.FindControl("chkis_showperson_fix1")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showperson_fix2 = ((CheckBox)FormView2.FindControl("chkis_showperson_fix2")).Checked == true ? "Y" : "N";

        if (((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text = "0";
        }
        if (((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text = "0";
        }
        if (ActivityType == "1")
        {

            myActivatyVO.personextcount_max = Convert.ToInt32(((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text);
            myActivatyVO.personextcount_min = Convert.ToInt32(((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text);
        }
        myActivatyVO.is_showidno = ((CheckBox)FormView2.FindControl("chkis_showidno")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showremark = ((CheckBox)FormView2.FindControl("chkis_showremark")).Checked == true ? "Y" : "N";
        myActivatyVO.remark_name = ((TextBox)FormView2.FindControl("txtremark_name")).Text;

        myActivatyVO.is_showteam_fix1 = ((CheckBox)FormView2.FindControl("chkis_showteam_fix1")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showteam_fix2 = ((CheckBox)FormView2.FindControl("chkis_showteam_fix2")).Checked == true ? "Y" : "N";

        if (((TextBox)FormView2.FindControl("txtteamextcount_max")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtteamextcount_max")).Text = "0";
        }

        if (((TextBox)FormView2.FindControl("txtteamextcount_min")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtteamextcount_min")).Text = "0";
        }



        if (ActivityType == "2" && chkis_showteam_fix2.Checked == true)
        {
            myActivatyVO.teamextcount_max = Convert.ToInt32(((TextBox)FormView2.FindControl("txtteamextcount_max")).Text);
            myActivatyVO.teamextcount_min = Convert.ToInt32(((TextBox)FormView2.FindControl("txtteamextcount_min")).Text);
        }

        myActivatyVO.is_grouplimit = rblgrouplimit.SelectedValue;
        myActivatyVO.notice = txtnotice.Text;
        myActivatyVO.active = "";

        try
        {
            ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
            myActivatyDAO.UpdateActivaty(myActivatyVO);


        }
        catch (Exception ex)
        {
            clsMyObj.ShowMessage("存檔失敗!");
            WriteErrorLog("SaveData", ex.Message, "0");
        }

    
    }

    //存檔
    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
       
        if (MyFormMode == FormViewMode.ReadOnly)
        {
            if (Session["History"] == null)
            {
                Response.Redirect("ActivityEditQuery.aspx");
            }
            else
            {
                Response.Redirect("HistoryActivityQuery.aspx?type=off");
            }
        }
        if (txtnotice.Text.Trim() == "")
        {
            clsMyObj.ShowMessage("注意事項不可空白");
            return;
        }

        ACMS.VO.ActivatyVO myActivatyVO = new ACMS.VO.ActivatyVO();

        myActivatyVO.id = ActivityID;
        myActivatyVO.activity_type = ActivityType;
        myActivatyVO.activity_info = FCKeditor1.Value ;
        myActivatyVO.org_id = ((DropDownList)FormView1.FindControl("ddlorg_id")).SelectedValue;
        myActivatyVO.activity_name = ((TextBox)FormView1.FindControl("txtactivity_name")).Text;
        myActivatyVO.people_type = ((TextBox)FormView1.FindControl("txtpeople_type")).Text;
        myActivatyVO.activity_startdate = ((WebForm_DatetimePicker)FormView1.FindControl("txtactivity_startdate")).DateTimeValue;
        myActivatyVO.activity_enddate = ((WebForm_DatetimePicker)FormView1.FindControl("txtactivity_enddate")).DateTimeValue;
        if (((TextBox)FormView1.FindControl("txtlimit_count")).Text == "" || ((TextBox)FormView1.FindControl("txtlimit_count")).Text == "無上限")
        {
            ((TextBox)FormView1.FindControl("txtlimit_count")).Text = "999999";
            ((TextBox)FormView1.FindControl("txtlimit2_count")).Text = "0";
        }
        if (((TextBox)FormView1.FindControl("txtlimit2_count")).Text == "" || ((TextBox)FormView1.FindControl("txtlimit2_count")).Text == "無")
        {
            ((TextBox)FormView1.FindControl("txtlimit2_count")).Text = "0";
        }
        myActivatyVO.limit_count = Convert.ToInt32(((TextBox)FormView1.FindControl("txtlimit_count")).Text);
        myActivatyVO.limit2_count = Convert.ToInt32(((TextBox)FormView1.FindControl("txtlimit2_count")).Text);

        if (((TextBox)FormView1.FindControl("txtteam_member_max")).Text == "")
        {
            ((TextBox)FormView1.FindControl("txtteam_member_max")).Text = "0";
        }
        if (((TextBox)FormView1.FindControl("txtteam_member_min")).Text == "")
        {
            ((TextBox)FormView1.FindControl("txtteam_member_min")).Text = "0";
        }

        if (ActivityType == "2")
        {
            myActivatyVO.team_member_max = Convert.ToInt32(((TextBox)FormView1.FindControl("txtteam_member_max")).Text);
            myActivatyVO.team_member_min = Convert.ToInt32(((TextBox)FormView1.FindControl("txtteam_member_min")).Text);
        }

        myActivatyVO.regist_startdate = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_startdate")).Text);
        myActivatyVO.regist_deadline = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_deadline")).Text);
        myActivatyVO.cancelregist_deadline = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtcancelregist_deadline")).Text);
        myActivatyVO.is_showfile = "Y";
        myActivatyVO.is_showprogress = ((CheckBox)FormView1.FindControl("chkis_showprogres")).Checked == true ? "Y" : "N";


        myActivatyVO.is_showperson_fix1 = ((CheckBox)FormView2.FindControl("chkis_showperson_fix1")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showperson_fix2 = ((CheckBox)FormView2.FindControl("chkis_showperson_fix2")).Checked == true ? "Y" : "N";

        if (((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text = "0";
        }
        if (((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text = "0";
        }
        if (ActivityType == "1")
        {

            myActivatyVO.personextcount_max = Convert.ToInt32(((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text);
            myActivatyVO.personextcount_min = Convert.ToInt32(((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text);
        }
        myActivatyVO.is_showidno = ((CheckBox)FormView2.FindControl("chkis_showidno")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showremark = ((CheckBox)FormView2.FindControl("chkis_showremark")).Checked == true ? "Y" : "N";
        myActivatyVO.remark_name = ((TextBox)FormView2.FindControl("txtremark_name")).Text;

        myActivatyVO.is_showteam_fix1 = ((CheckBox)FormView2.FindControl("chkis_showteam_fix1")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showteam_fix2 = ((CheckBox)FormView2.FindControl("chkis_showteam_fix2")).Checked == true ? "Y" : "N";

        if (((TextBox)FormView2.FindControl("txtteamextcount_max")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtteamextcount_max")).Text = "0";
        }

        if (((TextBox)FormView2.FindControl("txtteamextcount_min")).Text == "")
        {
            ((TextBox)FormView2.FindControl("txtteamextcount_min")).Text = "0";
        }
       


        if (ActivityType == "2" && chkis_showteam_fix2.Checked==true)
        {
            myActivatyVO.teamextcount_max = Convert.ToInt32(((TextBox)FormView2.FindControl("txtteamextcount_max")).Text);
            myActivatyVO.teamextcount_min = Convert.ToInt32(((TextBox)FormView2.FindControl("txtteamextcount_min")).Text);
        }

        myActivatyVO.is_grouplimit = rblgrouplimit.SelectedValue;
        myActivatyVO.notice = txtnotice.Text;
        myActivatyVO.active = "Y";

        try
        {
            ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
            myActivatyDAO.UpdateActivaty(myActivatyVO);


        }
        catch (Exception ex)
        {
            clsMyObj.ShowMessage("存檔失敗!");
            WriteErrorLog("SaveData", ex.Message, "0");
        }

        Response.Redirect("ActivityEditQuery.aspx");

    }

    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 3)
        {
 
           if ( rblgrouplimit.SelectedValue =="Y")
               if (GridView_GroupLimit.Rows.Count == 0)
               {
                   clsMyObj.ShowMessage("報名名單未建立");
                   e.Cancel = true;
               }

        }


        if (Wizard1.ActiveStepIndex == 0)
        {
            if (((TextBox)FormView1.FindControl("txtlimit_count")).Text == "999999")
            {
                ((TextBox)FormView1.FindControl("txtlimit_count")).Text = "無上限";
                ((TextBox)FormView1.FindControl("txtlimit2_count")).Text = "無";
            }
            if (((TextBox)FormView1.FindControl("txtregist_startdate")).Text != "")
            {
                ((TextBox)FormView1.FindControl("txtregist_startdate")).Text = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_startdate")).Text).ToString("yyyy/MM/dd");
            }
            if (((TextBox)FormView1.FindControl("txtregist_deadline")).Text != "")
            {
                ((TextBox)FormView1.FindControl("txtregist_deadline")).Text = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_deadline")).Text).ToString("yyyy/MM/dd");
            }
            if (((TextBox)FormView1.FindControl("txtcancelregist_deadline")).Text != "")
            {
                ((TextBox)FormView1.FindControl("txtcancelregist_deadline")).Text = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtcancelregist_deadline")).Text).ToString("yyyy/MM/dd");
            }

            
        }
        if (Wizard1.ActiveStepIndex == 1)
        {

            WebForm_DatetimePicker txtactivity_startdate = (FormView1.FindControl("txtactivity_startdate") as WebForm_DatetimePicker);
            WebForm_DatetimePicker txtactivity_enddate = (FormView1.FindControl("txtactivity_enddate") as WebForm_DatetimePicker);
            TextBox txtcancelregist_deadline = (FormView1.FindControl("txtcancelregist_deadline") as TextBox);
            TextBox txtregist_startdate = (FormView1.FindControl("txtregist_startdate") as TextBox);
            if (txtactivity_startdate.DateTimeValue > txtactivity_enddate.DateTimeValue)
            {
                clsMyObj.ShowMessage("「活動日期(起)」不能大於「活動日期(迄)」");

                e.Cancel = true;
            }

            if (txtactivity_startdate.DateTimeValue.Value.Date <= Convert.ToDateTime(txtcancelregist_deadline.Text).Date)
            {
                clsMyObj.ShowMessage("「取消報名截止日」需早於「活動日期(起)」");

                e.Cancel = true;
            }
           Int32 max1 ,min1 ;
           if (FormView1.Enabled == true)
           {
               if (((TextBox)FormView1.FindControl("txtlimit_count")).Text == "" || ((TextBox)FormView1.FindControl("txtlimit_count")).Text == "無上限")
               {
                   //((TextBox)FormView1.FindControl("txtlimit_count")).Text = "999999";
                   ((TextBox)FormView1.FindControl("txtlimit_count")).Text = "無上限";

                   ((TextBox)FormView1.FindControl("txtlimit2_count")).Text = "無";
                   max1 = 999999;
               }
               else
               {
                   try
                   {
                       max1 = Int32.Parse(((TextBox)FormView1.FindControl("txtlimit_count")).Text);

                       if (max1 <= 0)
                       {
                           if (ActivityType == "1")
                           {
                               clsMyObj.ShowMessage("活動人數上限不是正整數");
                           }
                           else
                           {
                               clsMyObj.ShowMessage("活動隊數上限不是正整數");
                           }
                           e.Cancel = true;
                       }
                   }

                   catch
                   {

                       max1 = 0;
                       if (ActivityType == "1")
                       {
                           clsMyObj.ShowMessage("活動人數上限不是整數");
                       }
                       else
                       {
                           clsMyObj.ShowMessage("活動隊數上限不是整數");
                       }
                       e.Cancel = true;

                   }
               }
               if (((TextBox)FormView1.FindControl("txtlimit2_count")).Text == "" || ((TextBox)FormView1.FindControl("txtlimit2_count")).Text == "無")
               {
                   // ((TextBox)FormView1.FindControl("txtlimit2_count")).Text = "0";
                   ((TextBox)FormView1.FindControl("txtlimit2_count")).Text = "無";
                   min1 = 0;
               }
               else
               {
                   try
                   {
                       min1 = Int32.Parse(((TextBox)FormView1.FindControl("txtlimit2_count")).Text);
                       if (min1 < 0)
                       {
                           if (ActivityType == "1")
                           {
                               clsMyObj.ShowMessage("活動備取人數不是正整數");
                           }
                           else
                           {
                               clsMyObj.ShowMessage("活動備取隊數不是正整數");
                           }
                           e.Cancel = true;
                       }
                   }
                   catch
                   {

                       min1 = 0;
                       if (ActivityType == "1")
                       {
                           clsMyObj.ShowMessage("活動備取人數不是整數");
                       }
                       else
                       {
                           clsMyObj.ShowMessage("活動備取隊數不是整數");
                       }
                       e.Cancel = true;

                   }
               }



               if (Convert.ToDateTime(txtregist_startdate.Text).Date <= DateTime.Today)
               {
                   clsMyObj.ShowMessage("「報名開始日」需晚於「今日」");

                   e.Cancel = true;
               }
               decimal max = 0;
               decimal min = 0;
               if (ActivityType == "2")
               {
                   if (((TextBox)FormView1.FindControl("txtteam_member_max")).Text == "")
                   {
                       ((TextBox)FormView1.FindControl("txtteam_member_max")).Text = "0";
                   }
                   if (((TextBox)FormView1.FindControl("txtteam_member_min")).Text == "")
                   {
                       ((TextBox)FormView1.FindControl("txtteam_member_min")).Text = "0";
                   }


                   max = decimal.Parse(((TextBox)FormView1.FindControl("txtteam_member_max")).Text);
                   min = decimal.Parse(((TextBox)FormView1.FindControl("txtteam_member_min")).Text);

                   if (max < min)
                   {
                       clsMyObj.ShowMessage("每隊上限不可小於下限");
                       e.Cancel = true;
                       return;
                   }
                   if (min < 2)
                   {
                       clsMyObj.ShowMessage("每隊下限不可小於2");
                       e.Cancel = true;
                       return;
                   }


               }



              
           }

        }

        if (Wizard1.ActiveStepIndex == 2)
        {
            GridView_GroupLimit.DataBind();
            string errMsg = "";
            ACMS.DAO.CustomFieldDAO cDAO = new ACMS.DAO.CustomFieldDAO();
            DataTable dt = cDAO.CheckCustFieldItemEmpty(ActivityID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    errMsg += dr["field_name"].ToString() +@"\n";

                }

                clsMyObj.ShowMessage(errMsg+"以上欄位沒有編輯選項");
                e.Cancel = true;
                return;
            }

            dt = cDAO.CheckCustFieldItemOutOfRangInt(ActivityID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    errMsg += dr["field_name"].ToString() + @"\n";

                }

                clsMyObj.ShowMessage(errMsg + "以上費用欄位合計值超過正負20億,超過整數的範圍!");
                e.Cancel = true;
                return;
            }
            dt = cDAO.CheckCustFieldItemNameDuplicate(ActivityID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    errMsg += dr["field_item_name"].ToString() + @"\n";

                }

                clsMyObj.ShowMessage(errMsg + "欄位名稱+編輯選項重覆");
                e.Cancel = true;
                return;
            }
            dt = cDAO.CheckCustFieldNameDuplicate(ActivityID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    errMsg += dr["field_name"].ToString() +@"\n";

                }

                clsMyObj.ShowMessage(errMsg + "欄位名稱重覆");
                e.Cancel = true;
                return;
            }

            if (FormView2.Enabled == true)
            {
                
                decimal max = 0;
                decimal min = 0;
                if (ActivityType == "2")
                {
                    //if (((TextBox)FormView2.FindControl("txtteamextcount_max")).Text == "")
                    //{
                    //    ((TextBox)FormView2.FindControl("txtteamextcount_max")).Text = "0";
                    //}
                    //if (((TextBox)FormView2.FindControl("txtteamextcount_min")).Text == "")
                    //{
                    //    ((TextBox)FormView2.FindControl("txtteamextcount_min")).Text = "0";
                    //}


                    //max = decimal.Parse(((TextBox)FormView2.FindControl("txtteamextcount_max")).Text);
                    //min = decimal.Parse(((TextBox)FormView2.FindControl("txtteamextcount_min")).Text);

                    //if (max < min)
                    //{
                    //    clsMyObj.ShowMessage("每隊攜伴上限不可小於下限");
                    //    e.Cancel = true;
                    //    return;
                    //}



                }
                else
                {
                    if (((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text == "")
                    {
                        ((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text = "0";
                    }
                    if (((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text == "")
                    {
                        ((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text = "0";
                    }


                    //max = decimal.Parse(((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text);
                    //min = decimal.Parse(((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text);

                    //if (max < min)
                    //{
                    //    clsMyObj.ShowMessage("攜伴上限不可小於下限");
                    //    e.Cancel = true;
                    //    return;
                    //}

                }

                if ((FormView2.FindControl("chkis_showperson_fix2") as TCheckBoxYN).YesNo == "Y")
                {
                    try
                    {
                        max = Int64.Parse(((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text);
                        min = Int64.Parse(((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text);

                        if (max < min)
                        {
                            clsMyObj.ShowMessage("攜伴上限不可小於下限");
                            e.Cancel = true;
                            return;
                        }
                        if (max <= 0)
                        {
                            clsMyObj.ShowMessage("攜伴上限不可小於等於0");
                            e.Cancel = true;
                            return;
                        }
                        if (min < 0)
                        {
                            clsMyObj.ShowMessage("攜伴下限不可小於0");
                            e.Cancel = true;
                            return;
                        }
                    }
                    catch
                    {
                        clsMyObj.ShowMessage("攜伴上限及下限需正整數");
                        e.Cancel = true;
                        return;


                    }


                }


            }
        }

        saveStep();
    }


    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 1)
        {
            if (string.Compare(FCKeditor1.Value.Trim(), "") == 0)
            {

                Wizard1.ActiveStepIndex =0;
                //clsMyObj.ShowMessage("活動內容不可空白");
              

            }

        }

        if (Wizard1.ActiveStepIndex == 1)
        {
           // ((Button)FormView1.FindControl("btnUpload")).OnClientClick = "window.document.getElementById(\"" + FormView1.FindControl("Image1").ClientID + "\").class=\"\";";
        }
      
    }
    protected void btnUpload_Init(object sender, EventArgs e)
    {
        
        ((Button)FormView1.FindControl("btnUpload")).OnClientClick = "document.getElementById('" + FormView1.FindControl("Image1").ClientID + "').className='plVisible'";


    }
    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        int i;
        bool chk1 = (GridView_GroupLimit.HeaderRow.FindControl("chk1") as CheckBox).Checked;
        for (i = 0; i < GridView_GroupLimit.Rows.Count; i++)
        {
            ((CheckBox)GridView_GroupLimit.Rows[i].FindControl("chk1")).Checked = chk1;
        }
    }
    protected void FormView2_DataBound(object sender, EventArgs e)
    {
        if ((FormView2.FindControl("chkis_showperson_fix2") as TCheckBoxYN).YesNo == "Y")
        {

            FormView2.FindControl("txtpersonextcount_max").Visible = true;
            FormView2.FindControl("txtpersonextcount_min").Visible = true;
        }
        else
        {
            FormView2.FindControl("txtpersonextcount_max").Visible = false;
            FormView2.FindControl("txtpersonextcount_min").Visible = false;
        }

    }
}


//自訂欄位設定
public partial class WebForm_ManageActivity_ActivityEdit
{
    //新增
    protected void btnAddCustomField_Click(object sender, EventArgs e)
    {
        try
        {
            ACMS.VO.CustomFieldVO myCustomFieldVO = new ACMS.VO.CustomFieldVO();

            myCustomFieldVO.activity_id = ActivityID;
            myCustomFieldVO.field_name = txtfield_name.Text;
            myCustomFieldVO.field_control = ddlfield_control.SelectedValue;

            ACMS.DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
            myCustomFieldDAO.INSERT(myCustomFieldVO);

            GridView_CustomField.DataBind();

            txtfield_name.Text = "";
            ddlfield_control.SelectedIndex = -1;
        }
        catch (Exception ex)

        {
            WriteErrorLog("AddCustomField", ex.Message, "0");
        }
    }

    //刪除
    protected void lbtnDeleteCustomField_Click(object sender, EventArgs e)
    {
        try
        {
            ACMS.VO.CustomFieldVO myCustomFieldVO = new ACMS.VO.CustomFieldVO();

            int intfield_id = (int)GridView_CustomField.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;

            ACMS.DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
            myCustomFieldDAO.DELETE(intfield_id);

            GridView_CustomField.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("DeleteCustomField", ex.Message, "0");
            ShowMessageForAjax(GridView_CustomField, ex.Message);

        }

    }

    //編輯選項
    protected void lbtnEditItem_Click(object sender, EventArgs e)
    {
        try
        {
            OpenListItem1.InitDataAndShow(Convert.ToInt32((sender as LinkButton).CommandArgument), (((sender as LinkButton).NamingContainer as GridViewRow).FindControl("ddlfield_control") as DropDownList).SelectedValue);
        }
        catch (Exception ex)
        {
            WriteErrorLog("EditItem", ex.Message, "0");
            ShowMessageForAjax(GridView_CustomField, ex.Message);

        }

    }

}

//族群限定
public partial class WebForm_ManageActivity_ActivityEdit
{
    //上傳族群限定名單
    protected void btnUpload_GroupLimit_Click(object sender, EventArgs e)
    {
        if (this.FileUpload_GroupLimit.HasFile)
        {
            try
            {

                HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload_GroupLimit.FileContent);
                HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(0);
                
                DataTable table = new DataTable();
                table.TableName = "table";

                HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                //for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                //{
                //    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                //    table.Columns.Add(column);
                //}

                //table.Columns.Add("id");
                //table.Columns["id"].AutoIncrement = true;
                table.Columns.Add("activity_id", typeof(System.Guid));
                table.Columns.Add("emp_id");
                bool isEmp=false;
                int rowCount = sheet.LastRowNum;
                int colii = 0;
                if (rowCount >= 1)
                {
                    HSSFRow row1 = (HSSFRow)sheet.GetRow(0);
                    for (int ii = 0; ii < row1.Cells.Count; ii++)
                    {
                        if (row1.Cells[ii].ToString ()  == "工號")
                        {
                            colii = ii;
                            isEmp = true;
                        }

                    }
                    if (isEmp == false)
                    {
                        clsMyObj.ShowMessage("沒有工號欄位");
                        return;
                
                    } 
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        HSSFRow row = (HSSFRow)sheet.GetRow(i);

                        DataRow dataRow = table.NewRow();

                        //for (int j = row.FirstCellNum; j < cellCount; j++)
                        //{
                        //    if (row.GetCell(j) != null)
                        //    {
                        //        dataRow[j] = row.GetCell(j).ToString();
                        //    }

                        //}
                        //andy 修正為只有工號
                        if (row == null)
                            continue;

                        dataRow["activity_id"] = ActivityID;
                        dataRow["emp_id"] = row.GetCell(colii).ToString();// +row.GetCell(1).ToString();

                        table.Rows.Add(dataRow);
                    }
                    catch (Exception ex1)
                    {

                        WriteErrorLog("UploadGroupLimit", ex1.Message, "0");
                        ShowMessageForAjax(GridView_CustomField, ex1.Message);
                    }
                }

                workbook = null;
                sheet = null;

                ACMS.DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
                myActivityGroupLimitDAO.UpdateDataSet(table, ActivityID);

                GridView_GroupLimit.DataBind();

            }
            catch (Exception ex)
            {
                WriteErrorLog("UploadGroupLimit", ex.Message, "0");
                clsMyObj.ShowMessage("無法正常讀取上傳檔資料!");
            }

        }
    }

    //開啟族群限定新增
    protected void btnAddGroupLimit_Click(object sender, EventArgs e)
    {
        try
        {
            OpenEmployeeSelector1.InitDataAndShow(ActivityID,MyFormMode,ActivityType  );
        }
        catch (Exception ex)
        {
            WriteErrorLog("btnAddGroupLimit", ex.Message, "0");
            clsMyObj.ShowMessage(ex.Message );
        }
    }

    //選取人員之後
    protected void GetEmployees_Click(object sender, EventArgs e)
    {
        GridView GridView_Employee = (GridView)sender;
        int i;

        ACMS.DAO.BaseDAO myBaseDAO = new ACMS.DAO.BaseDAO();
        using (SqlConnection myConn = myBaseDAO.MyConn())
        {
            myConn.Open();

           SqlTransaction trans = myConn.BeginTransaction();

            ACMS.DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();

            try
            {
                for (i = 0; i < GridView_Employee.Rows.Count; i++)
                {
                    if (((CheckBox)GridView_Employee.Rows[i].FindControl("chkRJRA")).Checked)//2011/4/7  打勾的新增,不打勾的要刪除
                    {


                        ACMS.VO.ActivityGroupLimitVO myActivityGroupLimitVO = new ACMS.VO.ActivityGroupLimitVO();

                        myActivityGroupLimitVO.activity_id = ActivityID;
                        myActivityGroupLimitVO.emp_id = GridView_Employee.DataKeys[i].Value.ToString();
                        try
                        {
                            myActivityGroupLimitDAO.INSERT(myActivityGroupLimitVO, trans);
                        }
                        catch
                        {
                        }

                    }
                    else
                    {

                        myActivityGroupLimitDAO.DELETE(GridView_Employee.DataKeys[i].Value.ToString(), ActivityID);
                    }
                }




                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clsMyObj.ShowMessage("加入族群限定失敗!");
                WriteErrorLog("GetEmployee", ex.Message, "0");
            }
        }



        GridView_GroupLimit.DataBind();

    }

    //刪除人員
    protected void lbtnDel_GroupLimit_Click(object sender, EventArgs e)
    {
        //ACMS.VO.CustomFieldVO myCustomFieldVO = new ACMS.VO.CustomFieldVO();
        //2011/4/7 修改為全選.刪除'
        //int intkeyID = (int)GridView_GroupLimit.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;

        //ACMS.DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
        //myActivityGroupLimitDAO.DELETE(intkeyID);
        //=============================================================================
        ACMS.DAO.ActivityGroupLimitDAO myDAO = new ACMS.DAO.ActivityGroupLimitDAO ();
        string empid ;
        try
        {
            foreach (GridViewRow gr in GridView_GroupLimit.Rows)
            {
                if (((CheckBox)gr.FindControl("chk1")).Checked)
                {
                    empid = ((HiddenField)gr.FindControl("hiID")).Value;
                    myDAO.DELETE(empid, ActivityID);
                }

            }
            GridView_GroupLimit.DataBind();
        }
        catch (Exception ex)

        {
            WriteErrorLog("Del_GroupLimit", ex.Message, "0");
        }
    }

    //匯出族群限定名單
    protected void btnExport_GroupLimit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            ACMS.DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
            table = myActivityGroupLimitDAO.SelectEmployeeByActivity_id(ActivityID);

            if (table != null && table.Rows.Count > 0)
            {
                //table.Columns.RemoveAt(0);
                table.Columns[0].ColumnName = "員工編號";
                table.Columns[1].ColumnName = "員工姓名";
                table.Columns[2].ColumnName = "分機";
                table.Columns[3].ColumnName = "Email";
                table.Columns[4].ColumnName = "部門編號";
                table.Columns[5].ColumnName = "部門名稱";
                table.Columns[6].ColumnName = "部門簡稱";

                // 產生 Excel 資料流。
                MemoryStream ms = DataTableRenderToExcel.RenderDataTableToExcel(table) as MemoryStream;
                // 設定強制下載標頭。
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", Server.UrlEncode("族群限定")));
                // 輸出檔案。
                Response.BinaryWrite(ms.ToArray());

                ms.Close();
                ms.Dispose();
            }
            else
            {
                clsMyObj.ShowMessage("沒有資料!");

            }
            if (table != null) table.Dispose();

        }
        catch (Exception ex)

        {
            WriteErrorLog("ExportLimitGrouop", ex.Message, "0");
        }



    }
}


public partial class WebForm_ManageActivity_ActivityEdit
{

    public FormViewMode MyFormMode
    {
        get { return (ViewState["MyFormMode"] == null ? FormViewMode.ReadOnly : (FormViewMode)ViewState["MyFormMode"]); }
        set { ViewState["MyFormMode"] = value; }
    }

    public Guid ActivityID
    {
        get
        {
            if (ViewState["ActivityID"] == null)
            {
                ViewState["ActivityID"] = Guid.NewGuid();
                return (Guid)ViewState["ActivityID"];
            }
            else
            {
                return new Guid(ViewState["ActivityID"].ToString());
            }
        }
        set { ViewState["ActivityID"] = value; }
    }

    public string ActivityType
    {
        get { return (ViewState["ActivityType"] == null ? "" : ViewState["ActivityType"].ToString()); }
        set { ViewState["ActivityType"] = value; }
    }

    //public bool IsShowEdit(object objTMP)
    //{
    //    return Convert.ToBoolean(objTMP);

    //}
}

