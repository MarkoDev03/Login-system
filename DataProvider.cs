using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace LoginSystemASP.NET
{
    public class DataProvider
    {
        public string ErrorMessage { get; set; }
        public int LoginProgress = 0;
        public int CreatingProgress = 0;
        public int ExistingAccount = 0;
        private string dataBaseConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=LOGINSYSTEM;Integrated Security=True";

        public bool CreateAccount(string username, string password, string biography, string gender)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText = "INSERT INTO ACCOUNTS_ (USERNAME, PASSWORD, BIOGRAPHY, GENDER) VALUES(@USERNAME, @PASSWORD, @BIOGRAPHY, @GENDER)";

                        sqlCommand.Parameters.Add("@USERNAME", System.Data.SqlDbType.NVarChar);
                        sqlCommand.Parameters.Add("@PASSWORD", System.Data.SqlDbType.NVarChar);
                        sqlCommand.Parameters.Add("@BIOGRAPHY", System.Data.SqlDbType.NVarChar);
                        sqlCommand.Parameters.Add("@GENDER", System.Data.SqlDbType.Char);


                        sqlCommand.Parameters["@USERNAME"].Value = username;
                        sqlCommand.Parameters["@PASSWORD"].Value = password;
                        sqlCommand.Parameters["@BIOGRAPHY"].Value = biography;
                        sqlCommand.Parameters["@GENDER"].Value = gender;

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

                            SqlCommand sqlCommandFoBiography = new SqlCommand("SELECT * FROM ACCOUNTS_ WHERE USERNAME='" + username + "' AND PASSWORD='" + password + "'", sqlConnection);
                            sqlCommandFoBiography.ExecuteNonQuery();
                            sqlDataReader = sqlCommandFoBiography.ExecuteReader();

                            while (sqlDataReader.Read())
                            {
                                user.Biography = sqlDataReader["BIOGRAPHY"].ToString();
                            }

                            return user;
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ACCOUNTS_ WHERE USERNAME = @USERNAME", sqlConnection);
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

                using(SqlCommand sqlCommand = new SqlCommand())
                {              
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "UPDATE PROFILEIMAGE SET PROFILE_IMAGE = @PROFILE_IMAGE, IMAGE_SIZE = '" +  Math.Round(size / 1000000, 2).ToString() + " MB', IMAGE_NAME = '" + name + "', IMAGE_TYPE = '" + type + "', TIME_POSTED = '" + time + "' WHERE USERNAME='" + user.Username + "'";
                    sqlCommand.Parameters.AddWithValue("@PROFILE_IMAGE", bytes);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void  DeleteAccount(string username)
        {
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = dataBaseConnectionString;
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "DELETE FROM ACCOUNTS_ WHERE USERNAME = '"+ username+ "'  DELETE FROM PROFILEIMAGE WHERE USERNAME = '"+username+"'";
                        sqlCommand.ExecuteNonQuery();
                    }

                   sqlConnection.Close();
                }

            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}