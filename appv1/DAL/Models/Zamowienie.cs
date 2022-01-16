namespace appv1.DAL.Models
{
    public class Zamowienie
    {
        
    
        public int ID { get; set; }

        public int UserId { get; set; }

        public DateTime Data { get; set; }
        public string Miejscowosc { get; set; }
        public string Ulica { get; set; }
        public string NumerDomu { get; set; }
        public string NumerMieszkania { get; set; }
        public string KodPocztowy { get; set; }


    }
}
