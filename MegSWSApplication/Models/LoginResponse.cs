namespace MegSWSApplication.Models
{
    public class LoginResponse
    {
        public string message { get; set; }
        public string token { get; set; }
        public UserInfo data { get; set; }
    }
}
