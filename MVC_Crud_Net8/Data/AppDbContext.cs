using Microsoft.EntityFrameworkCore;
using MVC_Crud_Net8.Models.Entities;

namespace MVC_Crud_Net8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
