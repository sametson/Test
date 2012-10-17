using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO
{
    public class IslenmisMalzemeBilgi
    {
        public int IslenmisMalzemeDetayId { get; set; }
        public decimal Miktar { get; set; }
        public decimal KatSayi { get; set; }
        public string Birim { get; set; }
    }
}
