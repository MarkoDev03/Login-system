using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginSystemASP.NET
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            DataProvider dataProvider = new DataProvider();

            string gender = "";

            if (chcFemale.Checked == true)
            {
                chcMale.Enabled = false;
                gender = "F";
            }
            else if (chcMale.Checked == true)
            {
                chcFemale.Enabled = false;
                gender = "M";
            }
            if (dataProvider.DoesThisAccountExists(txtUsername.Text) == 0)
            {
                dataProvider.CreateAccount(txtUsername.Text, txtPassword.Text, txtBiography.Text, gender);
            }
            else
            {
                lblProgress.Text = "This account already exists!";
            }
            if (txtPassword.Text != "" && txtUsername.Text != "" && txtPassword.Text != null && txtUsername.Text != null)
            {
                if (txtPassword.Text.Length > 6)
                {
                    if (dataProvider.CreatingProgress == 1)
                    {
                        User user = new User
                        {
                            Username = txtUsername.Text,
                            Password = txtPassword.Text,
                            Biography = txtBiography.Text,
                            Gender = gender
                        };

                        Session["user"] = user;

                        Response.Redirect("User.aspx");
                    }
                    else

                    {
                        //lblProgress.Text = dataProvider.ErrorMessage;
                    }
                }
                else
                {
                    lblProgress.Text = "Password must be longer then 6 charcaters!";
                }
            }
            else
            {
                lblProgress.Text = "Enter username and password!";
            }
        }
    }
}