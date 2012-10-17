using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.WinUI.Stok
{
    public static class MalzemeTabSecimi
    {
        public  enum  Tab
        {
        HamMalzeme,
            IslenmisMalzeme,
            SarjNumaralari
        }
        public static Tab seciliTab { get; set; }
    }
}
