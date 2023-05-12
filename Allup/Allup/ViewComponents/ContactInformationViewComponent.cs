using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Allup.ViewComponents
{
    public class ContactInformationViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ContactInformationViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ContactInformation contactInformation = await _context.ContactInformations.FirstOrDefaultAsync();
            return View(contactInformation);
        }
    }
}
