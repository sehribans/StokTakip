using Microsoft.AspNetCore.Mvc;

namespace StokTakip.Controllers
{
    public class DepoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DepoForm() { return View(); }


    }
}
