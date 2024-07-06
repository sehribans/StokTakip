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
           
            return View(_context.TblMenus.ToList());
        }
    }
}
