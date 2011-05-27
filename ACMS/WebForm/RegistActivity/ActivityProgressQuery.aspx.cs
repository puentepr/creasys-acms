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

public partial class WebForm_RegistActivity_ActivityProgressQuery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjectDataSource_Activity.SelectParameters["emp_id"].DefaultValue = clsAuth.ID;
            (this.Master as MyMasterPage).PanelMainGroupingText = "活動進度查詢";
           


        }

    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label NATIVE_NAMELabel;
            Label emp_idLabel;
            Label lblSEQNO;
            Label check_statusLabel;
            ACMS.DAO.LoginDAO myLoginDAO = new ACMS.DAO.LoginDAO();
            string UserData;

            string userName = Context.User.Identity.Name;
            userName = userName.Substring(userName.IndexOf("\\") + 1);
            myLoginDAO.CheckLogin(userName, out UserData);

            check_statusLabel = e.Item.FindControl("check_statusLabel") as Label;
            if (check_statusLabel.Text.IndexOf("備") > -1)
            {

                check_statusLabel.Visible = false;
            }
            
            if (DataList1.DataKeys[e.Item.ItemIndex].ToString() == clsAuth.ID)
            {



                e.Item.ForeColor = System.Drawing.Color.Red;
                NATIVE_NAMELabel = e.Item.FindControl("NATIVE_NAMELabel") as Label;
                emp_idLabel = e.Item.FindControl("emp_idLabel") as Label;
                NATIVE_NAMELabel.ForeColor = System.Drawing.Color.Red;
                emp_idLabel.ForeColor = System.Drawing.Color.Red;
                lblSEQNO = e.Item.FindControl("lblSEQNO") as Label;
                lblSEQNO.ForeColor = System.Drawing.Color.Red;
                check_statusLabel = e.Item.FindControl("check_statusLabel") as Label;
                check_statusLabel.ForeColor = System.Drawing.Color.Red;


            }
            else
            {
                if ((UserData.IndexOf("2") == -1 && UserData.IndexOf("1") == -1 && UserData.IndexOf("3") == -1) || UserData == "")//不是管理者才不秀全名
                {
                    NATIVE_NAMELabel = e.Item.FindControl("NATIVE_NAMELabel") as Label;
                    emp_idLabel = e.Item.FindControl("emp_idLabel") as Label;

                    NATIVE_NAMELabel.Text = NATIVE_NAMELabel.Text.Substring(0, 1) + "XX";
                    emp_idLabel.Text = "";
                }



            }

            check_statusLabel = e.Item.FindControl("check_statusLabel") as Label;
            if (check_statusLabel.Text == "已報到")
            {
                e.Item.BackColor = System.Drawing.Color.LightGreen;
            }
            if (check_statusLabel.Text == "未報到")
            {
                e.Item.BackColor = System.Drawing.Color.LightGray;
            }
            if (check_statusLabel.Text == "已完成")
            {
                e.Item.BackColor = System.Drawing.Color.Yellow;
            }

        }
        catch (Exception ex)
        {
            WriteErrorLog("DataBind", ex.Message, "0");

        }
    }

}
