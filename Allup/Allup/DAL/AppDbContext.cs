using Allup.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Allup.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<NewsletterInformation> NewsletterInformations { get; set; }
        public DbSet<NewsletterForm> NewsletterForms { get; set; }
    }
}
