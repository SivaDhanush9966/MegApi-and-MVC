using IndustryRegApi.Models;

namespace IndustryRegApi.DTO
{
    public class InvestmentPlanDTO: InvestmentPlan
    {
     
        public string ItemDescription { get; set; }
        public decimal Year1 { get; set; }
        public decimal Year2 { get; set; }
        public decimal Year3 { get; set; }
        public decimal Year4 { get; set; }
        public decimal Year5 { get; set; }
      
    }
}
