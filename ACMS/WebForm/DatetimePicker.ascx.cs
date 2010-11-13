using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_DatetimePicker : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

public partial class WebForm_DatetimePicker
{

    public DateTime? DateTimeValue
    {
        get
        {
            DateTime? myDateTime = null;

            try
            {
                myDateTime = Convert.ToDateTime(string.Format("{0} {1}:{2}:00", txtDate.Text, ddlHour.SelectedValue, ddlMinute.SelectedValue));

            }
            catch { }

            return myDateTime;

        }
        set
        {
            DateTime? myDateTime = value;

            if (myDateTime.HasValue)
            {
                txtDate.Text = myDateTime.Value.ToString("yyyy/MM/dd");
                ddlHour.SelectedValue = myDateTime.Value.Hour.ToString();
                ddlMinute.SelectedValue = myDateTime.Value.Minute.ToString();
            }

        }


    }

    public string ValidationGroup
    {
        set
        {
            chk_txtDate.ValidationGroup = value;
            chk_txtDate2.ValidationGroup = value;

       }


    }


    public string RequiredErrorMessage
    {
        set
        {
            chk_txtDate.ErrorMessage = value;
        }
    }

    public string FormateErrorMessage
    {
        set
        {
            chk_txtDate2.ErrorMessage = value;
        }
    }



}
