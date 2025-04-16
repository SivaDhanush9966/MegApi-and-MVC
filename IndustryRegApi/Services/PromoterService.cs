using System.Data;
using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using Microsoft.Data.SqlClient;

namespace IndustryRegApi.Services
{
    public class PromoterService : IPromoter
    {
        private readonly string _connStr;

        public PromoterService(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public string CreatePromoter(Promoter promoter)
        {
            string valid = "";

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand com = new SqlCommand("----------", connection, transaction))
                        {
                            com.CommandType = CommandType.StoredProcedure;

                            // Adding all Promoter class properties as parameters
                            com.Parameters.AddWithValue("@IDD_ID", promoter.IDD_ID);
                            com.Parameters.AddWithValue("@IDD_DIRECTOR_NO", promoter.IDD_DIRECTOR_NO);
                            com.Parameters.AddWithValue("@IDD_UNITID", promoter.IDD_UNITID);
                            com.Parameters.AddWithValue("@IDD_INVESTERID", promoter.IDD_INVESTERID);
                            com.Parameters.AddWithValue("@IDD_FIRSTNAME", promoter.IDD_FIRSTNAME);
                            com.Parameters.AddWithValue("@IDD_LASTNAME", promoter.IDD_LASTNAME);
                            com.Parameters.AddWithValue("@IDD_ADNO", promoter.IDD_ADNO);
                            com.Parameters.AddWithValue("@IDD_PAN", promoter.IDD_PAN);
                            com.Parameters.AddWithValue("@IDD_DINNO", promoter.IDD_DINNO);
                            com.Parameters.AddWithValue("@IDD_NATIONALITY", promoter.IDD_NATIONALITY);
                            com.Parameters.AddWithValue("@IDD_DOORNO", promoter.IDD_DOORNO);
                            com.Parameters.AddWithValue("@IDD_STREET", promoter.IDD_STREET);
                            com.Parameters.AddWithValue("@IDD_CITY", promoter.IDD_CITY);
                            com.Parameters.AddWithValue("@IDD_DISTRICT", promoter.IDD_DISTRICT);
                            com.Parameters.AddWithValue("@IDD_MANDAL", promoter.IDD_MANDAL);
                            com.Parameters.AddWithValue("@IDD_STATE", promoter.IDD_STATE);
                            com.Parameters.AddWithValue("@IDD_PINCODE", promoter.IDD_PINCODE);
                            com.Parameters.AddWithValue("@IDD_COUNTRY", promoter.IDD_COUNTRY);
                            com.Parameters.AddWithValue("@IDD_EMAIL", promoter.IDD_EMAIL);
                            com.Parameters.AddWithValue("@IDD_PHONE", promoter.IDD_PHONE);
                            com.Parameters.AddWithValue("@CREATEDDATE", promoter.CREATEDDATE);
                            com.Parameters.AddWithValue("@IDD_CITYName", promoter.IDD_CITYName);
                            com.Parameters.AddWithValue("@IDD_MANDALName", promoter.IDD_MANDALName);
                            com.Parameters.AddWithValue("@IDD_DISTRICTName", promoter.IDD_DISTRICTName);
                            com.Parameters.AddWithValue("@IDD_STATEName", promoter.IDD_STATEName);
                            com.Parameters.AddWithValue("@IDD_COUNTRYName", promoter.IDD_COUNTRYName);

                            com.Parameters.Add("@RESULT", SqlDbType.VarChar, 100);
                            com.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                            com.ExecuteNonQuery();
                            valid = com.Parameters["@RESULT"].Value.ToString();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return valid;
        }

        public Promoter GetPromoter(int userId)
        {
            Promoter promoter = null;

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();
                using (SqlCommand com = new SqlCommand("----", connection))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            promoter = new Promoter
                            {
                                IDD_ID = reader["IDD_ID"] != DBNull.Value ? Convert.ToInt32(reader["IDD_ID"]) : 0,
                                IDD_DIRECTOR_NO = reader["IDD_DIRECTOR_NO"] != DBNull.Value ? reader["IDD_DIRECTOR_NO"].ToString() : null,
                                IDD_UNITID = reader["IDD_UNITID"] != DBNull.Value ? Convert.ToInt32(reader["IDD_UNITID"]) : 0,
                                IDD_INVESTERID = reader["IDD_INVESTERID"] != DBNull.Value ? Convert.ToInt32(reader["IDD_INVESTERID"]) : 0,
                                IDD_FIRSTNAME = reader["IDD_FIRSTNAME"] != DBNull.Value ? reader["IDD_FIRSTNAME"].ToString() : null,
                                IDD_LASTNAME = reader["IDD_LASTNAME"] != DBNull.Value ? reader["IDD_LASTNAME"].ToString() : null,
                                IDD_ADNO = reader["IDD_ADNO"] != DBNull.Value ? reader["IDD_ADNO"].ToString() : null,
                                IDD_PAN = reader["IDD_PAN"] != DBNull.Value ? reader["IDD_PAN"].ToString() : null,
                                IDD_DINNO = reader["IDD_DINNO"] != DBNull.Value ? reader["IDD_DINNO"].ToString() : null,
                                IDD_NATIONALITY = reader["IDD_NATIONALITY"] != DBNull.Value ? reader["IDD_NATIONALITY"].ToString() : null,
                                IDD_DOORNO = reader["IDD_DOORNO"] != DBNull.Value ? reader["IDD_DOORNO"].ToString() : null,
                                IDD_STREET = reader["IDD_STREET"] != DBNull.Value ? reader["IDD_STREET"].ToString() : null,
                                IDD_CITY = reader["IDD_CITY"] != DBNull.Value ? reader["IDD_CITY"].ToString() : null,
                                IDD_DISTRICT = reader["IDD_DISTRICT"] != DBNull.Value ? reader["IDD_DISTRICT"].ToString() : null,
                                IDD_MANDAL = reader["IDD_MANDAL"] != DBNull.Value ? reader["IDD_MANDAL"].ToString() : null,
                                IDD_STATE = reader["IDD_STATE"] != DBNull.Value ? reader["IDD_STATE"].ToString() : null,
                                IDD_PINCODE = reader["IDD_PINCODE"] != DBNull.Value ? reader["IDD_PINCODE"].ToString() : null,
                                IDD_COUNTRY = reader["IDD_COUNTRY"] != DBNull.Value ? reader["IDD_COUNTRY"].ToString() : null,
                                IDD_EMAIL = reader["IDD_EMAIL"] != DBNull.Value ? reader["IDD_EMAIL"].ToString() : null,
                                IDD_PHONE = reader["IDD_PHONE"] != DBNull.Value ? reader["IDD_PHONE"].ToString() : null,
                                CREATEDDATE = reader["CREATEDDATE"] != DBNull.Value ? Convert.ToDateTime(reader["CREATEDDATE"]) : DateTime.MinValue,
                                IDD_CITYName = reader["IDD_CITYName"] != DBNull.Value ? reader["IDD_CITYName"].ToString() : null,
                                IDD_MANDALName = reader["IDD_MANDALName"] != DBNull.Value ? reader["IDD_MANDALName"].ToString() : null,
                                IDD_DISTRICTName = reader["IDD_DISTRICTName"] != DBNull.Value ? reader["IDD_DISTRICTName"].ToString() : null,
                                IDD_STATEName = reader["IDD_STATEName"] != DBNull.Value ? reader["IDD_STATEName"].ToString() : null,
                                IDD_COUNTRYName = reader["IDD_COUNTRYName"] != DBNull.Value ? reader["IDD_COUNTRYName"].ToString() : null
                            };
                        }
                    }
                }
            }

            return promoter;
        }
    }
}
