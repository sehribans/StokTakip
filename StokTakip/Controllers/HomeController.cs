
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakip.Models;
using StokTakip.Models.Vm;
using System.Diagnostics;

namespace StokTakip.Controllers
{

    public class HomeController : Controller
    {
        private readonly StokTakipContext _context;

        public HomeController(StokTakipContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new  UrunHaraketVm();
            vm.UrunListe = _context.TblUruns.ToList();
            vm.SepetListe= _context.TblSepets.Where(x=> vm.UrunListe.Select(m=> m.Id).Contains(x.UrunId ?? 0)).ToList();
            ViewBag.kategori = _context.TblKategoris.ToList();
            ViewBag.urun= vm.SepetListe.Sum(x => x.Adet);
            var siparis= _context.TblSiparis.Where(m=> m.KayitTarihi.Value.Month==DateTime.Now.Month).ToList();
            var sepet= _context.TblSepets.Where(x => siparis.Select(x => x.Id).Contains(x.SiparisId ?? 0)).ToList();
            var alınan=  siparis.Sum(z => z.AlinanTutar);
             var satıs=sepet.Sum(z => z.SatisTutar);
             var gelir= satıs-alınan;
             ViewBag.Gelir= gelir;
            return View();
        }

    }
}
