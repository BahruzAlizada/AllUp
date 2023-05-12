using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string SubTitle { get; set; }
        [Required(ErrorMessage ="Title can not be null")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsDeactive { get; set; }
    }
}
