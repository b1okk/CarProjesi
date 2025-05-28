using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarPorjesi.Models;

namespace CarPorjesi.Controllers
{
    public class HomeController : Controller
    {
        ArabaProjeEntities2 db = new ArabaProjeEntities2();
        public ActionResult Index()
        {
            var arabalar = db.Cars.ToList(); // Veritabanından tüm arabaları çek
            return View(arabalar); // View'e gönder
        }
        public ActionResult Hakımda()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult İletişim(string marka, string model, string fiyat)
        {
            ViewBag.Marka = marka;
            ViewBag.Model = model;
            ViewBag.Fiyat = fiyat;

            return View();
        }
    }
}