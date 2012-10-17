﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok.DTO
{
    public class SevkAlaniIslenmisMalzemeSevkGrid
    {
        public int Id { get; set; }
        public string IslenmisMalzemeBilgi { get; set; }
        public string SarjNo { get; set; }
        public string KasaNo { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}