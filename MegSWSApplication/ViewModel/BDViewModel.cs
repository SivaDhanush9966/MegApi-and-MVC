using MegSWSApplication.DTO;
using MegSWSApplication.Models.IndusRegModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MegSWSApplication.ViewModel
{
    public class BDViewModel
    {
        public  IndustryDetails IndsDetails  = new IndustryDetails();

        public IndustryDetailsDTO industryDetailsDTO = new IndustryDetailsDTO();
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Disttricts { get; set; }

        public List<SelectListItem> Talukas { get; set; }

        public List<SelectListItem> Villages { get; set; }


    }
}
