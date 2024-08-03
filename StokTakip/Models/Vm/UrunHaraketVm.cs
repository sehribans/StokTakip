namespace StokTakip.Models.Vm
{
    public class UrunHaraketVm
    {
        public List<TblSevkiyat>? SevkiyatList { get; set; }
        public List<TblDepo>? DepoList { get; set; }
        public List<TblSepet>? SepetListe { get; set; }
        public List<TblSipari>? SiparisListe { get; set; }
        public List<TblUrun>? UrunListe { get; set; }
        public List<TblCari>? CariList { get; set; }
    }
}
