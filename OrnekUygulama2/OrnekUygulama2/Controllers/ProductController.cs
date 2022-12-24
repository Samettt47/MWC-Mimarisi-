   using Microsoft.AspNetCore.Mvc;
using OrnekUygulama2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrnekUygulama2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            //Product listesi oluşturma 
            var products = new List<Product>
            {
             new Product {Id = 1 , ProductName = "A Product" ,Quantity = 10 },
             new Product {Id = 1 , ProductName = "B Product" ,Quantity = 10 },
             new Product {Id = 1, ProductName = "C Product"  ,Quantity = 10}
            };
            #region Model bazlı Veri Gönderimi
            // Elimizde ki datayı view'e göndermek istersek hem controllerda
            // hem de view de ayarlamalar yapılmalıdır
            // view direkt return edilir 
            // Eğerki model bazlı çalışıyorsak view'e de @Model keywordu ile türü belirlenir

            //return View(products);

            #endregion

            #region Veri Taşıma Kontrolleri 
            #region ViewBag 
            // View'e gnderileccek bir datayı dinamic şekilde tanımlanan bir değişken ile
            // taşımamızı sağlayan bir veri taşıma kontroludur.

            ViewBag.products = products;

            #endregion


            #region View Data
            // ViewBag de olduğu gibi action'daki datayı view' e taşımamızı sağlayan bir
            // kontroldur.
            // VİEWData ilgili datayı BOXİNG olarak taşır bu yüzden view de unboxing edilmesi gerekir 
            // kullanımı bu şekildedir. 

            //ViewData["products"] = products;
            #endregion


            #region TempData
            // ViewData da olduğu gibi action'daki datayı view' e taşımamızı sağlayan bir
            // kontroldur.
            // TempData ilgili datayı BOXİNG olarak taşır bu yüzden view de unboxing edilmesi gerekir 
            // VİEWDATA İLE TEMPDATA ARASINDA Kİ FARK AŞAĞIDADIR
            // BİZ ACTİONLAR ARASINDA BİR VERİ TAŞIMA YAPMAK İÇİN VİEWDATAYI KULLANAMAYIZ
            // FAKAT TEMPDATA İLE BİR COOCKİE ÜZERİNDEN İLGİLİ VERİ TAŞINIR 

            //TempData["products"] = products;

            // PEKİ BU YONLENDİRME NASIL YAPILIR AŞAĞIYA YAZILAN METOTA BAK 
            // Products bir komplex veridir bu yüzden sterilize edilmesi gerekir bu yüzden 
            // Bu hatayı kütüphane kullanarak sterilize işlemi edilir

            var data = JsonSerializer.Serialize(products); // bu yukarıda ki  kodun sterilize edilmiş halidir
           
            TempData["products"] = data ; 

            //TempData["x"] = 5;
            //ViewBag.x = 5;
            //ViewData["x"] = 5;

            #endregion




            #endregion



            return RedirectToAction("Index2"); // model bazlı için sadece bunu çalıştırırsak view de hata alırız 
            // eğer farklı bir controllera geçiş yapmak istersek bu fonksyionun overloadı vardı
            // buraya yazarak ulaşabılırız.  
        }

        public IActionResult Index2()
        {
            //var v1 = ViewBag.x;
            //var v2 = ViewData["x"];
            //var v3 = TempData["x"];

            var data = TempData["products"].ToString();
            List<Product> products =  JsonSerializer.Deserialize<List<Product>>(data);


            return View();
        }



        #region Controllerda olmayan view de olan bir action'ı (görünümü ) gösterme

        //public IActionResult GetProducts()
        //{
        //    Product product = new Product();

        //    // ÖRNEĞİN VERİ ÜRETİLDİ ÜRETİLEN BU VERİ VİEW FONSKYİONU İLE ÇAĞRILIR VE
        //    // BU VERİ GÖRSELLEŞTİRİLEREK VERİ REPONSE EDİLİR 
        //    ViewResult result = View("ahmet");  // belirtilen view isminde ki view dosyasını render eder

        //    //return View();
        //    return result;

        //} 
        #endregion

        #region View Yapılanması ve View'e veri taşıma kontrolleri(ViewBag, ViewData , TempData)

        //public IActionResult Index()
        //{
        //    return View();
        //}
        //public IActionResult Index2()
        //{
        //    return View();
        //}
        //public IActionResult Index3()
        //{
        //    return View();
        //}

        #endregion
        
        #region ViewResult 
        // view render edilip sonuc view olarak usera(clienta)  gönderilir 

        //public ViewResult GetProduts()
        //{
        //    ViewResult result = View();
        //    return result;
        //}
        #endregion

        #region PartialViewResult 
        // Yine bir view Dosyasını(.cshtml ) render etmemizi sağlayan bir action türüdür.
        // client tabanlı ise PartialviewResult kullanılır .(belirli bir view içinde ki parçayı temsil
        // eden yer partialviewResulttur

        //public PartialViewResult GetProducts()
        //{
        //    PartialViewResult result = PartialView();
        //        return result;
        //}

        #endregion

        #region JsonResult
        //public JsonResult GetProducts()
        //{
        //    JsonResult result = Json(new Product
        //    {
        //        Id = 5,
        //        ProductName = "Samet",
        //        Quantity = 15
        //    });
        //    return result;
        //}
        #endregion

        #region EmptyResult 
        // BAZEN İSTEKLERİN KARŞILIĞINDA BOŞ BİR RESULT DÖNDÜREBİLİRİZ 
        // RESPONSE VAR AMA RESULT YOK 

        //public EmptyResult GetProducts()
        //{
        //    return new EmptyResult();

        //}

        #endregion

        #region ContentResult
        // client tabanlılar da tercih ederiz . 

        //public ContentResult GetProducts()
        //{
        //    ContentResult result = Content("Sebebsiz Boş yere ayrılacaksan");
        //    return result;
        //}
        #endregion

        #region ActionResult !!
        // Bütün result türlerinin base classıdır diyebilirizz 
        // Ortak bir tür sağlanır 
        //public ActionResult GetProducts()
        //{
        //    if (true)
        //    {
        //        //....
        //        return Json(new object());

        //    }
        //    else if (true)
        //    {
        //        return Content("saddasdsa");

        //    }
        //    return View(); ; 
        //}

        #endregion

        #region NonAction

        //public IActionResult Index()
        //{
        //    X();
        //    return View();
        //}
        //[NonAction] // CONTROLLER İÇERİSİNDE NONACTİON İLE İŞARETLENEN FONKSİYONLAR
        //            // DIŞARIDAN REQUEST KARSILAMAZLAR SADECE OPERATİF YANİİ ALGORİTMA BARINDIRAN
        //            // İS MANTIGI YÜRÜTEN METOTTUR
        //public void X()
        //{



        //}

        #endregion
    }
}
