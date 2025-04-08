using System.Data;
using MegApi.Interfaces;
using MegApi.Models;
using Microsoft.Data.SqlClient;

namespace MegApi.Services
{
    public class UserInverstors : IUserInvestors
    {
        private readonly string _connStr;

        public UserInverstors(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("MIPASS");
        }
        public List<Investors> GetInvestors(string userId)
        {
            List<Investors> investorsList = new List<Investors>();
            SqlConnection con = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand("USP_GET_UNITS_BY_INVESTORS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            try
            {
                con.Open();
                SqlDataReader rdr =  cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Investors I = new Investors();
                    {
                        I.UNITID = Convert.ToInt32(rdr["UNITID"].ToString());
                        I.COMPANYNAME = rdr["COMPANYNAME"].ToString();
                        I.UNITADDRESS = rdr["UNITADDRESS"].ToString();
                        I.STATUS = rdr["STATUS"].ToString();
                        I.PREREGUIDNO = rdr["PREREGUIDNO"].ToString();
                        I.PRE_ESTB_APPROVALS = rdr["PRE_ESTB_APPROVALS"].ToString();
                        I.PRE_OPERATIONAL_APPROVALS = rdr["PRE_OPERATIONAL_APPROVALS"].ToString();
                        I.INCENTIVES = rdr["INCENTIVES"].ToString();
                    }
                    investorsList.Add(I);
                }
            }


            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return investorsList;
        }
    }
}
