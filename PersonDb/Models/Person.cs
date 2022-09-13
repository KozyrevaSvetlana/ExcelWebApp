using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDb.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
    }
}
