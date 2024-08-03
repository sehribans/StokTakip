using Microsoft.AspNetCore.Mvc;
using StokTakip.Models;
using StokTakip.Models.Vm;

namespace StokTakip.Controllers
{
    public class DepoController : Controller
    {
        private readonly StokTakipContext _context;

        public DepoController(StokTakipContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Sevkiyat= _context.TblSevkiyats.ToList();
            return View(_context.TblDepos.ToList());
        }
        public IActionResult DepoForm()
        {
            var vm = new DepoVm();
            vm.UrunListe = _context.TblUruns.ToList();
            vm.SevkiyatList = _context.TblSevkiyats.Where(s=> s.DepoId==0 || s.DepoId==null).ToList();
            ViewBag.ToplamTurtar = vm.SevkiyatList.Sum(m => m.AlisFiyat*m.Adet);
            vm.DepoList= _context.TblDepos.ToList();
        
            return View(vm);
        }
        [HttpPost]
        public IActionResult DepoPost(DepoVm model)
        {
          model.Depo.Tarih = DateTime.Now;
                _context.TblDepos.Add(model.Depo);
                _context.SaveChanges();
                var sondepos = _context.TblDepos.OrderBy(m => m.Id).LastOrDefault() ?? new();
                var sonsevkiyat = _context.TblSevkiyats.Where(m => m.DepoId == 0 || m.DepoId == null).ToList();

               var urun= _context.TblUruns.Where(x => sonsevkiyat.Select(m=> m.UrunId).Contains(x.Id)).ToList();
            foreach(var u in urun) {
                var girisadet = sonsevkiyat.FirstOrDefault(m => m.UrunId == u.Id);
                u.Stok = (short?)((u.Stok ?? 0) + (girisadet.Adet ?? 0));
                _context.TblUruns.Update(u);
                _context.SaveChanges();
            }
            foreach(var s in sonsevkiyat) {
                s.DepoId = sondepos.Id;
                _context.TblSevkiyats.Update(s);
                _context.SaveChanges();
            }
            
                return RedirectToAction("Index");
         }
        [HttpPost]
        public IActionResult AddOrUpdate(DepoVm model)
        {
            
                _context.TblSevkiyats.Add(model.Sevkiyat);
                _context.SaveChanges();
            return RedirectToAction("DepoForm", model);
           

   
        }
        public IActionResult Detay(int id)
        {
            var vm = new DepoVm();
            vm.DepoList= _context.TblDepos.ToList();
            vm.UrunListe= _context.TblUruns.ToList();
            vm.SevkiyatList= _context.TblSevkiyats.Where(x => x.DepoId == id).ToList();
          


            return View(vm);
        }
        public IActionResult Sil(int id)
        {
            var sil = _context.TblDepos.FirstOrDefault(m => m.Id == id);
            _context.TblSevkiyats.RemoveRange(_context.TblSevkiyats.Where(x => x.DepoId == sil.Id).ToList());
            _context.TblDepos.Remove(sil);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
