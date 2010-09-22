using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

                
                LinkButton lbtnRegistEdit = (e.Row.FindControl("lbtnRegistEdit") as LinkButton);
                if (Convert.ToDateTime(MyDataRowView["activity_startdate"]) > DateTime.Now && MyDataRowView["emp_id"]==DBNull.Value)
                {
                    //若未過期且本人沒報名則可以報名
                    lbtnRegistEdit.Text = "報名";
                    lbtnRegistEdit.CommandName = "Regist";
                }
                else
                {
                    //若已過期則不可以報名
                    lbtnRegistEdit.Visible = false;
                }

            }
            else
            {
                //已額滿顯示"額滿"
                (e.Row.FindControl("lblisfull") as Label).Text = "額滿";
            }

        }


    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView MyDataRowView = (e.Row.DataItem as DataRowView);
            int limit_count = (int)MyDataRowView["limit_count"];
            int RegistCount = (int)MyDataRowView["RegistCount"];

            if (limit_count - RegistCount > 0)
            {
                //未額滿顯示"可報名人數"
                (e.Row.FindControl("lblisfull0") as Label).Text = (limit_count - RegistCount).ToString();


                LinkButton lbtnRegistEdit = (e.Row.FindControl("lbtnRegistEdit0") as LinkButton);
                if (Convert.ToDateTime(MyDataRowView["activity_startdate"]) > DateTime.Now && MyDataRowView["emp_id"] == DBNull.Value)
                {
                    //若未過期且本人沒報名則可以報名
                    lbtnRegistEdit.Text = "報名";
                    lbtnRegistEdit.CommandName = "Regist";
                }
                else
                {
                    //若已過期則不可以報名
                    lbtnRegistEdit.Visible = false;
                }

            }
            else
            {
                //已額滿顯示"額滿"
                (e.Row.FindControl("lblisfull0") as Label).Text = "額滿";
            }

        }
    }
}