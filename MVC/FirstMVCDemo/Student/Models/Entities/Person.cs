using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string HoTen {  get; set; }
        public int Age { get; set; }
    }
}
