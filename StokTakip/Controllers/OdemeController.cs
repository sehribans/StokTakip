using Microsoft.AspNetCore.Mvc;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    public class OdemeController : Controller
    {
        private readonly StokTakipContext _context;

        public OdemeController(StokTakipContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.OdemeList = _context.TblOdemetipis.ToList();
            return View();
        }
        public IActionResult OdemeForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OdemeForm(TblOdemetipi model)
        {
            if(model.Id > 0) { 
            _context.TblOdemetipis.Update(model);  
         
            }
            else
            {
                _context.TblOdemetipis.Add(model);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Sil(int id)
        {
            _context.TblOdemetipis.Remove(_context.TblOdemetipis.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
