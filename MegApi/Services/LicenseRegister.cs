using System.ComponentModel;
using System.Data;
using MegApi.Interfaces;
using MegApi.Models;
using Microsoft.Data.SqlClient;

namespace MegApi.Services
{
    public class LicenseRegister : ILicense
    {
        private readonly string _connStr;

        public LicenseRegister(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("HTAX2O");
        }

        public string CreateLicense(LicenseeFullDetails licensee)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand("InsertOrUpdateUser", connection, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@UserId", licensee.UserId);
                    command.Parameters.AddWithValue("@ApplicantName", licensee.ApplicantName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FatherName", licensee.FatherName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DOB", licensee.DOB);

                    command.Parameters.AddWithValue("@Email", licensee.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Mobile", licensee.Mobile ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PAN", licensee.PAN ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Aadhaar", licensee.Aadhaar ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalDetailsAddress", licensee.PersonalDetailsAddress ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@PersonalAddrLine", licensee.PersonalAddrLine ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalVillage", licensee.PersonalVillage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalCity", licensee.PersonalCity ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalDistrict", licensee.PersonalDistrict ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalState", licensee.PersonalState ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalPincode", licensee.PersonalPincode ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@BusinessAddrLine", licensee.BusinessAddrLine ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessVillage", licensee.BusinessVillage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessCity", licensee.BusinessCity ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessDistrict", licensee.BusinessDistrict ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessState", licensee.BusinessState ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessPincode", licensee.BusinessPincode ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@CreatedBy", licensee.Createdby ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedIP", licensee.CreatedIp ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return "success";
        }

       
    }
}
