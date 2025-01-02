using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.Controllers
{
    [Route("room")]
    public class PockerRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
