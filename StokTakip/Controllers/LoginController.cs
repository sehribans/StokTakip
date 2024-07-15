using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    public class LoginController : Controller
    {
        private readonly StokTakipContext _c;

        public LoginController(StokTakipContext c)
        {
            _c = c;
        }

        [HttpGet]
        public IActionResult Index(bool? durum)
        {
            if (durum != null)
            {
                ViewBag.Durum = "E-Posta veya Parola Hatalıdır!";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string mail, string password)
        {
            var user = _c.TblKullanicis.FirstOrDefault(m => m.Eposta == mail && m.Parola == password);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("KullaniciAdi", user.Ad ?? string.Empty));
                claims.Add(new Claim("email", user.Eposta));
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction(nameof(Index), new { durum = false });
            }
        }

        public IActionResult LogOut()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Kayit()
        {

            return View();
        }

        [HttpPost]
        public IActionResult KayitYap(TblKullanici model)
        {
            var ekle = _c.TblKullanicis.Add(model);
            _c.SaveChanges();
            return RedirectToAction("Kayit");
        }
    }
}

