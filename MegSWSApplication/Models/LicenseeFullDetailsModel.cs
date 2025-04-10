namespace MegSWSApplication.Models
{
    public class LicenseeFullDetailsModel
    {
        public int UserId { get; set; }

        // Personal Details
        public string ApplicantName { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PAN { get; set; }
        public string Aadhaar { get; set; }
        public string PersonalDetailsAddress { get; set; }

        // Personal Address
        public string PersonalAddrLine { get; set; }
        public string PersonalVillage { get; set; }
        public string PersonalCity { get; set; }
        public string PersonalDistrict { get; set; }
        public string PersonalState { get; set; }
        public string PersonalPincode { get; set; }

        // Business Address
        public string BusinessAddrLine { get; set; }
        public string BusinessVillage { get; set; }
        public string BusinessCity { get; set; }
        public string BusinessDistrict { get; set; }
        public string BusinessState { get; set; }
        public string BusinessPincode { get; set; }

        //created details
        public string Createdby { get; set; }
        public string CreatedIp { get; set; }
    }
}
