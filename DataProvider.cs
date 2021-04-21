using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace LoginSystemASP.NET
{
    public class DataProvider
    {
        public string ErrorMessage { get; set; }
        public string UserEmail { get; set; }
        public string ResetPasswordCode;
        public bool _isPasswordChecked = false;
        public int LoginProgress = 0, CreatingProgress = 0;
        private string dataBaseConnectionString = "workstation id=LOGINSYSTEMMARKO.mssql.somee.com;packet size=4096;user id=perovicmarko123_SQLLogin_1;pwd=yuxkh8vxl2;data source=LOGINSYSTEMMARKO.mssql.somee.com;persist security info=False;initial catalog=LOGINSYSTEMMARKO";


        public bool CreateAccount(string username, string password, string biography, string gender, string email)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText = "INSERT INTO ACCOUNTS_ (USERNAME, PASSWORD, BIOGRAPHY, GENDER, EMAIL) VALUES(@USERNAME, @PASSWORD, @BIOGRAPHY, @GENDER, @EMAIL)";

                        sqlCommand.Parameters.Add("@USERNAME", System.Data.SqlDbType.NVarChar);
                        sqlCommand.Parameters.Add("@PASSWORD", System.Data.SqlDbType.NVarChar);
                        sqlCommand.Parameters.Add("@BIOGRAPHY", System.Data.SqlDbType.NVarChar);
                        sqlCommand.Parameters.Add("@GENDER", System.Data.SqlDbType.Char);
                        sqlCommand.Parameters.Add("@EMAIL", System.Data.SqlDbType.NVarChar);


                        sqlCommand.Parameters["@USERNAME"].Value = username;
                        sqlCommand.Parameters["@PASSWORD"].Value = password;
                        sqlCommand.Parameters["@BIOGRAPHY"].Value = biography;
                        sqlCommand.Parameters["@GENDER"].Value = gender;
                        sqlCommand.Parameters["@EMAIL"].Value = email;

                        sqlCommand.Connection = sqlConnection;

                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }

                ErrorMessage = "Account created!";
                CreatingProgress++;

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                CreatingProgress = 0;
                return false;
            }
        }

        public User LogIn(string username, string password)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText = "SELECT * FROM ACCOUNTS_ WHERE USERNAME='" + username + "' AND PASSWORD='" + password + "'";
                        sqlCommand.Connection = sqlConnection;

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        DataTable dataTable = new DataTable();

                        sqlDataAdapter.Fill(dataTable);

                        User user = new User();

                        if (dataTable.Rows.Count > 0)
                        {
                            LoginProgress++;
                            user.Username = username;
                            user.Password = password;

                            SqlDataReader sqlDataReader = null;

                            using (SqlCommand sqlCommandFoBiography = new SqlCommand())
                            {
                                sqlCommandFoBiography.Connection = sqlConnection;
                                sqlCommandFoBiography.CommandText = "SELECT * FROM ACCOUNTS_ WHERE USERNAME='" + username + "' AND PASSWORD='" + password + "'";
                                sqlCommandFoBiography.ExecuteNonQuery();
                                sqlDataReader = sqlCommandFoBiography.ExecuteReader();

                                while (sqlDataReader.Read())
                                {
                                    user.Biography = sqlDataReader["BIOGRAPHY"].ToString();
                                    user.Email = sqlDataReader["EMAIL"].ToString();
                                }

                                return user;
                            }
                        }
                        else
                        {
                            LoginProgress = 0;
                            return null;
                        }
                                                 
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public int DoesThisAccountExists(string username)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = dataBaseConnectionString;
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM ACCOUNTS_ WHERE USERNAME = @USERNAME";
                    sqlCommand.Parameters.AddWithValue("@USERNAME", username);

                    SqlDataReader reader = null;
                    reader = sqlCommand.ExecuteReader();

                    if (reader != null && reader.HasRows)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
            }
        }

        public void SetProfileImage(byte[] bytes, User user, double size, string name, string type, DateTime time)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = dataBaseConnectionString;

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection; sqlConnection.Open();

                    sqlCommand.CommandText = "INSERT INTO PROFILEIMAGE (USERNAME, PROFILE_IMAGE, IMAGE_SIZE, IMAGE_NAME, IMAGE_TYPE, TIME_POSTED) VALUES(@USERNAME, @PROFILE_IMAGE, @IMAGE_SIZE, @IMAGE_NAME, @IMAGE_TYPE, @TIME_POSTED)";

                    sqlCommand.Parameters.AddWithValue("@USERNAME", user.Username);
                    sqlCommand.Parameters.Add("@PROFILE_IMAGE", System.Data.SqlDbType.Image);
                    sqlCommand.Parameters.AddWithValue("@IMAGE_SIZE", Math.Round(size / 1000000, 2).ToString() + " MB");
                    sqlCommand.Parameters.AddWithValue("@IMAGE_NAME", name);
                    sqlCommand.Parameters.AddWithValue("@IMAGE_TYPE", type);
                    sqlCommand.Parameters["@PROFILE_IMAGE"].Value = bytes;
                    sqlCommand.Parameters.AddWithValue("@TIME_POSTED", time);

                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void UpdateProfileImage(byte[] bytes, User user, double size, string name, string type, DateTime time)
        {

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = dataBaseConnectionString;
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "UPDATE PROFILEIMAGE SET PROFILE_IMAGE = @PROFILE_IMAGE, IMAGE_SIZE = '" + Math.Round(size / 1000000, 2).ToString() + " MB', IMAGE_NAME = '" + name + "', IMAGE_TYPE = '" + type + "', TIME_POSTED = '" + time + "' WHERE USERNAME='" + user.Username + "'";
                    sqlCommand.Parameters.AddWithValue("@PROFILE_IMAGE", bytes);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void DeleteAccount(string username)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "DELETE FROM ACCOUNTS_ WHERE USERNAME = '" + username + "'  DELETE FROM PROFILEIMAGE WHERE USERNAME = '" + username + "'";
                        sqlCommand.ExecuteNonQuery();
                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void ResetPassword(string username,string code)
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConnection;
                        cmd.CommandText = "SELECT * FROM ACCOUNTS_ WHERE USERNAME='" + username + "'";

                        SqlDataReader reader = null;
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {

                            try
                            {

                                Random rnd = new Random();
                                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                                client.EnableSsl = true;
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.UseDefaultCredentials = false;
                                client.Credentials = new NetworkCredential("mmarko.perovici3@gmail.com", "-");

                                MailMessage msg = new MailMessage();

                                ResetPasswordCode = rnd.Next(1000, 9999).ToString();

                                msg.To.Add(reader["EMAIL"].ToString());
                                msg.From = new MailAddress("mmarko.perovici3@gmail.com");
                                msg.Subject = "Reset password";
                                msg.Body = "Your code is " + code;

                                client.Send(msg);

                            }
                            catch(Exception ex)
                            {
                                ErrorMessage = ex.Message;
                            }
                            
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ChangePassword(string username, string newPassword)
        {
            try
            {
                if (newPassword.Length > 6)
                {

                    using (SqlConnection sqlConnection = new SqlConnection())
                    {
                        sqlConnection.ConnectionString = dataBaseConnectionString;
                        sqlConnection.Open();

                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.Connection = sqlConnection;
                            sqlCommand.CommandText = "UPDATE ACCOUNTS_ SET PASSWORD = '" + newPassword + "' WHERE USERNAME = '" + username + "'";
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Password must be longer then 6 characters!";
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        
        public void CheckPreviousPasswordValidation(string username, string enterendPassword)
        {
            try
            {

                using(SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();

                    using(SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "SELECT * FROM ACCOUNTS_ WHERE USERNAME = '"+ username + "'";

                        SqlDataReader dataReader = null;

                        dataReader = sqlCommand.ExecuteReader();

                        while (dataReader.Read())
                        {
                            if (enterendPassword == dataReader["PASSWORD"].ToString())
                            {
                                _isPasswordChecked = true;
                            }
                            else
                            {
                                _isPasswordChecked = false;
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void UpdateBiography(string username, string newBiography)
        {

            try
            {

                using(SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();

                    using(SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "UPDATE ACCOUNTS_ SET BIOGRAPHY = '" + newBiography + "' WHERE USERNAME = '" + username + "'";
                    }

                }

            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}