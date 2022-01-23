using Microsoft.AspNetCore.Mvc;
using appv1.DAL.Contexts;
using appv1.DAL.Models;
using appv1.Interfaces;
using Newtonsoft.Json;
using appv1.Models;

namespace appv1.Controllers
{
    public class KategorieController : Controller
    {
        private readonly IObslugaBazyDanych _obslugaBazyDanych;



        public KategorieController(IObslugaBazyDanych obslugaBazyDanych)
        {
            _obslugaBazyDanych = obslugaBazyDanych;


        }
        public IActionResult Warzywa()
        {
            string name = ControllerContext.ActionDescriptor.ActionName;
            return View(_obslugaBazyDanych.GetKategory(name));
        }
        public IActionResult Owoce()
        {
            string name = ControllerContext.ActionDescriptor.ActionName;
            return View(_obslugaBazyDanych.GetKategory(name));
        }
        public IActionResult Nabial()
        {
            string name = ControllerContext.ActionDescriptor.ActionName;
            return View(_obslugaBazyDanych.GetKategory(name));
        }
        public IActionResult Slodycze()
        {
            string name = ControllerContext.ActionDescriptor.ActionName;
            return View(_obslugaBazyDanych.GetKategory(name));
        }


        private int isExist(int id)
        {
            List<Koszyk> cart = HttpContext.Session.GetComplexData<List<Koszyk>>("cart");
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ID.Equals(id))
                    return i;
            return -1;
        }
        public ActionResult UsunZKoszyka(int id)
        {
            Products product = _obslugaBazyDanych.Find(id);
            List<Koszyk> cart = HttpContext.Session.GetComplexData<List<Koszyk>>("cart");
            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Ilosc > 1)
                {
                    cart[index].Ilosc--;

                }
                else
                {
                    cart.RemoveAt(index);
                }

            }
            HttpContext.Session.SetComplexData("cart", cart);
            return RedirectToAction("Koszyk");
      
 
        }

    }
}