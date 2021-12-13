using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appv1.Interfaces;

namespace appv1.Services
{
    public class ObslugaBazyDanych : IObslugaBazyDanych
    {
        public string DodajZajeciaDoPlanu(string nazwa)
        {
            int id = new Random().Next();
            string komunikat = $"Zajęcia o nazwie {nazwa} zostały dodane do planu pod Id {id}";
            Console.WriteLine(komunikat);

            return komunikat;
        }
    }
}