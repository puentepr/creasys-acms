﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class HistoryWebForm_ActivityQuery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                for (int i = 2010; i <= DateTime.Now.Year + 1; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }

                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

                if (Request["type"] != null && Request["type"] == "off")
                {
                    (this.Master as MyMasterPage).PanelMainGroupingText = "歷史資料查詢";
                    GridView1.Columns[8].Visible = false;
                    ObjectDataSource1.SelectParameters["querytype"].DefaultValue = "off";
                }
                else
                {
                    (this.Master as MyMasterPage).PanelMainGroupingText = "報名狀態查詢";
                    ObjectDataSource1.SelectParameters["querytype"].DefaultValue = "";
                }

                btnQuery_Click(null, null);
            }
        }
        catch (Exception ex)
        {
            WriteErrorLog("PageLoad", ex.Message, "0");

        }

    }

    //查詢
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            string startdate = string.Format("{0}/{1}/1", ddlYear.SelectedValue, ddlMonth.SelectedValue);
            string enddate = string.Format("{0}/{1}/{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue)).ToString());

            ObjectDataSource1.SelectParameters["activity_startdate"].DefaultValue = startdate;
            ObjectDataSource1.SelectParameters["activity_enddate"].DefaultValue = enddate;
            ObjectDataSource1.SelectParameters["org_id"].DefaultValue = ddlUnit.SelectedValue;

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            WriteErrorLog("Query", ex.Message, "0");

        }


    }

    //檢視內容
    protected void lbtnViewActivity_Click(object sender, EventArgs e)
    {
        try
        {
            string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Value.ToString();
            string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

            Session["activity_id"] = activity_id;
            Session["activity_type"] = activity_type;
            Session["form_mode"] = "readonly";
            Session["History"] = "History";
            Response.Redirect("ActivityEdit.aspx");
        }
        catch (Exception ex)
        {
            WriteErrorLog("ViewActivity", ex.Message, "0");

        }

    }
    public string GetCustomField(string guid, string boss_id)
    {
        ACMS.DAO.CustomFieldValueDAO myCustFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(new Guid(guid), boss_id);
        ACMS.BO.CustomFieldItemBO myCustFieldItemBO = new ACMS.BO.CustomFieldItemBO();
        List<ACMS.VO.CustomFieldItemVO> custFieldItemList;
        string custFieldSt = "";
        decimal ttl = 0;
        string[] FieldIDs;

        foreach (ACMS.VO.CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        {
            if (custFieldVO.field_control.ToLower() == "textbox")
            {
                custFieldSt += custFieldVO.field_name + ":" + custFieldVO.field_value + "";
            }
            if (custFieldVO.field_control.ToLower() == "textboxlist")
            {
                ttl = 0;
                custFieldSt += custFieldVO.field_name + ":";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (ACMS.VO.CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + ":" + custFieldItem.field_item_text + "";
                            ttl += decimal.Parse(custFieldItem.field_item_text);
                        }
                }

                custFieldSt += "合計: " + ttl.ToString() + "";

            }
            if (custFieldVO.field_control.ToLower() == "checkboxlist")
            {


                custFieldSt += custFieldVO.field_name + ":";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (ACMS.VO.CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

                        }
                }
                custFieldSt += "";

            }
            if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
            {
                custFieldSt += custFieldVO.field_name + ":";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (ACMS.VO.CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

                        }
                }
                custFieldSt += "";
            }


        }

        return custFieldSt;
    }
    //匯出名單
    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[0].ToString();
            string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

            //「匯出名單」的功能需帶出「報名編號（團隊編號）、部門、工號、姓名、分機、e-mail與額外填寫欄位」等欄位資訊

            DataTable table = new DataTable();

            ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            table = myActivityRegistDAO.SelectEmployeesByID(new Guid(activity_id), activity_type);
            if (table.Rows.Count == 0)
            {
                clsMyObj.ShowMessage("沒有資料");
                return;
            }


            DataTable dt = new DataTable();


            dt.Columns.Add("報名編號", System.Type.GetType("System.String"));
            dt.Columns.Add("部門代號", System.Type.GetType("System.String"));
            dt.Columns.Add("部門", System.Type.GetType("System.String"));
            dt.Columns.Add("工號", System.Type.GetType("System.String"));
            dt.Columns.Add("姓名", System.Type.GetType("System.String"));
            dt.Columns.Add("分機", System.Type.GetType("System.String"));
            dt.Columns.Add("EMAIL", System.Type.GetType("System.String"));
            dt.Columns.Add("進度狀態", System.Type.GetType("System.String"));
            //=================================================================
            ACMS.DAO.CustomFieldValueDAO myCustFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();


            List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList;
            if (table.Rows[0]["activity_type"].ToString() == "2")
            {
                myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(new Guid(table.Rows[0]["id"].ToString()), table.Rows[0]["boss_id"].ToString());
                dt.Columns.Add("隊名", System.Type.GetType("System.String"));
            }
            else
            {
                myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(new Guid(table.Rows[0]["id"].ToString()), table.Rows[0]["emp_id"].ToString());
            }
            dt.Columns.Add("身份證_護照", System.Type.GetType("System.String"));
            ACMS.BO.CustomFieldItemBO myCustFieldItemBO = new ACMS.BO.CustomFieldItemBO();
            List<ACMS.VO.CustomFieldItemVO> myFieldVOS;
            foreach (ACMS.VO.CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
            {

                if (custFieldVO.field_control.ToLower() == "textbox")
                {
                    dt.Columns.Add(custFieldVO.field_name, System.Type.GetType("System.String"));
                }

                if (custFieldVO.field_control.ToLower() == "textboxlist")
                {

                    myFieldVOS = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                    foreach (ACMS.VO.CustomFieldItemVO myFieldvo in myFieldVOS)
                    {
                        dt.Columns.Add(custFieldVO.field_name + '_' + myFieldvo.field_item_name, System.Type.GetType("System.Decimal"));
                    }
                    dt.Columns.Add(custFieldVO.field_name + "合計", System.Type.GetType("System.Decimal"));
                }
                if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
                {
                    // dt.Columns.Add(custFieldVO.field_name, System.Type.GetType("System.String"));
                    myFieldVOS = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                    foreach (ACMS.VO.CustomFieldItemVO myFieldvo in myFieldVOS)
                    {
                        dt.Columns.Add(custFieldVO.field_name + '_' + myFieldvo.field_item_name, System.Type.GetType("System.String"));
                    }
                }
                if (custFieldVO.field_control.ToLower() == "checkboxlist")
                {
                    //dt.Columns.Add(custFieldVO.field_name, System.Type.GetType("System.String"));
                    myFieldVOS = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                    foreach (ACMS.VO.CustomFieldItemVO myFieldvo in myFieldVOS)
                    {
                        dt.Columns.Add(custFieldVO.field_name + '_' + myFieldvo.field_item_name, System.Type.GetType("System.String"));
                    }
                }


            }


            // dt.Columns.Add("自訂欄位", System.Type.GetType("System.String"));

            string teamName = "";
            int seqno = 0;
            int seqno1 = 0;
            DataRow dtDr;
            foreach (DataRow dr in table.Rows)
            {
                dtDr = dt.NewRow();
                if (dr["activity_type"].ToString() == "2")
                {
                    if (teamName != dr["boss_id"].ToString().Trim())
                    {
                        seqno++;
                    }
                    if (seqno > int.Parse(dr["team_max"].ToString()))
                    {
                        seqno1++;
                    }


                }
                else
                {
                    seqno++;
                    if (seqno > int.Parse(dr["team_max"].ToString()))
                    {
                        seqno1++;
                    }
                }
                if (seqno1 > 0)
                {
                    dtDr["報名編號"] = "備取:" + seqno1.ToString();
                }
                else
                {
                    dtDr["報名編號"] = "正取:" + seqno.ToString();
                }
                dtDr["部門代號"] = dr["DEPT_ID"].ToString();
                dtDr["部門"] = dr["C_DEPT_NAME"].ToString();
                dtDr["工號"] = dr["WORK_ID"].ToString();
                dtDr["姓名"] = dr["NATIVE_NAME"].ToString();
                dtDr["分機"] = dr["OFFICE_PHONE"].ToString();
                dtDr["EMAIL"] = dr["OFFICE_MAIL"].ToString();
                dtDr["進度狀態"] = dr["check_status"].ToString();
                dtDr["身份證_護照"] = dr["idno"].ToString();
                if (dr["activity_type"].ToString() == "2")
                {
                    if (dr["boss_id"].ToString() != teamName)
                    {
                        // dtDr["自訂欄位"] = GetCustomField(dr["id"].ToString(), dr["boss_id"].ToString());
                        GetCustomFieldNew(dr["id"].ToString(), dr["boss_id"].ToString(), ref dtDr);
                        dtDr["隊名"] = dr["team_name"].ToString().Trim();
                    }


                    teamName = dr["boss_id"].ToString().Trim();
                }
                else
                {
                    // dtDr["自訂欄位"] = GetCustomField(dr["id"].ToString(), dr["emp_id"].ToString());
                    GetCustomFieldNew(dr["id"].ToString(), dr["emp_id"].ToString(), ref dtDr);
                }
                dt.Rows.Add(dtDr);
            }

            if (table != null && table.Rows.Count > 0)
            {
                table.Columns[0].ColumnName = "員工編號";
                table.Columns[1].ColumnName = "員工姓名";
                table.Columns[2].ColumnName = "員工部門";

                // 產生 Excel 資料流。
                //MemoryStream ms = DataTableRenderToExcel.RenderDataTableToExcel(table) as MemoryStream;
                MemoryStream ms = DataTableRenderToExcel.RenderDataTableToExcel(dt) as MemoryStream;
                // 設定強制下載標頭。
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", Server.UrlEncode("RegistedList")));
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
        catch (Exception ex)
        {
            WriteErrorLog("ExportExcel", ex.Message, "0");

        }


    }

    public void GetCustomFieldNew(string guid, string boss_id, ref DataRow dr)
    {
        ACMS.DAO.CustomFieldValueDAO myCustFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(new Guid(guid), boss_id);
        ACMS.BO.CustomFieldItemBO myCustFieldItemBO = new ACMS.BO.CustomFieldItemBO();
        List<ACMS.VO.CustomFieldItemVO> custFieldItemList;
        string custFieldSt = "";
        decimal ttl = 0;
        string[] FieldIDs;

        foreach (ACMS.VO.CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        {
            if (custFieldVO.field_control.ToLower() == "textbox")
            {
                dr[custFieldVO.field_name] = custFieldVO.field_value;
            }
            custFieldSt = "";
            if (custFieldVO.field_control.ToLower() == "textboxlist")
            {
                ttl = 0;

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (ACMS.VO.CustomFieldItemVO custFieldItem in custFieldItemList)
                    {
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + ":" + custFieldItem.field_item_text + "";
                            ttl += decimal.Parse(custFieldItem.field_item_text);
                            dr[custFieldVO.field_name + "_" + custFieldItem.field_item_name] = decimal.Parse(custFieldItem.field_item_text);
                        }



                    }
                }

                custFieldSt += "合計: " + ttl.ToString() + "";
                dr[custFieldVO.field_name + "合計"] = ttl;

            }
            if (custFieldVO.field_control.ToLower() == "checkboxlist")
            {


                custFieldSt = "";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (ACMS.VO.CustomFieldItemVO custFieldItem in custFieldItemList)
                    {
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + custFieldItem.field_item_text + ",";
                            dr[custFieldVO.field_name + "_" + custFieldItem.field_item_name] = "V";
                        }

                    }
                }
                //dr[custFieldVO.field_name] = custFieldSt;  
            }
            if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
            {
                custFieldSt += "";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (ACMS.VO.CustomFieldItemVO custFieldItem in custFieldItemList)
                    {
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + custFieldItem.field_item_text + ",";
                            dr[custFieldVO.field_name + "_" + custFieldItem.field_item_name] = "V";
                        }
                    }
                }
                //dr[custFieldVO.field_name] = custFieldSt;
            }


        }


    }

    //取消報名
    protected void lbtnCancelRegist_Click(object sender, EventArgs e)
    {
        //string activity_id = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[0].ToString();
        //string activity_type = GridView1.DataKeys[((sender as LinkButton).NamingContainer as GridViewRow).RowIndex].Values[1].ToString();

        //string regist_deadline = (sender as LinkButton).CommandArgument;
        //string cancelregist_deadline = (sender as LinkButton).CommandName;

        //if (activity_type == "1")
        //{
        //    OpenRegistedByMeEmpSelector1.activity_id = activity_id;
        //    OpenRegistedByMeEmpSelector1.regist_by = "";
        //    OpenRegistedByMeEmpSelector1.regist_deadline = regist_deadline;
        //    OpenRegistedByMeEmpSelector1.cancelregist_deadline = cancelregist_deadline;
        //    OpenRegistedByMeEmpSelector1.InitDataAndShow();
        //}
        //else
        //{
        //    Response.Redirect("RegistActivity_Team.aspx");
        //}

    }


    ////取消個人報名後
    //protected void CancelPersonRegist_OnClick(object sender, EventArgs e)
    //{
    //    GridView1.DataBind();
    //}

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DateTime dd;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ((Label)e.Row.FindControl("Label1")).Text = ((Label)e.Row.FindControl("Label1")).Text.Replace("\r\n", "<br/>");
            dd=DateTime.Parse ( ((Label)e.Row.FindControl("lblactivity_startdate")).Text);
            ((Label)e.Row.FindControl("lblactivity_startdate")).Text=dd.ToString("yyyy/MM/dd HH:mm");
             dd=DateTime.Parse ( ((Label)e.Row.FindControl("lblactivity_enddate")).Text);
            ((Label)e.Row.FindControl("lblactivity_enddate")).Text=dd.ToString("yyyy/MM/dd HH:mm");
            dd = DateTime.Parse(((Label)e.Row.FindControl("lblregist_deadline")).Text);
            ((Label)e.Row.FindControl("lblregist_deadline")).Text = dd.ToString("yyyy/MM/dd");
            dd = DateTime.Parse(((Label)e.Row.FindControl("lblcancelregist_deadline")).Text);
            ((Label)e.Row.FindControl("lblcancelregist_deadline")).Text = dd.ToString("yyyy/MM/dd");


        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            GridView1.Visible = false;
            lblGrideView1.Visible = true;

        }
        else
        {
            GridView1.Visible = true;
            lblGrideView1.Visible = false;
        }

        //foreach (GridViewRow gr in GridView1.Rows)
        //{
        //    ((Label)gr.FindControl("lblactivity_startdate")).Text = ((Label)gr.FindControl("lblactivity_startdate")).Text.Replace("-", "/").Replace("T", " ");
        //    ((Label)gr.FindControl("lblactivity_enddate")).Text = ((Label)gr.FindControl("lblactivity_enddate")).Text.Replace("-", "/").Replace("T", " ");
        //}
    }
    protected void rblActivity_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblActivity_type.SelectedValue == "1")
        {
            GridView1.Columns[6].HeaderText = "報名人數";
        }
        else
        {
            GridView1.Columns[6].HeaderText = "報名隊數";
        }
        GridView1.DataBind();

    }
}