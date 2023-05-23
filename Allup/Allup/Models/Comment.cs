using System;
using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Message can not be null")]
        public string Message { get; set; }
        [Required(ErrorMessage ="Name can not be null")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email can not be null")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public bool IsDeactive { get;set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow.AddHours(4);
        public BlogDetail BlogDetail { get; set; }
        public int BlogDetailId { get;set; }
    }
}
