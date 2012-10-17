using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using System.Collections;

namespace FirmaTakip.Services
{
    public class Firma
    {
        public static event EventHandler IslemYapildi;
        private static IObjectContainer db;
        public Firma()
        {
            db = Services.Admin.db;
        }

        /// <summary>
        /// Firma Kaydetme ve Güncelleme
        /// (id bilgisi verilirse güncelleme yapar, verilmezse veya 0 verilirse kaydetme yapar)
        /// </summary>
        /// <param name="id">Firma id</param>
        /// <param name="adi">Firma Adı</param>
        /// <param name="tel">Firma Tel</param>
        /// <param name="faks">Firma Faks</param>
        /// <param name="cep">Firma Cep</param>
        /// <param name="adres">Firma Adres</param>
        public void Kaydet(Guid? id, string adi, string tel, string faks, string cep, string adres)
        {
            Entity.Firma firma = null;

            if (id == null)
            {

                firma = new Entity.Firma
                {
                    Id = Guid.NewGuid(),
                    Adi = adi,
                    Tel = tel,
                    Faks = faks,
                    Cep = cep,
                    Adres = adres,
                    IsDeleted = false
                };
            }

            else
            {
                firma = SGetById(id.Value);
                firma.Adi = adi;
                firma.Tel = tel;
                firma.Faks = faks;
                firma.Cep = cep;
                firma.Adres = adres;
                firma.IsDeleted = false;
            }
            db.Store(firma);
            db.Commit();
            if (IslemYapildi!=null)
                IslemYapildi(null, null);

        }

        /// <summary>
        /// Firmayı siler
        /// </summary>
        /// <param name="id">Silinecek firmanın id bilgisi</param>
        public void Sil(Guid id)
        {
            Entity.Firma firma = SGetById(id);
            firma.IsDeleted = true;
            db.Store(firma);
            db.Commit();
            if (IslemYapildi != null)
                IslemYapildi(null, null);
        }

        /// <summary>
        /// Silinen firmayı geri getirmek için kullanılır
        /// </summary>
        /// <param name="id">Geri getirilecek firmanın id biligisi</param>
        public void SilGeriAl(Guid id)
        {
            Entity.Firma firma = SGetById(id);
            firma.IsDeleted = false;
            db.Store(firma);
            db.Commit();
        }

        /// <summary>
        /// Silinmemiş firmaların tamamını getirir
        /// </summary>
        /// <returns>Adı,Cep,Tel,Faks,Adres</returns>
        public IList<DTO.FirmaList> GetAll()
        {
            return (from Entity.Firma q in db where q.IsDeleted==false
                    select new DTO.FirmaList
                    {
                        Id = q.Id,
                        Adi = q.Adi,
                        Cep = q.Cep,
                        Faks = q.Faks,
                        Tel = q.Tel,
                        Adres = q.Adres

                    }).ToList();
        }

        /// <summary>
        /// Silinmemiş firmaların tamamını getirir
        /// Tabloya doldurulacak formatta getirir
        /// </summary>
        /// <returns>Id,Adı,Cep,Tel,Faks</returns>
        public IEnumerable<DTO.FirmaListTablo> GetAllforTablo( )
        {
            return (from Entity.Firma q in db
                    where q.IsDeleted == false
                    select new DTO.FirmaListTablo
                    {
                        Id = q.Id,
                        Adi = q.Adi,
                        Cep = q.Cep,
                        Faks = q.Faks,
                        Tel = q.Tel

                    }).ToList();
        }

        /// <summary>
        /// Verilen id bilgisine sahip firma gelir
        /// </summary>
        /// <param name="Id">Getirilecek firmanın id si</param>
        /// <returns>Id,Adı,Cep,Tel,Faks,Adres</returns>
        public DTO.FirmaEdit GetById(Guid Id)
        {
            return (from Entity.Firma q in db
                    where q.Id == Id
                    select new DTO.FirmaEdit
                    {
                        Id = q.Id,
                        Adi = q.Adi,
                        Cep = q.Cep,
                        Faks = q.Faks,
                        Tel = q.Tel,
                        Adres = q.Adres
                    }).FirstOrDefault();
        }

        /// <summary>
        /// Service katmanı için yazılmıştır
        /// id si verilen firmayı Entity.Firma cinsinden
        /// geri döndürür
        /// </summary>
        /// <param name="Id">Firma Id bilgisi</param>
        /// <returns>Entity.Firma</returns>
        public Entity.Firma SGetById(Guid Id)
        {
            return (from Entity.Firma q in db
                    where q.Id == Id
                    select q).FirstOrDefault();
        }
    }
}
