using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class TagsController : Controller
	{
		private readonly AppDbContext _db;

        public TagsController(AppDbContext db)
        {
			_db = db;
        }

		#region Index
		public async Task<IActionResult> Index()
		{
			List<Tag> tags = await _db.Tags.ToListAsync();
			return View(tags);
		}
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(Tag tag)
		{
			if(tag == null)
			{
				ModelState.AddModelError("Name", "Tags name can not be null");
				return View();
			}

			await _db.Tags.AddAsync(tag);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		#endregion

		#region Update
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null)
				return NotFound();
			Tag dbtag = await _db.Tags.FirstOrDefaultAsync(x => x.Id == id);
			if (dbtag == null)
				return BadRequest();

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Update(int? id,Tag tag)
		{
			if (id == null)
				return NotFound();
			Tag dbtag = await _db.Tags.FirstOrDefaultAsync(x => x.Id == id);
			if (dbtag == null)
				return BadRequest();

			if (tag.Name == null)
			{
				ModelState.AddModelError("Name", "Tags name can not be null");
				return View();
			}

			dbtag.Name = tag.Name;

			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		#endregion

	}
}
