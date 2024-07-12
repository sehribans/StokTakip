using Microsoft.AspNetCore.Mvc;
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
          
            return View(_context.TblSepets.ToList());
        }

        [HttpGet]
        public IActionResult SatisForm()
        {
           SatisVm satisVm = new SatisVm();
            satisVm.OdemetipiListe = _context.TblOdemetipis.ToList();
            satisVm.UrunListe= _context.TblUruns.ToList();
            satisVm.CariListe= _context.TblCaris.ToList();



            return View(satisVm);
        }

        [HttpPost]
        public IActionResult SatisForm(SatisVm  model)
        {
           var Siparis= new TblSipari { 
             CariId= model.Siparis.CariId,
             AlinanTutar= model.Siparis.AlinanTutar,
             KayitTarihi=DateTime.Now,
             Aciklama= model.Siparis.Aciklama,
             OdemeSeklİ= model.Siparis.OdemeSeklİ,

          };
         _context.TblSiparis.Add(Siparis);
            _context.SaveChanges();
            model.CariListe = _context.TblCaris.ToList();
            model.OdemetipiListe = _context.TblOdemetipis.ToList();
          var son=  _context.TblSiparis.OrderBy(m=> m.Id).LastOrDefault();
     var siparisID= _context.TblSepets.Where(m => m.SiparisId == 0);

          foreach(var item in siparisID)
            {
                item.SiparisId = son.Id;
                _context.TblSepets.Update(item);   
            }

                return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AddOrUpdate(SatisVm model)
        {

            var sepet = model.SepetListe ?? new List<TblSepet>();
            sepet.Add(new TblSepet
            {
                SiparisId= 0,
                UrunId = model.Sepet.UrunId,
                Adet = model.Sepet.Adet,
                SatisTutar = model.Sepet.SatisTutar,
                ToplamTutar = model.Sepet.Adet * model.Sepet.SatisTutar
            }); ;

            model.SepetListe = sepet;
            

            
            model.CariListe = _context.TblCaris.ToList();
            model.OdemetipiListe = _context.TblOdemetipis.ToList();
          
            _context.SaveChanges();
            return View("SatisForm", model);
        }
    }
}
