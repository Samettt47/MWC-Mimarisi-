using Microsoft.AspNetCore.Mvc;
using OrnekUygulama2.Models;
using OrnekUygulama2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            Product product = new Product
            {
                Id = 1,
                ProductName = "A Product",
                Quantity = 15
            };
            User user = new User()
            {
                Id = 1,
                Name = "Samet",
                LastName = "Bayraktar"
            };

            #region ViewModel ile View'e Tuple  göndermek 
            // ViewModel ile view'e göndermek iiçin model katmanında bir klasor acarız
            // bu klasor ıcıne propertyler ekleriz 
            // bu propertyler User ve Product nesnelerinden değişken üretilir.
            // yani şunlar yazılı --> Public Product product {get ; set ; }
            // ve --> public User user {get ; set ; } yazılır 

            //UserProduct userproduct = new UserProduct() {
            //    User = user,
            //    Product = product
            //};


            #endregion

            //return View(userproduct);

            var userProduct = (product, user); // tuple oluşturularak view' gönderilmesi.

            ViewBag.product = userProduct;

            return View(userProduct);
        }
    }
}
