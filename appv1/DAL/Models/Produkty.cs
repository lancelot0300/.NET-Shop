using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appv1.DAL.Models
{
    public class Products
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }

        public string KodProduktu { get; set; }

        public int Ilosc { get; set; }

        public decimal Cena { get; set; }

        public string Kategoria { get; set; }

    }
}
