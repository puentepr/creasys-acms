using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class WebForm_ActivityManagementQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //(this.Master as MyMasterPage).PanelMainGroupingText = "活動紀錄查詢";
        if (!IsPostBack)
        {
            btnQuery_Click(null, null);
        }

    }

    //新增個人活動
    protected void btnAddActivity_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityEdit.aspx?ActivityType=1");
    }

    //新增團隊活動
    protected void btnAddActivityTeam_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityEdit.aspx?ActivityType=2");
    }


    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["activity_name"].DefaultValue = txtactivity_name.Text;
        ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = txtactivity_startdate.Text;
        ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = txtactivity_enddate.Text;

        GridView1.DataBind();
    }



    //編輯活動
    protected void lbtnEditActivaty_Click(object sender, EventArgs e)
    {
        string theGUID = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
        Response.Redirect(string.Format("ActivityEdit.aspx?ActivityID={0}", theGUID));
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


}