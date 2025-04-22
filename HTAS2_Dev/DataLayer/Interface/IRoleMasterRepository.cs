using HTAS2_Dev.Models;

namespace HTAS2_Dev.DataLayer.Interface
{
    public interface IRoleMasterRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
