using IndustryRegApi.Models;

namespace IndustryRegApi.Interface
{
    public interface IIndustryFile
    {
        Task<string> SaveIndustryFileDetailsAsync(IndustryFileDetails fileDetails);
        Task<IEnumerable<IndustryFileDetails>> GetAllFilesAsync();

        Task<IndustryFileDetails> GetFileDetailsByIdAsync(int slno);
    }
}
