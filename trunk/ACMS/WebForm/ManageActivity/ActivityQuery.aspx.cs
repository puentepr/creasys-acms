using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class WebForm_ActivityQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for(int i=2010 ;i<=DateTime.Now.Year+1;i++)
            {
                ddlYear.Items.Add(i.ToString());
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            if (Request["type"] != null && Request["type"] == "off")
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "歷史資料查詢";
                GridView1.Columns[7].Visible = false;
                ObjectDataSource1.SelectParameters["querytype"].DefaultValue = "off";
            }
            else
            {
                (this.Master as MyMasterPage).PanelMainGroupingText = "報名狀態查詢";
                ObjectDataSource1.SelectParameters["querytype"].DefaultValue = "";
            }

            btnQuery_Click(null, null);
        }


    }

    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string startdate = string.Format("{0}/{1}/1", ddlYear.SelectedValue, ddlMonth.SelectedValue);
        string enddate = string.Format("{0}/{1}/{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue)).ToString());
        
        ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = startdate;
        ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = enddate;
        ObjectDataSource1.SelectParameters["org_id"].DefaultValue = ddlUnit.SelectedValue;

        GridView1.DataBind();

    }

    //檢視內容
    protected void lbtnViewActivity_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

        Session["activity_id"] = activity_id;
        Session["activity_type"] = activity_type;
        Session["form_mode"] = "readonly";
        Response.Redirect("ActivityEdit.aspx");
    }

    //匯出名單
    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[0].ToString();
        string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

        DataTable table = new DataTable();

        ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
        table = myActivityRegistDAO.SelectEmployeesByID(new Guid(activity_id), activity_type);

        if (table != null && table.Rows.Count > 0)
        {
            table.Columns[0].ColumnName = "員工編號";
            table.Columns[1].ColumnName = "員工姓名";
            table.Columns[2].ColumnName = "員工部門";

            // 產生 Excel 資料流。
            MemoryStream ms = DataTableRenderToExcel.RenderDataTableToExcel(table) as MemoryStream;
            // 設定強制下載標頭。
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", Server.UrlEncode("匯出名單")));
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

    //取消報名
    protected void lbtnCancelRegist_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[0].ToString();
        string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

        string regist_deadline = (sender as LinkButton).CommandArgument;
        string cancelregist_deadline = (sender as LinkButton).CommandName;

        if (activity_type == "1")
        {
            OpenRegistedByMeEmpSelector1.activity_id = activity_id;
            OpenRegistedByMeEmpSelector1.regist_by = "";
            OpenRegistedByMeEmpSelector1.regist_deadline = regist_deadline;
            OpenRegistedByMeEmpSelector1.cancelregist_deadline = cancelregist_deadline;
            OpenRegistedByMeEmpSelector1.InitDataAndShow();
        }
        else
        {
            Response.Redirect("RegistActivity_Team.aspx");
        }

    }


    ////取消個人報名後
    //protected void CancelPersonRegist_OnClick(object sender, EventArgs e)
    //{
    //    GridView1.DataBind();
    //}

}