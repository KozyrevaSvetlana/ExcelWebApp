using System.ComponentModel.DataAnnotations;

namespace PersonsWebApp.Models
{
    public class PersonViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }
        [Required(ErrorMessage = "Укажите пол")]
        public char Gender { get; set; }
        [Required(ErrorMessage = "Укажите возраст")]
        public int Age { get; set; }
    }
}
