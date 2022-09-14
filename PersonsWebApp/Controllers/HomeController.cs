using Microsoft.AspNetCore.Mvc;
using PersonDb.InterFaces;
using PersonsWebApp.Models;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.InkML;

namespace PersonsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonsRepository persons;
        public HomeController(IPersonsRepository persons)
        {
            this.persons = persons;
        }
        public async Task<IActionResult> Index()
        {
            var personsDB = await persons.GetAllPersonsAsync();
            var personsVM = Helpers.MappingPersonDBToPersonVM(personsDB);
            var maleFemaleList = (from plane in personsVM
                                  group plane by plane.Gender into grouppenPlanes
                                  where grouppenPlanes.Count() > 1
                                  select new Male
                                  {
                                      Gender = grouppenPlanes.Key,
                                      Count = grouppenPlanes.Count()
                                  })
          .ToList();
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            var male = maleFemaleList.FirstOrDefault(x=> x.Gender == "М")?.Count;
            var female = maleFemaleList.FirstOrDefault(x => x.Gender == "Ж")?.Count;
            ViewBag.Male = JsonConvert.SerializeObject(male, _jsonSetting);
            ViewBag.Female = JsonConvert.SerializeObject(female, _jsonSetting);
            return View(personsVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                List<PersonViewModel> newPersons = new List<PersonViewModel>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new MemoryStream())
                {
                    uploadedFile.CopyTo(stream);
                    Helpers.TrySafeFromExcel(uploadedFile, ModelState, newPersons);
                }
                if (ModelState.ErrorCount > 0)
                {
                    var personsDB = persons.GetAllPersonsAsync().Result;
                    var personsVM = Helpers.MappingPersonDBToPersonVM(personsDB);
                    return View("Index", personsVM);
                }
                var newPersonsDB = Helpers.MappingPersonVMToPersonDB(newPersons);
                await persons.AddAsync(newPersonsDB);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}