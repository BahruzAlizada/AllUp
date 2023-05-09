using Microsoft.AspNetCore.Mvc;

namespace Allup.Controllers
{
	public class ShopController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
