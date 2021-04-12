using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginSystemASP.NET
{
    public partial class User1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = Session["user"] as User;
            lblBiography.Text = user.Biography;
            lblUsername.Text = user.Username;
        }

        protected void logOut_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("Index.aspx");
        }
    }
}