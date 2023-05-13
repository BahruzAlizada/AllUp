using Allup.Models;
using System.Collections.Generic;

namespace Allup.ViewsModel
{
	public class HomeVM
	{
		public List<Category> Categories { get; set; }
		public List<Blog> Blogs { get; set; }
        public NewsletterInformation NewsletterInformation { get; set; }
        public List<NewsletterForm> NewsletterForm { get; set; }
    }
}
