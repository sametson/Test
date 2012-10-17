using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class Sarj
    {
        public static event EventHandler IslemYapildi;

        private FirmaTakipDB db;

        public Sarj()
        {
            db = new FirmaTakipDB();
        }

        public string Kaydet(string no, int hamMalzemeId)
        {
            if (GetbySarjNo(no, hamMalzemeId).Count() == 0)
            {
                db.SarjEkle(no.ToUpper(), hamMalzemeId);
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

        public string Degistir(int sarjId, string no, int hamMalzemeId)
        {
            Stok.Sarj sarj = GetbyId(sarjId);

            if (sarj.No==no.ToUpper())
            {
                if (GetbySarjNo(no, hamMalzemeId).Count()==0)
                {
                    return "Aynı numaralı ikinci bir parça kaydı yapamazsınız";
                }
                else
                {
                    db.SarjGuncelle(sarjId, no.ToUpper(), hamMalzemeId);
                    if (IslemYapildi != null)
                    {
                        IslemYapildi(null, null);
                    }
                    return null;
                }
            }
            else
            {
                if (GetbySarjNo(no, hamMalzemeId).Count() ==1)
                {
                    return "Aynı numaralı ikinci bir parça kaydı yapamazsınız";
                }
                else
                {
                    db.SarjGuncelle(sarjId, no.ToUpper(), hamMalzemeId);
                    if (IslemYapildi != null)
                    {
                        IslemYapildi(null, null);
                    }
                    return null;
                }
            }

            if (GetbySarjNo(no, hamMalzemeId).Count() > 1)
            {
                return "Aynı numaralı ikinci bir parça kaydı yapamazsınız";
            }
            else
            {
                db.SarjGuncelle(sarjId, no.ToUpper(), hamMalzemeId);
                if (IslemYapildi != null)
                {
                    IslemYapildi(null, null);
                }
                return null;
            }
        }

        public IEnumerable GetAll()
        {
            return db.Sarj.Where(x => x.HamMalzeme.IsAktif == true).ToList();
        }

        public IEnumerable<DTO.SarjNumaralariGrid> GetAllforGrid()
        {
            return (from q in db.Sarj
                    where q.HamMalzeme.IsAktif == true
                    select new DTO.SarjNumaralariGrid
                    {
                        Id = q.Id,
                        SarjNo = q.No,
                        HamMalzeme = q.HamMalzeme.No + " (" + q.HamMalzeme.Adi + ")"
                    }).ToList();
        }

        public IEnumerable<DTO.HamStokDetayImalatGrid> GetAllforImalatSarjGrid(int hamMalzemeId, int islenmisMalzemeId)
        {
            decimal katSayi = db.IslenmisMalzeme.Where(x => x.Id == islenmisMalzemeId).FirstOrDefault().KatSayi;
            return (from hsd in db.HamStokDetay
                    where hsd.HamStok.IsAktif==true &&  hsd.Sarj.HamMalzemeId == hamMalzemeId && (hsd.Miktar - hsd.SevkMiktar) != 0
                    select new DTO.HamStokDetayImalatGrid
                    {
                        Tarih = hsd.HamStok.Tarih,
                        Id = hsd.Id,
                        SarjNo = hsd.Sarj.No,
                        GelenMiktar = hsd.Miktar,
                        SevkMiktar = hsd.SevkMiktar,
                        KalanMiktar = hsd.Miktar - hsd.SevkMiktar,
                        HamMalzemeBirim = hsd.Sarj.HamMalzeme.Birim.Adi,
                        CikanAdet = (hsd.Miktar - hsd.SevkMiktar) * katSayi,
                        IslenmisMalzemeBirim = db.IslenmisMalzeme.Where(x => x.Id == islenmisMalzemeId).FirstOrDefault().Birim.Adi

                    }).ToList();
        }

        public IEnumerable GetAllbyMalzemeId(int hamMalzemeId)
        {
            return db.Sarj.Where(x => x.HamMalzemeId == hamMalzemeId).Where(X => X.HamMalzeme.IsAktif == true).ToList();
        }

        public Stok.Sarj GetbyId(int id)
        {
            return db.Sarj.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Stok.Sarj> GetbySarjNo(string no, int hamMalzemeId)
        {
            return db.Sarj.Where(x => x.HamMalzemeId == hamMalzemeId).Where(x => x.No == no.ToUpper()).ToList();
        }
    }
}
