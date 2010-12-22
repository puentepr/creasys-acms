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
using System.Configuration;

public partial class WebForm_ManageActivity_ActivityEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //FormView1.EditItemTemplate = FormView1.ItemTemplate;
        //FormView2.EditItemTemplate = FormView2.ItemTemplate;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //族群限定的上傳與匯出
        (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(btnUpload_GroupLimit);
        (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(btnExport_GroupLimit);

        if (!IsPostBack)
        {
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

                if (Session["form_mode"].ToString() == "readonly" || myActivatyVO.activity_enddate <= DateTime.Now)
                {
                    //唯讀模式:活動已結束也要是唯讀
                    MyFormMode = FormViewMode.ReadOnly;

                    FTB_FreeTextBox.ReadOnly = true;
                    rblgrouplimit.Enabled = false;
                    Panel_GroupLimit.Enabled = false;
                    txtnotice.Enabled = false;
                }

            }
            
            Session["form_mode"] = null;
            Session["activity_type"] = null;
            Session["activity_id"] = null;

            //如果已經過了活動開始報名日，則某些功能需唯讀
            if (myActivatyVO.regist_startdate <= DateTime.Now)
            {
                FormView1.Enabled = false;
                FormView2.Enabled = false;
                PanelCustomFieldC.Enabled = false;
                rblgrouplimit.Enabled = false;
                Panel_GroupLimit.Enabled = false;
            }

            //取得FormView外的欄位初始值
            FTB_FreeTextBox.Text = myActivatyVO.activity_info;
            rblgrouplimit.SelectedValue = myActivatyVO.is_grouplimit;
            txtnotice.Text = myActivatyVO.notice;

            //活動資訊-活動內容
            ObjectDataSource_Activaty.SelectParameters["id"].DefaultValue = ActivityID.ToString();
            //活動資訊-上傳檔
            ObjectDataSource_UpFiles.SelectParameters["dirName"].DefaultValue = Server.MapPath(Path.Combine("/ACMS/UpFiles", ActivityID.ToString()));
           
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
                (FormView1.FindControl("chk_txtlimit_count") as RequiredFieldValidator).ErrorMessage = "活動人數上限必填";
                (FormView1.FindControl("chk_txtlimit_count2") as CompareValidator).ErrorMessage = "活動人數上限必填數字";
                (FormView1.FindControl("chk_txtlimit2_count") as RequiredFieldValidator).ErrorMessage = "活動備取人數必填";
                (FormView1.FindControl("chk_txtlimit2_count2") as CompareValidator).ErrorMessage = "活動備取人數必填數字";
                (FormView1.FindControl("trteam_member_max") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = false;
                (FormView1.FindControl("trteam_member_min") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = false;

                (FormView2.FindControl("PanelCustomFieldA1") as Panel).Visible = true;
                (FormView2.FindControl("PanelCustomFieldB1") as Panel).Visible = false;
                (FormView2.FindControl("PanelCustomFieldB2") as Panel).Visible = false;

                PanelCustomFieldC.GroupingText = "個人自訂欄位";
            }
            else
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "新增團隊活動";
                (FormView1.FindControl("lbllimit_count_team") as Literal).Visible = true;
                (FormView1.FindControl("lbllimit2_count_team") as Literal).Visible = true;
                (FormView1.FindControl("chk_txtlimit_count") as RequiredFieldValidator).ErrorMessage = "活動隊數上限必填";
                (FormView1.FindControl("chk_txtlimit_count2") as CompareValidator).ErrorMessage = "活動隊數上限必填數字";
                (FormView1.FindControl("chk_txtlimit2_count") as RequiredFieldValidator).ErrorMessage = "活動備取隊數必填";
                (FormView1.FindControl("chk_txtlimit2_count2") as CompareValidator).ErrorMessage = "活動備取隊數必填數字";

                (FormView1.FindControl("trteam_member_max") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = true;
                (FormView1.FindControl("trteam_member_min") as System.Web.UI.HtmlControls.HtmlTableRow).Visible = true;

                (FormView2.FindControl("PanelCustomFieldA1") as Panel).Visible = false;
                (FormView2.FindControl("PanelCustomFieldB1") as Panel).Visible = true;
                (FormView2.FindControl("PanelCustomFieldB2") as Panel).Visible = true;

                PanelCustomFieldC.GroupingText = "團隊自訂欄位";

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
        FileUpload myFileUpload = (FileUpload)FormView1.FindControl("FileUpload1");

        if (myFileUpload.HasFile)
        {
            if (ConfigurationManager.AppSettings["ValidExtention"].ToLower().IndexOf(Path.GetExtension(myFileUpload.FileName).Replace(".","").ToLower()) ==-1)
            {
                clsMyObj.ShowMessage(string.Format("副檔名只能是[{0}]!", ConfigurationManager.AppSettings["ValidExtention"]));
                return;
            }


            try
            {
                DirectoryInfo myDirectoryInfo = new DirectoryInfo(Server.MapPath(Path.Combine("/ACMS/UpFiles", ActivityID.ToString())));

                if (!myDirectoryInfo.Exists)
                {
                    myDirectoryInfo.Create();
                }

                FileStream myFileStream = new FileStream(Path.Combine(myDirectoryInfo.FullName, myFileUpload.FileName), FileMode.Create);
                myFileStream.Write(myFileUpload.FileBytes, 0, myFileUpload.FileBytes.Length);
            }
            catch (Exception ex)
            {
                clsMyObj.ShowMessage("檔案上傳時發生錯誤!");
            }

            GridView GridView_UpFiles = (GridView)FormView1.FindControl("GridView_UpFiles");

            GridView_UpFiles.DataBind();
        }
    }

    //下載附件檔
    protected void lbtnFileDownload_Click(object sender, EventArgs e)
    {
        GridView GridView_UpFiles = (GridView)FormView1.FindControl("GridView_UpFiles");
        FileInfo myFileInfo = new FileInfo(GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());

        if (myFileInfo.Exists)
        {
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", Server.UrlEncode((myFileInfo.Name))));
            // 輸出檔案。
            Response.WriteFile(myFileInfo.FullName);
        }
    }

    //刪除附件檔
    protected void lbtnFileDelete_Click(object sender, EventArgs e)
    {
        GridView GridView_UpFiles = (GridView)FormView1.FindControl("GridView_UpFiles");
        FileInfo myFileInfo = new FileInfo(GridView_UpFiles.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString());

        if (myFileInfo.Exists)
        {
            myFileInfo.Delete();
        }

        GridView_UpFiles.DataBind();
    }

    //勾選改變時要必填
    protected void chkis_showperson_fix2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkis_showperson_fix2 = (CheckBox)sender;
        chk_txtpersonextcount_min.Visible = chkis_showperson_fix2.Checked;
        chk_txtpersonextcount_min2.Visible = chkis_showperson_fix2.Checked;

        chk_txtpersonextcount_max.Visible = chkis_showperson_fix2.Checked;
        chk_txtpersonextcount_max2.Visible = chkis_showperson_fix2.Checked;
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

    //存檔
    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (MyFormMode == FormViewMode.ReadOnly)
        {
            Response.Redirect("ActivityEditQuery.aspx");
        }

        ACMS.VO.ActivatyVO myActivatyVO = new ACMS.VO.ActivatyVO();

        myActivatyVO.id = ActivityID;
        myActivatyVO.activity_type = ActivityType;
        myActivatyVO.activity_info = FTB_FreeTextBox.Text;
        myActivatyVO.org_id = ((DropDownList)FormView1.FindControl("ddlorg_id")).SelectedValue;
        myActivatyVO.activity_name = ((TextBox)FormView1.FindControl("txtactivity_name")).Text;
        myActivatyVO.people_type = ((TextBox)FormView1.FindControl("txtpeople_type")).Text;
        myActivatyVO.activity_startdate = ((WebForm_DatetimePicker)FormView1.FindControl("txtactivity_startdate")).DateTimeValue;
        myActivatyVO.activity_enddate = ((WebForm_DatetimePicker)FormView1.FindControl("txtactivity_enddate")).DateTimeValue;
       // if (((TextBox)FormView1.FindControl("txtlimit_count")).Text=="")
       // {
       //     ((TextBox)FormView1.FindControl("txtlimit_count")).Text="0";
       // }
       //if( ( (TextBox)FormView1.FindControl("txtlimit2_count")).Text=="")
       // {
       //     ((TextBox)FormView1.FindControl("txtlimit2_count")).Text="0";
       // }
        myActivatyVO.limit_count = Convert.ToInt32(((TextBox)FormView1.FindControl("txtlimit_count")).Text);
        myActivatyVO.limit2_count = Convert.ToInt32(((TextBox)FormView1.FindControl("txtlimit2_count")).Text);

        //if (((TextBox)FormView1.FindControl("txtteam_member_max")).Text == "")
        //{
        //    ((TextBox)FormView1.FindControl("txtteam_member_max")).Text = "0";
        //}
        //if (((TextBox)FormView1.FindControl("txtteam_member_min")).Text == "")
        //{
        //    ((TextBox)FormView1.FindControl("txtteam_member_min")).Text = "0";
        //}

        if (ActivityType == "2")
        {
            myActivatyVO.team_member_max = Convert.ToInt32(((TextBox)FormView1.FindControl("txtteam_member_max")).Text);
            myActivatyVO.team_member_min = Convert.ToInt32(((TextBox)FormView1.FindControl("txtteam_member_min")).Text);
        }

        myActivatyVO.regist_startdate = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_startdate")).Text);
        myActivatyVO.regist_deadline = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtregist_deadline")).Text);
        myActivatyVO.cancelregist_deadline = Convert.ToDateTime(((TextBox)FormView1.FindControl("txtcancelregist_deadline")).Text);
        myActivatyVO.is_showfile = ((CheckBox)FormView1.FindControl("chkis_showfile")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showprogress = ((CheckBox)FormView1.FindControl("chkis_showprogres")).Checked == true ? "Y" : "N";


        myActivatyVO.is_showperson_fix1 = ((CheckBox)FormView2.FindControl("chkis_showperson_fix1")).Checked == true ? "Y" : "N";
        myActivatyVO.is_showperson_fix2 = ((CheckBox)FormView2.FindControl("chkis_showperson_fix2")).Checked == true ? "Y" : "N";

       //if (((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text=="")
       // {
       //     ((TextBox)FormView2.FindControl("txtpersonextcount_max")).Text="0";
       // }
       //if (((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text == "")
       //{
       //    ((TextBox)FormView2.FindControl("txtpersonextcount_min")).Text = "0";
       //}
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

        //if (((TextBox)FormView2.FindControl("txtteamextcount_max")).Text == "")
        //{
        //    ((TextBox)FormView2.FindControl("txtteamextcount_max")).Text="0";
        //}

        //if (((TextBox)FormView2.FindControl("txtteamextcount_min")).Text == "")
        //{
        //    ((TextBox)FormView2.FindControl("txtteamextcount_min")).Text = "0";
        //}
       


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

            Response.Redirect("ActivityEditQuery.aspx");
        }
        catch (Exception ex)
        {
            clsMyObj.ShowMessage("存檔失敗!");
        }

    }

    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 1)
        {
            WebForm_DatetimePicker txtactivity_startdate = (FormView1.FindControl("txtactivity_startdate") as WebForm_DatetimePicker);
            WebForm_DatetimePicker txtactivity_enddate = (FormView1.FindControl("txtactivity_enddate") as WebForm_DatetimePicker);
            TextBox txtcancelregist_deadline = (FormView1.FindControl("txtcancelregist_deadline") as TextBox);

            if (txtactivity_startdate.DateTimeValue > txtactivity_enddate.DateTimeValue)
            {
                clsMyObj.ShowMessage("「活動日期(起)」不能大於「活動日期(迄)」");
       
                e.Cancel = true;
            }

            if (txtactivity_startdate.DateTimeValue.Value.Date <=Convert.ToDateTime(txtcancelregist_deadline.Text).Date)
            {
                clsMyObj.ShowMessage("「取消報名截止日」需早於「活動日期(起)」");

                e.Cancel = true;
            }
        }
    }
}


//自訂欄位設定
public partial class WebForm_ManageActivity_ActivityEdit
{
    //新增
    protected void btnAddCustomField_Click(object sender, EventArgs e)
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

    //刪除
    protected void lbtnDeleteCustomField_Click(object sender, EventArgs e)
    {
        ACMS.VO.CustomFieldVO myCustomFieldVO = new ACMS.VO.CustomFieldVO();

        int intfield_id = (int)GridView_CustomField.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;

        ACMS.DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
        myCustomFieldDAO.DELETE(intfield_id);

        GridView_CustomField.DataBind();
    }

    //編輯選項
    protected void lbtnEditItem_Click(object sender, EventArgs e)
    {
        OpenListItem1.InitDataAndShow(Convert.ToInt32((sender as LinkButton).CommandArgument), (((sender as LinkButton).NamingContainer as GridViewRow).FindControl("ddlfield_control") as DropDownList).SelectedValue);
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

                int rowCount = sheet.LastRowNum;

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
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

                    dataRow["activity_id"] = ActivityID;
                    dataRow["emp_id"] = row.GetCell(0).ToString() + row.GetCell(1).ToString();

                    table.Rows.Add(dataRow);

                }

                workbook = null;
                sheet = null;

                ACMS.DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
                myActivityGroupLimitDAO.UpdateDataSet(table, ActivityID);

                GridView_GroupLimit.DataBind();

            }
            catch (Exception ex)
            {

                clsMyObj.ShowMessage("無法正常讀取上傳檔資料!");
            }

        }
    }

    //開啟族群限定新增
    protected void btnAddGroupLimit_Click(object sender, EventArgs e)
    {
        OpenEmployeeSelector1.InitDataAndShow(ActivityID);
    }

    //選取人員之後
    protected void GetEmployees_Click(object sender, EventArgs e)
    {
        GridView GridView_Employee = (GridView)OpenEmployeeSelector1.FindControl("GridView_Employee");
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
                    if (((CheckBox)GridView_Employee.Rows[i].FindControl("CheckBox1")).Checked)
                    {
                        ACMS.VO.ActivityGroupLimitVO myActivityGroupLimitVO = new ACMS.VO.ActivityGroupLimitVO();

                        myActivityGroupLimitVO.activity_id = ActivityID;
                        myActivityGroupLimitVO.emp_id = GridView_Employee.DataKeys[i].Value.ToString();
                        myActivityGroupLimitDAO.INSERT(myActivityGroupLimitVO, trans);
                    }
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clsMyObj.ShowMessage("加入族群限定失敗!");
            }
        }



        GridView_GroupLimit.DataBind();

    }

    //刪除人員
    protected void lbtnDel_GroupLimit_Click(object sender, EventArgs e)
    {
        //ACMS.VO.CustomFieldVO myCustomFieldVO = new ACMS.VO.CustomFieldVO();

        int intkeyID = (int)GridView_GroupLimit.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;

        ACMS.DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
        myActivityGroupLimitDAO.DELETE(intkeyID);

        GridView_GroupLimit.DataBind();
    }

    //匯出族群限定名單
    protected void btnExport_GroupLimit_Click(object sender, EventArgs e)
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

