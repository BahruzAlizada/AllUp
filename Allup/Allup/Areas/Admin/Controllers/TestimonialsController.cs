using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialsController : Controller
    {
        private readonly AppDbContext _db;

        public TestimonialsController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Testimonial> testimonial = await _db.Testimonials.ToListAsync();
            return View(testimonial);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            await _db.Testimonials.AddAsync(testimonial);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Testimonial dbtestimonial = await _db.Testimonials.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbtestimonial == null)
                return BadRequest();

            return View(dbtestimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Testimonial testimonial)
        {
            if (id == null)
                return NotFound();
            Testimonial dbtestimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbtestimonial == null)
                return BadRequest();

            dbtestimonial.FullName = testimonial.FullName;
            dbtestimonial.Description=testimonial.Description;
            dbtestimonial.Email=testimonial.Email;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Testimonial dbtestimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbtestimonial == null)
                return BadRequest();

            return View(dbtestimonial);
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Testimonial dbtestimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbtestimonial == null)
                return BadRequest();

            if (dbtestimonial.IsDeactive)
                dbtestimonial.IsDeactive = false;
            else
                dbtestimonial.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
