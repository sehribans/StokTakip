using Microsoft.AspNetCore.Mvc;

namespace StokTakip.Controllers
{
    public class SatisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SatisForm()
        {
            return View();
        }
    }
}
