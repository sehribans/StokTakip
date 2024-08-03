using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StokTakip.Models;
using StokTakip.Models.Vm;

namespace StokTakip.Controllers
{
    public class SatisController : Controller
    {
        private readonly StokTakipContext _context;

        public SatisController(StokTakipContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           
         ViewBag.siparis=  _context.TblSiparis.ToList();
           ViewBag.Cari= _context.TblCaris.ToList();
            ViewBag.Odeme= _context.TblOdemetipis.ToList();
            return View(_context.TblSepets.ToList());
        }
         [HttpGet]
         public IActionResult SatisForm(int? id)
         {
            SatisVm satisVm = new SatisVm();
             satisVm.OdemetipiListe = _context.TblOdemetipis.ToList();
             satisVm.UrunListe= _context.TblUruns.ToList();
             satisVm.CariListe= _context.TblCaris.ToList();
            satisVm.SepetListe= _context.TblSepets.Where(m=> m.CariId==id).ToList();
            satisVm.SepetListe = _context.TblSepets.Where(s => s.SiparisId == 0 || s.SiparisId == null).ToList();
            ViewBag.ToplamTurtar = satisVm.SepetListe.Sum(m => m.SatisTutar * m.Adet);
            ViewBag.Id = id;
            return View(satisVm);


         }
        [HttpPost]
        public IActionResult SatisPost(SatisVm model)
        {
           
               model.Siparis.KayitTarihi= DateTime.Now;
               _context.TblSiparis.Add(model.Siparis);
                _context.SaveChanges();
                var sonsiparis = _context.TblSiparis.OrderBy(m => m.Id).LastOrDefault() ?? new();
               var sonsepet = _context.TblSepets.Where(m => m.SiparisId== 0 || m.SiparisId == null).ToList();
           
               var urun= _context.TblUruns.Where(x => sonsepet.Select(m=> m.UrunId).Contains(x.Id)).ToList();
            foreach(var u in urun) {
                var cikisadet = sonsepet.FirstOrDefault(m => m.UrunId == u.Id);
                u.Stok = (short?)((u.Stok ?? 0) - (cikisadet.Adet ?? 0));
                _context.TblUruns.Update(u);
                _context.SaveChanges();
            }
            
            foreach (var s in sonsepet) { 
                s.SiparisId = sonsiparis.Id;
                _context.TblSepets.Update(s);
                _context.SaveChanges();
            }
            var sonurun = _context.TblSepets.OrderBy(m => m.Id).LastOrDefault() ?? new();
            var caribakiye = _context.TblCaris.FirstOrDefault(x => x.Id==sonurun.CariId);
            caribakiye.Bakiye = sonsepet.Sum(x => x.SatisTutar * x.Adet);
            caribakiye.Bakiye= sonsiparis.AlinanTutar-caribakiye.Bakiye;
            _context.TblCaris.Update(caribakiye);

            _context.SaveChanges();
                return RedirectToAction("Index");
           
    
        }

        [HttpPost]
        public IActionResult AddOrUpdate(SatisVm model)
        {
            if (ModelState.IsValid)
            {
                
                 
               _context.TblSepets.Add(model.Sepet);
                _context.SaveChanges();
          
                return RedirectToAction("SatisForm", model);
              
            }
            return View("SatisForm", model);


        }

        public IActionResult Detay(int id)
        {
            var vm = new SatisVm();
            vm.Siparis= _context.TblSiparis.FirstOrDefault(m => m.Id == id);

            vm.UrunListe= _context.TblUruns.ToList();
            vm.SepetListe= _context.TblSepets.Where(m=>m.SiparisId==id).ToList();  

            //urunad adet toplamfiyat birimfiyat 
            return View(vm);
        }
        public IActionResult Sil(int id) {
            var sil = _context.TblSiparis.FirstOrDefault(m => m.Id == id);
            _context.TblSepets.RemoveRange(_context.TblSepets.Where(x => x.SiparisId == sil.Id).ToList());
          _context.TblSiparis.Remove(sil);
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
   


}
   

