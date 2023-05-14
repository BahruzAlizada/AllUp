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
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<About> about = await _db.Abouts.ToListAsync();
            return View(about);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            About dbabout = await _db.Abouts.FirstOrDefaultAsync(x=>x.Id== id);
            if(dbabout == null)
                return BadRequest();

            return View(dbabout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,About about)
        {
            if (id == null)
                return NotFound();
            About dbabout = await _db.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbabout == null)
                return BadRequest();

            #region Image
            if(about.Photo != null)
            {
                if (!about.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select image type");
                    return View();
                }
                if (about.Photo.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "Max 256Kb");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                about.Image = await dbabout.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath, folder, dbabout.Image);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                dbabout.Image = about.Image;
            }
            #endregion

            

            dbabout.Description=about.Description;
            dbabout.TeamDescription=about.TeamDescription;
            dbabout.CompanyDescription=about.CompanyDescription;
            dbabout.TestimonialDescription=about.TestimonialDescription;
            dbabout.Title = about.Title;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            About dbabout = await _db.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if(dbabout==null)
                return BadRequest();

            return View(dbabout);
        }
        #endregion
    }
}
