using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class IslenmisMalzeme
    {
        public static event EventHandler IslemYapildi;

        private FirmaTakipDB db;

        public IslenmisMalzeme()
        {
            db = new FirmaTakipDB();
        }

        public string Kaydet(string adi, string no, int hamMalzemeId, int birimId, decimal katSayi)
        {
            if (GetbyNo(no).Count() == 0)
            {
                db.IslenmisMalzemeEkle(adi.ToUpper(), no.ToUpper(), hamMalzemeId, birimId, katSayi);
                if (IslemYapildi != null)
                {
                    IslemYapildi(null, null);
                }
                return null;
            }
            else
            {
                return "Aynı numaralı ikinci bir parça kaydı yapamazsınız";
            }
        }

        public string Degistir(int islenmisMalzemeId, string adi, string no, int hamMalzemeId, int birimId, decimal katSayi)
        {
            if (GetbyNo(no).Count() > 1 || GetDetaybyIslenmisMalzemeId(islenmisMalzemeId).Count() != 0)
            {
                if (GetDetaybyIslenmisMalzemeId(islenmisMalzemeId).Count()!=0)
                {
                    return "İşlenmiş malzemenin sevkiyatı gerçekleştiği için ham malzemesi değiştirilemiyor";
                }
                return "Aynı numaralı ikinci bir parça kaydı yapamazsınız";

            }
            else
            {
                db.IslenmisMalzemeGuncelle(islenmisMalzemeId, adi, no.ToUpper(), hamMalzemeId, birimId, katSayi);
                if (IslemYapildi != null)
                {
                    IslemYapildi(null, null);
                }
                return null;
            }
        }

        public void DetayKaydet(int islenmisMalzemeId, long hamStokDetayId, decimal miktar,DateTime tarih)
        {
            db.IslenmisMalzemeDetayEkle(islenmisMalzemeId, hamStokDetayId, miktar,tarih);
            if (IslemYapildi != null)
            {
                IslemYapildi(null, null);
            }

        }

        public void DeleteDetay(int islenmisMalzemeDetayId)
        {
            db.IslenmisMalzemeDetaySil(islenmisMalzemeDetayId);
            if (IslemYapildi != null)
            {
                IslemYapildi(null, null);
            }
        }

        public void DetayTarihDegistir(int islenmisMalzemeDetayId, DateTime tarih)
        {
            Stok.IslenmisMalzemeDetay imd = db.IslenmisMalzemeDetay.Where(x => x.Id == islenmisMalzemeDetayId).FirstOrDefault();
            imd.Tarih = tarih;
            db.SaveChanges();
        }

        public string DetayMiktarDegistir(int islenmisMalzemeDetayId, bool isKullanildi, decimal miktar)
        {
            var islenmisMalzemeDetay = GetDetaybyIslenmisMalzemeDetayId(islenmisMalzemeDetayId);

            decimal yeniKullanilan;

            switch (isKullanildi)
            {
                case true:
                    yeniKullanilan = islenmisMalzemeDetay.SevkMiktar + miktar;
                    if (yeniKullanilan > islenmisMalzemeDetay.Miktar)
                    {
                        return "Miktar Yeterli Değil";

                    }
                    else
                    {

                        islenmisMalzemeDetay.SevkMiktar = yeniKullanilan;
                        db.SaveChanges();
                        return null;
                    }

                    break;

                case false:
                    yeniKullanilan = islenmisMalzemeDetay.SevkMiktar - miktar;
                    if (yeniKullanilan > islenmisMalzemeDetay.Miktar)
                    {
                        return "Miktar Yeterli Değil";

                    }
                    else
                    {
                        islenmisMalzemeDetay.SevkMiktar = yeniKullanilan;
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

        public IEnumerable GetAllbyStatus(bool durum)
        {
            return db.IslenmisMalzeme.Where(X => X.IsAktif == durum).ToList();
        }

        public Stok.IslenmisMalzeme GetbyId(int id)
        {
            return db.IslenmisMalzeme.Where(x => x.Id == id).Where(x => x.IsAktif == true).SingleOrDefault();
        }

        public IEnumerable<Stok.IslenmisMalzemeDetay>  GetDetaybyIslenmisMalzemeId(int islenmisMalzemeId)
        {
            return db.IslenmisMalzemeDetay.Where(x => x.IslenmisMalzeme.IsAktif == true).Where(x => x.IslenmisMalzemeId == islenmisMalzemeId).ToList();
        }

        public Stok.IslenmisMalzemeDetay GetDetaybyIslenmisMalzemeDetayId(int islenmisMalzemeDetayId)
        {
            return db.IslenmisMalzemeDetay.Where(x => x.IslenmisMalzeme.IsAktif == true).Where(x => x.Id == islenmisMalzemeDetayId).SingleOrDefault();
        }

        public IEnumerable<DTO.IslenmisMalzemeDetayGrid> GetAllDetayforImalatDetayGrid()
        {
            return (from imd in db.IslenmisMalzemeDetay
                    where imd.IslenmisMalzeme.IsAktif == true && (imd.Miktar-imd.SevkMiktar)>0
                    select new DTO.IslenmisMalzemeDetayGrid
                    {
                        Id = imd.Id,
                        IslenmisMalzemeBilgi = imd.IslenmisMalzeme.No + "/" + imd.IslenmisMalzeme.Adi,
                        SarjNo = imd.HamStokDetay.Sarj.No,
                        Miktar = imd.Miktar,
                        SevkMiktar = imd.SevkMiktar,
                        KalanMiktar=imd.Miktar-imd.SevkMiktar,
                        Birim = imd.IslenmisMalzeme.Birim.Adi,
                        Tarih = imd.Tarih
                    });
        }

        public IEnumerable<DTO.IslenmisMalzemeDetaySevkAlaniGrid> GetAllDetayforSevkAlaniGrid()
        {
            return (from imd in db.IslenmisMalzemeDetay
                    where imd.IslenmisMalzeme.IsAktif == true && (imd.Miktar-imd.SevkMiktar)!=0
                    select new DTO.IslenmisMalzemeDetaySevkAlaniGrid
                    {
                        Id = imd.Id,
                        IslenmisMalzemeBilgi = imd.IslenmisMalzeme.No + "/" + imd.IslenmisMalzeme.Adi,
                        SarjNo=imd.HamStokDetay.Sarj.No,
                        Miktar = imd.Miktar,
                        SevkMiktar = imd.SevkMiktar,
                        KalanMiktar=imd.Miktar-imd.SevkMiktar,
                        Birim = imd.IslenmisMalzeme.Birim.Adi,
                        Tarih = imd.Tarih
                    });
        }

        public Stok.IslenmisMalzemeDetay GetDetaybyHamStokDetayId(long hamStokDetayId)
        {
            return db.IslenmisMalzemeDetay.Where(x => x.IslenmisMalzeme.IsAktif == true).Where(x => x.HamStokDetayId == hamStokDetayId).SingleOrDefault();
        }

        public IEnumerable<Stok.IslenmisMalzeme> GetbyNo(string no)
        {
            return db.IslenmisMalzeme.Where(x => x.No == no.ToUpper()).Where(x => x.IsAktif == true).ToList();
        }

        public IEnumerable<DTO.IslenmisMalzemeGrid> GetAllbyStatusforGrid(bool durum)
        {
            return (from q in db.IslenmisMalzeme
                    where q.IsAktif == durum
                    select new DTO.IslenmisMalzemeGrid
                    {
                        Id = q.Id,
                        Adi = q.Adi,
                        No = q.No,
                        RefMalzemeAdi = q.HamMalzeme.Adi,
                        RefMalzemeNo = q.HamMalzeme.No,
                        KatSayi = q.KatSayi,
                        Birim = q.Birim.Adi
                    });
        }

        public IEnumerable<DTO.IslenmisMalzemeImalatGrid> GetAllbyStatusforImalatGrid(bool durum)
        {
            return (from q in db.IslenmisMalzeme
                    where q.IsAktif == durum 
                    select new DTO.IslenmisMalzemeImalatGrid
                    {
                        Id = q.Id,
                        IslenmisMalzeme = q.No + "/" + q.Adi,
                        HamMalzeme = q.HamMalzeme.No + "/" + q.HamMalzeme.Adi

                    });
        }

        public void AktifDurumDegistir(int islenmisMalzemeId, bool durum)
        {
            db.IslenmisMalzemeSil(islenmisMalzemeId, durum);
            if (IslemYapildi != null)
            {
                IslemYapildi(null, null);
            }
        }
    }
}
