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
    public class BannersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public BannersController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Banner> banners = await _db.Banners.ToListAsync();
            return View(banners);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Banner banner)
        {
            #region Image
            if(banner.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can not be null");
                return View();
            }
            if (!banner.Photo.IsImage())
            {
				ModelState.AddModelError("Photo", "Select image type");
				return View();
			}
            if (banner.Photo.IsOlder256Kb())
            {
				ModelState.AddModelError("Photo", "Max 256Kb");
				return View();
			}
            string folder = Path.Combine(_env.WebRootPath, "assets", "images");
            banner.Image = await banner.Photo.SaveFileAsync(folder);
            #endregion

            await _db.Banners.AddAsync(banner);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Banner dbbanner = await _db.Banners.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbbanner == null)
                return BadRequest();

            return View(dbbanner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

		public async Task<IActionResult> Update(int? id,Banner banner)
		{
			if (id == null)
				return NotFound();
			Banner dbbanner = await _db.Banners.FirstOrDefaultAsync(x => x.Id == id);
			if (dbbanner == null)
				return BadRequest();

			#region Image
			if (banner.Photo == null)
			{
				ModelState.AddModelError("Photo", "Photo can not be null");
				return View();
			}
			if (!banner.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "Select image type");
				return View();
			}
			if (banner.Photo.IsOlder256Kb())
			{
				ModelState.AddModelError("Photo", "Max 256Kb");
				return View();
			}
			string folder = Path.Combine(_env.WebRootPath, "assets", "images");
			banner.Image = await banner.Photo.SaveFileAsync(folder);
            string path = Path.Combine(_env.WebRootPath, folder, dbbanner.Image);
            if(System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            dbbanner.Image = banner.Image;
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
			Banner dbbanner = await _db.Banners.FirstOrDefaultAsync(x => x.Id == id);
			if (dbbanner == null)
				return BadRequest();

            if (dbbanner.IsDeactive)
                dbbanner.IsDeactive = false;
            else
                dbbanner.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
		}
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
			if (id == null)
				return NotFound();
			Banner dbbanner = await _db.Banners.FirstOrDefaultAsync(x => x.Id == id);
			if (dbbanner == null)
				return BadRequest();

            return View(dbbanner);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeletePost(int? id)
        {
			if (id == null)
				return NotFound();
			Banner dbbanner = await _db.Banners.FirstOrDefaultAsync(x => x.Id == id);
			if (dbbanner == null)
				return BadRequest();

            _db.Remove(dbbanner);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
		}
        #endregion
    }
}
