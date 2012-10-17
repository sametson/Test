using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class Hurda
    {
        public static event EventHandler IslemYapildi;
        private FirmaTakipDB db;
        public Hurda()
        {
            db = new FirmaTakipDB();
        }

        public string Kaydet(int islenmisMalzemeDetayId,decimal miktar,string aciklama,DateTime tarih)
        {
            string mesaj = new Stok.Services.IslenmisMalzeme().DetayMiktarDegistir(islenmisMalzemeDetayId, true, miktar);
            if (mesaj==null)
            {
                db.HurdaEkle(islenmisMalzemeDetayId, miktar, aciklama,tarih);
                if (IslemYapildi != null)
                    IslemYapildi(null, null);
            }
            return mesaj;
        }

        public string MiktarDegistir(int hurdaId,bool isKullanildi,decimal miktar)
        {
            var hurdaAlani = GetbyId(hurdaId);

            decimal yeniKullanilan;

            switch (isKullanildi)
            {
                case true:
                    yeniKullanilan = hurdaAlani.SevkMiktar + miktar;
                    if (yeniKullanilan > hurdaAlani.Miktar)
                    {
                        return "Miktar Yeterli Değil";

                    }
                    else
                    {
                        hurdaAlani.SevkMiktar = yeniKullanilan;
                        db.SaveChanges();
                        return null;
                    }

                    break;

                case false:
                    yeniKullanilan = hurdaAlani.SevkMiktar - miktar;
                    if (yeniKullanilan > hurdaAlani.Miktar)
                    {
                        return "Beklenmedik bir hata oluştu";

                    }
                    else
                    {
                        hurdaAlani.SevkMiktar = yeniKullanilan;
                        db.SaveChanges();
                        return null;
                    }
                    break;
            }
            if (IslemYapildi != null)
            {
                IslemYapildi(null, null);
            }
            return null;
        }

        public string Delete(int hurdaId)
        {
            var seciliHurda = GetbyId(hurdaId);
            string mesaj = new Stok.Services.IslenmisMalzeme().DetayMiktarDegistir(seciliHurda.IslenmisMalzemeDetayId, false, seciliHurda.Miktar);
            if (mesaj == null)
            {
                db.HurdaSil(seciliHurda.Id);
                if (IslemYapildi != null)
                    IslemYapildi(null, null);
            }
            return mesaj;
        }

        public Stok.HurdaAlani GetbyId(int hurdaId)
        {
            return db.HurdaAlani.Where(x => x.Id == hurdaId).FirstOrDefault();
        }

        public void TarihDegistir(int hurdaId,DateTime tarih)
        {
            Stok.HurdaAlani hurdaAlani = GetbyId(hurdaId);
            hurdaAlani.Tarih = tarih;
            db.SaveChanges();
            if (IslemYapildi!=null)
                IslemYapildi(null, null);
        }

        public IEnumerable<DTO.HurdaIslenmisMalzemeSevkGrid> GridHurdaAlaniMevcutDurumTablo()
        {
            return (from hurda in db.HurdaAlani
                    where hurda.Miktar > hurda.SevkMiktar
                    select new DTO.HurdaIslenmisMalzemeSevkGrid
                    {
                        Id = hurda.Id,
                        Aciklama = hurda.Aciklama,
                        Birim = hurda.IslenmisMalzemeDetay.IslenmisMalzeme.Birim.Adi,
                        IslenmisMalzemeBilgi = hurda.IslenmisMalzemeDetay.IslenmisMalzeme.No + "/" + hurda.IslenmisMalzemeDetay.IslenmisMalzeme.Adi,
                        KalanMiktar = (hurda.Miktar - hurda.SevkMiktar),
                        Miktar = hurda.Miktar,
                        SevkMiktar = hurda.SevkMiktar,
                        Tarih = hurda.Tarih,
                        SarjNo=hurda.IslenmisMalzemeDetay.HamStokDetay.Sarj.No

                    }).ToList();
        }
    }
}
