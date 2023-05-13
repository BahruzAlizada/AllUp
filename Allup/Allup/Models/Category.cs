using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allup.Models
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		public string CategoryName { get; set; }
		public string Image { get; set; }
		[NotMapped]
		public IFormFile Photo { get; set; }
		public bool IsMain { get; set; }
		public List<Category> Children { get; set; }
		public Category Parent { get; set; }
		public int? ParentId { get; set; }
		public bool IsDeactive { get; set; }
	}
}
