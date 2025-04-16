using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace IndustryRegApi.Services
{
    public class IndustryDetailsService : IIndustryDetails
    {
        private readonly string _connStr;

        public IndustryDetailsService(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("MIPASS");
        }

        public async Task<IndustryDetails> GetIndustryDetailsByIdAsync(string id)
        {
            var industryDetails = new IndustryDetails();

            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = new SqlCommand("USP_GET_INDUSTRY_BY_ID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        industryDetails.UserID = reader["UserID"]?.ToString();
                        industryDetails.CompanyName = reader["CompanyName"]?.ToString();
                        // map all other fields similarly
                    }
                }
            }

            return industryDetails;
        }

        public async Task<IEnumerable<IndustryDetails>> GetAllIndustryDetailsAsync()
        {
            var list = new List<IndustryDetails>();

            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = new SqlCommand("USP_GET_ALL_INDUSTRIES", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var details = new IndustryDetails
                        {
                            UserID = reader["UserID"]?.ToString(),
                            CompanyName = reader["CompanyName"]?.ToString()
                            // map other fields
                        };

                        list.Add(details);
                    }
                }
            }

            return list;
        }

        public async Task<IndustryDetails> CreateIndustryDetails(IndustryDetails details)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("USP_INS_INDUSTRY_DETAILS", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserID", details.UserID);
                cmd.Parameters.AddWithValue("@CompanyName", details.CompanyName);
                // add remaining parameters

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }

            return details;
        }

        public async Task<bool> UpdateIndustryDetailsAsync(string id, IndustryDetails updatedDetails)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("USP_UPDATE_INDUSTRY_DETAILS", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@CompanyName", updatedDetails.CompanyName);
                // add remaining fields to update

                await conn.OpenAsync();
                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }

            return rowsAffected > 0;
        }
    }
}
