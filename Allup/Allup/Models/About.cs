using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allup.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CompanyDescription { get; set; }
        public string TeamDescription { get; set; }
        public string TestimonialDescription { get; set; }
    }
}
