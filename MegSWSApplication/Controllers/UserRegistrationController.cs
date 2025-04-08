using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MegSWSApplication.Controllers
{
    public class UserRegistrationController : Controller
    {
        public ActionResult SaveUser()
        {
            return View();
        }

        // POST: Send data to API
        [HttpPost]
        public async Task<ActionResult> SaveUser(UserRegDetails user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            // Capture IP Address and Host
            string userIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            string hostName = Dns.GetHostEntry(HttpContext.Connection.RemoteIpAddress).HostName;
            user.IPAddress = $"{userIp} ({hostName})";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7149/"); // Replace later
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/UserReg/UserRegister", content); // Replace later
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "User registered successfully!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please try again later.");
                    return View(user);
                }
            }
        }


    }
}
