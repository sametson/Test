using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FirmaTakip.WinUI.Stok.Services
{
    public class Birim
    {
        public static event EventHandler IslemYapildi;

        private FirmaTakipDB db;
        public Birim()
        {
            db = new FirmaTakipDB();
        }
        public IEnumerable GetAll()
        {
            return db.Birim.ToList();
        }
    }
}
