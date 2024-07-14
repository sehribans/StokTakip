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
            ViewBag.Id = id;
            return View(satisVm);


         }
        [HttpPost]
        public IActionResult SatisPost(SatisVm model)
        {
            if (ModelState.IsValid)
            {

               model.Siparis.KayitTarihi= DateTime.Now;
               _context.TblSiparis.Add(model.Siparis);
                _context.SaveChanges();
                var sonsiparis = _context.TblSiparis.OrderBy(m => m.Id).LastOrDefault() ?? new();
                var sonsepet = _context.TblSepets.OrderBy(m => m.Id).LastOrDefault() ?? new();
                sonsepet.CariId = sonsiparis.CariId;
                sonsepet.SiparisId = sonsiparis.Id;
               
                _context.TblSepets.Update(sonsepet);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
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
   

    // [HttpPost]
    // public IActionResult SatisPost(SatisVm  model)
    // {


    //     var Siparis = new TblSipari
    //     {
    //         CariId = model.Siparis.CariId,
    //         AlinanTutar = model.Siparis.AlinanTutar,
    //         KayitTarihi = DateTime.Now,
    //         Aciklama = model.Siparis.Aciklama,
    //         OdemeSeklİ = model.Siparis.OdemeSeklİ,

    //     };
    //     _context.TblSiparis.Add(Siparis);
    //     _context.SaveChanges();
    //     model.CariListe = _context.TblCaris.ToList();
    //     model.OdemetipiListe = _context.TblOdemetipis.ToList();
    //     var son = _context.TblSiparis.OrderBy(m => m.Id).LastOrDefault();
    //     var siparisID = _context.TblSepets.Where(m => m.SiparisId == 0);

    //     foreach (var item in siparisID)
    //     {
    //         item.SiparisId = son.Id;
    //         _context.TblSepets.Update(item);
    //     }

    //     return RedirectToAction("Index");
    // }


    // [HttpPost]
    // public IActionResult AddOrUpdate(SatisVm model)
    // {

    //     var sepet = model.SepetListe ?? new List<TblSepet>();
    //     sepet.Add(new TblSepet
    //     {
    //         SiparisId = 0,
    //         UrunId = model.Sepet.UrunId,
    //         Adet = model.Sepet.Adet,
    //         SatisTutar = model.Sepet.SatisTutar,
    //         ToplamTutar = model.Sepet.Adet * model.Sepet.SatisTutar
    //     }); ;

    //     model.SepetListe = sepet;



    //     model.CariListe = _context.TblCaris.ToList();
    //     model.OdemetipiListe = _context.TblOdemetipis.ToList();

    //     _context.SaveChanges();
    //     return View("SatisForm", model);
    // }
    //public IActionResult GetSepetListe(int Id) {

    //     var sepetListe = _context.TblSepets.Where(s => s.CariId == Id).ToList();

    //     return Json(sepetListe);
    // }
}
   

