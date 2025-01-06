using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.ViewComponents
{
    public class Menu : ViewComponent
    {
        public Menu()
        {

        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
