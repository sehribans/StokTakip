using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblUrun
    {
        public int Id { get; set; }
        public string? Urunadi { get; set; }
        public short? Stok { get; set; }
        public string? StokKodu { get; set; }
        public decimal? Fiyat { get; set; }
        public short? Kategoriid { get; set; }
    }
}
