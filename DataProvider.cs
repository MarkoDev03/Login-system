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

        public bool CreateAccount(string username, string password, string biography, string gender)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=DataBaseSystem;Integrated Security=True";
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
                    sqlConnection.ConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=DataBaseSystem;Integrated Security=True";
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
                sqlConnection.ConnectionString = "Data Source=DESKTOP-DV7E1D5\\SQLEXPRESS;Initial Catalog=DataBaseSystem;Integrated Security=True";
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
    }
}