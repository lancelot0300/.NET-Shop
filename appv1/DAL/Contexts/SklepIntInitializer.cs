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
        }

       
        private static void InitializeProducts(SklepContext Context)
        {
            if (Context.Products.Any())
            {
                return;
            }

            var products = new Products[]
            {
                new Products{Nazwa = "Perogi",      KodProduktu = "P1" , Ilosc = 10, Cena = 5},
                new Products{Nazwa = "Fasolka",      KodProduktu = "F1" , Ilosc = 10, Cena = 1 }
                
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
                new Login{UserName = "admin",      Password = "1234"}
              

            };
            foreach (Login c in login)
            {
                Context.Login.Add(c);
            }
            Context.SaveChanges();
            
        }
    }

}