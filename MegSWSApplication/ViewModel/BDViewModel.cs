using MegSWSApplication.DTO;
using MegSWSApplication.Models.IndusRegModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MegSWSApplication.ViewModel
{
    public class BDViewModel
    {
        public IndustryDetailsDTO IndsDetails { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Districts { get; set; } // for the first dropdown
        public List<SelectListItem> Talukas { get; set; } // for the second
        public List<SelectListItem> Villages { get; set; } // for the third
        public bool IsMeghalayaSelectedAuth { get; set; } // for the first dropdown

        public bool IsMeghalayaSelectedProp { get; set; } // for the second dropdown

        public int ScrollPosition { get; set; }

    }
}
