using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageRole_ManageUnit : System.Web.UI.Page
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
        myUnitDAO.InsertUnit(myUnitVO);

        GridView1.DataBind();
    }
}
