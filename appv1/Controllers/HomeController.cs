using Microsoft.AspNetCore.Mvc;
using appv1.DAL.Contexts;
using appv1.DAL.Models;
using appv1.Interfaces;
using Newtonsoft.Json;

namespace appv1.Controllers
{
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
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

        public IActionResult Koszyk()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
            {
                ViewBag.Error = "Brak produktów w koszyku";
                return RedirectToAction("Products");
            }
            else
            {
                return View();
            }
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
            bool admin = false;
            foreach (var user in users)
            {
                if (user.UserName == username && user.Password == password)
                {
                    zdany = true;
                    ;
                    if (user.Admin == 1)
                    {
                        admin = true;
                    }
                }

            }

            if (zdany == true)
            {
                if (admin == true)
                {
                    HttpContext.Session.SetString("admin", "admin");
                }
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

            var users = obslugaBazyDanych.GetUsers();
            foreach (var obiekt in users)
            {
                if (user.UserName == obiekt.UserName)
                {
                    ViewBag.error = "Użytkownij już istnieje";
                    return View();
                }
               
            }
            obslugaBazyDanych.Zarejestruj(user);
            ViewBag.error = "Udało się";
            return View("Login", user);
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
        [HttpPost]
        public ActionResult Buy(int id)
        {

            Products product = obslugaBazyDanych.Find(id);
            if (HttpContext.Session.GetString("cart") == null)
            {
                
                List<Koszyk> cart = new List<Koszyk>
                {
                    new Koszyk { Product = product, Ilosc = 1 }
                };
                HttpContext.Session.SetComplexData("cart", cart);
                return RedirectToAction("Koszyk");
            }
            else
            {
                
                    List<Koszyk> cart = HttpContext.Session.GetComplexData<List<Koszyk>>("cart");
                    int index = isExist(id);
                    if (index != -1)
                    {
                        cart[index].Ilosc++;
                    }
                    else
                    {
                        cart.Add(new Koszyk { Product = product, Ilosc = 1 });
                    }
                    HttpContext.Session.SetComplexData("cart", cart);
                
            }
            return RedirectToAction("Koszyk");


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
            Products product = obslugaBazyDanych.Find(id);
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

