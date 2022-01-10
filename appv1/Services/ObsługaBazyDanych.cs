using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appv1.Interfaces;
using appv1.DAL.Models;
using appv1.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace appv1.Services
{
    public class ObslugaBazyDanych : IObslugaBazyDanych
    {
        public SklepContext Context { get; set; }

        public void DodajProduct(Products products)
        {
            if (products.KodProduktu == "")
                throw new ArgumentException("Kod Produktu musi być podany");


            Context.Products.Add(products);
            Context.SaveChanges();
        }


        public void UsunProduct(int id)
        {
            Products product = Context.Products.Find(id);
            Context.Products.Remove(product);
            Context.SaveChanges();
        }

        public List<Products> GetProducts()
        {
            List<Products> products = Context.Products.ToList();

            return products;
        }
    }
}