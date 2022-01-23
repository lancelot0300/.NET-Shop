using System.ComponentModel.DataAnnotations;

namespace appv1.DAL.Models
{
    public class Zamowienie
    {

        public int ID { get; set; }
        [Required]
        public int UserId { get; set; }

        public DateTime Data { get; set; }
        [Required]
        public string Miejscowosc { get; set; }
        [Required]
        [StringLength(20)]
        public string Ulica { get; set; }
        [Required]
        public string NumerDomu { get; set; }
        [Required]
        public string NumerMieszkania { get; set; }
        [Required]
        [StringLength(6, ErrorMessage="Niepoprawny kod pocztowy")]
        public string KodPocztowy { get; set; }


    }
}
