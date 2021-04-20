using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginSystemASP.NET
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["username"].ToString();
            lblProgress.Text = username;
            DataProvider dataProvider = new DataProvider();
            lblProgress.Text = dataProvider.ResetPasswordCode;

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            if(txtCode.Text.Trim() == Session["code"].ToString())
            {
                Response.Redirect("NewPassword.aspx");
            }
            else
            {
                lblProgress.Text = "Wrong code!";
            }
        }
    }
}