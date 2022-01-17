using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appv1.DAL.Models
{
    public class KoszykDoBazy
    {
        public int ID { get; set; }

        public int ProductId { get; set; }  

        public int Ilosc { get; set; }
        public int ZamowienieId { get; set; }


    }
}