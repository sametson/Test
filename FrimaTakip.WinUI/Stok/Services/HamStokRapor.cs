using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class HamStokRapor
    {
        private FirmaTakipDB db;

        public HamStokRapor()
        {
            db = new FirmaTakipDB();
        }

        public IEnumerable<DTO.Rapor.HamMalzemeAna> Genel()
        {
            var hamStokDetay = db.HamStokDetay.Where(x => x.HamStok.IsAktif == true).GroupBy(x => x.Sarj.HamMalzemeId).Select(x => new { HamMalzemeId = x.Key, Miktar = x.Sum(y => y.Miktar), SevkMiktar = x.Sum(y => y.SevkMiktar) });

            var islenmisMalzemeDetay = db.IslenmisMalzemeDetay.Where(x => x.HamStokDetay.HamStok.IsAktif == true).GroupBy(x => x.HamStokDetay.Sarj.HamMalzemeId).Select(x => new { HamMalzemeId = x.Key, Miktar = x.Sum(y => y.Miktar), SevkMiktar = x.Sum(y => y.SevkMiktar) });

            var sevkAlani = db.SevkAlani.Where(x => x.IslenmisMalzemeDetay.HamStokDetay.HamStok.IsAktif == true).Where(x => x.SevkEdildi == false).GroupBy(x => x.IslenmisMalzemeDetay.HamStokDetay.Sarj.HamMalzemeId).Select(x => new { HamMalzemeId = x.Key, Miktar = x.Sum(y => y.Miktar) });

            var hurdaAlani = db.HurdaAlani.Where(x => x.IslenmisMalzemeDetay.HamStokDetay.HamStok.IsAktif == true).GroupBy(x => x.IslenmisMalzemeDetay.HamStokDetay.Sarj.HamMalzemeId).Select(x => new { HamMalzemeId = x.Key, Miktar = x.Sum(y => y.Miktar), SevkMiktar = x.Sum(y => y.SevkMiktar) });

            return (from hm in db.HamMalzeme.Where(x => x.IsAktif == true)
                    from hsd in hamStokDetay.Where(x => x.HamMalzemeId == hm.Id)
                    from imd in islenmisMalzemeDetay.Where(x => x.HamMalzemeId == hm.Id).DefaultIfEmpty()
                    from sa in sevkAlani.Where(x => x.HamMalzemeId == hm.Id).DefaultIfEmpty()
                    from ha in hurdaAlani.Where(x => x.HamMalzemeId == hm.Id).DefaultIfEmpty()
                    select new DTO.Rapor.HamMalzemeAna
                    {
                        BirimAdi = hm.Birim.Adi,
                        HamMiktar = hsd.Miktar - hsd.SevkMiktar,
                        UretimdekiMiktar = (imd.Miktar == null) ? 0 : imd.Miktar - imd.SevkMiktar,
                        SevkAlaniMiktar = (sa.Miktar == null) ? 0 : sa.Miktar,
                        HurdaAlaniMiktar = (ha.Miktar == null) ? 0 : ha.Miktar - ha.SevkMiktar,
                        HamMalzemeAdi = hm.Adi,
                        HamMalzemeId = hm.Id,
                        HamMalzemeNo = hm.No,


                    }).OrderBy(x=>x.HamMalzemeNo).ToList();
        }

        public IEnumerable<DTO.Rapor.IslenenMalzemeParti> IslenenMalzemelerinBilgisi(int hamMalzemeId)
        {
            DateTime tarih = DateTime.MinValue.Date;
            return (
                    from hsd in db.HamStokDetay.Where(x=>x.HamStok.IsAktif==true).Where(x=>x.Sarj.HamMalzemeId==hamMalzemeId)
                    from imd in db.IslenmisMalzemeDetay.Where(x=>x.HamStokDetayId==hsd.Id).DefaultIfEmpty().Where(x=>(x.Miktar-x.SevkMiktar)>0)
                    select new DTO.Rapor.IslenenMalzemeParti
                    {
                        SarjId = hsd.SarjId,
                        SarjNo = hsd.Sarj.No,
                        AmbaraGirisTarih = hsd.HamStok.Tarih,
                        BirimAdi = hsd.Sarj.HamMalzeme.Birim.Adi,
                        HamMalzemeBilgi = hsd.Sarj.HamMalzeme.No + "/" + hsd.Sarj.HamMalzeme.Adi,
                        UretimdekiMiktar=(imd.Miktar==null) ? 0 : imd.Miktar-imd.SevkMiktar,
                        UretimeSevkTarih = (imd.Tarih == null) ? tarih : imd.Tarih,
                       


                    }).ToList();

        }

        public IEnumerable<DTO.Rapor.HamStokParti> HamStokPartiBilgisi(int hamMalzemeId)
        {
            
            return (
                    from hsd in db.HamStokDetay.Where(x => x.HamStok.IsAktif == true).Where(x => x.Sarj.HamMalzemeId == hamMalzemeId)                    
                    select new DTO.Rapor.HamStokParti
                    {
                        SarjId = hsd.SarjId,
                        SarjNo = hsd.Sarj.No,
                        AmbaraGirisTarih = hsd.HamStok.Tarih,
                        BirimAdi = hsd.Sarj.HamMalzeme.Birim.Adi,
                        HamMalzemeBilgi = hsd.Sarj.HamMalzeme.No + "/" + hsd.Sarj.HamMalzeme.Adi,
                        KalanHamMiktar = hsd.Miktar - hsd.SevkMiktar,
                        GelenHamMiktar = hsd.Miktar                       
                    }).ToList();

        }

        public IEnumerable<DTO.Rapor.SevkAlaniParti> SevkAlaniPartiBilgisi(int hamMalzemeId)
        {


            DateTime tarih = DateTime.MinValue.Date;
            return (
                    from hsd in db.HamStokDetay.Where(x => x.HamStok.IsAktif == true).Where(x => x.Sarj.HamMalzemeId == hamMalzemeId)
                    from imd in db.IslenmisMalzemeDetay.Where(x => x.HamStokDetayId == hsd.Id).DefaultIfEmpty()
                    from sa in db.SevkAlani.Where(x => x.SevkEdildi == false).Where(x => x.IslenmisMalzemeDetayId == imd.Id).DefaultIfEmpty().Where(x=>x.SevkEdildi==false)
                    select new DTO.Rapor.SevkAlaniParti
                    {
                        SarjId = hsd.SarjId,
                        SarjNo = hsd.Sarj.No,
                        AmbaraGirisTarih = hsd.HamStok.Tarih,
                        BirimAdi = hsd.Sarj.HamMalzeme.Birim.Adi,
                        HamMalzemeBilgi = hsd.Sarj.HamMalzeme.No + "/" + hsd.Sarj.HamMalzeme.Adi,
                        SevkAlaniMiktar = (sa.Miktar == null) ? 0 : sa.Miktar,
                        SevkAlaniSevkTarih = (sa.Tarih == null) ? tarih : sa.Tarih,


                    }).ToList();

        }

        public IEnumerable<DTO.Rapor.HurdaAlaniParti> HurdaAlaniPartiBilgisi(int hamMalzemeId)
        {


            DateTime tarih = DateTime.MinValue.Date;
            return (
                    from hsd in db.HamStokDetay.Where(x => x.HamStok.IsAktif == true).Where(x => x.Sarj.HamMalzemeId == hamMalzemeId)
                    from imd in db.IslenmisMalzemeDetay.Where(x => x.HamStokDetayId == hsd.Id).DefaultIfEmpty()
                    from ha in db.HurdaAlani.Where(x => x.IslenmisMalzemeDetayId == imd.Id).DefaultIfEmpty().Where(x=>(x.Miktar>x.SevkMiktar))
                    select new DTO.Rapor.HurdaAlaniParti
                    {
                        SarjId = hsd.SarjId,
                        SarjNo = hsd.Sarj.No,
                        AmbaraGirisTarih = hsd.HamStok.Tarih,
                        BirimAdi = hsd.Sarj.HamMalzeme.Birim.Adi,
                        HamMalzemeBilgi = hsd.Sarj.HamMalzeme.No + "/" + hsd.Sarj.HamMalzeme.Adi,
                        HurdaAlaniMiktar = (ha.Miktar == null) ? 0 : ha.Miktar - ha.SevkMiktar,
                        HurdaAlaniSevkTarih = (ha.Tarih == null) ? tarih : ha.Tarih


                    }).ToList();

        }
    }
}
