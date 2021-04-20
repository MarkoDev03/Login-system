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
        DataProvider dataProvider;
        protected void Page_Load(object sender, EventArgs e)
        {
            dataProvider = new DataProvider();
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                Random rnd = new Random();
                Session["code"] = rnd.Next(1000, 9999).ToString();
                dataProvider.ResetPassword(txtUsername.Text, Session["code"].ToString());
                Label3.Text = dataProvider.UserEmail;

                Session["username"] = txtUsername.Text;

                Response.Redirect("ResetPassword.aspx");
            }
            else
            {
                lblProgress.Text = "Enter your username!";
            }

        }
    }
}