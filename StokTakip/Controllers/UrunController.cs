using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using StokTakip.Models;
using StokTakip.Models.Vm;

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
            ViewBag.Kategori = _context.TblKategoris.ToList();
            return View();
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
        public IActionResult SatisIncele(int id) { 
            var Vm = new UrunHaraketVm();
            Vm.SepetListe = _context.TblSepets.Where(m=>m.UrunId == id).ToList();
            Vm.SiparisListe=_context.TblSiparis.Where(x => Vm.SepetListe.Select(x => x.SiparisId).Contains(x.Id)).ToList();
            Vm.SevkiyatList = _context.TblSevkiyats.Where(x => x.UrunId == id).ToList();
            Vm.DepoList= _context.TblDepos.Where(x => Vm.SevkiyatList.Select(x=> x.Id).Contains(x.Id)).ToList();
            Vm.CariList = _context.TblCaris.Where(x => Vm.SepetListe.Select(x=> x.CariId).Contains(x.Id)).ToList(); 
            return View(Vm); 
        }
        public IActionResult GirisIncele(int id)
        {
            var Vm = new UrunHaraketVm();
            Vm.SepetListe = _context.TblSepets.Where(m => m.UrunId == id).ToList();
            Vm.SiparisListe = _context.TblSiparis.Where(x => Vm.SepetListe.Select(x => x.SiparisId).Contains(x.Id)).ToList();
            Vm.SevkiyatList = _context.TblSevkiyats.Where(x => x.UrunId == id).ToList();
            Vm.DepoList = _context.TblDepos.Where(x => Vm.SevkiyatList.Select(x => x.DepoId).Contains(x.Id)).ToList();
            Vm.CariList = _context.TblCaris.Where(x => Vm.SepetListe.Select(x => x.CariId).Contains(x.Id)).ToList();
            return View(Vm);
        }
    }

}

