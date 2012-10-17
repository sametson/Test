using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmaTakip.Entity;

namespace FirmaTakip.DTO
{
    public class BakimEdit
    {
        public Guid Id { get; set; }

        public string ServisRaporNo { get; set; }

        public string Ariza { get; set; }

        public DateTime Tarih { get; set; }

        public decimal Tutar { get; set; }

        public Guid TezgahId { get; set; }

        public Guid FirmaId { get; set; }

    }
}
