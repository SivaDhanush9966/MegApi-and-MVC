using MegSWSApplication.Filters;
using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MegSWSApplication.Controllers
{
    [SessionAuthorize]
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
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserID");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                
                var response = await client.GetAsync($"https://localhost:7149/api/Inverstor/GetInvestors?userId={1001}");

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
