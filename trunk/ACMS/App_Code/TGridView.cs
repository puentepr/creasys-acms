﻿using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TServerControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TGridView runat=server></{0}:TGridView>")]
    public class TGridView : GridView
    {

        #region "EmptyGridView"

        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            int rows = base.CreateChildControls(dataSource, dataBinding);

            //  no data rows created, create empty table if enabled
            if (rows == 0 && (this.ShowFooterWhenEmpty || this.ShowHeaderWhenEmpty))
            {
                //  create the table
                Table table = this.CreateChildTable();

                DataControlField[] fields;
                if (this.AutoGenerateColumns)
                {
                    PagedDataSource source = new PagedDataSource();
                    source.DataSource = dataSource;

                    System.Collections.ICollection autoGeneratedColumns = this.CreateColumns(source, true);
                    fields = new DataControlField[autoGeneratedColumns.Count];
                    autoGeneratedColumns.CopyTo(fields, 0);
                }
                else
                {
                    fields = new DataControlField[this.Columns.Count];
                    this.Columns.CopyTo(fields, 0);
                }

                if (this.ShowHeaderWhenEmpty)
                {
                    //  create a new header row
                    GridViewRow headerRow = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
                    this.InitializeRow(headerRow, fields);

                    //  add the header row to the table
                    table.Rows.Add(headerRow);
                }

                //  create the empty row
                GridViewRow emptyRow = new GridViewRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.ColumnSpan = fields.Length;
                cell.Width = Unit.Percentage(100);

                //  respect the precedence order if both EmptyDataTemplate
                //  and EmptyDataText are both supplied ...
                if (this.EmptyDataTemplate != null)
                {
                    this.EmptyDataTemplate.InstantiateIn(cell);
                }
                else if (!string.IsNullOrEmpty(this.EmptyDataText))
                {
                    cell.Controls.Add(new LiteralControl(EmptyDataText));
                }

                emptyRow.Cells.Add(cell);
                table.Rows.Add(emptyRow);

                if (this.ShowFooterWhenEmpty)
                {
                    //  create footer row
                    GridViewRow footerRow = base.CreateRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
                    this.InitializeRow(footerRow, fields);

                    //  add the footer to the table
                    table.Rows.Add(footerRow);
                }

                this.Controls.Clear();
                this.Controls.Add(table);
            }

            return rows;
        }

        //[Category("Behavior")]
        //[Themeable(true)]
        //[Bindable(BindableSupport.No)]
        public bool ShowHeaderWhenEmpty
        {
            get
            {
                if (this.ViewState["ShowHeaderWhenEmpty"] == null)
                {
                    this.ViewState["ShowHeaderWhenEmpty"] = false;
                }

                return (bool)this.ViewState["ShowHeaderWhenEmpty"];
            }
            set
            {
                this.ViewState["ShowHeaderWhenEmpty"] = value;
            }
        }

        //[Category("Behavior")]
        //[Themeable(true)]
        //[Bindable(BindableSupport.No)]
        public bool ShowFooterWhenEmpty
        {
            get
            {
                if (this.ViewState["ShowFooterWhenEmpty"] == null)
                {
                    this.ViewState["ShowFooterWhenEmpty"] = false;
                }

                return (bool)this.ViewState["ShowFooterWhenEmpty"];
            }
            set
            {
                this.ViewState["ShowFooterWhenEmpty"] = value;
            }
        }


        #endregion

        #region "MyGridView"

        protected override void PerformSelect()
        {
            if (base.DataSourceObject is SqlDataSource)
            {
                (base.DataSourceObject as SqlDataSource).Selected += this.SqlDataSource1_Selected;
            }
            else if (base.DataSourceObject is ObjectDataSource)
            {
                (base.DataSourceObject as ObjectDataSource).Selected += this.ObjectDataSource1_Selected;
            }

            base.PerformSelect();


        }

        protected void SqlDataSource1_Selected(object sender, System.Web.UI.WebControls.SqlDataSourceStatusEventArgs e)
        {
            //MyObj.ShowMessage(e.AffectedRows)
            //MyObj.ShowMessage(e.AffectedRows)
            //Me.lblRowCountInfo.Text = String.Format("Page <b>{0}</b>/{1}, {2} records", Me.ddlPageIndex.SelectedValue, Me.ddlPageIndex.Items.Count, e.AffectedRows)
            ViewState["TotalRowCount"] = e.AffectedRows;
        }

        protected void ObjectDataSource1_Selected(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
        {
            //MyObj.ShowMessage(e.AffectedRows)
            if (e.ReturnValue is DataTable)
            {
                ViewState["TotalRowCount"] = (e.ReturnValue as DataTable).Rows.Count;

            }
            else
            {
                ViewState["TotalRowCount"] = 0;
            
            }
        }


        protected void MyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //If e.Row.Attributes("DefaultBackColor") = "" Then
                // e.Row.Attributes("DefaultBackColor") = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor)
                //End If
                //Response.Write(Me.AlternatingRowStyle.BackColor)


                if (this.AllowHoverEffect == true)
                {
                    if (e.Row.RowState == DataControlRowState.Edit)
                    {
                        e.Row.Attributes["onmouseout"] = string.Format("highlight(this, '{0}');", System.Drawing.ColorTranslator.ToHtml(this.EditRowStyle.BackColor));
                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes["onmouseout"] = string.Format("highlight(this, '{0}');", System.Drawing.ColorTranslator.ToHtml(this.AlternatingRowStyle.BackColor));
                    }
                    else if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes["onmouseout"] = string.Format("highlight(this, '{0}');", System.Drawing.ColorTranslator.ToHtml(this.RowStyle.BackColor));
                    }
                    else if (e.Row.RowState == DataControlRowState.Selected)
                    {
                        e.Row.Attributes["onmouseout"] = string.Format("highlight(this, '{0}');", System.Drawing.ColorTranslator.ToHtml(this.SelectedRowStyle.BackColor));
                    }
                    else
                    {
                        e.Row.Attributes["onmouseout"] = string.Format("highlight(this, '{0}');", System.Drawing.ColorTranslator.ToHtml(this.RowStyle.BackColor));
                    }

                    e.Row.Attributes["onmouseover"] = string.Format("highlight(this, '{0}');", System.Drawing.ColorTranslator.ToHtml(this.MouseOverColor));
                }

                //MyObj.ShowMessage(String.Format("{0} {1}", e.Row.RowIndex, e.Row.RowState))

                if (this.AllowHoverSelect == true)
                {
                    // 非修改模式下才可自由點選
                    if (this.EditIndex == -1)
                    {
                        e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this, "Select$" + e.Row.RowIndex.ToString());
                    }
                    //e.Row.Attributes("onclick") = "this.onmouseover();return false;"
                    //e.Row.Attributes("onclick") = "this.onmouseover();" + Me.Page.ClientScript.GetPostBackClientHyperlink(Me, "Select$" + e.Row.RowIndex.ToString())

                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {

                if (this.AllowSorting == true)
                {
                    for (int i = 0; i <= this.Columns.Count - 1; i++)
                    {
                        if (string.IsNullOrEmpty(this.MySortExpression) & !string.IsNullOrEmpty(this.Columns[i].SortExpression))
                        {
                            this.MySortExpression = this.Columns[i].SortExpression;
                        }

                        if (!string.IsNullOrEmpty(this.MySortExpression) & this.MySortExpression == this.Columns[i].SortExpression)
                        {
                            Image img = new Image();

                            if (this.MySortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
                            {
                                img.ImageUrl = this.AscendingImageUrl;
                            }
                            else
                            {
                                img.ImageUrl = this.DescendingImageUrl;
                            }

                            img.Style.Add("margin-left", "3pt");

                            e.Row.Cells[i].Controls.Add(img);
                        }
                    }
                }
            }
        }


        public System.Drawing.Color MouseOverColor
        {
            get { return (System.Drawing.Color)ViewState["MouseOverColor"]; }
            set { ViewState["MouseOverColor"] = value; }
        }

        public SortDirection MySortDirection
        {
            get { return (SortDirection)ViewState["MySortDirection"]; }
            set { ViewState["MySortDirection"] = value; }
        }

        public string MySortExpression
        {
            get { return (string)ViewState["MySortExpression"]; }
            set { ViewState["MySortExpression"] = value; }
        }

        public string AscendingImageUrl
        {
            get { return (string)ViewState["AscendingImageUrl"]; }
            set { ViewState["AscendingImageUrl"] = value; }
        }

        public string DescendingImageUrl
        {
            get { return (string)ViewState["DescendingImageUrl"]; }
            set { ViewState["DescendingImageUrl"] = value; }
        }

        public string SortSQL
        {
            get
            {
                if (!string.IsNullOrEmpty(this.MySortExpression))
                {
                    return this.MySortExpression + " " + (this.MySortDirection == System.Web.UI.WebControls.SortDirection.Ascending ? "ASC" : "DESC");
                }
                else
                {
                    return "";
                }
            }
        }

        public string OrderBySQL
        {
            get
            {
                if (!string.IsNullOrEmpty(this.MySortExpression))
                {
                    return "Order by " + this.MySortExpression + " " + (this.MySortDirection == System.Web.UI.WebControls.SortDirection.Ascending ? "ASC" : "DESC");
                }
                else
                {
                    return "";
                }
            }
        }

        protected override void OnSorting(System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            if (this.MySortExpression == e.SortExpression)
            {

                if (this.MySortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
                {
                    this.MySortDirection = System.Web.UI.WebControls.SortDirection.Descending;
                }
                else
                {
                    this.MySortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                }
            }
            else
            {
                this.MySortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                this.MySortExpression = e.SortExpression;
            }

            base.OnSorting(e);
        }

        public bool AllowHoverSelect
        {
            get
            {
                if (ViewState["AllowHoverSelect"] == null)
                {
                    return true;
                }
                else
                {
                    return Convert.ToBoolean(ViewState["AllowHoverSelect"]);
                }
            }
            set { ViewState["AllowHoverSelect"] = value; }
        }

        public bool AllowHoverEffect
        {
            get
            {
                if (ViewState["AllowHoverEffect"] == null)
                {
                    return true;
                }
                else
                {
                    return Convert.ToBoolean(ViewState["AllowHoverEffect"]);
                }
            }
            set { ViewState["AllowHoverEffect"] = value; }
        }

        /// <summary>
        /// only for DataSourceID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int TotalRowCount
        {
            get { return Convert.ToInt32(ViewState["TotalRowCount"]); }
            set { ViewState["TotalRowCount"] = value; }
        }

        #endregion

    }
}