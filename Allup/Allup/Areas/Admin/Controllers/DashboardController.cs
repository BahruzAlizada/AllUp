using Allup.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.BlogCount = await _db.Blogs.CountAsync();
            ViewBag.CategoryCount = await _db.Categories.Where(x=>x.IsMain).CountAsync();
            return View();
        }
    }
}
