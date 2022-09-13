using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
                return await databaseContext.Persons.ToListAsync();
        }

        public async Task AddAsync(IEnumerable<Person> persons)
        {
            await databaseContext.Persons.AddRangeAsync(persons);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<Person> GetPersonByIdAsync(Guid id)
            => await databaseContext.Persons.FirstOrDefaultAsync(p => p.Id == id);
    }
}
