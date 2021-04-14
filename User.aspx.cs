using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginSystemASP.NET
{
    public partial class User1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            User user = Session["user"] as User;

            lblBiography.Text = user.Biography;
            lblUsername.Text = user.Username;

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=DataBaseSystem;Integrated Security=True";
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
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".gif")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);

                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=DataBaseSystem;Integrated Security=True";


                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection; sqlConnection.Open();
                        User user = Session["user"] as User;

                        sqlCommand.CommandText = "INSERT INTO PROFILEIMAGE (USERNAME, USER_ID, PROFILE_IMAGE) VALUES(@USERNAME, @USER_ID, @PROFILE_IMAGE)";

                            sqlCommand.Parameters.AddWithValue("@USERNAME", user.Username);
                            sqlCommand.Parameters.AddWithValue("@USER_ID", "id_" + user.Username);
                            sqlCommand.Parameters.Add("@PROFILE_IMAGE", System.Data.SqlDbType.VarBinary);                         
                            sqlCommand.Parameters["@PROFILE_IMAGE"].Value = bytes;

                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        Response.Redirect("User.aspx");
                    }
                }
            }
            else
            {
                Label1.Text = "Error occured while loading data!";
            }
        }

       
    }
}