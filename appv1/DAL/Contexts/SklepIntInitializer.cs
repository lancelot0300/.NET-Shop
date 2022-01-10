using appv1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appv1.DAL.Contexts
{
    public static class SklepIntInitializer
    {
        public static void Initialize(SklepContext Context)
        {
            Context.Database.EnsureCreated();
            InitializeProducts(Context);
        }

       
        private static void InitializeProducts(SklepContext Context)
        {
            if (Context.Products.Any())
            {
                return;
            }

            var products = new Products[]
            {
                new Products{Nazwa = "Perogi",      KodProduktu = "P1"},
                new Products{Nazwa = "Fasolka",      KodProduktu = "F1" }
                
            };
            foreach (Products c in products)
            {
                Context.Products.Add(c);
            }
            Context.SaveChanges();
        }
    }

}