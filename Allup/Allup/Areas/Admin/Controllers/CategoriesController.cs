using Allup.DAL;
using Allup.Helpers;
using Allup.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;
        private IWebHostEnvironment _env;

        public CategoriesController(AppDbContext db,IWebHostEnvironment env)
        {
            _env = env;
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _db.Categories.OrderByDescending(x => x.IsMain).Include(x => x.Children).
                                                                                Include(X => X.Children).ToListAsync();
            return View(categories);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MainCategories = await _db.Categories.Where(x=>x.IsMain).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Category category,int catId)
        {
            ViewBag.MainCategories = await _db.Categories.Where(x => x.IsMain).ToListAsync();
            if (category.IsMain)
            {
                #region Exist
                bool isExist = await _db.Categories.AnyAsync(x => x.CategoryName == category.CategoryName);
                if (isExist)
                {
                    ModelState.AddModelError("CategoryName", "This category already is exist");
                    return View();
                }
                #endregion

                #region Image
                if (category.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Photo can not be null");
                    return View();
                }

                if (!category.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select image type");
                    return View();
                }

                if (category.Photo.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "Max 256Kb");
                    return View();
                }

                string folder = Path.Combine(_env.WebRootPath,"assets","images");
                category.Image = await category.Photo.SaveFileAsync(folder);
                #endregion
            }
            else
            {
                category.ParentId = catId;
            }

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.MainCategories = await _db.Categories.Where(x=>x.IsMain).ToListAsync();

            if (id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcategory == null)
                return BadRequest();

          
                return View(dbcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Category category,int? catId)
        {
            ViewBag.MainCategories = await _db.Categories.Where(x=>x.IsMain).ToListAsync();

            if (id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcategory == null)
                return BadRequest();

          

            if (dbcategory.IsMain)
            {
                #region Exist
                bool isExist = await _db.Categories.Where(x => x.IsMain).AnyAsync(x => x.CategoryName == category.CategoryName && x.Id != id);
                if (isExist)
                {
                    ModelState.AddModelError("CategoryName", "This category already is exist ");
                    return View();
                }
                #endregion

                #region Image
                if (category.Photo != null)
                {
                    if (!category.Photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Select image type");
                        return View();
                    }

                    if (category.Photo.IsOlder256Kb())
                    {
                        ModelState.AddModelError("Photo", "Max 256Kb");
                        return View();
                    }

                    string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                    category.Image = await category.Photo.SaveFileAsync(folder);
                    string path = Path.Combine(_env.WebRootPath, folder, dbcategory.Image);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    dbcategory.Image = category.Image;
                }
                #endregion
                dbcategory.CategoryName = category.CategoryName;
            }
            else
            {
                dbcategory.ParentId = catId;
                dbcategory.CategoryName = category.CategoryName;
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbcategory == null)
                return BadRequest();

            if (dbcategory.IsDeactive)
                dbcategory.IsDeactive = false;
            else
                dbcategory.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
