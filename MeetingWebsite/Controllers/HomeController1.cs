using Microsoft.AspNetCore.Mvc;

namespace MeetingWebsite.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
