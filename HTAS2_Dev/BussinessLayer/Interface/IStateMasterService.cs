using HTAS2_Dev.Models;

namespace HTAS2_Dev.BussinessLayer.Interface
{
    public interface IStateMasterService
    {
        List<StateMaster> GetAllStates();
    }
}
