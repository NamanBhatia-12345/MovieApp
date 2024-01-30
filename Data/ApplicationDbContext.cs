using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Models;

namespace MovieApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
