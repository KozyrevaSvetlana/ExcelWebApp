using PersonDb.InterFaces;
using PersonDb.Models;

namespace PersonDb.Repositories
{
    public class PersonsDbRepository : IPersonsRepository
    {
        private readonly DatabaseContext databaseContext;
        public PersonsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public PersonsDbRepository()
        {
        }

        public IEnumerable<Person> AllPersons
        {
            get
            {
                return databaseContext.Persons.ToList();
            }
        }

        public void Add(Person newPerson)
        {
            databaseContext.Persons.Add(newPerson);
            databaseContext.SaveChanges();
        }

        public Person GetPersonById(Guid id)
        {
            return databaseContext.Persons.FirstOrDefault(p => p.Id == id);
        }
    }
}
