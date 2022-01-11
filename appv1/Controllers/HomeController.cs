using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appv1.DAL.Contexts;
using appv1.DAL.Models;
using appv1.Interfaces;

namespace appv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IObslugaBazyDanych obslugaBazyDanych;

        private readonly SklepContext bazaDanych;

        public HomeController(IObslugaBazyDanych obslugaBazyDanych, SklepContext bazaDanych)
        {
            this.obslugaBazyDanych = obslugaBazyDanych;
            this.bazaDanych = bazaDanych;

            obslugaBazyDanych.Context = bazaDanych;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

 

        [HttpGet]
        [Route("{controller}/Products")]
        public IActionResult Products()
        {
            return View(obslugaBazyDanych.GetProducts());
        }





        [Route("{controller}/v1/Echo/{message}")]
        [HttpGet]
        public IActionResult Echo(string message = null)
        {
            return Ok(message);
        }

        [HttpPost]
        [Route("{controller}/UsunProduct/{id}")]
        public IActionResult UsunProduct(int id)
        {
            obslugaBazyDanych.UsunProduct(id);

            return View("Products", obslugaBazyDanych.GetProducts());
        }

    }
}