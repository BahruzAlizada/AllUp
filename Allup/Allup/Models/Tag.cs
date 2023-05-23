using System.Collections.Generic;

namespace Allup.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BlogTags> BlogTags { get; set; }
    }
}
