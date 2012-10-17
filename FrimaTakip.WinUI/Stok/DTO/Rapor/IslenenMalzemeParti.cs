using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO.Rapor
{
    public class IslenenMalzemeParti
    {
        public int SarjId { get; set; }
        public string HamMalzemeBilgi { get; set; }
        public string SarjNo { get; set; }
        public DateTime AmbaraGirisTarih { get; set; } 
        public DateTime UretimeSevkTarih { get; set; }
        public decimal UretimdekiMiktar { get; set; }
        public string BirimAdi { get; set; }
    }
}
