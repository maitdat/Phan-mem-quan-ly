using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models.Entities
{
    public class Employee : Person
    {
        [Required]
        [Range(1,100000)]
        public string MaNhanVien {  get; set; }
        [Required]
        [MaxLength(50)]
        public string ViTri { get; set; }
    }
}
