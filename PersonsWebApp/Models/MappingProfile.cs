using PersonDb.Models;

namespace PersonsWebApp.Models
{
    public static class MappingProfile
    {
        public static List<PersonViewModel> MappingPersonDBToPersonVM(List<Person> personsDB)
        {
            var persons = new List<PersonViewModel>(personsDB.Count());
            foreach (var personDB in personsDB)
            {
                persons.Add(new PersonViewModel()
                {
                    Id = personDB.Id,
                    Name = personDB.Name,
                    City=personDB.City,
                    Gender =personDB.Gender,
                    Age =personDB.Age
                });
            }
            return persons;
        }
    }
}
