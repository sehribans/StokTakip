using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblDepo
    {
        public int Id { get; set; }
        public string? SevkiyatAd { get; set; }
        public string? Aciklama { get; set; }

        public DateTime? Tarih { get; set; }
    }
}
