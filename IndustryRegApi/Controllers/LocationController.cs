using System.Data;
using IndustryRegApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace IndustryRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LocationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = new List<Country>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_COUNTRY", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    countries.Add(new Country
                    {
                        Id = Convert.ToInt32(rdr["MC_ID"]),
                        Name = rdr["MC_NAME"].ToString()
                    });
                }
                con.Close();
            }
            return Ok(countries);
        }

        [HttpGet("states")]
        public IActionResult GetStates()
        {
            var states = new List<State>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_STATE", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    states.Add(new State
                    {
                        Id = Convert.ToInt32(rdr["MS_ID"]),
                        Name = rdr["MS_NAME"].ToString()
                    });
                }
                con.Close();
            }
            return Ok(states);
        }

        [HttpGet("districts")]
        public IActionResult GetDistricts()
        {
            var districts = new List<District>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_DISTRICT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    districts.Add(new District
                    {
                        Code = (int)rdr["DistrictCode"],
                        Name = (string)rdr["DistrictName"]
                    });
                }
                con.Close();
            }
            return Ok(districts);
        }

        [HttpGet("mandals/{districtId}")]
        public IActionResult GetMandals(int districtId)
        {
            var mandals = new List<dynamic>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_MANDALS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DISTRICT", districtId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    mandals.Add(new
                    {
                        Name = rdr["MANDALNAME"],
                        Code = rdr["MANDALCODE"]
                    });
                }
                con.Close();
            }
            return Ok(mandals);
        }

        [HttpGet("villages/{mandalId}")]
        public IActionResult GetVillages(int mandalId)
        {
            var villages = new List<Village>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_VILLAGE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MANDALCD", mandalId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    villages.Add(new Village
                    {
                        Name = (string)rdr["villagename"],
                        Code = (int)rdr["villagecode"]
                    });
                }
                con.Close();
            }
            return Ok(villages);
        }

        [HttpGet("constitution-types")]
        public IActionResult GetConstitutionTypes()
        {
            var list = new List<ConstitutionType>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                using (SqlCommand cmd = new SqlCommand("USP_GETCONSTITUTIONTYPEMASTER", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new ConstitutionType
                            {
                                ConstId = Convert.ToInt32(rdr["CONST_ID"]),
                                ConstType = rdr["CONST_TYPE"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(list);
        }

        [HttpGet("sectors")]
        public IActionResult GetSectors()
        {
            var sectors = new List<Sector>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                using (SqlCommand cmd = new SqlCommand("USP_GET_SECTOR", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            sectors.Add(new Sector
                            {
                                SectorId = Convert.ToInt32(rdr["SectorId"]),
                                SectorName = rdr["SectorName"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(sectors);
        }

        [HttpGet("lineofactivities")]
        public IActionResult GetLineOfActivities([FromQuery] string? sector)
        {
            var activities = new List<LineofActivity>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                using (SqlCommand cmd = new SqlCommand("USP_GET_LINEOFACTIVITY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sector", string.IsNullOrEmpty(sector) ? DBNull.Value : sector);

                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            activities.Add(new LineofActivity
                            {
                                IntLineofActivityId = Convert.ToInt32(rdr["intLineofActivityid"]),
                                LineofActivity_Name = rdr["LineofActivity_Name"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(activities);
        }

        [HttpGet("pcb-category")]
        public IActionResult GetPcbCategory([FromQuery] string lineofactivityId)
        {
            var result = new List<PcbCategory>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                using (SqlCommand cmd = new SqlCommand("USP_GET_PCBCATEGORY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LineofactivityID", lineofactivityId);

                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(new PcbCategory
                            {
                                IntLineofActivityId = Convert.ToInt32(rdr["intLineofActivityid"]),
                                LineofActivity_Name = rdr["LineofActivity_Name"].ToString(),
                                PCBCategory = rdr["PCBcategory"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("mandals")]
        public IActionResult GetMandalsByDistrict([FromQuery] int districtId)
        {
            var result = new List<Mandal>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MIPASS")))
            {
                using (SqlCommand cmd = new SqlCommand("USP_GET_MANDALS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DISTRICT", districtId);

                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(new Mandal
                            {
                                MandalName = rdr["MANDALNAME"].ToString(),
                                MandalCode = rdr["MANDALCODE"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }

    }
}
