using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblSepet
    {
        public int Id { get; set; }
        public int? SiparisId { get; set; }
        public int? UrunId { get; set; }
        public short? Adet { get; set; }
        public decimal? SatisTutar { get; set; }
        public decimal? Maliyet { get; set; }
        public decimal? ToplamTutar { get; set; }

    }
}
