using MegSWSApplication.Models;
using MegSWSApplication.Models.IndusRegModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MegSWSApplication.Controllers.IndustryReg
{
    public class InvestmentPlanController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvestmentPlanController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            InvestmentPlan  model = new InvestmentPlan();

            using (var client = new HttpClient())
            {
                var userId = _httpContextAccessor.HttpContext.Session.GetString("UserID");
                client.BaseAddress = new Uri("https://localhost:5000");

                HttpResponseMessage response = await client.GetAsync($"Licence?userId={userId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                   // model = JsonConvert.DeserializeObject<InvestmentPlanController>(result);
                }
            }

            return View(model);
        }
    }
}
