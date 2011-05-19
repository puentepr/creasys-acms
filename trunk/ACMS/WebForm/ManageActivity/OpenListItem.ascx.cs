using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageActivity_OpenListItem : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACMS.VO.CustomFieldItemVO myCustomFieldItemVO = new ACMS.VO.CustomFieldItemVO();

        myCustomFieldItemVO.field_id = FieldID;
        myCustomFieldItemVO.field_item_name = txtfield_item_name.Text;
        myCustomFieldItemVO.field_item_text = txtfield_item_text.Text;

        ACMS.DAO.CustomFieldItemDAO myCustomFieldItemDAO = new ACMS.DAO.CustomFieldItemDAO();
        myCustomFieldItemDAO.INSERT(myCustomFieldItemVO);

        GridView_CustomFieldItem.DataBind();

        txtfield_item_name.Text = "";
        txtfield_item_text.Text = "";

        this.mpSearch.Show();
    }
    protected void lbtnDel_CustomFieldItem_Click(object sender, EventArgs e)
    {
        ACMS.VO.CustomFieldItemVO myCustomFieldItemVO = new ACMS.VO.CustomFieldItemVO();

        int intfield_item_id = (int)GridView_CustomFieldItem.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value;

        ACMS.DAO.CustomFieldItemDAO myCustomFieldItemDAO = new ACMS.DAO.CustomFieldItemDAO();
        myCustomFieldItemDAO.DELETE(intfield_item_id);

        GridView_CustomFieldItem.DataBind();

        this.mpSearch.Show();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }
}

public partial class WebForm_ManageActivity_OpenListItem
{
    public int FieldID
    {
        get
        {
            if (ViewState["FieldID"] == null)
            {
                ViewState["FieldID"] =0;
            }

            return Convert.ToInt32(ViewState["FieldID"]);
        }
        set { ViewState["FieldID"] = value; }
    }


    public void InitDataAndShow(int field_id,string controltype)
    {
        this.mpSearch.Show();

        if (controltype == "textboxlist")
        {
            lblValue.Visible = true;
            txtfield_item_text.Visible = true;

            GridView_CustomFieldItem.Columns[1].Visible = true;
        }
        else
        {
            lblValue.Visible = false;
            txtfield_item_text.Visible = false;

            GridView_CustomFieldItem.Columns[1].Visible = false;
        }

        FieldID = field_id;

        ObjectDataSource_CustomFieldItem.SelectParameters["field_id"].DefaultValue = field_id.ToString();

        txtfield_item_name.Text = "";
        txtfield_item_text.Text = "";
    }

}

