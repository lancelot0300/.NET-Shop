using appv1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;

namespace appv1.DAL.Contexts
{
    public static class SklepIntInitializer
    {
        public static void Initialize(SklepContext Context)
        {
            Context.Database.EnsureCreated();
            InitializeProducts(Context);
            InitializeLogin(Context);
            InitializeCart(Context);
            InitializeZamowienia(Context);
      }

       
        private static void InitializeProducts(SklepContext Context)
        {
            if (Context.Products.Any())
            {
                return;
            }

            var products = new Products[]
            {
                new Products{Nazwa = "Haribo",      KodProduktu = "S1" , Ilosc = 10, Cena = 5 , Kategoria = "Slodycze"},
                new Products{Nazwa = "Marchewa",      KodProduktu = "W1" , Ilosc = 10, Cena = 5 , Kategoria = "Warzywa"},
                new Products{Nazwa = "Mleko",      KodProduktu = "N1" , Ilosc = 1, Cena = 5 , Kategoria = "Nabial"},
                new Products{Nazwa = "Jablko",      KodProduktu = "O1" , Ilosc = 10, Cena = 1, Kategoria = "Owoce" }
                
            };
            foreach (Products c in products)
            {
                Context.Products.Add(c);
            }
            Context.SaveChanges();
        }
        private static void InitializeLogin(SklepContext Context)
        {
            if (Context.Login.Any())
            {
                return;
            }

            var login = new Login[]
            {
                new Login{UserName = "admin",      Password = "1234" , Admin= 1},
                new Login{UserName = "pawel",      Password = "1234", Admin= 1}


            };
            foreach (Login c in login)
            {
                Context.Login.Add(c);
            }
            Context.SaveChanges();
            
        }
        private static void InitializeCart(SklepContext Context)
        {
            if (Context.Koszyk.Any())
            {
                return;
            }

            var koszyk = new KoszykDoBazy[]
            {
                new KoszykDoBazy{ProductId = 5 , Ilosc = 1, ZamowienieId= 1  }

            };
            foreach (KoszykDoBazy c in koszyk)
            {
                Context.Koszyk.Add(c);
            }
            Context.SaveChanges();
        }
        private static void InitializeZamowienia(SklepContext Context)
        {
            if (Context.Zamowienia.Any())
            {
                return;
            }

            var zamowienie = new Zamowienie[]
            {
                new Zamowienie{
                    UserId = 1,
                    Ulica = "test",
                    Data = DateTime.Now ,
                    KodPocztowy = "test",
                    Miejscowosc = "test",
                    NumerDomu = "test",
                    NumerMieszkania = "test" }


            };
            foreach (Zamowienie c in zamowienie)
            {
                Context.Zamowienia.Add(c);
            }
            Context.SaveChanges();
        }
    }

}