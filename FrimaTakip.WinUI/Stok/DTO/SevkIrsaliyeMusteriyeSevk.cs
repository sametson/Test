using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO
{
    public class SevkIrsaliyeMusteriyeSevk
    {
        public int Id { get; set; }
        public string IrsaliyeNo { get; set; }
        public string Musteri { get; set; }
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; }
    }
}
