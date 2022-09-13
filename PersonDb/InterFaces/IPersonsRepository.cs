using PersonDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDb.InterFaces
{
    public interface IPersonsRepository
    {
        IEnumerable<Person> AllPersons { get; }
        Person GetPersonById(Guid id);
        void Add(Person newPerson);
    }
}
