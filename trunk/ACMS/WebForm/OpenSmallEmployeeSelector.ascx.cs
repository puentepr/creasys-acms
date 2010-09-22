using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_OpenSmallEmployeeSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

public partial class WebForm_OpenSmallEmployeeSelector
{
    public void InitDataAndShow()
    {
        this.mpSearch.Show();    

    }

    public string TitleName
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }


    public string OkName
    {
        get { return btnOK.Text; }
        set { btnOK.Text = value; }
    }


}
