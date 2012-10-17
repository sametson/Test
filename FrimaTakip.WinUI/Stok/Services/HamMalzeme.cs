using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class HamMalzeme
    {
        public static event EventHandler IslemYapildi;

        private FirmaTakipDB db;

        public HamMalzeme()
        {
            db = new FirmaTakipDB();
        }

        public string Kaydet(string adi, string no, int birimId)
        {
            if (GetbyNo(no).Count() == 0)
            {
                db.HamMalzemeEkle(adi.ToUpper(), no.ToUpper(), birimId);
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

        public string Degistir(int hamMalzemeId, string adi, string no, int birimId)
        {
            if (GetbyNo(no).Count() > 1)
            {

                return "Aynı numaralı ikinci bir parça kaydı yapamazsınız";
            }
            else
            {
                db.HamMalzemeGuncelle(hamMalzemeId, adi.ToUpper(), no.ToUpper(), birimId);
                if (IslemYapildi != null)
                {
                    IslemYapildi(null, null);
                }
                return null;
            }
        }

        public IEnumerable GetAllbyStatus(bool durum)
        {
            return db.HamMalzeme.Where(x => x.IsAktif == durum).ToList();
        }

        public IEnumerable<DTO.HamMalzemeGrid> GetAllbyStatusforGrid(bool durum)
        {
            return (from q in db.HamMalzeme
                    where q.IsAktif == true
                    select new DTO.HamMalzemeGrid
                    {
                        Id = q.Id,
                        Adi = q.Adi,
                        No = q.No,
                        Birim = q.Birim.Adi
                    }).ToList();
        }

        public Stok.HamMalzeme GetbyId(int id)
        {
            return db.HamMalzeme.Where(x => x.Id == id).Where(x => x.IsAktif == true).FirstOrDefault();
        }

        public IEnumerable<Stok.HamMalzeme> GetbyNo(string no)
        {
            return db.HamMalzeme.Where(x => x.No == no.ToUpper()).Where(x => x.IsAktif == true).ToList();
        }

        public void AktifDurumDegistir(bool durum, int hamMalzemeId)
        {
            db.HamMalzemeSil(hamMalzemeId, durum);

            if (IslemYapildi != null)
            {
                IslemYapildi(null, null);
            }
        }

    }
}
