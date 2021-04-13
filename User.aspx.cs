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

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "SELECT IMAGE FROM ACCOUNTS_ WHERE USERNAME='" + user.Username + "|'";
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    byte[] bytes = (byte[])sqlCommand.ExecuteScalar();
                    string basex = Convert.ToBase64String(bytes);
                    Image1.ImageUrl = "data:Image/png;base64," + basex;

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

                        sqlCommand.CommandText = "INSERT INTO ACCOUNTS_ (USERNAME, PASSWORD, BIOGRAPHY, GENDER, USER_ID,IMAGE) VALUES(@USERNAME, @PASSWORD, @BIOGRAPHY, @GENDER, @USER_ID,@IMAGE)";


                        SqlCommand sqlCommand2 = new SqlCommand("SELECT * FROM ACCOUNTS_ WHERE USERNAME = @USERNAME", sqlConnection);

                        sqlCommand2.Parameters.AddWithValue("@USERNAME", user.Username);
                        SqlDataReader reader = sqlCommand2.ExecuteReader();

                        if (reader != null && reader.HasRows)
                        {
                            sqlCommand.Parameters.AddWithValue("@USERNAME", user.Username+"|");
                            sqlCommand.Parameters.AddWithValue("@PASSWORD", user.Password);
                            sqlCommand.Parameters.AddWithValue("@BIOGRAPHY", user.Biography);
                            sqlCommand.Parameters.AddWithValue("@GENDER", 'M');
                            sqlCommand.Parameters.AddWithValue("@USER_ID", "id_" + user.Username);
                            sqlCommand.Parameters.Add("@IMAGE", System.Data.SqlDbType.VarBinary);
                            
                            sqlCommand.Parameters["@IMAGE"].Value = bytes;
                        }
                        reader.Close();

                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            else
            {
                Label1.Text = "Error occured while loading data!";
            }

        }

        protected void btnSaveImage_Click(object sender, EventArgs e)
        {

            User user = Session["user"] as User;
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=DataBaseSystem;Integrated Security=True";

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "SELECT IMAGE FROM ACCOUNTS_ WHERE USERNAME='"+user.Username+"|'";
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    byte[] bytes = (byte[])sqlCommand.ExecuteScalar();
                    string basex = Convert.ToBase64String(bytes);
                    Image1.ImageUrl = "data:Image/png;base64," + basex;

                }


            }

        }
    }
}