using System.IO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PersonDb.InterFaces;
using PersonsWebApp.Models;
using System.Diagnostics;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Drawing.Drawing2D;
using ExcelDataReader;
using Microsoft.Win32;

namespace PersonsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonsRepository persons;
        IWebHostEnvironment appEnvironment;
        public HomeController(IPersonsRepository persons, IWebHostEnvironment appEnvironment)
        {
            this.persons = persons;
            this.appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            var personsDB = persons.AllPersons;
            var personsVM = MappingProfile.MappingPersonDBToPersonVM(personsDB);
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
                    stream.Position = 1;
                    try
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            while (reader.Read())
                            {
                                newPersons.Add(new PersonViewModel
                                {
                                    Name = reader.GetValue(0).ToString(),
                                    City = reader.GetValue(1).ToString(),
                                    Gender = reader.GetValue(2).ToString(),
                                    Age = Int32.Parse(reader.GetValue(3).ToString())
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Неверные данные");
                        var personsDB = persons.AllPersons;
                        var personsVM = MappingProfile.MappingPersonDBToPersonVM(personsDB);
                        return View("Index", personsVM);
                    }
                }
                return Ok(newPersons);
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