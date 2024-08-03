namespace StokTakip.Models.Vm
{
    public class DepoVm
    {
        public List<TblSevkiyat> SevkiyatList { get; set; }
        public TblSevkiyat Sevkiyat { get; set; }
        public List<TblDepo> DepoList { get; set; }
        public TblDepo Depo { get; set; }
        public List<TblUrun>? UrunListe { get; set; }
    }
}
