using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace FirmaTakip.Services
{
    public class Raporlama
    {
        private static IObjectContainer db;

        public Raporlama()
        {
            db = Services.Admin.db;
        }

        /// <summary>
        /// Tezgahlara bakım yapılan yılları verir
        /// </summary>
        /// <returns></returns>
        public IList<DTO.Rapor.TezgahGenel> GetBakimYillari()
        {
            //return _Session.CreateQuery("select distinct YEAR(Tarih) as Yıllar from Bakim");
            var a = (from Entity.Bakim q in db
                     select new { q.Tarih.Year }).Distinct();

            return (from b in a
                   select new DTO.Rapor.TezgahGenel{Yil=b.Year}).ToList();
        }

        /// <summary>
        /// Girilen yılın içerisindeki bakım harcamalarının tutarını tezgah 
        /// bazında guruplar
        /// </summary>
        /// <param name="yil">grafiği istenen yıl</param>
        /// <returns>Tezgah ve yıl içerisindeki toplam bakım tutarı</returns>
        public IList<DTO.Rapor.YillikBakimPasta> YillikTezgahBakimPastasi(int yil)
        {
           return (from Entity.Bakim b in db where b.Tarih.Year==yil
                     group b by new {marka=b.Tezgah.Marka,model=b.Tezgah.Model,serino=b.Tezgah.SeriNo }into g
                     select new DTO.Rapor.YillikBakimPasta
                     {
                         TezgahBilgisi = string.Format("{0}/{1}({2})" ,g.Key.marka, g.Key.model, g.Key.serino),
                         Tutar=g.Sum(x=>x.Tutar)
                     }
                  ).ToList();
        }

        /// <summary>
        /// Bir tezgahın belirli bir yıl içerisindeki bakımın 
        /// aylara dağılımını verir
        /// </summary>
        /// <param name="yil">grafik yılı</param>
        /// <param name="tezgahId">grafiği istenen tezgah</param>
        /// <returns>Bakım ayı(sayı cinsinden) ve o ay ki tutar</returns>
        public IList<DTO.Rapor.YillikBakimDetay> YillikTezgahBakimAylik(int yil,Guid tezgahId)
        {
            return (from Entity.Bakim b in db
                    where b.Tarih.Year == yil && b.Tezgah.Id==tezgahId
                    group b by new { ay = b.Tarih.Month } into g
                    select new DTO.Rapor.YillikBakimDetay
                    {
                        Ay = g.Key.ay,
                        Tutar = g.Sum(x => x.Tutar)
                    }
                   ).ToList();
        }

        /// <summary>
        /// Bir tezgahın tüm bakımlarının yıllara göre bilgisini verir
        /// </summary>
        /// <param name="tezgahId">Tezgahın Id bilgisi</param>
        /// <returns>Yıl ve Tutar</returns>
        public IList<DTO.Rapor.TezgahGenel> RaporTezgahGenel(Guid tezgahId)
        {
            return (from Entity.Bakim b in db
                    where b.Tezgah.Id == tezgahId
                    group b by new { yil = b.Tarih.Year } into g
                    select new DTO.Rapor.TezgahGenel
                    {
                        Yil = g.Key.yil,
                        Tutar = g.Sum(x => x.Tutar)
                    }
                   ).ToList();
        }

    }
}
