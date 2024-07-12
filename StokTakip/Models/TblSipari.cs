using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblSipari
    {
        public int Id { get; set; }
        public int? CariId { get; set; }
        public DateTime? KayitTarihi { get; set; }
        public int? OdemeSeklİ { get; set; }
        public decimal? AlinanTutar { get; set; }
        public string? Aciklama { get; set; }
    }
}
