using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.Entity
{
    public class Tezgah
    {
        public Guid Id { get; set; }

        public string Marka { get; set; }
        
        public string Model { get; set; }

        public string SeriNo { get; set; }

        public string Yil { get; set; }

        public bool IsDeleted { get; set; }
    }
}
