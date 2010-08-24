using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TServerControl;

public partial class MyPager : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, System.EventArgs e)
    {
        this.GV.DataBound += this.gv_DataBound;
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            //Me.BindddlCurrentPage()
            //Me.BindPageButton()
        }
    }

    //protected void gv_PageIndexChanged(object sender, System.EventArgs e)
    //{
    //    GridView gv = (GridView)sender;

    //    this.BindddlCurrentPage(gv.PageIndex);
    //    this.BindPageButton();
    //}

    protected void gv_DataBound(object sender, System.EventArgs e)
    {
        TGridView gv = (TGridView)sender;

        this.BindddlCurrentPage(gv.PageIndex);
        this.BindPageButton();
    }


   

    protected void ddlCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;

        GridViewCommandEventArgs args = new GridViewCommandEventArgs(ddl, new CommandEventArgs("Page", ddl.SelectedValue));
        this.RaiseBubbleEvent(ddl, args);
    }

}

partial class MyPager
{
    public event PageButtonRefreshedEventHandler PageButtonRefreshed;
    public delegate void PageButtonRefreshedEventHandler(object sender, EventArgs e);

    TGridView _GV;

    private TGridView GV
    {
        get
        {
            if (_GV == null)
            {

                Control parent = this.Parent;

                while (parent != null)
                {
                    if (parent is TGridView)
                    {
                        _GV = (TGridView)parent;

                        return _GV;   
                    }

                    parent = parent.Parent;
                }

                return null;

            }
            else
            {
                return _GV;            
            }
        }
    }

    public int TotalRowCount
    {
        get
        {
            return GV.TotalRowCount; 
        }

    }


    public void BindddlCurrentPage()
    {
        BindddlCurrentPage(0);
    }


    public void BindddlCurrentPage(int pageIndex)
    {
        if (GV == null)
        {
            return;
        }

        if (this.ddlCurrentPage.Items.Count == 0)
        {
            for (int i = 1; i <= this.GV.PageCount; i++)
            {
                this.ddlCurrentPage.Items.Add(i.ToString());
            }
        }

        this.ddlCurrentPage.Items[pageIndex].Selected = true;
    }

    public void BindPageButton()
    {
        if (GV == null)
        {
            return;
        }

        if (this.ddlCurrentPage.SelectedValue == "1")
        {
            this.btnFirst.Enabled = false;
            this.btnPrev.Enabled = false;
        }
        else
        {
            this.btnFirst.Enabled = true;
            this.btnPrev.Enabled = true;
        }

        if (this.ddlCurrentPage.SelectedValue == this.ddlCurrentPage.Items.Count.ToString())
        {
            this.btnLast.Enabled = false;
            this.btnNext.Enabled = false;
        }
        else
        {
            this.btnLast.Enabled = true;
            this.btnNext.Enabled = true;
        }

        this.lblRowCountInfo.Text = string.Format("Page <b>{0}</b>/{1}, {2} records", this.ddlCurrentPage.SelectedValue, this.ddlCurrentPage.Items.Count, this.TotalRowCount);

        if (PageButtonRefreshed != null)
        {
            PageButtonRefreshed(this, new EventArgs());
        }
    }

    public string RowCountInfo
    {
        get { return this.lblRowCountInfo.Text; }
        set { this.lblRowCountInfo.Text = value; }
    }



}
