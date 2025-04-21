using MegSWSApplication.DTO;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace MegSWSApplication.ViewModel
{
    public class PromoterViewModel
    {
        public PromoterDTO PromoterDetails { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Districts { get; set; } // for the first dropdown
        public List<SelectListItem> Talukas { get; set; } // for the second
        public List<SelectListItem> Villages { get; set; } // for the third
    }
}
