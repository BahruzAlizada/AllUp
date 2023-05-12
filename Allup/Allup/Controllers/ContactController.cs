using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Allup.Controllers
{
	public class ContactController : Controller
	{
		private readonly AppDbContext _db;

        public ContactController(AppDbContext db)
        {
			_db = db;
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Index(ContactForm form)
		{

			await _db.ContactForms.AddAsync(form);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
