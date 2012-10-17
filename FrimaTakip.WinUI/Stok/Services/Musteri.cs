using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class Musteri
    {
        private FirmaTakipDB db;
        public static event EventHandler IslemYapildi;
        public Musteri()
        {
            db = new FirmaTakipDB();
        }

        public void Kaydet(string adi)
        {
            Stok.Musteri musteri = new Stok.Musteri();
            musteri.Adi = adi;
            musteri.IsAktif = true;
            db.AddToMusteri(musteri);
            db.SaveChanges();

            if (IslemYapildi != null)
                IslemYapildi(null, null);
        }
        public void Degistir(int id,string adi)
        {
            Stok.Musteri musteri = GetbyId(id);
            musteri.Adi = adi;
            db.SaveChanges();
            if (IslemYapildi != null)
                IslemYapildi(null, null);
        }
        public void Sil(int id)
        {
            Stok.Musteri musteri = GetbyId(id);
            musteri.IsAktif = false;
            db.SaveChanges();
            if (IslemYapildi != null)
                IslemYapildi(null, null);
        }

        public IEnumerable<Stok.Musteri> GetAllbyStatus(bool durum)
        {
            return db.Musteri.Where(x => x.IsAktif == durum).ToList();
         }
        public Stok.Musteri GetbyId(int id)
        {
            return db.Musteri.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
