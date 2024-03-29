﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_ManageRole_ManageRole : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (this.Master as MyMasterPage).PanelMainGroupingText = "角色人員管理";
        }
    }


    //開窗選人
    protected void btnQueryPerson_Click(object sender, EventArgs e)
    {
        try
        {
            OpenEmployeeSelector1.TitleName = "選取人員";
            OpenEmployeeSelector1.InitDataAndShow();
        }
        catch (Exception ex)
        {
            WriteErrorLog("QueryPerson", ex.Message, "0");

        }

    }

    //選取人員之後
    protected void GetEmployees_Click(object sender, GetEmployeeEventArgs e)
    {
        txtEmployee.Text = e.id;
    }



    //新增
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            ACMS.VO.RoleUserMappingVO myRoleUserMappingVO = new ACMS.VO.RoleUserMappingVO();
            myRoleUserMappingVO.role_id = Convert.ToInt32(ddlRole.SelectedValue);

            if (ddlRole.SelectedIndex > 1)
            {
                myRoleUserMappingVO.unit_id = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            else
            {
                myRoleUserMappingVO.unit_id = 0;
            }

            myRoleUserMappingVO.emp_id = txtEmployee.Text.Replace("(", "").Replace(")", "");

            ACMS.DAO.RoleUserMappingDAO myRoleUserMappingDAO = new ACMS.DAO.RoleUserMappingDAO();
            myRoleUserMappingDAO.InsertRoleUserMapping(myRoleUserMappingVO);

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("Insert", ex.Message, "0");

        }
    }

    //刪除
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();

            ACMS.VO.RoleUserMappingVO myRoleUserMappingVO = new ACMS.VO.RoleUserMappingVO();
            myRoleUserMappingVO.id = Convert.ToInt32(id);

            ACMS.DAO.RoleUserMappingDAO myRoleUserMappingDAO = new ACMS.DAO.RoleUserMappingDAO();
            myRoleUserMappingDAO.DeleteRoleUserMapping(myRoleUserMappingVO);

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("Delete", ex.Message, "0");

        }
    }

    //變更選取角色
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        PlaceHolder1.Visible = ((sender as DropDownList).SelectedIndex > 1);
    }


}

public partial class WebForm_ManageRole_ManageRole
{
    //protected bool BooleanConverter(object strTMP)
    //{
    //    return (strTMP.ToString() == "Y" ? true : false);
    //}
}