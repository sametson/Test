using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace FirmaTakip.Services
{
    public class Tezgah
    {
        private static IObjectContainer db;

        public static event EventHandler KayitYapildi;

        public Tezgah()
        {
            db = Services.Admin.db;
        }

        /// <summary>
        /// Tezgah kaydetme ve güncelleme
        /// id bilgisi verilirse güncelleme yapar, verilmezse veya 0 verilirse kaydetme yapar
        /// </summary>
        /// <param name="id">Tezgah id</param>
        /// <param name="marka">Tezgahın Marka</param>
        /// <param name="model">Tezgahın Model</param>
        /// <param name="seriNo">Tezgahın Seri No</param>
        /// <param name="yil">Tezgah yılı</param>
        public void Kaydet(Guid? id, string marka, string model, string seriNo, string yil)
        {
            Entity.Tezgah tezgah = null;

            if (id == null)
            {
                tezgah = new Entity.Tezgah
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Marka = marka,
                    Model = model,
                    SeriNo = seriNo,
                    Yil = yil,
                };
            }
            else
            {
                tezgah = SGetById(id.Value);
                tezgah.Yil = yil;
                tezgah.SeriNo = seriNo;
                tezgah.Model = model;
                tezgah.Marka = marka;
                tezgah.IsDeleted = false;
            }
            db.Store(tezgah);
            db.Commit();
            if (KayitYapildi != null)
                KayitYapildi(null, null);
        }

        /// <summary>
        /// Tezgahı siler
        /// </summary>
        /// <param name="id">Silinecek tezgahın id bilgisi</param>
        public void Sil(Guid id)
        {
            var tezgah = SGetById(id);
            tezgah.IsDeleted = true;
            db.Store(tezgah);
            db.Commit();
            if (KayitYapildi != null)
                KayitYapildi(null, null);
        }

        /// <summary>
        /// Silinen tezgahı geri getirir
        /// </summary>
        /// <param name="id">Geri getirelecek tezgahın id bilgisi</param>
        public void SilGeriAl(Guid id)
        {
            var tezgah = SGetById(id);
            tezgah.IsDeleted = false;
            db.Store(tezgah);
        }

        /// <summary>
        /// Silinmemiş tezgahların tamamını getirir
        /// </summary>
        /// <returns>Marka,Model,Seri No, Yıl</returns>
        public IList<DTO.TezgahList> GetAll()
        {
            return (from Entity.Tezgah q in db
                    where q.IsDeleted == false
                    select new DTO.TezgahList
                    {
                        Id = q.Id,
                        Marka = q.Marka,
                        Model = q.Model,
                        SeriNo = q.SeriNo,
                        Yil = q.Yil,

                    }).ToList();
        }

        /// <summary>
        /// Verilen id bilgisine sahip tezgah gelir 
        /// </summary>
        /// <param name="id">Getirilecek tezgahın id si</param>
        /// <returns>Marka,Model,Seri No, Yıl</returns>
        public DTO.TezgahEdit GetById(Guid id)
        {
            return (from Entity.Tezgah q in db
                    where q.Id == id
                    select new DTO.TezgahEdit
                    {
                        Id = q.Id,
                        Marka = q.Marka,
                        Model = q.Model,
                        SeriNo = q.SeriNo,
                        Yil = q.Yil,

                    }).SingleOrDefault();
        }

        /// <summary>
        /// id si verilen tezgahın bilgisini verir
        /// </summary>
        /// <param name="id">Tezgah Id si</param>
        /// <returns>{marka}/{model}/{serino}</returns>
        public DTO.TezgahBilgi GetByIdBilgi(Guid id)
        {
            return (from Entity.Tezgah q in db
                    where q.Id == id
                    select new DTO.TezgahBilgi
                    {
                        TezgahBilgisi = string.Format("{0}/{1}/{2}", q.Marka, q.Model, q.SeriNo)

                    }).SingleOrDefault();
        }

        /// <summary>
        /// Entity katmanı için yazılmıştır
        /// id si verilen tezgahı Entity.Tezgah cinsinden
        /// verir
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity.Tezgah</returns>
        public Entity.Tezgah SGetById(Guid id)
        {
            return (from Entity.Tezgah q in db
                    where q.Id == id
                    select q).SingleOrDefault();
        }

    }

}
