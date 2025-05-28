using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarPorjesi.Models;

namespace CarPorjesi.Controllers
{
    public class AdminController : Controller
    {
        ArabaProjeEntities2 db = new ArabaProjeEntities2();


        // Anasayfa
        public ActionResult Index()
        {
            var araba = db.Cars.ToList();
            return View(araba);
        }

        // GET: Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(Cars car, HttpPostedFileBase photo)
        {
            if (photo != null && photo.ContentLength > 0) // Eğer fotoğraf varsa
            {
                // Fotoğrafı kaydetmek için dosya adı ve yolu
                string fileName = Path.GetFileName(photo.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/assets/"), fileName);

                // Fotoğrafı kaydet
                photo.SaveAs(filePath);

                // Fotoğrafın yolunu veritabanına kaydet
                car.PhotoPath = "/Content/assets/" + fileName;
            }
            else
            {
                car.PhotoPath = "Fotoğraf Yok"; // Fotoğraf yoksa bunu kaydedelim
            }

            // Veritabanına kaydetme
            db.Cars.Add(car);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var araba = db.Cars.Find(id);
            return View(araba);
        }
        [HttpPost]
        public ActionResult Edit(Cars car, HttpPostedFileBase photo)
        {
            var mevcutAraba = db.Cars.Find(car.ID);
            if (mevcutAraba == null)
            {
                return HttpNotFound();
            }

            // Alanları güncelle
            mevcutAraba.Marka = car.Marka;
            mevcutAraba.Model = car.Model;
            mevcutAraba.Yıl = car.Yıl;
            mevcutAraba.Fiyat = car.Fiyat;
            mevcutAraba.Plaka = car.Plaka;
            mevcutAraba.Renk = car.Renk;
            mevcutAraba.km = car.km;
            mevcutAraba.YakıtTipi = car.YakıtTipi;
            mevcutAraba.VitesTipi = car.VitesTipi;
            mevcutAraba.Durum = car.Durum;

            // Fotoğraf güncellemesi
            if (photo != null && photo.ContentLength > 0)
            {
                string fileName = Path.GetFileName(photo.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/assets/"), fileName);
                photo.SaveAs(path);
                mevcutAraba.PhotoPath = "/Content/assets/" + fileName;
            }

            db.SaveChanges(); // Güncellemeyi veritabanına kaydet
            return RedirectToAction("Index"); // Listeye dön
        }


        public ActionResult Araçsil(int id)
        {
            var Araç = db.Cars.Find(id);
            db.Cars.Remove(Araç);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Diğer aksiyonlar (About, Contact vb.)
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // Giriş işlemi (POST)
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Kullanıcı adı ve şifre kontrolü
            if (username == "Admin" && password == "1234")
            {
                // Giriş başarılı, Admin Paneline yönlendir
                return RedirectToAction("Index"); // Admin paneline yönlendir
            }
            else
            {
                // Hatalı giriş, kullanıcıya hata mesajı göster
                ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
                return View(); // Login sayfasına geri dön
            }
        }

    }
}
