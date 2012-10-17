using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO.Rapor
{
    public class HurdaAlaniParti
    {
        public int SarjId { get; set; }
        public string HamMalzemeBilgi { get; set; }
        public string SarjNo { get; set; }
        public DateTime AmbaraGirisTarih { get; set; }
        public DateTime HurdaAlaniSevkTarih { get; set; }
        public decimal HurdaAlaniMiktar { get; set; }
        public string BirimAdi { get; set; }
    }
}
