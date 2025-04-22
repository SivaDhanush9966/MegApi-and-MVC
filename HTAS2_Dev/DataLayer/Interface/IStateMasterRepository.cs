using HTAS2_Dev.Models;

namespace HTAS2_Dev.DataLayer.Interface
{
    public interface IStateMasterRepository
    {
        List<StateMaster> GetAllStates();
    }
}
