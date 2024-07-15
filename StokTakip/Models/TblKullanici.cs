using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblKullanici
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public string? Parola { get; set; }
    }
}
