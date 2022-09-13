using PersonDb.Models;

namespace PersonsWebApp.Models
{
    public static class MappingProfile
    {
        public static List<PersonViewModel> MappingPersonDBToPersonVM(IEnumerable<Person> personsDB)
        {
            var persons = new List<PersonViewModel>();
            foreach (var personDB in personsDB)
            {
                persons.Add(new PersonViewModel()
                {
                    Id = personDB.Id,
                    Name = personDB.Name,
                    City=personDB.City,
                    Age =personDB.Age
                });
            }
            return persons;
        }
    }
}
