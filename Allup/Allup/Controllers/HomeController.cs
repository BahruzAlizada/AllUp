using Allup.DAL;
using Allup.Models;
using Allup.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Allup.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
			_db = db;
        }

        public async Task<IActionResult> Index()
		{
			HomeVM homeVM = new HomeVM
			{
				Categories = await _db.Categories.Where(x=>x.IsMain).Where(x=>!x.IsDeactive).ToListAsync(),
				Blogs = await _db.Blogs.Where(x=>!x.IsDeactive).OrderByDescending(x=>x.Id).Take(6).ToListAsync(),
				NewsletterInformation = await _db.NewsletterInformations.FirstOrDefaultAsync()
			};
			return View(homeVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(string Email)
        {
			NewsletterForm form = new NewsletterForm
			{
				Email=Email,
			};

			await _db.NewsletterForms.AddAsync(form);
			await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
		{
			return View();
		}
	}
}
