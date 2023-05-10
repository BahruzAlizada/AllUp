using Allup.Models;
using Microsoft.EntityFrameworkCore;

namespace Allup.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
