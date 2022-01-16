using Microsoft.AspNetCore.Mvc;
using appv1.DAL.Contexts;
using appv1.DAL.Models;
using appv1.Interfaces;
using Newtonsoft.Json;
using appv1.Models;

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
            Login login = HttpContext.Session.GetComplexData<Login>("user");
            if (login == null )
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
            Login login = HttpContext.Session.GetComplexData<Login>("user");
            if (login == null)
            {
                ViewBag.Message = "Zaloguj się aby dodać produkty";
                return View("Login");
            }
           else if (string.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
            {
                ViewBag.Message = "Brak produktów w koszyku";
                return View("Products");
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
        public IActionResult UsunProduct(int id)
        {

            obslugaBazyDanych.UsunProduct(id);

            return View("Products");


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

            Login user = obslugaBazyDanych.User(username, password);


            if (user != null)
            {
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetComplexData("user", user);

                return View("Index");
            }
            else 
            {
                ViewBag.Message = "Nieudało się zalogować";
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


        [HttpPost]
        public ActionResult Buy(int id)
        {

            Products product = obslugaBazyDanych.Find(id);
            int ilosc = obslugaBazyDanych.SprawdzIlosc(id);
            if ( ilosc == 0)
            {
                ViewBag.Message = "Brak produktu: " + product.Nazwa;
                return View("Index");
            }
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
                    if (cart[index].Ilosc > ilosc)
                    {
                        ViewBag.Message = "Nie można dodać tak dużej ilośći " + product.Nazwa +", maksymalnie: " + ilosc;
                        return View("Koszyk");
                    }
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

       


        [HttpGet]
        public IActionResult DodajProdukt()
        {
            Login login = HttpContext.Session.GetComplexData<Login>("user"); 
            if (login == null)
            {
                ViewBag.Message = "Musisz się zalogować";
                return View("Login");
            }
            else
            {
                if (login.Admin == 0)
                {
                    ViewBag.Message = "Musisz być administratorem";
                    return View("Index");
                }
                return View();
            }

        }
        [HttpPost]
        public IActionResult DodajProdukt(Products products)
        {
            try
            {
                if (products.Cena.ToString().Contains(".")) throw new Exception();

                obslugaBazyDanych.DodajProduct(products);
                return View("Products", obslugaBazyDanych.GetProducts());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Zamowienie()
            {
               return View();
            }


        [HttpPost]
        public IActionResult Zamowienie(Zamowienie zamowienie, List<Koszyk> koszyk)

        {
            koszyk = HttpContext.Session.GetComplexData<List<Koszyk>>("cart");
            obslugaBazyDanych.DodajZamowienie(zamowienie, koszyk);

            HttpContext.Session.Remove("cart");

            return View("Index");
           
        }
      
    }
}

