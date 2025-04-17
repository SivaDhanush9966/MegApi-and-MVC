using MegSWSApplication.Models.IndusRegModels;
using MegSWSApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MegSWSApplication.Controllers.IndustryReg
{
    public class IndustryRegController : Controller
    {
        public IActionResult BasicDetails(BDViewModel model)
        {
            return View(model);
        }
    }
}
