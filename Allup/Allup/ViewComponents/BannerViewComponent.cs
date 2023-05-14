using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allup.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BannerViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Banner> banners = await _context.Banners.Where(x=>!x.IsDeactive).OrderByDescending(x=>x.Id).Take(2).ToListAsync();
            return View(banners);
        }
    }
}
