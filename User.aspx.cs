using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace LoginSystemASP.NET
{
    public partial class User1 : System.Web.UI.Page
    {
        User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["user"] as User;

            lblBiography.Text = user.Biography;
            lblUsername.Text = user.Username;

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=LOGINSYSTEM;Integrated Security=True";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "SELECT PROFILE_IMAGE FROM PROFILEIMAGE WHERE USERNAME='" + user.Username + "'";
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.ExecuteNonQuery();

                    if ((byte[])sqlCommand.ExecuteScalar() != null)
                    {
                        byte[] bytes = (byte[])sqlCommand.ExecuteScalar();
                        string basex = Convert.ToBase64String(bytes);
                        Image1.ImageUrl = "data:Image/png;base64," + basex;
                        btnOpenImage.Text = "Update";
                    }
                    else
                    {

                        btnOpenImage.Text = "Post";
                    }
                }
            }
        }

        protected void logOut_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("Index.aspx");
        }

        protected void btnOpenImage_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(postedFile.FileName);
            double fileSize = postedFile.ContentLength;
            DateTime dateTime = DateTime.Now;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".gif")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);

                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                DataProvider dataProvider = new DataProvider();

                SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=LOGINSYSTEM;Integrated Security=True");
                SqlCommand sqlCommand2 = new SqlCommand("SELECT PROFILE_IMAGE FROM PROFILEIMAGE WHERE USERNAME='" + user.Username + "'", sqlConnection);
                sqlConnection.Open();

                if ((byte[])sqlCommand2.ExecuteScalar() == null)
                {
                    dataProvider.SetProfileImage(bytes, user, fileSize, fileName, fileExtension, dateTime);
                }
                else
                {
                    dataProvider.UpdateProfileImage(bytes, user, fileSize, fileName, fileExtension, dateTime);
                }

                Response.Redirect("User.aspx");

            }
            else
            {
                Label1.Text = "Error occured while loading data!";
            }
        }

        protected void btnDeleteAccpount_Click(object sender, EventArgs e)
        {
            User user1 = Session["user"] as User;
            DataProvider dataProvider = new DataProvider();
            dataProvider.DeleteAccount(user1.Username);
            Label1.Text = dataProvider.ErrorMessage;
            Response.Redirect("Index.aspx");
        }

    }
}