using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class HamStok
    {
        FirmaTakipDB db;

        public HamStok()
        {
            db = new FirmaTakipDB();
        }

        public int HamStokKaydet(DateTime tarih, string irsaliyeNo, string aciklama)
        {
            Stok.HamStok hamStok = new Stok.HamStok();
            if (GetbyIrsaliyeNo(irsaliyeNo).Count() == 0)
            {
                hamStok.Tarih = tarih;
                hamStok.IrsaliyeNo = irsaliyeNo.ToUpper();
                hamStok.Aciklama = aciklama;
                hamStok.IsAktif = true;
                db.AddToHamStok(hamStok);
                db.SaveChanges();
            }
            else
            {
                return 0;
            }

            return hamStok.Id;
        }

        public int HamStokGuncelle(DateTime tarih, string irsaliyeNo, string aciklama, int hamStokId)
        {
            Stok.HamStok hamStok = GetbyId(hamStokId);
            if (hamStok.IrsaliyeNo == irsaliyeNo.ToUpper())
            {
                if (GetbyIrsaliyeNo(irsaliyeNo).Count() == 1)
                {
                    hamStok.Tarih = tarih;
                    hamStok.IrsaliyeNo = irsaliyeNo.ToUpper();
                    hamStok.Aciklama = aciklama;
                    hamStok.IsAktif = true;
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
                    hamStok.Tarih = tarih;
                    hamStok.IrsaliyeNo = irsaliyeNo.ToUpper();
                    hamStok.Aciklama = aciklama;
                    hamStok.IsAktif = true;
                    db.SaveChanges();
                }
                else
                {

                    return 0;
                }
            }


            return hamStok.Id;
        }

        public void HamStokDetayKaydet(long hamStokDetayId, int sarjId, int hamStokId, decimal miktar)
        {
            if (hamStokDetayId == 0)
            {
                db.HamStokDetayEkle(sarjId, hamStokId, miktar);
            }
            else
            {
                db.HamStokDetayGuncelle(hamStokDetayId, sarjId, hamStokId, miktar);
            }
        }

        public IEnumerable<Stok.HamStokDetay> GetHamStokDetayByHamStokId(int hamStokId)
        {
            return db.HamStokDetay.Where(x => x.HamStokId == hamStokId).ToList();
        }

        public Stok.HamStokDetay GetHamStokDetaybyHamStokDetayId(long hamStokDetayId)
        {
            return db.HamStokDetay.Where(x => x.Id == hamStokDetayId).FirstOrDefault();
        }

        public IEnumerable<Stok.HamStok> GetAllbyStatus(bool durum)
        {
            return db.HamStok.Where(x => x.IsAktif == durum).ToList();
        }

        public Stok.HamStok GetbyId(int id)
        {
            return db.HamStok.Where(x => x.Id == id).FirstOrDefault();
        }

        public string DeleteHamStok(int hamStokId)
        {
            if (GetHamStokDetayByHamStokId(hamStokId).Count() != 0)
            {
                return "Önce içeriği siliniz";
            }
            else
            {
                db.HamStokSil(hamStokId, false);
            }
            return null;
        }

        public IEnumerable<Stok.HamStok> GetbyIrsaliyeNo(string irsaliyeNo)
        {

            return db.HamStok.Where(x => x.IsAktif == true).Where(x => x.IrsaliyeNo == irsaliyeNo.ToUpper()).ToList();

        }

        public void DeleteHamStokDetaybyId(long hamStokDetayId)
        {

            db.HamStokDetaySil(hamStokDetayId);

        }



        public string MiktarAyarla(long hamStokDetayId, decimal? islenmisMalzemeSevkMiktar, decimal katSayi, decimal miktar, bool isKullanildi)
        {

            var hamStokDetay = GetHamStokDetaybyHamStokDetayId(hamStokDetayId);
            decimal yeniKullanilan;

            switch (isKullanildi)
            {
                case true:
                    yeniKullanilan = hamStokDetay.SevkMiktar + miktar / katSayi;
                    if (yeniKullanilan > hamStokDetay.Miktar)
                    {
                        return "Miktar Yeterli Değil";

                    }
                    else
                    {

                        hamStokDetay.SevkMiktar = yeniKullanilan;
                        db.SaveChanges();
                        return null;
                    }

                    break;

                case false:
                    if (islenmisMalzemeSevkMiktar == 0)
                    {
                        yeniKullanilan = hamStokDetay.SevkMiktar - miktar / katSayi;
                        hamStokDetay.SevkMiktar = yeniKullanilan;
                        db.SaveChanges();
                        return null;
                    }
                    else
                    {
                        return "Seçili kayda ait işlemler silinmeden bu kayıt silinemez";
                    }
                    break;

                default:
                    return null;
                    break;



            }
        }
    }
}
