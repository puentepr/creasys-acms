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
using System.IO;
using System.Data.SqlClient;
using System.Text;

public partial class WebForm_ActivityCheck : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master.Master.FindControl("ScriptManager1") as ScriptManager).RegisterPostBackControl(btnExport);
          
        if (!IsPostBack)
        {
               
            (this.Master as MyMasterPage).PanelMainGroupingText = "活動進度登錄";
            ddlActivity.DataBind();
            ddlC_NAME.DataBind();
            ddlC_NAME.Items.Insert(0, new ListItem("請選擇", ""));
            ddlDEPT_ID.DataBind();
            btnQuery_Click(null, null);
           
        }


    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        


        ViewState["activity_id"] = ddlActivity.SelectedValue;
        ViewState["DEPT_ID"] = ddlDEPT_ID.SelectedValue;

        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = ddlActivity.SelectedValue;
        ObjectDataSource1.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedItem.Text ;
        ObjectDataSource1.SelectParameters["emp_id"].DefaultValue = txtemp_id.Text;
        ObjectDataSource1.SelectParameters["emp_name"].DefaultValue = txtemp_name.Text;
        ObjectDataSource1.SelectParameters["UnderDept"].DefaultValue = cbUnderDept.Checked.ToString();
        ObjectDataSource1.SelectParameters["COMPANY_CODE"].DefaultValue = ddlC_NAME.SelectedValue ;
        
        GridView1.DataBind();
        
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ViewState["activity_id"] == null)
        {
            return;
        }

        DataTable table = new DataTable();
        DataTable dt = new DataTable();
        DataRow dtDr;

        try
        {
            ACMS.DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
            //table = mySelectorDAO.ActivityCheckQuery(ViewState["activity_id"].ToString(), ViewState["DEPT_ID"].ToString(), txtemp_id.Text, txtemp_name.Text,cbUnderDept.Checked,ddlC_NAME.SelectedValue  );
            table = mySelectorDAO.ActivityCheckQuery(ViewState["activity_id"].ToString(), "", "", "", true, "");
            if (table.Rows.Count == 0)
            {
                clsMyObj.ShowMessage("沒有報名資料");
                return;
            }

            //‧進度excel匯出( 依報名編號、部門、工號、姓名、分機、E-MAIL、進度狀態 )


            dt.Columns.Add("報名編號", System.Type.GetType("System.String"));
            dt.Columns.Add("部門代號", System.Type.GetType("System.String"));
            dt.Columns.Add("部門", System.Type.GetType("System.String"));
            dt.Columns.Add("工號", System.Type.GetType("System.String"));
            dt.Columns.Add("姓名", System.Type.GetType("System.String"));
            dt.Columns.Add("分機", System.Type.GetType("System.String"));
            dt.Columns.Add("EMAIL", System.Type.GetType("System.String"));
            dt.Columns.Add("進度狀態", System.Type.GetType("System.String"));
          
            string teamName = "";
            int seqno = 0;
            int seqno1 = 0;

            foreach (DataRow dr in table.Rows)
            {
                dtDr = dt.NewRow();
                if (dr["activity_type"].ToString() == "2")
                {
                    if (teamName != dr["boss_id"].ToString().Trim())
                    {
                        seqno++;
                    }
                    if (seqno > int.Parse(dr["team_max"].ToString()))
                    {
                        seqno1++;
                    }
                    teamName = dr["boss_id"].ToString().Trim();

                }
                else
                {
                    seqno++;
                    if (seqno > int.Parse(dr["team_max"].ToString()))
                    {
                        seqno1++;
                    }
                }
                if (seqno1 > 0)
                {
                    dtDr["報名編號"] = "備取:" + seqno1.ToString();
                }
                else
                {
                    dtDr["報名編號"] = "正取:" + seqno.ToString();
                }
                dtDr["部門代號"] = dr["DEPT_ID"].ToString();
                dtDr["部門"] = dr["C_DEPT_NAME"].ToString();
                dtDr["工號"] = dr["WORK_ID"].ToString();
                dtDr["姓名"] = dr["NATIVE_NAME"].ToString();
                dtDr["分機"] = dr["OFFICE_PHONE"].ToString();
                dtDr["EMAIL"] = dr["OFFICE_MAIL"].ToString();
                dtDr["進度狀態"] = dr["check_status"].ToString();


                dt.Rows.Add(dtDr);
            }

            if (table != null && table.Rows.Count > 0)
            {
                table.Columns[0].ColumnName = "員工編號";
                table.Columns[1].ColumnName = "員工姓名";
                table.Columns[2].ColumnName = "員工部門";
                table.Columns[3].ColumnName = "登錄狀態";
                

                // 產生 Excel 資料流。
                //MemoryStream ms = DataTableRenderToExcel.RenderDataTableToExcel(table) as MemoryStream;
                MemoryStream ms = DataTableRenderToExcel.RenderDataTableToExcel(dt) as MemoryStream;
                // 設定強制下載標頭。
                //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", Server.UrlEncode("報名登錄狀態")));
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", Server.UrlEncode("RegistedStatus")));
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
        catch (Exception ex)
        {
            WriteErrorLog("ExportDoc", ex.Message, "0");
        }
        finally
        {
            if (table != null) table.Dispose();
            if (dt != null) dt.Dispose();         
        }

    }

    //更新
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ViewState["activity_id"] == null)
        {
            return;
        }

        ACMS.DAO.BaseDAO myBaseDAO = new ACMS.DAO.BaseDAO();
        SqlCommand cmd = new SqlCommand(); ;
        using (SqlConnection myConn = myBaseDAO.MyConn())
        {
            myConn.Open();

            using (SqlTransaction trans = myConn.BeginTransaction())
            {
                try
                {
                  

                    cmd.Connection = myConn;
                    cmd.Transaction = trans;

                    foreach (GridViewRow gvr in GridView1.Rows)
                    {

                        if (gvr.RowType == DataControlRowType.DataRow && (gvr.FindControl("CheckBox1") as CheckBox).Checked)
                        {
                            string emp_id = GridView1.DataKeys[gvr.RowIndex].Values[0].ToString();
                            string activity_type = GridView1.DataKeys[gvr.RowIndex].Values[1].ToString();

                            string status = ddlcheck_status.SelectedValue;

                            SqlParameter[] sqlParams = new SqlParameter[3];

                            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
                            sqlParams[0].Value = new Guid(ViewState["activity_id"].ToString());
                            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
                            sqlParams[1].Value = emp_id;
                            sqlParams[2] = new SqlParameter("@check_status", SqlDbType.Int);
                            sqlParams[2].Value = status;

                            StringBuilder sb = new StringBuilder();

                            if (activity_type == "1")
                            {
                                sb.AppendLine("UPDATE ActivityRegist ");
                            }
                            else
                            {
                                sb.AppendLine("UPDATE ActivityTeamMember ");
                            }

                            sb.AppendLine("set check_status=@check_status ");
                            sb.AppendLine("WHERE activity_id=@activity_id and emp_id=@emp_id; ");

                            cmd.CommandText = sb.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(sqlParams);
                            cmd.ExecuteNonQuery();

                        }

                    }

                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    clsMyObj.ShowMessage("更新失敗!");
                    WriteErrorLog("Update", ex.Message, "0");
                }
                finally
                { 
                if (cmd !=null ) cmd.Dispose();
                }

            }


        }

        GridView1.DataBind();

    }


    protected void ddlC_NAME_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDEPT_ID.DataBind();
      
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            GridView1.Visible = false;
            lblGrideView1.Visible = true;

        }
        else
        {
            GridView1.Visible = true;
            lblGrideView1.Visible = false;
        }
    }
   
}
