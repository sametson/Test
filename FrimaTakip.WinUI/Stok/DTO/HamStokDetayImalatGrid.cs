using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO
{
    public class HamStokDetayImalatGrid
    {
        public DateTime Tarih { get; set; }
        public decimal GelenMiktar { get; set; }
        public decimal SevkMiktar { get; set; }
        public decimal KalanMiktar { get; set; }
        public string HamMalzemeBirim { get; set; }
        public decimal CikanAdet { get; set; }
        public string IslenmisMalzemeBirim { get; set; }
        public string SarjNo { get; set; }
        public long Id { get; set; }
    }
}
