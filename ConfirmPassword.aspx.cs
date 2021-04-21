using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginSystemASP.NET
{
    public partial class ConfirmPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheckPasswod_Click(object sender, EventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.CheckPreviousPasswordValidation(Session["username"].ToString(), txtPassword.Text);

            if(dataProvider._isPasswordChecked == true)
            {
                Response.Redirect("NewPassword.aspx");
            }
            else
            {
                lblProgress.Text = "Wrong password!";
            }
        }
    }
}