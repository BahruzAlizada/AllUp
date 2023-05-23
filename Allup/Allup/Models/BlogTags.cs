using System.ComponentModel.DataAnnotations.Schema;

namespace Allup.Models
{
    public class BlogTags
    {
        public int Id { get; set; }
        public BlogDetail BlogDetail { get; set; }
        public int BlogId { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
