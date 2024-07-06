using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    public class UrunController : Controller
    {

        private readonly StokTakipContext _context;

        public UrunController(StokTakipContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? Id, int? Max, int? Min)
        {
            var Urunler = new List<TblUrun>();
            if (Max == null && Min == null)
            {
                if (Id == null)
                {
                    Urunler = _context.TblUruns.ToList();
                }
                else
                {
                    Urunler = _context.TblUruns.Where(x => x.Kategoriid == Id).ToList();
                }
            }
            else
            {
                Urunler = _context.TblUruns.Where(x => x.Fiyat <= Max || x.Fiyat >= Min).ToList();

                ViewBag.max = Max;
                ViewBag.min = Min;
            }

            var kategoriler = _context.TblKategoris.ToList();
            ViewBag.katagori = kategoriler;

            return View(Urunler);
        }
        public IActionResult UrunForm()
        {

            var kategoriler = _context.TblKategoris.ToList();
            ViewBag.katagori = kategoriler;

            return View();
        }
        [HttpPost]
        public IActionResult UrunForm(TblUrun m)
        {
            if (m.Id > 0)
            {
                _context.TblUruns.Update(m);
            }
            else
            {
                _context.TblUruns.Add(m);
            }
          
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Kategori()
        {

            return View(_context.TblKategoris.ToList());
        }
        public IActionResult KategoriForm()
        {

            return View();
        }
        [HttpPost]
        public IActionResult KategoriForm(TblKategori m)
        {
            _context.TblKategoris.Add(m);
            _context.SaveChanges();

            return RedirectToAction("Kategori");
        }

        public IActionResult KSil(int Id)
        {


            _context.TblKategoris.Remove(_context.TblKategoris.Find(Id));
            _context.SaveChanges();
            return RedirectToAction("Kategori");

        }
        public IActionResult USil(int Id)
        {


            _context.TblUruns.Remove(_context.TblUruns.Find(Id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //    public IActionResult Filtrele(int Id)
        //    {


        //        return RedirectToAction("Index");
        //    }

    }

}

