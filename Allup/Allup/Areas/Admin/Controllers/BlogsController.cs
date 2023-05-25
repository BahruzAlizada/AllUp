using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly AppDbContext _db;

        public BlogsController(AppDbContext db)
        {
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
        
        #endregion
    }
}
