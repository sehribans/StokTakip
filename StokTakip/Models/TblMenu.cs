using System;
using System.Collections.Generic;

namespace StokTakip.Models
{
    public partial class TblMenu
    {
        public int Id { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? Url { get; set; }
    }
}
