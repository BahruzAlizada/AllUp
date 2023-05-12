using System;
using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
    public class ContactForm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name can not be null")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email can not be null")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Subject can not be null")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Message can not be null")]
        public string Message { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
