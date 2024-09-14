using FirstMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; } = default!;
    }
}
