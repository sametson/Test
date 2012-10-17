using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmaTakip.Entity;

namespace FirmaTakip.DTO
{
    public class BakimList
    {
        public Guid Id { get; set; }

        public string ServisRaporNo { get; set; }

        public string Ariza { get; set; }

        public DateTime Tarih { get; set; }

        public decimal Tutar { get; set; }

        public Tezgah Tezgah { get; set; }

        public Firma Firma { get; set; }

    }
}
