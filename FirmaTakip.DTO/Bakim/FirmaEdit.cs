using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmaTakip.DTO
{
    public class FirmaEdit
    {
        public Guid Id { get; set; }

        public string Adi { get; set; }

        public string Tel { get; set; }

        public string Faks { get; set; }

        public string Cep { get; set; }

        public string Adres { get; set; }

        public bool IsDeleted { get; set; }
    }
}
