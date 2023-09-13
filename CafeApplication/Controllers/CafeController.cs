using CafeApplication.Models.Cafe;
using CafeApplication.Models.Cafe.Database;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using static System.Collections.Specialized.BitVector32;

namespace KafeUygulamasiMVC.Controllers
{
    public class CafeController : Controller
    {       

        private readonly IWebHostEnvironment _whe;//root klasörüne resim atmak için gerekli yapılardır
 
        public CafeController(IWebHostEnvironment whe)//root klasörüne resim atmak için gerekli yapılardır
        {
            _whe = whe;
        }




        public IActionResult HomePage()
        {
            List<Setting> homePage = SettingSeed.Setting.Where(x => x.GroupName == "Home Page").ToList();//burada çakma database oluşturarak (json yerine yapılabilir veri tutması için) sıcak içeceklere action ver demek istedik.
            ViewBag.HomePage = homePage;
            return View();
        }
        public IActionResult About()
        {
            List<Setting> about = SettingSeed.Setting.Where(x => x.GroupName == "About").ToList();//burada çakma database oluşturarak (json yerine yapılabilir veri tutması için) sıcak içeceklere action ver demek istedik.
            ViewBag.About = about;
            return View();
        }
        public IActionResult Contact()
        {

            return View();
        }
        public IActionResult HotDrinks()
        {
            //ViewBag.Product = ProductSeed.Products.Where(x => x.Type ==CafeApplication.Models.Cafe.Type.HotDrink);//burada çakma database oluşturarak (json yerine yapılabilir veri tutması için) sıcak içeceklere action ver demek istedik.
            List<Product> products = ProductSeed.Products.Where(x => x.Type == CafeApplication.Models.Cafe.Type.HotDrink).ToList();
            return View(products);
        }

        public IActionResult ColdDrinks()
        {
            //ViewBag.Product = ProductSeed.Products.Where(x => x.Type == CafeApplication.Models.Cafe.Type.ColdDrink);//burada çakma database oluşturarak (json yerine yapılabilir veri tutması için) sıcak içeceklere action ver demek istedik.
            List<Product> products = ProductSeed.Products.Where(x => x.Type == CafeApplication.Models.Cafe.Type.ColdDrink).ToList();
            return View(products);
        }

        public IActionResult Foods()
        {
            //ViewBag.Product = ProductSeed.Products.Where(x => x.Type == CafeApplication.Models.Cafe.Type.Food);//burada çakma database oluşturarak (json yerine yapılabilir veri tutması için) sıcak içeceklere action ver demek istedik.
            List<Product> products = ProductSeed.Products.Where(x => x.Type == CafeApplication.Models.Cafe.Type.Food).ToList();
            return View(products);
        }

        public IActionResult Desserts()
        {
            //ViewBag.Product = ProductSeed.Products.Where(x => x.Type == CafeApplication.Models.Cafe.Type.Dessert);//burada çakma database oluşturarak (json yerine yapılabilir veri tutması için) sıcak içeceklere action ver demek istedik.
            List<Product> products = ProductSeed.Products.Where(x => x.Type == CafeApplication.Models.Cafe.Type.Dessert).ToList();
            return View(products);
        }


        public IActionResult ProductDetail(int? id)
        {
            if (id != null)//ürün bulunmazsa ana sayfada bu hatayı ver uyarısı için oluşturduk
            {
                Product product = ProductSeed.Products.FirstOrDefault(x => x.ID == id);//burada çakma database oluşturarak (json yerine yapılabilir veri tutması için) sıcak içeceklere action ver demek istedik.
                return View(product);
            }
            else
            {
                TempData["Error"] = "Ürün Bulunamadı!";
                return RedirectToAction("HomePage");

            }
        }




        //************************************************************************************************************




        /*---------------------------------------UPDATE İŞLEMİ---------------------------------------*/
        public IActionResult Product(int id)
        {
            Product product = ProductSeed.Products.FirstOrDefault(x => x.ID == id);

            return View(product);

        }

        public IActionResult ProductUpdate(Product product, int id)
        {
            ResimYukle(product, id);
            ProductSeed.Update(product);
            return SayfalaraYonlendir(product);
        }
        /*---------------------------------------UPDATE İŞLEMİ---------------------------------------*/




        //************************************************************************************************************




        /*---------------------------------------CREATE İŞLEMİ---------------------------------------*/
        public IActionResult ProductAdd()
        {          
            return View();
        }

        [HttpPost]  //AYNI ADDA YÖNLENDİRME İŞLEMİ YAPARKEN ACTİON ADINDAN SORUN OLMASIN DİYE BELİRTTİK
        public IActionResult ProductAdd(Product product, int id)
        {
            ResimYukle(product, id);
            ProductSeed.Create(product);
            return SayfalaraYonlendir(product);
        }
        /*---------------------------------------CREATE İŞLEMİ---------------------------------------*/



        //************************************************************************************************************


        /*---------------------------------------DELETE İŞLEMİ---------------------------------------*/

        public IActionResult ProductDelete(int id)
        {
            Product product = ProductSeed.Products.FirstOrDefault(x => x.ID == id);
            bool isDeleted=ProductSeed.Delete(product);

            if (!isDeleted)
            {
                TempData["Error"] = "Product was not deleted";
            };

            return SayfalaraYonlendir(product);
        }
        /*---------------------------------------DELETE İŞLEMİ---------------------------------------*/




        //************************************************************************************************************


        /*------------------------işlemlerden sonra ilgili sayfalara yönlendiren metottur.---------------------------------*/
        private IActionResult SayfalaraYonlendir(Product product)
        {
            switch (product.Type)
            {
                case CafeApplication.Models.Cafe.Type.HotDrink: return RedirectToAction("HotDrinks");
                case CafeApplication.Models.Cafe.Type.ColdDrink: return RedirectToAction("ColdDrinks");
                case CafeApplication.Models.Cafe.Type.Dessert: return RedirectToAction("Desserts");
                case CafeApplication.Models.Cafe.Type.Food: return RedirectToAction("Foods");

                default: return RedirectToAction("HomePage");
            }
        }/*------------------------işlemlerden sonra ilgili sayfalara yönlendiren metottur.---------------------------------*/



        //************************************************************************************************************



        /*---------------------------------------resim çekmek için yapılan metot---------------------------------------*/
        private void ResimYukle(Product product, int id)
        {
            var dosyaYolu = Path.Combine(_whe.WebRootPath, "images");//resim güncellemek için ayrıca bir images klasörü oluşturduk ve kodunu aşağıda oluşturduk. WebRootPath ifadesi Resimlerin olduğu wwwroot kalsörü yolunu göetirir.
            if (!Directory.Exists(dosyaYolu))
            {
                Directory.CreateDirectory(dosyaYolu);
            }

            if (product.File == null)
            {
                product.ImagePath = ProductSeed.Products.FirstOrDefault(x => x.ID == id).ImagePath;//eğer null hatası gelirse eski resmin dosya yolunu al.(bunu resim seçmeden güncelleme yapınca aldığımız hataya karşılık yaptık.)
            }
            else
            {
                var tamDosyaYolu = Path.Combine(dosyaYolu, product.File.FileName);
                using (var dosyaAkisi = new FileStream(tamDosyaYolu, FileMode.Create))
                {
                    product.File.CopyToAsync(dosyaAkisi);

                }

                /*product.ImagePath = @"\images\" + product.File.FileName;*/ /*burada eksik gelen resim yoluna @"\images\" ekleyerek birleştirmiş oluruz.*/
                product.ImagePath = Path.Combine(@"\images\", product.File.FileName);///yukarıdaki kodun ayınsı sadece Path.Combine ile iki veriyi birleştirmiş olduk.

            }/*---------------------------------------resim çekmek için yapılan metot---------------------------------------*/
        }

        //************************************************************************************************************




















    }





}
