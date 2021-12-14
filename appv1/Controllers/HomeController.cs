using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using appv1.Interfaces;
using appv1.DAL.Models;
using appv1.DAL.Contexts;
using System.Linq;

namespace appv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IObslugaBazyDanych obslugaBazyDanych;
        private readonly DziekanatContext bazaDanychDziekanatu;

        public HomeController(IObslugaBazyDanych obslugaBazyDanych, DziekanatContext bazaDanychDziekanatu)
        {
            this.obslugaBazyDanych = obslugaBazyDanych;
            this.bazaDanychDziekanatu = bazaDanychDziekanatu;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [HttpPost]
        [Route("{controller}/Dodaj/{nazwa}")]
        public IActionResult DodajDoPlanu(string nazwa = null)
        {
            if (nazwa == null) return BadRequest(new { komunikat = "Brak danych wejściowych" });

            string komunikat = obslugaBazyDanych.DodajZajeciaDoPlanu(nazwa);

            return Ok(new { komunikat = komunikat });
        }

        [HttpGet]
        [Route("{controller}/PlanZajec")]
        public IActionResult PlanZajec()
        {
            List<Zajecia> planZajec = bazaDanychDziekanatu.Zajecia.ToList();

            return View(planZajec);
        }
        [HttpGet]
        [Route("{controller}/Students")]
        public IActionResult Students()
        {
            List<Student> Students = bazaDanychDziekanatu.Students.ToList();

            return View(Students);
        }

    }
}
