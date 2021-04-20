using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginSystemASP.NET
{
    public partial class NewPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNewPassword_Click(object sender, EventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ChangePassword(Session["username"].ToString(), txtPassword.Text);
            lblProgress.Text = dataProvider.ErrorMessage;
            dataProvider.LogIn(Session["username"].ToString(), txtPassword.Text);

            if (dataProvider.LoginProgress == 1)
            {
                lblProgress.Text = "Logged in!";
                Session["user"] = dataProvider.LogIn(Session["username"].ToString(), txtPassword.Text);
                Response.Redirect("User.aspx");
            }
            else
            {
                lblProgress.Text = "Wrong password or username!";
            }
        }
    }
}