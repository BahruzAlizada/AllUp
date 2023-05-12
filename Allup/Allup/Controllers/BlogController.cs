using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allup.Controllers
{
	public class BlogController : Controller
	{
		private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
			_db = db;
        }

        public async Task<IActionResult> Index(string search)
		{
			ViewBag.BlogCount = await _db.Blogs.Where(x=>!x.IsDeactive).CountAsync();

			List<Blog> blogs = new List<Blog>();
			if (!string.IsNullOrEmpty(search))
				blogs = await _db.Blogs.Where(x => x.Title.Contains(search)).ToListAsync();
			else
				blogs = await _db.Blogs.OrderByDescending(x=>x.Id).Take(6).ToListAsync();
			return View(blogs);
		}

		public async Task<IActionResult> Loadmore(int skipCount)
		{
            int BlogCount = await _db.Blogs.Where(x => !x.IsDeactive).CountAsync();
			if (BlogCount <= skipCount)
			{
				return Content(".");
			}

			List<Blog> blogs = await _db.Blogs.Where(x=>!x.IsDeactive).OrderByDescending(x => x.Id).Skip(skipCount).Take(6).ToListAsync();
			return PartialView("_BlogLoadMorePartial", blogs);
		}
	}
}
