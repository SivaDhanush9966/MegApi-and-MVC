using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MegApi.Models
{
    public class License1
    {
        public int UserId { get; set; }
        public int L1Id { get; set; }
        public string PermanentAddressLine { get; set; }
        public string PermanentVillage { get; set; }
        public string PermanentCityOrTown { get; set; }
        public string PermanentDistrict { get; set; }
        public string PermanentState { get; set; }
        public string PermanentPinCode { get; set; }
        public string PersonalAddressLine { get; set; }
        public string PersonalVillage { get; set; }
        public string PersonalCityOrTown { get; set; }
        public string PersonalDistrict { get; set; }
        public string PersonalState { get; set; }
        public string PersonalPinCode { get; set; }
        public string BusinessAddressLine { get; set; }
        public string BusinessVillage { get; set; }
        public string BusinessCityOrTown { get; set; }
        public string BusinessDistrict { get; set; }
        public string BusinessState { get; set; }
        public string BusinessPinCode { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIP { get; set; }
    }
}
