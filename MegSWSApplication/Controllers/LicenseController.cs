using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MegSWSApplication.Controllers
{
    public class LicenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitLicenseData(LicenseeFullDetailsModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);

            using (var client = new HttpClient())
            {
                string apiUrl = "https://localhost:7149/api/License/InsL1"; // API

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        ViewBag.ApiResponse = "✅ Submitted successfully! Response: " + responseContent;
                    }
                    else
                    {
                        ViewBag.ApiResponse = $"❌ Submission failed! Status: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ApiResponse = "⚠️ Error occurred: " + ex.Message;
                }
            }

            // Return same view with message
            return View("Index", model);
        }
    }
}
