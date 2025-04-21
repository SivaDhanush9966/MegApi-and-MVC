using MegSWSApplication.Models.IndusRegModels;
using Microsoft.AspNetCore.Mvc;

namespace MegSWSApplication.Controllers.IndustryReg
{
    public class PromoterRegController : Controller
    {
        public IActionResult PromoterDetailsIdx(IndustryPromoter model)
        {
            return View(model);
        }
    }
}
