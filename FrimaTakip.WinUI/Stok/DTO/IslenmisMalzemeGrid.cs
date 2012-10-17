using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO
{
    public class IslenmisMalzemeGrid
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string No { get; set; }
        public string RefMalzemeAdi { get; set; }
        public string RefMalzemeNo { get; set; }
        public string Birim { get; set; }
        public decimal KatSayi { get; set; }
    }
}
