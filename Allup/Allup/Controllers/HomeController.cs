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
				Categories = await _db.Categories.Where(x=>x.IsMain).ToListAsync()
			};
			return View(homeVM);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
