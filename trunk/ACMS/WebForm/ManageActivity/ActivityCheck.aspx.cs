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

public partial class WebForm_ActivityCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (this.Master as MyMasterPage).PanelMainGroupingText = "報名登錄狀態管理";
            ddlActivity.DataBind();
            btnQuery_Click(null, null);
        }

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ViewState["activity_id"] = ddlActivity.SelectedValue;
        ViewState["DEPT_ID"] = ddlDEPT_ID.SelectedValue;

        ObjectDataSource1.SelectParameters["activity_id"].DefaultValue = ddlActivity.SelectedValue;
        ObjectDataSource1.SelectParameters["DEPT_ID"].DefaultValue = ddlDEPT_ID.SelectedValue;
        ObjectDataSource1.SelectParameters["emp_id"].DefaultValue = txtemp_id.Text;
        ObjectDataSource1.SelectParameters["emp_name"].DefaultValue = txtemp_name.Text;

        GridView1.DataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ViewState["activity_id"] == null)
        {
            return;
        }

        DataTable table = new DataTable();
        ACMS.DAO.SelectorDAO mySelectorDAO = new ACMS.DAO.SelectorDAO();
        table = mySelectorDAO.ActivityCheckQuery(ViewState["activity_id"].ToString(), ViewState["DEPT_ID"].ToString(), txtemp_id.Text, txtemp_name.Text);

        if (table != null && table.Rows.Count > 0)
        {
            table.Columns[0].ColumnName = "員工編號";
            table.Columns[1].ColumnName = "員工姓名";
            table.Columns[2].ColumnName = "員工部門";
            table.Columns[3].ColumnName = "登錄狀態";

            // 產生 Excel 資料流。
            MemoryStream ms = DataTableRenderToExcel.RenderDataTableToExcel(table) as MemoryStream;
            // 設定強制下載標頭。
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", Server.UrlEncode("報名登錄狀態")));
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

    //更新
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ViewState["activity_id"] == null)
        {
            return;
        }

        ACMS.DAO.BaseDAO myBaseDAO = new ACMS.DAO.BaseDAO();

        using (SqlConnection myConn = myBaseDAO.MyConn())
        {
            myConn.Open();

            using (SqlTransaction trans = myConn.BeginTransaction())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

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
                            sqlParams[0].Value = new Guid( ViewState["activity_id"].ToString());
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
                }

            }


        }

        GridView1.DataBind();

    }
}
