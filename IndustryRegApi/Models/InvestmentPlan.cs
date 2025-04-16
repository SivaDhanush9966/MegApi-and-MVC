namespace IndustryRegApi.Models
{
    public class InvestmentPlan
    {
        public int IbpId { get; set; }
        public int IbpUnitId { get; set; }
        public int IbpInvesterId { get; set; }
        public decimal Year1 { get; set; }
        public decimal Year2 { get; set; }
        public decimal Year3 { get; set; }
        public decimal Year4 { get; set; }
        public decimal Year5 { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MrpId { get; set; }
    }
}
