using Allup.DAL;
using Allup.Helpers;
using Allup.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BioController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public BioController(AppDbContext db,IWebHostEnvironment env)
        {
            _env = env;
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Bio> bios = await _db.Bios.ToListAsync();
            return View(bios);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Bio dbbio = await _db.Bios.FirstOrDefaultAsync(x => x.Id == id);
            if (dbbio == null)
                return BadRequest();

            return View(dbbio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Bio bio)
        {
            if (id == null)
                return NotFound();
            Bio dbbio = await _db.Bios.FirstOrDefaultAsync(x => x.Id == id);
            if (dbbio == null)
                return BadRequest();

            #region Image
            if (bio.Photo != null)
            {
                if (!bio.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "select image type");
                    return View();
                }
                if (bio.Photo.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "select image type");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                dbbio.HeaderImage = await dbbio.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath, folder, dbbio.HeaderImage);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                dbbio.HeaderImage = bio.HeaderImage;
            }
            #endregion

            dbbio.HeaderDescription = bio.HeaderDescription;
            dbbio.HeaderPhone= bio.HeaderPhone;
            dbbio.FooterPhone= bio.FooterPhone;
            dbbio.FooterAddress = bio.FooterAddress;
            dbbio.FooterEmail = bio.FooterEmail;
            dbbio.FooterOpenDay = bio.FooterOpenDay;
            dbbio.FooterCloseDay = bio.FooterCloseDay;
            dbbio.FooterOpenTime = bio.FooterOpenTime;
            dbbio.FooterCloseTime = dbbio.FooterCloseTime;
           
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Bio dbbio = await _db.Bios.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbbio == null)
                return BadRequest();

            return View(dbbio);
        }
        #endregion
    }
}
