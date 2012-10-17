using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace FirmaTakip.Services
{
    public class Kullanici
    {
        private static IObjectContainer db;
        public Kullanici()
        {
            db = Services.Admin.db;
        }

        public void Kaydet(string adi, string tel, string faks, string email, string adres)
        {

            Entity.Kullanici kullanici = null;
            kullanici = SGetById();


            if (kullanici == null)
            {

                kullanici = new Entity.Kullanici
                {
                    Id = 1,
                    Adi = adi,
                    Tel = tel,
                    Faks = faks,
                    Email = email,
                    Adres = adres
                };
            }

            else
            {
                kullanici.Id = 1;
                kullanici.Adi = adi;
                kullanici.Tel = tel;
                kullanici.Faks = faks;
                kullanici.Email = email;
                kullanici.Adres = adres;
            }
            db.Store(kullanici);
            db.Commit();

        }

        public DTO.Kullanici Get()
        {
            return (from Entity.Kullanici q in db
                    where q.Id == 1
                    select new DTO.Kullanici
                    {
                        Adi = q.Adi,
                        Adres = q.Adres,
                        Email = q.Email,
                        Faks = q.Faks,
                        Tel = q.Tel

                    }).FirstOrDefault();
        }

        public Entity.Kullanici SGetById()
        {
            return (from Entity.Kullanici q in db
                    where q.Id == 1
                    select q).FirstOrDefault();
        }
    }
}
