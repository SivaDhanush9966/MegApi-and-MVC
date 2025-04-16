using IndustryRegApi.Models;

namespace IndustryRegApi.Interface
{
    public interface IPromoter
    {
        public string CreatePromoter(Promoter promoter);
        public Promoter GetPromoter(int userId);
    }
}
