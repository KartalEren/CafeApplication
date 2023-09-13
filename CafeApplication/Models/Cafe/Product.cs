namespace CafeApplication.Models.Cafe
{
    public class Product:Base
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }//resmin dosyasını tutar. IFormFile: dosyaların içeriğini tutar.
    }
}
