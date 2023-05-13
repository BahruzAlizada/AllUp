using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
    public class NewsletterForm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Email can not be null")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
