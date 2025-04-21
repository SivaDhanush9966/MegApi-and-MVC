using MegSWSApplication.DTO;
using MegSWSApplication.DTO.Master;
using MegSWSApplication.Models.IndusRegModels;
using MegSWSApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace MegSWSApplication.Controllers.IndustryReg
{
    public class IndustryRegController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndustryRegController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1️⃣ GET: show the form with Districts loaded
        [HttpGet]
        public async Task<IActionResult> BasicDetails()
        {
            var model = new BDViewModel
            {
                IndsDetails = new IndustryDetailsDTO(),
                Districts = await LoadDistrictsAsync(),
                Talukas = new List<SelectListItem>(),
                Villages = new List<SelectListItem>()
            };
            return View(model);
        }

        // 2️⃣ POST: handle submit, re‑populate on error
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BasicDetails(BDViewModel model, string action)
        {
            if (!ModelState.IsValid)
            {
                model.Districts = await LoadDistrictsAsync();
                model.Talukas = await LoadTalukasAsync(model.IndsDetails.PropLocDist);
                model.Villages = await LoadVillagesAsync(model.IndsDetails.PropLocTaluka);
                return View(model);
            }

            // TODO: save model.IndsDetails into your database here…

            if (action == "saveDraft")
                TempData["Message"] = "Saved as draft";

            return RedirectToAction("BasicDetails");
        }

         //3️⃣ AJAX helper: all Districts(no parameter)

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> BasicDetails(BDViewModel model, string action)
        //{
        //    // Always load districts
        //    model.Districts = await LoadDistrictsAsync();

        //    // Only load Talukas if a district is selected
        //    if (model.IndsDetails.PropLocDist != null)
        //        model.Talukas = await LoadTalukasAsync(model.IndsDetails.PropLocDist);

        //    // Only load Villages if a taluka is selected
        //    if (model.IndsDetails.PropLocTaluka != null)
        //        model.Villages = await LoadVillagesAsync(model.IndsDetails.PropLocTaluka);

        //    if (action == "saveDraft" || action == "next")
        //    {
        //        if (!ModelState.IsValid)
        //            return View(model);

        //        // Save your model to DB here...
        //        TempData["Message"] = "Saved";

        //        return RedirectToAction("BasicDetails");
        //    }

        //    // If action is just due to dropdown change (form submitted by onchange), just return the view
        //    return View(model);
        //}


        [HttpGet]
        public async Task<IActionResult> GetDistricts()
        {
            var items = await LoadDistrictsAsync();
            return Json(items);
        }

        // 4️⃣ AJAX helper: Mandals/Talukas by districtId
        [HttpGet]
        public async Task<IActionResult> GetTalukas(int districtId)
        {
            var items = await LoadTalukasAsync(districtId.ToString());
            return Json(items);
        }

        // 5️⃣ AJAX helper: Villages by talukaId
        [HttpGet]
        public async Task<IActionResult> GetVillages(int talukaId)
        {
            var items = await LoadVillagesAsync(talukaId.ToString());
            return Json(items);
        }

        // ──── Helpers ───────────────────────────────────────────────────────────

        private async Task<List<SelectListItem>> LoadDistrictsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var dtos = await client.GetFromJsonAsync<List<DistrictDTO>>("https://localhost:7120/api/Location/districts");
            return dtos
                .Select(d => new SelectListItem { Value = d.Code.ToString(), Text = d.Name })
                .ToList();
        }

        private async Task<List<SelectListItem>> LoadTalukasAsync(string districtId)
        {
            if (!int.TryParse(districtId, out var id))
                return new List<SelectListItem>();

            var client = _httpClientFactory.CreateClient();
            var dtos = await client.GetFromJsonAsync<List<MandalDTO>>(
                             $"https://localhost:7120/api/Location/mandals?districtId={districtId}");
            return dtos
                .Select(m => new SelectListItem { Value = m.MandalCode.ToString(), Text = m.MandalName })
                .ToList();
        }

        private async Task<List<SelectListItem>> LoadVillagesAsync(string talukaId)
        {
            if (!int.TryParse(talukaId, out var id))
                return new List<SelectListItem>();

            var client = _httpClientFactory.CreateClient();

            // Use your exact request URL pattern here:
            var url = $"https://localhost:7120/api/Location/villages/{id}";

            var dtos = await client.GetFromJsonAsync<List<VillageDTO>>(url);

            return dtos
                .Select(v => new SelectListItem { Value = v.Code.ToString(), Text = v.Name })
                .ToList();
        }
    }
}
