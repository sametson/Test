using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;


namespace FirmaTakip.Services
{
    public class Bakim
    {
        public static event EventHandler IslemYapildi;
        private static IObjectContainer db;

        public Bakim()
        {
            db = Services.Admin.db;
        }

        /// <summary>
        /// Bakım kaydetme ve güncelleme
        /// id bilgisi verilirse güncelleme yapar,null verilirse kaydetme yapar
        /// </summary>
        /// <param name="id">Bakım ID</param>
        /// <param name="servisNo">Servis Bakım No</param>
        /// <param name="ariza">Arıza ve Bakım Açıklaması</param>
        /// <param name="tarih">Bakım Tarihi</param>
        /// <param name="tutar">Bakım Tutarı</param>
        /// <param name="tezgahId">Bakım Yapılan Tezgah Id</param>
        /// <param name="firmaId">Bakım Yapan Firma Id</param>
        public void Kaydet(Guid? id, string servisNo, string ariza, DateTime tarih, decimal tutar, Guid tezgahId, Guid firmaId)
        {
            Entity.Bakim bakim = null;

            if (id == null)
            {  
                bakim = new Entity.Bakim
                {
                    Ariza=ariza,
                    Firma= new Services.Firma().SGetById(firmaId),
                    Id=Guid.NewGuid(),
                    ServisRaporNo=servisNo,
                    Tarih=tarih,
                    Tezgah = new Services.Tezgah().SGetById(tezgahId),
                    Tutar=tutar                    
                };
            }
            else
            {
                bakim = new Services.Bakim().SGetById(id.Value);
                bakim.Ariza = ariza;
                bakim.Firma = new Services.Firma().SGetById(firmaId);
                bakim.Id = id.Value;
                bakim.ServisRaporNo = servisNo;
                bakim.Tarih = tarih;
                bakim.Tezgah = new Services.Tezgah().SGetById(tezgahId);
                bakim.Tutar = tutar;
            }
            db.Store(bakim);
            db.Commit();

            if (IslemYapildi != null)
                IslemYapildi(null, null);
        }

        /// <summary>
        /// Bakım kaydını siler
        /// </summary>
        /// <param name="id">Bakım ID bilgisi</param>
        public void Sil(Guid id)
        {
            Entity.Bakim bakim = new Services.Bakim().SGetById(id);
            db.Delete(bakim);
            db.Commit();
            if (IslemYapildi != null)
                IslemYapildi(null, null);
        }

        /// <summary>
        /// Bakımların tamamını getirir
        /// </summary>
        /// <returns>Arıza,Firma,Id,Servis Rapor No,Tarih,Tezgah,Tutar</returns>
        public IList<DTO.BakimList> GetAll()
        {
            return (from Entity.Bakim q in db
                    select new DTO.BakimList
                    {
                        Ariza=q.Ariza,
                        Firma=q.Firma,
                        Id=q.Id,
                        ServisRaporNo=q.ServisRaporNo,
                        Tarih=q.Tarih,
                        Tezgah=q.Tezgah,
                        Tutar=q.Tutar
                    }).ToList();
        }

        /// <summary>
        /// Grid Controle doldurulacak formatta tüm bakımları getirir
        /// </summary>
        /// <returns>Bakım Id,Servis Rapor No,Tutar,Tarih,Firma,Tezgah</returns>
        public IList<DTO.BakimListTablo> GetAllforTablo()
        {
            return (from Entity.Bakim q in db
                    select new DTO.BakimListTablo
                    {
                        Id = q.Id,
                        ServisRaporNo = q.ServisRaporNo,
                        Tutar = q.Tutar,
                        Tarih = q.Tarih,
                        Firma = q.Firma.Adi,
                        Tezgah = string.Format("{0}/{1}({2})",q.Tezgah.Marka,q.Tezgah.Model,q.Tezgah.SeriNo)
                    }).ToList();
        }

        /// <summary>
        /// Id bilgisi verilen bakımı getirir
        /// </summary>
        /// <param name="Id">Bakım Id bilgisi</param>
        /// <returns>Servis Rapor No,Tarih,Tutar,Arıza,Firma Id,Bakım Id,Tezgah Id</returns>
        public DTO.BakimEdit GetById(Guid Id)
        {
            return (from Entity.Bakim q in db where q.Id==Id
                    select new DTO.BakimEdit
                    {
                        ServisRaporNo = q.ServisRaporNo,
                        Tarih = q.Tarih,
                        Tutar = q.Tutar,
                        Ariza = q.Ariza,
                        FirmaId = q.Firma.Id,
                        Id = q.Id,
                        TezgahId = q.Tezgah.Id,

                    }).SingleOrDefault();
        }

        /// <summary>
        /// Service katmanı için yazılmıştır
        /// Id bilgisi verilen bakımı Entity.Bakım cinsinden
        /// getirir
        /// </summary>
        /// <param name="Id">Bakım Id bilgisi</param>
        /// <returns>Entity.Bakım</returns>
        public Entity.Bakim SGetById(Guid Id)
        {
            return (from Entity.Bakim q in db where q.Id==Id
                    select q).SingleOrDefault();
        }
    }
}
