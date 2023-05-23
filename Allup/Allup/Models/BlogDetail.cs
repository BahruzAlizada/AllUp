using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allup.Models
{
    public class BlogDetail
    {
        public int Id { get; set; }
        public List<BlogTags> BlogTags { get; set; }
        public Blog Blog { get; set; }
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
