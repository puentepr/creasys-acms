using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebForm_RegistActivity_ActivityQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master as MyMasterPage).PanelMainGroupingText = "已報名活動查詢";
    }




    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView MyDataRowView = (e.Row.DataItem as DataRowView);
            int limit_count = (int)MyDataRowView["limit_count"];
            int RegistCount = (int)MyDataRowView["RegistCount"];

            if (limit_count - RegistCount > 0)
            {
                //未額滿顯示"可報名人數"
                (e.Row.FindControl("lblisfull") as Label).Text = (limit_count - RegistCount).ToString();
            }
            else
            {
                //已額滿顯示"額滿"
                (e.Row.FindControl("lblisfull") as Label).Text = "額滿";
            }


            LinkButton lbtnRegistEdit = (e.Row.FindControl("lbtnRegistEdit") as LinkButton);

            if (Convert.ToDateTime(MyDataRowView["activity_enddate"]) < DateTime.Now)
            {
                //已過期顯示"檢視"
                lbtnRegistEdit.Text = "檢視";
                lbtnRegistEdit.CommandName = "View";
            }
            else
            {
                //若已報則可"編輯"
                lbtnRegistEdit.Text = "編輯";
                lbtnRegistEdit.CommandName = "Edit";
            }

        }


    }

}