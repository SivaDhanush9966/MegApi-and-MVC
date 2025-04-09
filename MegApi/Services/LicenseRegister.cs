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

        public string CreateLicense(License1 license)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand("InsertLicence", connection, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Add parameters as defined in the stored procedure
                    command.Parameters.AddWithValue("@UserId", license.UserId);
                    command.Parameters.AddWithValue("@PermanentAddressLine", license.PermanentAddressLine ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PermanentVillage", license.PermanentVillage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PermanentCityOrTown", license.PermanentCityOrTown ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PermanentDistrict", license.PermanentDistrict ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PermanentState", license.PermanentState ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PermanentPinCode", license.PermanentPinCode ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@PersonalAddressLine", license.PersonalAddressLine ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalVillage", license.PersonalVillage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalCityOrTown", license.PersonalCityOrTown ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalDistrict", license.PersonalDistrict ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalState", license.PersonalState ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonalPinCode", license.PersonalPinCode ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@BusinessAddressLine", license.BusinessAddressLine ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessVillage", license.BusinessVillage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessCityOrTown", license.BusinessCityOrTown ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessDistrict", license.BusinessDistrict ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessState", license.BusinessState ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BusinessPinCode", license.BusinessPinCode ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@CreatedBy", license.CreatedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedIP", license.CreatedIP ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Re-throw to preserve stack trace
                }
            }

            return "sucess";
        }
    }
}
