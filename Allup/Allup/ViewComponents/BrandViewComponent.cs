using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allup.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BrandViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();
            return View(brands);
        }
    }
}
