using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageRole_ManageUnit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (this.Master as MyMasterPage).PanelMainGroupingText = "主辦單位設定";
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        ACMS.VO.UnitVO myUnitVO = new ACMS.VO.UnitVO();
        myUnitVO.name = txtname.Text;

        ACMS.DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();
        if (myUnitDAO.chkDuplicateName(0, txtname.Text))
        {
            clsMyObj.ShowMessage(txtname.Text+ "已重覆.無法新增");
            return;
        }
        myUnitDAO.InsertUnit(myUnitVO);

        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ACMS.BO.UnitBO bbl = new ACMS.BO.UnitBO();
        int id = int.Parse (GridView1.DataKeys[e.RowIndex].Value.ToString ()) ;
        string name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtname_edit")).Text;
        if (bbl .chkDuplicateName(id,name))
        {
            clsMyObj.ShowMessage(name + "已重覆.無法存檔");
            e.Cancel = true;
            return;
        }

       

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ACMS.BO.UnitBO bbl = new ACMS.BO.UnitBO();
        int id = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
        string name = ((Label)GridView1.Rows[e.RowIndex].FindControl("Label1")).Text;
        if (bbl.isStart(id))
        {
            clsMyObj.ShowMessage(name + "已使用無法刪除資料");
            e.Cancel = true;
            return;
        }
            bbl.Delete(id);
            GridView1.DataBind();
            e.Cancel = true;
       
  
    }
  
}
