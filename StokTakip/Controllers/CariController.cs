using Microsoft.AspNetCore.Mvc;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    public class CariController : Controller
    {
        private readonly StokTakipContext _context;

        public CariController(StokTakipContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           
            return View(_context.TblCaris.ToList());
        }
        public IActionResult CariForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CariForm(TblCari model)
        {
            if(model.Id> 0)
            {
                _context.TblCaris.Update(model);
                _context.SaveChanges();
            }
            else
            {
                model.Bakiye = 0;
                model.OlusturmaTarihi= DateTime.Now;
                _context.TblCaris.Add(model);
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        public IActionResult CariDetay(int id)
        {
            

            return View();
        }
        public IActionResult Sil(int id)
        {
            _context.TblCaris.Remove(_context.TblCaris.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
