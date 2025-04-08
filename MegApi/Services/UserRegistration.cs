using MegApi.Interfaces;
using MegApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MegApi.Services
{
    public class UserRegistration : IUserRegService
    {
        private readonly string _connStr;

        public UserRegistration(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("MIPASS");
        }



        string IUserRegService.InsertUserRegDetails(UserRegDetails Userregdtls)
        {
            string valid = "";

            SqlConnection connection = new SqlConnection(_connStr);
            SqlTransaction transaction = null;
            connection.Open();
            transaction = connection.BeginTransaction();
            try
            {
                SqlCommand com = new SqlCommand("USP_INS_INVESTER_LOGIN_DETAILS", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Transaction = transaction;
                com.Connection = connection;

                //@Fullname Varchar(100) ,  
                //@EntityName varchar(100) ,  
                //@emailid varchar(50),  
                //@mobile bigint,
                //@pwd varchar(100) ,  
                //@PanNo varchar(15),  
                //@dob datetime,
                //@Ipaddress varchar(50)
                com.Parameters.AddWithValue("@Fullname", Userregdtls.Fullname);
                com.Parameters.AddWithValue("@EntityName", Userregdtls.CompanyName);
                com.Parameters.AddWithValue("@emailid", Userregdtls.Email);
                com.Parameters.AddWithValue("@mobile", Userregdtls.MobileNo);
                com.Parameters.AddWithValue("@pwd", Userregdtls.Password);
                com.Parameters.AddWithValue("@PanNo", Userregdtls.PANno);
                com.Parameters.AddWithValue("@dob", Userregdtls.DateofBirth);
                com.Parameters.AddWithValue("@Ipaddress", Userregdtls.IPAddress);
                com.Parameters.Add("@RESULT", SqlDbType.VarChar, 100);
                com.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();

                valid = com.Parameters["@RESULT"].Value.ToString();


                //valid = Convert.ToString(com.ExecuteNonQuery());



                transaction.Commit();
                connection.Close();
            }

            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return valid;

        }


        public UserInfo GetUserInfo(string username, string password, string ipAddress)
        {
            var objUserInf = new UserInfo();

            string connectionString = _connStr; // Replace with your actual connection string

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_VALIDATE_INVESTERUSERS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@emailid", username);
                        cmd.Parameters.AddWithValue("@PWD", password);
                        cmd.Parameters.AddWithValue("@IPADDRESS", ipAddress);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    objUserInf.Userid = reader["InvesterId"] != DBNull.Value ? Convert.ToString(reader["InvesterId"]) : "";
                                    objUserInf.Fullname = reader["Fullname"] != DBNull.Value ? Convert.ToString(reader["Fullname"]) : "";
                                    objUserInf.Email = reader["emailid"] != DBNull.Value ? Convert.ToString(reader["emailid"]) : "";
                                    objUserInf.EntityName = reader["EntityName"] != DBNull.Value ? Convert.ToString(reader["EntityName"]) : "";
                                    objUserInf.PANno = reader["PanNo"] != DBNull.Value ? Convert.ToString(reader["PanNo"]) : "";
                                    objUserInf.RoleId = reader["ROLEID"] != DBNull.Value ? Convert.ToString(reader["ROLEID"]) : "";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ideally, log the exception instead of throwing directly
                throw;
            }

            return objUserInf;
        }

    }




}


