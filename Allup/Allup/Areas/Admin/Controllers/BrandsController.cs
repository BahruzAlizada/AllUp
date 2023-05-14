using Allup.DAL;
using Allup.Helpers;
using Allup.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BrandsController : Controller
	{
		private readonly AppDbContext _db;
		private readonly IWebHostEnvironment _env;

        public BrandsController(AppDbContext db,IWebHostEnvironment env)
        {
			_env = env;
            _db = db;
        }

		#region Index
		public async Task<IActionResult> Index()
		{
			List<Brand> brands = await _db.Brands.ToListAsync();
			return View(brands);
		}
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(Brand brand)
		{
			#region Image
			if (brand.Photo == null)
			{
				ModelState.AddModelError("Photo", "Photo can not be null");
				return View();
			}
			if (!brand.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "Select image type");
				return View();
			}
			if (brand.Photo.IsOlder256Kb())
			{
				ModelState.AddModelError("Photo", "Max 256Kb");
				return View();
			}
			string folder = Path.Combine(_env.WebRootPath, "assets", "images","brand");
			brand.Image = await brand.Photo.SaveFileAsync(folder);
			#endregion

			await _db.Brands.AddAsync(brand);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		#endregion

		#region Update
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null)
				return NotFound();
			Brand dbbrand = await _db.Brands.FirstOrDefaultAsync(x => x.Id == id);
			if (dbbrand == null)
				return BadRequest();

			return View(dbbrand);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Update(int? id,Brand brand)
		{
			if (id == null)
				return NotFound();
			Brand dbbrand = await _db.Brands.FirstOrDefaultAsync(x => x.Id == id);
			if (dbbrand == null)
				return BadRequest();

			#region Image
			if (brand.Photo == null)
			{
				ModelState.AddModelError("Photo", "Photo can not be null");
				return View();
			}
			if (!brand.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "Select image type");
				return View();
			}
			if (brand.Photo.IsOlder256Kb())
			{
				ModelState.AddModelError("Photo", "Max 256Kb");
				return View();
			}
			string folder = Path.Combine(_env.WebRootPath, "assets", "images","brand");
			brand.Image = await brand.Photo.SaveFileAsync(folder);
			string path = Path.Combine(_env.WebRootPath, folder, dbbrand.Image);
			if (System.IO.File.Exists(path))
				System.IO.File.Delete(path);
			dbbrand.Image = brand.Image;
			#endregion

			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		#endregion

		#region Activity
		public async Task<IActionResult> Activity(int? id)
		{
			if (id == null)
				return NotFound();
			Brand dbbrand = await _db.Brands.FirstOrDefaultAsync(x => x.Id == id);
			if (dbbrand == null)
				return BadRequest();

			if (dbbrand.İsDeactive)
				dbbrand.İsDeactive = false;
			else
				dbbrand.İsDeactive=true;

			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		#endregion
	}
}
