using System.Collections.Generic;

namespace Allup.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public string Image { get; set; }
		public bool IsMain { get; set; }
		public List<Category> Children { get; set; }
		public Category Parent { get; set; }
		public int? ParentId { get; set; }
		public bool IsDeactive { get; set; }
	}
}
