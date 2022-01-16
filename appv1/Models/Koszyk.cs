using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using appv1.DAL.Models;

namespace appv1.Models
{
    public class Koszyk
    {
        public int ID { get; set; }

 
        public Products Product { get; set; }

        public int Ilosc { get; set; }
        public int ZamowienieId { get; set; }

    }
}