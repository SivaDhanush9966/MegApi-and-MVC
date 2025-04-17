using MegSWSApplication.Models.IndusRegModels;
using Microsoft.AspNetCore.Mvc;

namespace MegSWSApplication.Controllers.IndustryReg
{
    public class IndustryRegController : Controller
    {
        public IActionResult BasicDetails(IndustryDetails model)
        {
            return View(model);
        }


    }
}
