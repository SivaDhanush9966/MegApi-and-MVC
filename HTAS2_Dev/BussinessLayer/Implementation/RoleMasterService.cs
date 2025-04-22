using HTAS2_Dev.BussinessLayer.Interface;
using HTAS2_Dev.DataLayer.Implementation;
using HTAS2_Dev.DataLayer.Interface;
using HTAS2_Dev.Models;

namespace HTAS2_Dev.BussinessLayer.Implementation
{
    public class RoleMasterService : IRoleMasterService
    {
        private readonly IRoleMasterRepository _roleMasterRepository;
        public RoleMasterService(IRoleMasterRepository roleMasterRepository)
        {
            _roleMasterRepository = roleMasterRepository;
        }
        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return _roleMasterRepository.GetAllRolesAsync();

        }
    }
}
