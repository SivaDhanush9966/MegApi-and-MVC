using MegSWSApplication.DTO;
using MegSWSApplication.DTO.Master;
using MegSWSApplication.Models.IndusRegModels;
using MegSWSApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;

namespace MegSWSApplication.Controllers.IndustryReg
{
    public class PromoterRegController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PromoterRegController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1️⃣ GET: show the form with Districts loaded
        [HttpGet]
        public async Task<IActionResult> PromoterDetailsIdx()
        {
            ViewBag.IsMeghalaya = false; // Default value
            var model = new PromoterViewModel
            {
                PromoterDetails = new PromoterDTO(),
                States = await LoadStateAsync(),
                Districts = new List<SelectListItem>(), // Empty initially
                Talukas = new List<SelectListItem>(), // Empty initially
                Villages = new List<SelectListItem>(), // Empty initially
            };

            return View(model);
        }
       

        // 2️⃣ POST: handle submit, re‑populate on error
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoterDetailsIdx(PromoterViewModel model, string action)
        {
            model.States = await LoadStateAsync();

            if (!ModelState.IsValid)
            {
                if (model.PromoterDetails.IDD_STATE == "23")
                {
                    ViewBag.IsMeghalaya = (model.PromoterDetails.IDD_STATE == "23");
                    model.Districts = await LoadDistrictsAsync(model.PromoterDetails.IDD_STATE);
                    model.Talukas = await LoadTalukasAsync(model.PromoterDetails.IDD_DISTRICT);
                    model.Villages = await LoadVillagesAsync(model.PromoterDetails.IDD_MANDAL);
                }
                else
                {
                    model.Districts = new List<SelectListItem>();
                    model.Talukas = new List<SelectListItem>();
                    model.Villages = new List<SelectListItem>();
                }

                return View(model);
            }

            // Save to DB
            if (action == "saveDraft")
                TempData["Message"] = "Saved as draft";

            return RedirectToAction("PromoterDetailsIdx");

        }



        [HttpGet]
        public async Task<IActionResult> GetState()
        {
            var items = await LoadStateAsync();
            return Json(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistricts(int stateId)
        {
            var items = await LoadDistrictsAsync(stateId.ToString());
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
        private async Task<List<SelectListItem>> LoadStateAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var dtos = await client.GetFromJsonAsync<List<StateDTO>>("https://localhost:7120/api/Location/states");
            return dtos
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();
        }

        private async Task<List<SelectListItem>> LoadDistrictsAsync(string stateId)
        {
            if (!int.TryParse(stateId, out var id))
                return new List<SelectListItem>();

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
