using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmaTakip.Entity;

namespace FirmaTakip.DTO
{
    public class BakimListTablo
    {
        public Guid Id { get; set; }

        public string ServisRaporNo { get; set; }

        public DateTime Tarih { get; set; }

        public decimal Tutar { get; set; }

        public string Tezgah { get; set; }

        public string Firma { get; set; }
    }
}
