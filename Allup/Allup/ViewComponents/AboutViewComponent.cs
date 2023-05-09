using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Allup.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public AboutViewComponent(AppDbContext context)
        {
            _context=context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            About about = await _context.Abouts.FirstOrDefaultAsync();
            return View(about);
        }
    }
}
