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

        private readonly SklepContext bazaDanychDziekanatu;

        public HomeController(IObslugaBazyDanych obslugaBazyDanych, SklepContext bazaDanychDziekanatu)
        {
            this.obslugaBazyDanych = obslugaBazyDanych;
            this.bazaDanychDziekanatu = bazaDanychDziekanatu;

            obslugaBazyDanych.Context = bazaDanychDziekanatu;
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
        public IActionResult DodajZajecia()
        {
            return View();
        }

        [HttpGet]
        [Route("{controller}/Products")]
        public IActionResult Products()
        {
            return View(obslugaBazyDanych.GetProducts());
        }


        [HttpGet]
        [Route("{controller}/DodajStudenta")]
        public IActionResult DodajStudenta()
        {
            return View();
        }

       

        [Route("{controller}/v1/Echo/{message}")]
        [HttpGet]
        public IActionResult Echo(string message = null)
        {
            return Ok(message);
        }

    }
}