using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO
{
    public class IslenmisMalzemeDetaySevkAlaniGrid
    {
        public int Id { get; set; }
        public string IslenmisMalzemeBilgi { get; set; }
        public string SarjNo { get; set; }
        public decimal Miktar { get; set; }
        public decimal SevkMiktar { get; set; }
        public decimal KalanMiktar { get; set; }
        public string Birim { get; set; }
        public DateTime Tarih { get; set; }
    }
}
