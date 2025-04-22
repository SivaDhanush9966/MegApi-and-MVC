using HTAS2_Dev.DataLayer.Interface;
using HTAS2_Dev.Models;
using MySqlConnector;
using System.Data;

namespace HTAS2_Dev.DataLayer.Implementation
{
    public class StateMasterRepository : IStateMasterRepository
    {
        private readonly string _connectionString;

        public StateMasterRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HtaxMysql")
                            ?? throw new InvalidOperationException("Database connection string is missing.");
        }

        public List<StateMaster> GetAllStates()
        {
            List<StateMaster> states = new List<StateMaster>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("USP_GET_ALL_STATES", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new StateMaster
                            {
                                
                                StateName = reader["STATE_NAME"].ToString(),
                                StateCode = Convert.ToInt64(reader["STATE_CODE"])
                            });
                        }
                    }
                }
            }

            return states;
        }
    }
}
