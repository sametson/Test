using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class SevkIrsaliye
    {
        private FirmaTakipDB db;
        public SevkIrsaliye()
        {
            db = new FirmaTakipDB();
        }

        public int Kaydet(string irsaliyeNo, int musteriId, DateTime tarih, string aciklama)
        {
            Stok.SevkIrsaliye sevkIrsaliye = new Stok.SevkIrsaliye();
            if (GetbyIrsaliyeNo(irsaliyeNo).Count() == 0)
            {
                sevkIrsaliye.IrsaliyeNo = irsaliyeNo.ToUpper();
                sevkIrsaliye.MusteriId = musteriId;
                sevkIrsaliye.Tarih = tarih;
                sevkIrsaliye.Aciklama = aciklama;
                db.AddToSevkIrsaliye(sevkIrsaliye);
                db.SaveChanges();
            }
            else
            {
                return 0;
            }

            return sevkIrsaliye.Id;
        }

        public int Degistir(int irsaliyeId, string irsaliyeNo, int musteriId, DateTime tarih, string aciklama)
        {
            Stok.SevkIrsaliye sevkIrsaliye = GetbyId(irsaliyeId);
            if (sevkIrsaliye.IrsaliyeNo == irsaliyeNo.ToUpper())
            {
                if (GetbyIrsaliyeNo(irsaliyeNo).Count() == 1)
                {
                    sevkIrsaliye.IrsaliyeNo = irsaliyeNo.ToUpper();
                    sevkIrsaliye.MusteriId = musteriId;
                    sevkIrsaliye.Tarih = tarih;
                    sevkIrsaliye.Aciklama = aciklama;
                    db.SaveChanges();
                }
                else
                {

                    return 0;
                }
            }
            else
            {
                if (GetbyIrsaliyeNo(irsaliyeNo).Count() == 0)
                {
                    sevkIrsaliye.IrsaliyeNo = irsaliyeNo.ToUpper();
                    sevkIrsaliye.MusteriId = musteriId;
                    sevkIrsaliye.Tarih = tarih;
                    sevkIrsaliye.Aciklama = aciklama;
                    db.SaveChanges();
                }
                else
                {

                    return 0;
                }
            }

            return sevkIrsaliye.Id;
        }

        public int Sil(int irsaliyeId)
        {
            var seciliSevkIrsaliye = GetbyId(irsaliyeId);

            if (GetDetayByIrsaliyeId(irsaliyeId).Count() == 0)
            {
                db.SevkIrsaliye.DeleteObject(seciliSevkIrsaliye);
                db.SaveChanges();
            }
            else
            {
                return 0;
            }

            return irsaliyeId;
        }

        public IEnumerable<Stok.SevkIrsaliye> GetbyIrsaliyeNo(string irsaliyeNo)
        {
            return db.SevkIrsaliye.Where(x => x.IrsaliyeNo == irsaliyeNo.ToUpper()).ToList();
        }

        public Stok.SevkIrsaliye GetbyId(int Id)
        {
            return db.SevkIrsaliye.Where(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<DTO.SevkIrsaliyeMusteriyeSevk> GetAllforGrid()
        {
            return (from q in db.SevkIrsaliye
                    select new DTO.SevkIrsaliyeMusteriyeSevk
                    {
                        Id = q.Id,
                        Aciklama = q.Aciklama,
                        IrsaliyeNo = q.IrsaliyeNo,
                        Musteri = q.Musteri.Adi,
                        Tarih = q.Tarih
                    }).ToList();
        }


        //Detay İşlemleri

        public void DetayKaydet(int sevkIrsaliyeId, bool isHurda, decimal miktar, int? sevkAlaniId, int? hurdaAlaniId)
        {
            Stok.SevkIrsaliyeDetay irsaliyeDetay = new SevkIrsaliyeDetay();
            irsaliyeDetay.SevkIrsaliyeId = sevkIrsaliyeId;
            irsaliyeDetay.Miktar = miktar;
            irsaliyeDetay.IsHurda = isHurda;

            switch (isHurda)
            {
                case true:
                    irsaliyeDetay.SevkAlaniId = null;
                    irsaliyeDetay.HurdaAlaniId = hurdaAlaniId.Value;
                    break;

                case false:
                    irsaliyeDetay.SevkAlaniId = sevkAlaniId.Value;
                    irsaliyeDetay.HurdaAlaniId = null;
                    break;

                default:
                    break;
            }
            db.AddToSevkIrsaliyeDetay(irsaliyeDetay);
            db.SaveChanges();
        }

        public void DetayDegistir(long irsaliyeDetayId, int sevkIrsaliyeId, bool isHurda, decimal miktar, int? sevkAlaniId, int? hurdaAlaniId)
        {
            Stok.SevkIrsaliyeDetay irsaliyeDetay = GetDetaybyId(irsaliyeDetayId);
            irsaliyeDetay.SevkIrsaliyeId = sevkIrsaliyeId;
            irsaliyeDetay.Miktar = miktar;
            irsaliyeDetay.IsHurda = isHurda;

            switch (isHurda)
            {
                case true:
                    irsaliyeDetay.SevkAlaniId = null;
                    irsaliyeDetay.HurdaAlaniId = hurdaAlaniId.Value;
                    break;

                case false:
                    irsaliyeDetay.SevkAlaniId = sevkAlaniId.Value;
                    irsaliyeDetay.HurdaAlaniId = null;
                    break;

                default:
                    break;
            }
            db.SaveChanges();
        }

        public void DeleteDetayById(long irsaliyeDetayId)
        {
            var irsaliyeDetay = GetDetaybyId(irsaliyeDetayId);

            if (irsaliyeDetay.IsHurda == true)
            {
                new Stok.Services.Hurda().MiktarDegistir(irsaliyeDetay.HurdaAlaniId.Value, false, irsaliyeDetay.Miktar);
            }
            else
            {
                new Stok.Services.SevkAlani().MiktarDegistir(irsaliyeDetay.SevkAlaniId.Value, false);
            }
            db.SevkIrsaliyeDetay.DeleteObject(irsaliyeDetay);
            db.SaveChanges();
        }

        public Stok.SevkIrsaliyeDetay GetDetaybyId(long detayId)
        {
            return db.SevkIrsaliyeDetay.Where(x => x.Id == detayId).FirstOrDefault();
        }

        public IEnumerable<DTO.SevkIrsaliyeDetayMusteriyeSevk> GetDetayByIrsaliyeId(int irsaliyeId)
        {
            return (from detay in db.SevkIrsaliyeDetay
                    where detay.SevkIrsaliyeId == irsaliyeId
                    select new DTO.SevkIrsaliyeDetayMusteriyeSevk
                    {
                        Birim = detay.IsHurda == true ? detay.HurdaAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Birim.Adi : detay.SevkAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Birim.Adi,
                        HurdaAlaniId = detay.IsHurda == true ? detay.HurdaAlaniId.Value : 0,
                        Id = detay.Id,
                        MalzemeBilgi = detay.IsHurda == true ? detay.HurdaAlani.IslenmisMalzemeDetay.IslenmisMalzeme.No + "/" + detay.HurdaAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Adi : detay.SevkAlani.IslenmisMalzemeDetay.IslenmisMalzeme.No + "/" + detay.SevkAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Adi,
                        MalzemeDurumu = detay.IsHurda == true ? "Hurda" : "Mamül",
                        Miktar = detay.Miktar,
                        SarjNo = detay.IsHurda == true ? detay.HurdaAlani.IslenmisMalzemeDetay.HamStokDetay.Sarj.No : detay.SevkAlani.IslenmisMalzemeDetay.HamStokDetay.Sarj.No,
                        SevkAlaniId = detay.IsHurda == true ? 0 : detay.SevkAlaniId.Value,
                        SevkIrsaliyeId = detay.SevkIrsaliyeId
                    }).ToList();
        }


        //Cookie İşlemleri

        public string CookieKaydet(bool isHurda, decimal miktar, int? sevkAlaniId, int? hurdaAlaniId)
        {

            Stok.CookieSevkIrsaliyeDetay cookieDetay = new CookieSevkIrsaliyeDetay();
            cookieDetay.Miktar = miktar;
            cookieDetay.IsHurda = isHurda;

            switch (isHurda)
            {
                case true:
                    cookieDetay.SevkAlaniId = null;
                    cookieDetay.HurdaAlaniId = hurdaAlaniId.Value;
                    var hurdaAlani = new Stok.Services.Hurda().GetbyId(hurdaAlaniId.Value);
                    if (hurdaAlani.Miktar < miktar + hurdaAlani.SevkMiktar)
                    {
                        return "Miktar yeterli değil";
                    }
                    new Stok.Services.Hurda().MiktarDegistir(hurdaAlaniId.Value, true, miktar);
                    break;

                case false:
                    cookieDetay.SevkAlaniId = sevkAlaniId.Value;
                    cookieDetay.HurdaAlaniId = null;
                    new Stok.Services.SevkAlani().MiktarDegistir(sevkAlaniId.Value, true);
                    break;

                default:
                    break;
            }
            db.AddToCookieSevkIrsaliyeDetay(cookieDetay);
            db.SaveChanges();
            return null;
        }

        public void DeleteAllCookie()
        {
            var result = db.CookieSevkIrsaliyeDetay;
            foreach (var item in result)
            {
                db.CookieSevkIrsaliyeDetay.DeleteObject(item);
            }
            db.SaveChanges();
        }

        public void DeleteCookie(bool isHurda, decimal? miktar, int? sevkAlaniId, int? hurdaAlaniId)
        {
            Stok.CookieSevkIrsaliyeDetay cookie;
            if (isHurda == true)
            {
                cookie = db.CookieSevkIrsaliyeDetay.Where(x => x.HurdaAlaniId == hurdaAlaniId.Value).Where(x => x.Miktar == miktar).FirstOrDefault();
                new Stok.Services.Hurda().MiktarDegistir(cookie.HurdaAlaniId.Value, false, cookie.Miktar);
            }
            else
            {
                cookie = db.CookieSevkIrsaliyeDetay.Where(x => x.SevkAlaniId == sevkAlaniId.Value).FirstOrDefault();
                new Stok.Services.SevkAlani().MiktarDegistir(cookie.SevkAlaniId.Value, false);
            }
            db.CookieSevkIrsaliyeDetay.DeleteObject(cookie);
            db.SaveChanges();
        }

        public void GeriAlCookie()
        {
            var result = db.CookieSevkIrsaliyeDetay.ToList();

            foreach (var item in result)
            {
                switch (item.IsHurda)
                {
                    case true:
                        new Stok.Services.Hurda().MiktarDegistir(item.HurdaAlaniId.Value, false, item.Miktar);
                        break;

                    case false:
                        new Stok.Services.SevkAlani().MiktarDegistir(item.SevkAlaniId.Value, false);
                        break;

                    default:
                        break;
                }

                db.CookieSevkIrsaliyeDetay.DeleteObject(item);
            }
            db.SaveChanges();
        }
    }
}
