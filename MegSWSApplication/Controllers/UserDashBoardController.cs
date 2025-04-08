using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MegSWSApplication.Controllers
{
    public class UserDashBoardController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDashBoardController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            List<Investors> investors = new List<Investors>();
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // 🔁 Replace URL below with actual endpoint
                var response = await client.GetAsync($"https://your-api-url/api/UserReg/GetInvestors?userId={userId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    investors = JsonConvert.DeserializeObject<List<Investors>>(json);
                }
            }

            return View(investors);
        }
    }
}
