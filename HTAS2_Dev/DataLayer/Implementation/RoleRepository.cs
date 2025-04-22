using HTAS2_Dev.DataLayer.Interface;
using HTAS2_Dev.Models;
using MySqlConnector;
using System.Data;

namespace HTAS2_Dev.DataLayer.Implementation
{
    public class RoleRepository : IRoleService
    {
        private readonly IConfiguration _configuration;

        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            var roles = new List<Role>();

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("HtaxMysql")))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("USP_GET_Roles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            roles.Add(new Role
                            {
                                ID = reader.GetInt32("ID"),
                                ROLE = reader["ROLE"].ToString(),
                                ROLE_DESCRIPTION = reader["ROLE_DESCRIPTION"].ToString(),
                                REMARKS = reader["REMARKS"].ToString(),
                                IS_ACTIVE = reader.GetBoolean("IS_ACTIVE"),
                                IS_DELETED = reader.GetBoolean("IS_DELETED"),
                                IP_ADRESS = reader["IP_ADRESS"].ToString(),
                                LAT_LONG = reader["LAT_LONG"].ToString(),
                                APP_SOURCE = reader["APP_SOURCE"].ToString(),
                                CREATED_BY = reader["CREATED_BY"].ToString(),
                                CREATED_DATE = reader["CREATED_DATE"] as DateTime?,
                                MODIFIED_BY = reader["MODIFIED_BY"].ToString(),
                                MODIFIED_DATE = reader["MODIFIED_DATE"] as DateTime?
                            });
                        }
                    }
                }
            }

            return roles;
        }

        }
}
