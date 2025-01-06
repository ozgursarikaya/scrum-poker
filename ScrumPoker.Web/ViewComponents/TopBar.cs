using Microsoft.AspNetCore.Mvc;

namespace ScrumPoker.Web.ViewComponents
{
    public class TopBar : ViewComponent
    {
        public TopBar()
        {
            
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
