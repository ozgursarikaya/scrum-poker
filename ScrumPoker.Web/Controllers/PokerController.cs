using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
	public class PokerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
