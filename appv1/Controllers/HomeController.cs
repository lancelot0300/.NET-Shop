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

        private readonly DziekanatContext bazaDanychDziekanatu;

        public HomeController(IObslugaBazyDanych obslugaBazyDanych, DziekanatContext bazaDanychDziekanatu)
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

        [HttpPost]
        public IActionResult DodajZajecia(Zajecia zajecia)
        {
            try
            {
                obslugaBazyDanych.DodajZajecia(zajecia);
                return View("DodanoZajecia", zajecia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{controller}/Students")]
        public IActionResult Students()
        {
            List<Student> students = bazaDanychDziekanatu.Students.ToList();
            return View(students);
        }
        [HttpGet]
        [Route("{controller}/PlanZajec")]
        public IActionResult PlanZajec()
        {
            return View(obslugaBazyDanych.GetCourses());
        }
        [HttpDelete]
        [Route("{controller}/UsunZajecia/{id}")]
        public IActionResult UsunZajecia(int id)
        {
            obslugaBazyDanych.UsunZajecia(id);

            return View("Zajecia", obslugaBazyDanych.GetCourses());
        }


        [HttpGet]
        [Route("{controller}/DodajStudenta")]
        public IActionResult DodajStudenta()
        {
            return View();
        }

        [HttpPost]
        [Route("{controller}/DodajStudenta")]
        public IActionResult DodajStudenta(Student student)
        {
            try
            {
                obslugaBazyDanych.DodajStudenta(student);
                return View("DodanoStudenta", student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{controller}/DodanoStudenta")]
        public IActionResult DodanoStudenta()
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