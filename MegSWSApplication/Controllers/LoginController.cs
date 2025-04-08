using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MegSWSApplication.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please fill all required fields.";
                return View(model);
            }

            // Convert to JSON and send to API
            using (var client = new HttpClient())
            {
                string apiUrl = "https://localhost:7149/api/UserReg/UserLogin"; // Replace with your actual API URL
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Login Successful!";
                    // Optionally redirect or set session

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
