using IndustryRegApi.Interface;
using IndustryRegApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IndustryRegApi.Services
{
    public class InvestmentPlanService: IInvestmentPlan
    {
        private readonly string _connStr;

        public InvestmentPlanService(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("MIPASS");
        }

        public async Task<InvestmentPlan> GetInvestmentPlanByIdAsync(int id)
        {
            var plan = new InvestmentPlan();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_INVESTMENT_PLAN_BY_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IBP_ID", id);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        plan.IbpId = Convert.ToInt32(reader["IBP_ID"]);
                        plan.IbpUnitId = Convert.ToInt32(reader["IBP_UNITID"]);
                        plan.IbpInvesterId = Convert.ToInt32(reader["IBP_INVESTERID"]);
                        plan.Year1 = Convert.ToDecimal(reader["YEAR1"]);
                        plan.Year2 = Convert.ToDecimal(reader["YEAR2"]);
                        plan.Year3 = Convert.ToDecimal(reader["YEAR3"]);
                        plan.Year4 = Convert.ToDecimal(reader["YEAR4"]);
                        plan.Year5 = Convert.ToDecimal(reader["YEAR5"]);
                        plan.CreatedDate = Convert.ToDateTime(reader["CREATEDDATE"]);
                        plan.MrpId = Convert.ToInt32(reader["MRPID"]);
                    }
                }
            }
            return plan;
        }

        public async Task<IEnumerable<InvestmentPlan>> GetAllInvestmentPlansAsync()
        {
            var plans = new List<InvestmentPlan>();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_ALL_INVESTMENT_PLANS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        plans.Add(new InvestmentPlan
                        {
                            IbpId = Convert.ToInt32(reader["IBP_ID"]),
                            IbpUnitId = Convert.ToInt32(reader["IBP_UNITID"]),
                            IbpInvesterId = Convert.ToInt32(reader["IBP_INVESTERID"]),
                            Year1 = Convert.ToDecimal(reader["YEAR1"]),
                            Year2 = Convert.ToDecimal(reader["YEAR2"]),
                            Year3 = Convert.ToDecimal(reader["YEAR3"]),
                            Year4 = Convert.ToDecimal(reader["YEAR4"]),
                            Year5 = Convert.ToDecimal(reader["YEAR5"]),
                            CreatedDate = Convert.ToDateTime(reader["CREATEDDATE"]),
                            MrpId = Convert.ToInt32(reader["MRPID"])
                        });
                    }
                }
            }
            return plans;
        }

        public async Task<InvestmentPlan> CreateInvestmentPlanAsync(InvestmentPlan plan)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("USP_INSERT_INVESTMENT_PLAN", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IBP_UNITID", plan.IbpUnitId);
                cmd.Parameters.AddWithValue("@IBP_INVESTERID", plan.IbpInvesterId);
                cmd.Parameters.AddWithValue("@YEAR1", plan.Year1);
                cmd.Parameters.AddWithValue("@YEAR2", plan.Year2);
                cmd.Parameters.AddWithValue("@YEAR3", plan.Year3);
                cmd.Parameters.AddWithValue("@YEAR4", plan.Year4);
                cmd.Parameters.AddWithValue("@YEAR5", plan.Year5);
                cmd.Parameters.AddWithValue("@CREATEDDATE", plan.CreatedDate);
                cmd.Parameters.AddWithValue("@MRPID", plan.MrpId);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }

            return plan;
        }
    }
}
