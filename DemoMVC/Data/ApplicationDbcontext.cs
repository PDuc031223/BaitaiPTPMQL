
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Data;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Daily> Daily { get; set; }
        public DbSet<HeThongPhanPhoi> HeThongPhanPhois { get; set; }

        public class HeThongPhanPhoi
        {
            public int Id { get; set; }
        }
        public DbSet<MvcMovie.Data.HeThongPhanPhoi> HeThongPhanPhoi_1 { get; set; } = default!;
    }
    public class HeThongPhanPhoi
    {
        public int Id {get; set; }
    }

    public class Daily
    {
         public int Id { get; set; }
    }
}