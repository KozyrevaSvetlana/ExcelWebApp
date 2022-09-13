using PersonDb.Models;

namespace PersonDb
{
    public static class PersonGenerator
    {
        private static Random random = new Random();

        public static List<Person> GeneradeRandomPersons(int count)
        {
            var persons = new List<Person>();
            for (int i = 0; i < count; i++)
            {
                int gender = random.Next(0, 2);
                var person = new Person();
                person.Id = Guid.NewGuid();
                person.Gender = (GenderEnum)gender;
                if (gender == 0)
                {
                    person.Name = namesMale[random.Next(0, namesMale.Count())];
                }
                else
                {
                    person.Name = namesFemale[random.Next(0, namesMale.Count())];
                }
                person.City = cities[random.Next(0, cities.Count())];
                person.Age = random.Next(18, 80);
                persons.Add(person);
            }
            return persons;
        }

        private static string[] namesMale = new string[]
        {
            "Петя", "Женя", "Олег", "Саша", "Дима","Денис","Вова","Ваня","Слава","Рома"
        };
        private static string[] namesFemale = new string[]
        {
            "Настя", "Катя", "Ира", "Алиса", "Света","Оля","Маша","Аня","Оксана","Лиза"
        };
        private static string[] cities = new string[]
        {
            "Москва", "Санкт-Петербург", "Нижний Новгород", "Екатеринбур", "Краснодар",
            "Сочи","Самара","Ярославль","Новосибирск","Курган"
        };
    }
}
