using IndustryRegApi.Models;

namespace IndustryRegApi.Interface
{
    public interface IInvestmentPlan
    {
        Task<InvestmentPlan> GetInvestmentPlanByIdAsync(int id);
        Task<IEnumerable<InvestmentPlan>> GetAllInvestmentPlansAsync();
        Task<InvestmentPlan> CreateInvestmentPlanAsync(InvestmentPlan plan);
    }
}
