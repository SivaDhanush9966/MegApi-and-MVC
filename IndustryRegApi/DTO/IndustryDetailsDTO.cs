using IndustryRegApi.Models;

namespace IndustryRegApi.DTO
{
    public class IndustryDetailsDTO:IndustryDetails
    {
        public string UserID { get; set; }
        public int InvesterID { get; set; }
        public string IPAddress { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPAN { get; set; }
        public string CompnyType { get; set; }
        public string CompnyProposal { get; set; }
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


        public string NatureofActivity { get; set; }
        public string ManfActivity { get; set; }
        public string Manfproduct { get; set; }
        public string ServiceActivity { get; set; }
        public string ServiceTobeProviding { get; set; }
        public string ProductionNO { get; set; }
        public string ServiceNo { get; set; }
        public string ProductManufactured { get; set; }

        public string AnnualCapacity { get; set; }
        public string MeasurementUnits { get; set; }
        public DateTime ProjectDCP { get; set; }

        public string EstimatedProjCost { get; set; }
        public string LandAreainSqft { get; set; }
        public string CivilConstr { get; set; }
        public string PlantnMachineryCost { get; set; }
        public string BuildingAreaSqm { get; set; }
        public string WaterReqKLD { get; set; }
        public string PowerReqKV { get; set; }
        public string WasteDetails { get; set; }
        public string HazWasteDetails { get; set; }
        public string CapitalInvestment { get; set; }
        public string FixedAssets { get; set; }
        public string LandValue { get; set; }
        public string BuildingValue { get; set; }
        public string WorkingCapital { get; set; }
        public string EquityAmount { get; set; }
        public string UnsecuredLoan { get; set; }
        public string CetralSchemeAmount { get; set; }
        public string StateSchemeAmount { get; set; }
        public string Sector { get; set; }
        public string LineofAct { get; set; }
        public string pollutionCatgy { get; set; }
        public string workingLoan { get; set; }





    }
}
