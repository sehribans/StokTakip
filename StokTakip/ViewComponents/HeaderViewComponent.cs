using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using StokTakip.Models;

namespace StokTakip.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly StokTakipContext _context;

        public HeaderViewComponent(StokTakipContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var menu = _context.TblMenus.ToList();
            var alt= _context.TblAltmenus.ToList();

            var menuler = (menu, alt);


            return View(menuler);
        }
    }
}
