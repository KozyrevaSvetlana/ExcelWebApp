using PersonDb.Models;

namespace PersonDb.InterFaces
{
    public interface IPersonsRepository
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> GetPersonByIdAsync(Guid id);
        Task AddAsync(IEnumerable<Person> persons);
    }
}
