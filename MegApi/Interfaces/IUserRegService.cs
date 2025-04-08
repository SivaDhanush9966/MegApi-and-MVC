using MegApi.Models;

namespace MegApi.Interfaces
{
    public interface IUserRegService
    {
        public string InsertUserRegDetails(UserRegDetails user);
        public UserInfo GetUserInfo(string username, string password, string ipAddress);
    }
}
