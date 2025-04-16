using IndustryRegApi.Models;

namespace IndustryRegApi.Interface
{
    public interface IIndustryDetails
    {
        Task<IndustryDetails> GetIndustryDetailsByIdAsync(string id);
        Task<IEnumerable<IndustryDetails>> GetAllIndustryDetailsAsync();
        Task<IndustryDetails> CreateIndustryDetails(IndustryDetails details);
        Task<bool> UpdateIndustryDetailsAsync(string id, IndustryDetails updatedDetails);
    }
}
