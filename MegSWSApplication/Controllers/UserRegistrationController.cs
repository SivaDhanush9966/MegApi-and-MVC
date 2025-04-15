using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
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
                client.BaseAddress = new Uri("https://localhost:5000"); // Replace later
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/user/register", content); // Replace later
                if (response.IsSuccessStatusCode)
                {

                    //SendConfirmationEmail(user.Email, user.Fullname);
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

        //private void SendConfirmationEmail(string toEmail, string userName)
        //{
        //    try
        //    {
        //        var fromEmail = "approfessiontax@gmail.com"; // Replace with your sender email
        //        var fromPassword = "ptihoqbvvwtsfqaz";       // Replace with your email password or app password

        //        var subject = "Registration Successful";
        //        var body = $"<h3>Hi {userName},</h3><p>Your registration was successful!</p>";

        //        var smtpClient = new SmtpClient("smtp.gmail.com")
        //        {
        //            Port = 587,
        //            Credentials = new NetworkCredential(fromEmail, fromPassword),
        //            EnableSsl = true,
        //        };

        //        var mailMessage = new MailMessage
        //        {
        //            From = new MailAddress(fromEmail),
        //            Subject = subject,
        //            Body = body,
        //            IsBodyHtml = true,
        //        };

        //        mailMessage.To.Add(toEmail);
        //        smtpClient.Send(mailMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error (or silently fail)
        //        Console.WriteLine("Email error: " + ex.Message);
        //    }
        //}



    }
}
