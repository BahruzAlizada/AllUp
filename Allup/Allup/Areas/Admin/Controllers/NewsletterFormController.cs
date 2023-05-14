using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsletterFormController : Controller
    {
        private readonly AppDbContext _db;

        public NewsletterFormController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<NewsletterForm> form = await _db.NewsletterForms.ToListAsync();
            return View(form);
        }
    }
}
