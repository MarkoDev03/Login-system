using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginSystemASP.NET
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.LogIn(txtUsername.Text, txtPassword.Text);

            if(dataProvider.LoginProgress == 1)
            {
                lblProgress.Text = "Logged in!";
                Session["user"] = dataProvider.LogIn(txtUsername.Text, txtPassword.Text);
                Response.Redirect("User.aspx");
            }
            else
            {
                lblProgress.Text = "Wrong password or username!";
            }
        }
    }
}