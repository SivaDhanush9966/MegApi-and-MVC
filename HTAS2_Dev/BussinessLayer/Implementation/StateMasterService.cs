using HTAS2_Dev.BussinessLayer.Interface;
using HTAS2_Dev.DataLayer.Interface;
using HTAS2_Dev.Models;

namespace HTAS2_Dev.BussinessLayer.Implementation
{
    public class StateMasterService : IStateMasterService
    {
        private readonly IStateMasterRepository _stateMasterRepository;
        public StateMasterService(IStateMasterRepository stateMasterRepository)
        {
            _stateMasterRepository = stateMasterRepository;
        }        
        List<StateMaster> IStateMasterService.GetAllStates()
        {
            return _stateMasterRepository.GetAllStates();
        }
    }
}
