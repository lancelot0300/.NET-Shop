using Microsoft.AspNetCore.Mvc;
using appv1.DAL.Contexts;
using appv1.DAL.Models;
using appv1.Interfaces;
using Newtonsoft.Json;

namespace appv1.Controllers
{
    public class KategorieController : Controller
    {
        private readonly IObslugaBazyDanych obslugaBazyDanych;

        private readonly SklepContext bazaDanych;



        public KategorieController(IObslugaBazyDanych obslugaBazyDanych, SklepContext bazaDanych)
        {
            this.obslugaBazyDanych = obslugaBazyDanych;
            this.bazaDanych = bazaDanych;

            obslugaBazyDanych.Context = bazaDanych;

        }
        public IActionResult Warzywa()
        {
            return View();
        }
        public IActionResult Owoce()
        {
            return View();
        }
        public IActionResult Nabial()
        {
            return View();
        }
        public IActionResult Slodycze()
        {
            return View();
        }
    }
}