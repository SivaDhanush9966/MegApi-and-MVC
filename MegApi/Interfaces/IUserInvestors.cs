using MegApi.Models;

namespace MegApi.Interfaces
{
    public interface IUserInvestors
    {
        public List<Investors> GetInvestors(string userId);
    }
}
