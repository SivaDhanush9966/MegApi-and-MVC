using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MegSWSApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please fill all required fields.";
                return View(model);
            }

            using (var client = new HttpClient())
            {
                string apiUrl = "https://localhost:7149/api/UserReg/UserLogin";
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponse>(jsonResult);

                    // Store token and user info in session
                    HttpContext.Session.SetString("JWToken", result.token);
                    HttpContext.Session.SetString("FullName", result.data.Fullname);
                    HttpContext.Session.SetString("UserID", result.data.Userid);
                    HttpContext.Session.SetString("PAN", result.data.PANno);
                    HttpContext.Session.SetString("Email", result.data.Email);
                    HttpContext.Session.SetString("EntityName", result.data.EntityName);
                    
                    return RedirectToAction("Index", "UserDashboard");
                }
                else
                {
                    ViewBag.Message = "Invalid credentials!";
                }
            }

            return View(model);
        }

    }
}
