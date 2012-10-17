using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO
{
    public class SevkIrsaliyeDetayMusteriyeSevk
    {
        public long Id { get; set; }
        public int SevkIrsaliyeId { get; set; }
        public int SevkAlaniId { get; set; }
        public int HurdaAlaniId { get; set; }
        public string MalzemeBilgi { get; set; }
        public string SarjNo { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string MalzemeDurumu { get; set; }
    }
}
