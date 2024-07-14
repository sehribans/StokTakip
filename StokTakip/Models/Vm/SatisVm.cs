namespace StokTakip.Models.Vm
{
    public class SatisVm
    {
      public List<TblCari>? CariListe { get; set; } 
      public TblCari? Cari { get; set; } 
      public List<TblOdemetipi>? OdemetipiListe { get;set; }

     public List<TblUrun>? UrunListe { get; set; }

    public TblSipari? Siparis { get; set; }
    public List<TblSipari>? SiparisListe { get; set; }

        public TblSepet? Sepet { get; set; }   
        public List<TblSepet>? SepetListe { get; set; }

       
    }
}
