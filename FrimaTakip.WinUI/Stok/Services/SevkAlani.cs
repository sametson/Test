using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class SevkAlani
    {
        public static event EventHandler IslemYapildi;
        private FirmaTakipDB db;

        public SevkAlani()
        {
            db=new Stok.FirmaTakipDB();
        }

        public string Kaydet(int islenmisMalzemeDetayId, decimal miktar, string aciklama, string kasaNo,DateTime tarih)
        {
            string mesaj = new Stok.Services.IslenmisMalzeme().DetayMiktarDegistir(islenmisMalzemeDetayId, true, miktar);
            if (mesaj == null)
            {
                db.SevkAlaniEkle(islenmisMalzemeDetayId,kasaNo,miktar,aciklama,tarih);
                if (IslemYapildi != null)
                    IslemYapildi(null, null);
            }
            return mesaj;
        }

        public string Delete(int sevkAlaniId)
        {
            var seciliSevkAlani = GetbyId(sevkAlaniId);
            string mesaj = new Stok.Services.IslenmisMalzeme().DetayMiktarDegistir(seciliSevkAlani.IslenmisMalzemeDetayId, false, seciliSevkAlani.Miktar);
            if (mesaj == null)
            {
                db.SevkAlaniSil(seciliSevkAlani.Id);
                if (IslemYapildi != null)
                    IslemYapildi(null, null);
            }
            return mesaj;
        }

        public Stok.SevkAlani GetbyId(int sevkAlaniId)
        {
            return db.SevkAlani.Where(x => x.Id == sevkAlaniId).FirstOrDefault();
        }

        public void TarihDegistir(int sevkAlaniId, DateTime tarih)
        {
            Stok.SevkAlani sevkAlani = GetbyId(sevkAlaniId);
            sevkAlani.Tarih = tarih;
            db.SaveChanges();
            if (IslemYapildi != null)
                IslemYapildi(null, null);
        }

        public IEnumerable<DTO.SevkAlaniIslenmisMalzemeSevkGrid> GridSevkAlaniMevcutDurumTablo()
        {
            return (from sevk in db.SevkAlani                    
                    where sevk.SevkEdildi == false
                    select new DTO.SevkAlaniIslenmisMalzemeSevkGrid
                    {
                        Aciklama = sevk.Aciklama,
                        Birim = sevk.IslenmisMalzemeDetay.IslenmisMalzeme.Birim.Adi,
                        Id = sevk.Id,
                        IslenmisMalzemeBilgi = sevk.IslenmisMalzemeDetay.IslenmisMalzeme.No + "/" + sevk.IslenmisMalzemeDetay.IslenmisMalzeme.Adi,
                        KasaNo = sevk.KasaNo,
                        Miktar = sevk.Miktar,
                        Tarih = sevk.Tarih,
                        SarjNo=sevk.IslenmisMalzemeDetay.HamStokDetay.Sarj.No

                    }).ToList();
        }

        public string MiktarDegistir(int sevkAlaniId, bool isKullanildi)
        {
            var sevkAlani = GetbyId(sevkAlaniId);

            switch (isKullanildi)
            {
                case true:
                    sevkAlani.SevkEdildi = true;
                    db.SaveChanges();
                    break;

                case false:
                    sevkAlani.SevkEdildi = false;
                    db.SaveChanges();
                    break;
            }
            if (IslemYapildi != null)
            {
                IslemYapildi(null, null);
            }
            return null;
        }
    }
}
