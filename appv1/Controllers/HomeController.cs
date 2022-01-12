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
                return View("Login");
            }
            else
            {
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
            return View();
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
                return View("Success");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");

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
                obslugaBazyDanych.Zarejestruj(user);
                return View("Login", user);
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }
    }
}