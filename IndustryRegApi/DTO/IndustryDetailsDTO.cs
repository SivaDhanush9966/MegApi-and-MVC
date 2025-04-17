using IndustryRegApi.Models;

namespace IndustryRegApi.DTO
{
    public class IndustryDetailsDTO:IndustryDetails
    {
        public string CompanyName { get; set; }
        public string CompanyPAN { get; set; }
        public string CompnyRegDt { get; set; }
        public string CompnyType { get; set; }
        public string CompnyProposal { get; set; }
        public string UdyamorIEMNo { get; set; }
        public string GSTNo { get; set; }


        public string AuthReprName { get; set; }
        public string AuthReprMobile { get; set; }
        public string AuthReprEmail { get; set; }
        public string AuthReprLocality { get; set; }
        public string AuthReprDist { get; set; }
        public string AuthReprDistID { get; set; }
        public string AuthReprTaluka { get; set; }
        public string AuthReprTalukaID { get; set; }
        public string AuthReprVillage { get; set; }
        public string AuthReprVillageID { get; set; }
        public string AuthReprPincode { get; set; }


        public string LandType { get; set; }
        public string PropLocDoorno { get; set; }
        public string PropLocLocality { get; set; }
        public string PropLocDist { get; set; }
        public string PropLocDistID { get; set; }
        public string PropLocTaluka { get; set; }
        public string PropLocTalukaID { get; set; }
        public string PropLocVillage { get; set; }
        public string PropLocVillageID { get; set; }
        public string PropLocPincode { get; set; }


         
    }
}
