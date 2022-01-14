using System.ComponentModel.DataAnnotations;


namespace appv1.DAL.Models
{
    public class Koszyk
    {
        public Products Product
        {
            get;
            set;
        }

        public int Ilosc
        {
            get;
            set;
        }

    }
}