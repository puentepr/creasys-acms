using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = clsAuth.UserID + " " + clsAuth.email + " " +clsAuth.emp_cname+ " " +clsAuth.dept_id+ " " +clsAuth.dept_name+ " " +clsAuth.role_ids+ " " +clsAuth.role_names;




    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
