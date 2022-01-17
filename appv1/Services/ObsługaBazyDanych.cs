using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appv1.Interfaces;
using appv1.DAL.Models;
using appv1.DAL.Contexts;
using Microsoft.AspNetCore.Http;
using appv1.Models;

namespace appv1.Services
{
    public class ObslugaBazyDanych : IObslugaBazyDanych
    {
        public SklepContext Context { get; set; }

        public void DodajProduct(Products product)
        {
            var products = GetProducts();

            if (product.KodProduktu == "")
                throw new ArgumentException("Kod Produktu musi być podany");
 

            Context.Products.Add(product);
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
        public List<KoszykDoBazy> GetKoszykZam(int id)
        {
            List <Zamowienie> zamowienia = Context.Zamowienia.ToList();
            List<KoszykDoBazy> koszyk = Context.Koszyk.ToList();
            var koszyk2 = new List<KoszykDoBazy>();
            foreach (Zamowienie z in zamowienia)
            {
                if(z.ID == id)
                {
                    foreach (KoszykDoBazy k in koszyk)
                    {
                        if (k.ZamowienieId == z.ID)
                        {
                            koszyk2.Add(k);
                        }
                    }
                }
            }
            return koszyk2;
        }
        public List<Products> GetDaneOPro(int id)
        {
            List <Products> products =  GetProducts();
            List<KoszykDoBazy> koszyk = GetKoszykZam( id);
            var list = new List<Products>();
            foreach (KoszykDoBazy k in koszyk)
            {
               foreach(Products p in products)
                {
                    if (p.ID == k.ProductId)
                    {
                        list.Add(p);
                    }
                }
            }
            return list;
        }

        public List<Products> GetKategory(string name)
        {
            List<Products> products = GetProducts();
            List<Products> newa = new List<Products>();
            foreach (Products p in products)
            {
                if (p.Kategoria == name)
                {
                    newa.Add(p);
                }
            }
            return newa;
        }



        public List<Login> GetUsers()
        {
            List<Login> users = Context.Login.ToList();

            return users;
        }

        public Login User(string username, string password)
        {
            List<Login> users = GetUsers();

            foreach (var user in users)
            {
                if (user.UserName == username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public void Zarejestruj(Login user)
        {

            Context.Login.Add(user);
            Context.SaveChanges();

        }

        public Products Find(int id)
        {
            Products product = Context.Products.Find(id);
            return product;
        }
        public int GetOrderId()
        {

            var Order = Context.Zamowienia.ToList();
            var lastOrder = Order.LastOrDefault();
            int OrderId = lastOrder.ID;
            return OrderId;
        }


        public void DodajZamowienie(Zamowienie zamowienie, List<Koszyk> koszyk)
        {
            try {
                foreach (var k in koszyk)
                {
                    KoszykDoBazy ko = new KoszykDoBazy();

                    ko.ProductId = k.Product.ID;
                    ko.ZamowienieId = GetOrderId() + 1;
                    ko.Ilosc = k.Ilosc;

                    UsunIlosc(k.Product.ID, k.Ilosc);

                    Context.Koszyk.Add(ko);
                    Context.SaveChanges();
                }

                Context.Zamowienia.Add(zamowienie);
                Context.SaveChanges();
            }
            catch
            {
                return;
            }
                 

             async void UsunIlosc(int id, int ilosc)
            {
                var product = Context.Products.Find(id);
                product.Ilosc = product.Ilosc - ilosc;
                Context.Products.Update(product);
            }
        }
        public int SprawdzIlosc(int id)
        {
            var product = Context.Products.Find(id);
            return product.Ilosc;
        }

    }
}