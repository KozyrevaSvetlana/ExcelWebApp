using AutoMapper;
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
        public IActionResult Index()
        {
            var personsDB = persons.AllPersons;
            var personsVM = MappingProfile.MappingPersonDBToPersonVM(personsDB);
            return View(personsVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}