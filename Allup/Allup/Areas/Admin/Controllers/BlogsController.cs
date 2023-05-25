using Allup.DAL;
using Allup.Helpers;
using Allup.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public BlogsController(AppDbContext db,IWebHostEnvironment env)
        {
            _env= env;
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _db.Blogs.Include(x => x.Detail).ThenInclude(x => x.BlogTags).ThenInclude(x => x.Tag).ToListAsync();
            return View(blogs);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Tags = await _db.Tags.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Blog blog, int[] tagsId)
        {
            ViewBag.Tags = await _db.Tags.ToListAsync();

            #region Exist
            bool isExist = await _db.Blogs.AnyAsync(x => x.Title == blog.Title);
            if (isExist)
            {
                ModelState.AddModelError("Title", "This title already is Exist");
                return View();
            }
            #endregion

            #region Image
            if (blog.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can not be null");
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "photo can not be null");
                return View();
            }
            if (blog.Photo.IsOlder256Kb())
            {
                ModelState.AddModelError("Photo", "Max 256Kb");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "assets", "images");
            blog.Image = await blog.Photo.SaveFileAsync(folder);
            #endregion

            #region Tags
            List<BlogTags> blogTags = new List<BlogTags>();
            foreach (int tagid in tagsId)
            {
                BlogTags bgtg = new BlogTags
                {
                    TagId = tagid
                };
                blogTags.Add(bgtg);
            }
            #endregion

            blog.Detail = new BlogDetail();
            blog.Detail.BlogTags = blogTags;
            blog.CreatedTime = DateTime.UtcNow.AddHours(4);

            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
		}
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Tags = await _db.Tags.ToListAsync();
			if (id == null)
				return NotFound();
			Blog dbblog = await _db.Blogs.Include(x => x.Detail).ThenInclude(x => x.BlogTags).ThenInclude(x => x.Tag).FirstOrDefaultAsync(x => x.Id == id);
			if (dbblog == null)
				return BadRequest();

            return View(dbblog);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]

		public async Task<IActionResult> Update(int? id, int[] tagsId,Blog blog)
		{
			ViewBag.Tags = await _db.Tags.ToListAsync();
			if (id == null)
				return NotFound();
			Blog dbblog = await _db.Blogs.Include(x => x.Detail).ThenInclude(x => x.BlogTags).ThenInclude(x => x.Tag).FirstOrDefaultAsync(x => x.Id == id);
			if (dbblog == null)
				return BadRequest();

			#region Exist
			bool isExist = await _db.Blogs.AnyAsync(X => X.Title == blog.Title && X.Id != id);
			if (isExist)
			{
				ModelState.AddModelError("Title", "This Title already is exist");
				return View();
			}
			#endregion

			#region Image
			if (blog.Photo != null)
            {
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select image type");
                    return View();
                }
                if (blog.Photo.IsOlder256Kb())
                {
					ModelState.AddModelError("Photo", "Max 256Kb");
					return View();
				}
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                blog.Image = await dbblog.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath, folder, dbblog.Image);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                dbblog.Image = blog.Image;
            }
            #endregion

            #region Tags
            List<BlogTags> blogTags = new List<BlogTags>();
            foreach (int tagid in tagsId)
            {
                BlogTags bgtg = new BlogTags
                {
                    TagId = tagid,
                };
                blogTags.Add(bgtg);
            }
            dbblog.Detail = new BlogDetail();
            dbblog.Detail.BlogTags = blogTags;
			#endregion

			dbblog.Title = blog.Title;
			dbblog.Description = blog.Description;
            dbblog.Author = blog.Author;
            
            await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		#endregion

		#region Detail
		public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Blog dbblog = await _db.Blogs.Include(x=>x.Detail).ThenInclude(x=>x.BlogTags).ThenInclude(x=>x.Tag).FirstOrDefaultAsync(x => x.Id == id);
            if (dbblog == null)
                return BadRequest();

            return View(dbblog);
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Blog dbblog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbblog == null)
                return BadRequest();

            if (dbblog.IsDeactive)
                dbblog.IsDeactive = false;
            else
                dbblog.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
