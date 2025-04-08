using MegSWSApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace MegSWSApplication.Controllers
{
    public class UserDashBoardController : Controller
    {
        public IActionResult Index(UserInfo userInfo)
        {
            return View(userInfo);
        }
    }
}
