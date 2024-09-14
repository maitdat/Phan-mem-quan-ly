using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models.Entities
{
    public class Student
    {
        [Key]
        public long MaSinhVien { get; set; }
        public string TenSinhVien { get; set; }
    }
}
