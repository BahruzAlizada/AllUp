using Allup.DAL;
using Allup.Models;
using Allup.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Allup.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
			_db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Categories = await _db.Categories.Where(x => x.IsMain).Where(x => !x.IsDeactive).ToListAsync(),
                Blogs = await _db.Blogs.Where(x => !x.IsDeactive).OrderByDescending(x => x.Id).Take(6).ToListAsync(),
                NewsletterInformation = await _db.NewsletterInformations.FirstOrDefaultAsync()
            };
            return View(homeVM);
        }
        #endregion


        #region Subscripe
        public async Task<IActionResult> Subscripe(string email)
        {
            if (email == null)
                return Content("Email can not be null !");


            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
                return Content("This is not Email Address !");
            else
            {
                NewsletterForm form = new NewsletterForm
                {
                    Email = email
                };
                bool isExsit = await _db.NewsletterForms.AnyAsync(x => x.Email == email);
                if (isExsit)
                    return Content("This Email already Subscripe");

                await _db.NewsletterForms.AddAsync(form);
                await _db.SaveChangesAsync();

                List<NewsletterForm> forms = await _db.NewsletterForms.ToListAsync();
                string message = "Yeni blog və productlarla tanış olmaq üçün səhifəmizə daxil ola bilərsiniz";
                 
                    
                string title = "Salam Əziz İzləyicimiz";
                foreach (NewsletterForm sub in forms)
                {
                    await Helpers.Helper.SendMailAsync(title,message,sub.Email);
                }
            }



            return Content("Ok");
        }
        #endregion

        #region Error
        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}
