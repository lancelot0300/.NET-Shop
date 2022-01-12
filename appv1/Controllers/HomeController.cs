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

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                ViewBag.Error = "Zaloguj się aby wejść na Stronę główną";
                return View("Login");
            }

            else
            {
                ViewBag.Message = "Jesteś Zalogowany";
                return View();
            }

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

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                ViewBag.Message = "Jesteś Zalogowany";
                return View("Index");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {

            var users = obslugaBazyDanych.GetUsers();
            bool zdany = false;
            foreach (var user in users)
            {
                if (user.UserName == username && user.Password == password)
                {
                    zdany = true;
                }

            }

            if (zdany == true)
            {
                HttpContext.Session.SetString("username", username);
                return View("Index");
            }
            else
            {
                ViewBag.error = "Zły login lub hasło";
                return View("Login");

            }



        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Login user)
        {
            try
            {
                var users = obslugaBazyDanych.GetUsers();  
                foreach ( var obiekt in users)
                {
                    if (user.UserName == obiekt.UserName)
                    {
                        ViewBag.error = "Użytkownij już istnieje";
                        return View();
                    }
                    else
                    {
                        obslugaBazyDanych.Zarejestruj(user); 
                    }
                }
                return View("Login", user);

            }
            catch (Exception ex)
            {
                return View("Index");
            }


        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");

        }

        [HttpGet]
        public IActionResult DodajProdukt()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                ViewBag.Error = "Musisz się zalogować";
                return View("Login");
            }
            else
            {
                return View();
            }
           
        }

        [HttpPost]
        public IActionResult DodajProdukt(Products products)
        {
            try
            {
                obslugaBazyDanych.DodajProduct(products);
                return View("Products", obslugaBazyDanych.GetProducts());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}