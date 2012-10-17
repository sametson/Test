using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO.Rapor
{
    public class HamMalzemeAna
    {
        public int HamMalzemeId { get; set; }
        public string HamMalzemeAdi { get; set; }
        public string HamMalzemeNo { get; set; }
        public decimal HamMiktar { get; set; }
        public decimal UretimdekiMiktar { get; set; }
        public decimal SevkAlaniMiktar { get; set; }
        public decimal HurdaAlaniMiktar { get; set; }
        public string BirimAdi { get; set; }
    }
}
