using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblOdemetipi
    {
        public int Id { get; set; }
        public string? OdemeAdi { get; set; }
        public decimal? Komisyon { get; set; }
    }
}
