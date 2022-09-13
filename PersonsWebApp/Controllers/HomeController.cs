using System.IO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PersonDb.InterFaces;
using PersonsWebApp.Models;
using System.Diagnostics;

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