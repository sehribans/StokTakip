using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblSevkiyat
    {
        public int Id { get; set; }
        public int?  DepoId { get; set; }
        public int? UrunId { get; set; }
        public short? Adet { get; set; }
        public decimal? AlisFiyat { get; set; }
    }
}
