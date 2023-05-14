using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Description can not be null")]
        [MaxLength(400)]
        public string Description { get; set; }
        [Required(ErrorMessage ="Fullname can not be null")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Email can not be null")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsDeactive { get; set; }
    }
}
