using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IndustryRegApi.Services
{
    public class IndustryFileService : IIndustryFile
    {
        private readonly string _connStr;
        private readonly IWebHostEnvironment _env;

        public IndustryFileService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _connStr = configuration.GetConnectionString("MIPASS");
            _env = env;
        }

        public async Task<string> SaveIndustryFileDetailsAsync(IndustryFileDetails fileDetails)
        {
            string filePath = "";
            if (fileDetails.file != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Path.GetFileName(fileDetails.file.FileName);
                filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileDetails.file.CopyToAsync(stream);
                }
            }

            using SqlConnection conn = new SqlConnection(_connStr);
            using SqlCommand cmd = new SqlCommand("USP_INSERT_INDUSTRYFILEDETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UNITID", fileDetails.UNITID);
            cmd.Parameters.AddWithValue("@INVESTORID", fileDetails.INVESTORID);
            cmd.Parameters.AddWithValue("@FILETYPE", fileDetails.FILETYPE);
            cmd.Parameters.AddWithValue("@FILEPATH", filePath);
            cmd.Parameters.AddWithValue("@FILENAME", fileDetails.file?.FileName ?? "");
            cmd.Parameters.AddWithValue("@FILEDESCRIPTION", fileDetails.FILEDESCRIPTION ?? "");
            cmd.Parameters.AddWithValue("@DEPTID", fileDetails.DEPTID ?? "");
            cmd.Parameters.AddWithValue("@APPROVALID", fileDetails.APPROVALID ?? "");
            cmd.Parameters.AddWithValue("@CREATED_BY", fileDetails.CREATED_BY ?? "");
            cmd.Parameters.AddWithValue("@IRQID", fileDetails.IRQID ?? "");
            cmd.Parameters.AddWithValue("@RESPONSEFILE_BY", fileDetails.RESPONSEFILE_BY ?? "");
            cmd.Parameters.AddWithValue("@DPRVERIFICATION_STATUS", fileDetails.DPRVERIFICATION_STATUS ?? "");
            cmd.Parameters.AddWithValue("@DPRVERIFICATION_BY", fileDetails.DPRVERIFICATION_BY ?? "");
            cmd.Parameters.AddWithValue("@DPRVERIFICATION_BYIP", fileDetails.DPRVERIFICATION_BYIP ?? "");
            cmd.Parameters.AddWithValue("@CREATED_DT", DateTime.Now);

            conn.Open();
            await cmd.ExecuteNonQueryAsync();

            return "File details saved successfully.";
        }

        public async Task<IEnumerable<IndustryFileDetails>> GetAllFilesAsync()
        {
            List<IndustryFileDetails> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            using SqlCommand cmd = new SqlCommand("USP_GET_ALL_INDUSTRYFILEDETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new IndustryFileDetails
                {
                    SLNO = reader["SLNO"] != DBNull.Value ? Convert.ToInt32(reader["SLNO"]) : 0,
                    UNITID = reader["UNITID"]?.ToString(),
                    INVESTORID = reader["INVESTORID"]?.ToString(),
                    FILETYPE = reader["FILETYPE"]?.ToString(),
                    FILEPATH = reader["FILEPATH"]?.ToString(),
                    FILENAME = reader["FILENAME"]?.ToString(),
                    FILEDESCRIPTION = reader["FILEDESCRIPTION"]?.ToString(),
                    // Add others as needed
                });
            }

            return list;
        }

        public async Task<IndustryFileDetails> GetFileDetailsByIdAsync(int slno)
        {
            IndustryFileDetails fileDetails = null;

            using SqlConnection conn = new SqlConnection(_connStr);
            using SqlCommand cmd = new SqlCommand("USP_GET_INDUSTRYFILEDETAILS_BY_ID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SLNO", slno);

            conn.Open();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                fileDetails = new IndustryFileDetails
                {
                    SLNO = Convert.ToInt32(reader["SLNO"]),
                    UNITID = reader["UNITID"]?.ToString(),
                    INVESTORID = reader["INVESTORID"]?.ToString(),
                    FILETYPE = reader["FILETYPE"]?.ToString(),
                    FILEPATH = reader["FILEPATH"]?.ToString(),
                    FILENAME = reader["FILENAME"]?.ToString(),
                    FILEDESCRIPTION = reader["FILEDESCRIPTION"]?.ToString(),
                    DEPTID = reader["DEPTID"]?.ToString(),
                    APPROVALID = reader["APPROVALID"]?.ToString(),
                    CREATED_BY = reader["CREATED_BY"]?.ToString(),
                    CREATED_DT = reader["CREATED_DT"] != DBNull.Value ? Convert.ToDateTime(reader["CREATED_DT"]) : DateTime.MinValue,
                    MODIFIED_BY = reader["MODIFIED_BY"]?.ToString(),
                    MODIFIED_DT = reader["MODIFIED_DT"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["MODIFIED_DT"]) : null,
                    IRQID = reader["IRQID"]?.ToString(),
                    RESPONSEFILE_BY = reader["RESPONSEFILE_BY"]?.ToString(),
                    DPRVERIFICATION_STATUS = reader["DPRVERIFICATION_STATUS"]?.ToString(),
                    DPRVERIFICATION_BY = reader["DPRVERIFICATION_BY"]?.ToString(),
                    DPRVERIFICATION_DATE = reader["DPRVERIFICATION_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DPRVERIFICATION_DATE"]) : null,
                    DPRVERIFICATION_BYIP = reader["DPRVERIFICATION_BYIP"]?.ToString()
                };
            }

            return fileDetails;
        }

    }
}
