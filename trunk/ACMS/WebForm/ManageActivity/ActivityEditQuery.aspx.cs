using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class WebForm_ActivityEditQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //(this.Master as MyMasterPage).PanelMainGroupingText = "活動紀錄查詢";
        if (!IsPostBack)
        {
            Session.Remove("History");
            btnQuery_Click(null, null);
        }

    }

    //新增個人活動
    protected void btnAddActivity_Click(object sender, EventArgs e)
    {
        Session["form_mode"] = "new";
        Session["activity_type"] = "1";       
        Response.Redirect("ActivityEdit.aspx");
    }

    //新增團隊活動
    protected void btnAddActivityTeam_Click(object sender, EventArgs e)
    {
        Session["form_mode"] = "new";
        Session["activity_type"] = "2";
        Response.Redirect("ActivityEdit.aspx");
    }


    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["activity_name"].DefaultValue = txtactivity_name.Text;
        ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = txtactivity_startdate.Text;
        ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = txtactivity_enddate.Text;

        GridView1.DataBind();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataRowView drv = (DataRowView)(e.Row.DataItem);

            //if (string.IsNullOrEmpty(drv["NATIVE_NAME"].ToString().Trim()))
            //{
            //    (e.Row.FindControl("ibtnAdd") as ImageButton).Visible = true;
            //    (e.Row.FindControl("ibtnDel") as ImageButton).Visible = false;
            //}
            //else
            //{
            //    (e.Row.FindControl("ibtnAdd") as ImageButton).Visible = false;
            //    (e.Row.FindControl("ibtnDel") as ImageButton).Visible = true;
            //}
        }
    }

    //編輯活動
    protected void lbtnEditActivaty_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        Session["form_mode"] = "edit";
        Session["activity_id"] = activity_id;
        Response.Redirect("ActivityEdit.aspx");
    }

    //刪除活動
    protected void lbtnDelActivaty_Click(object sender, EventArgs e)
    {
        string theGUID = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

        ACMS.DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();

        myActivatyDAO.DeleteActivatyByID(theGUID);


        //刪除相關檔案
        DirectoryInfo myDirectoryInfo = new DirectoryInfo(Server.MapPath(Path.Combine("/ACMS/UpFiles", theGUID)));

        if (myDirectoryInfo.Exists)
        {
            myDirectoryInfo.Delete(true);
        }

        GridView1.DataBind();

    }


    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        foreach ( GridViewRow gr in GridView1.Rows )
        {
            ((Label)gr.FindControl("Label2")).Text = ((Label)gr.FindControl("Label2")).Text.Replace("\r\n", "<br/>");
        }
    }

   // 報名預覽
    protected void lbtnPreviewRegist_Click(object sender, EventArgs e)
    {
        string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
       // clsMyObj.ShowMessage (((LinkButton ) sender ).CommandArgument.ToString());

        Session["form_mode"] = "preview";
        Session["activity_id"] = activity_id;
        if (((LinkButton)sender).CommandArgument.ToString() == "1")
        {
            Response.Redirect("~/WebForm/RegistActivity/RegistActivity_Person.aspx");
        }
        else
        {
            Response.Redirect("~/WebForm/RegistActivity/RegistActivity_Team.aspx");
        }
    }

    
}