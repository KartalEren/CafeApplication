namespace CafeApplication.Models.Cafe.Database
{
    public class SettingSeed
    {

        private static List<Setting> _setting = new List<Setting>()
        {
            new Setting {ID=1,GroupName="Home Page",Value=@"\HomePage\AnaSlayt.png",Key="Ana Slayt",Description="",Link="/"},
            new Setting {ID=2,GroupName="Home Page",Value=@"\HomePage\Başlangıç.png",Key="Başlangıç",Description = "Başlangıç",Link="Cafe/Foods"},
            new Setting {ID=3,GroupName="Home Page",Value=@"\HomePage\Hamburger.png",Key="Hamburger",Description = "Hamburger",Link="Cafe/Foods"},
            new Setting {ID=4,GroupName="Home Page",Value=@"\HomePage\Kahvaltı.png",Key="Kahvaltı",Description = "Kahvaltı",Link="Cafe/Foods"},
            new Setting {ID=5,GroupName="Home Page",Value=@"\HomePage\Pizza.png",Key="Pizza",Description = "Pizza",Link="Cafe/Foods"},
            new Setting {ID=6,GroupName="Home Page",Value=@"\HomePage\Salata.png",Key="Salata",Description = "Salata",Link="Cafe/Foods"},
            new Setting {ID=7,GroupName="Home Page",Value=@"\HomePage\Sıcakİçecekler.jpg", Key = "Sıcak İçecekler",Description = "Sıcak İçecekler",Link="Cafe/Sıcak İçecekler"},
            new Setting {ID=8,GroupName="Home Page",Value=@"\HomePage\Soğukİçecekler.jpg",Key="Soğuk İçecekler",Description = "Soğuk İçecekler",Link="Cafe/Soğuk İçecekler"},
            new Setting {ID=9,GroupName="Home Page",Value=@"\HomePage\Tatlılar.jpg", Key = "Tatlılar",Description = "Tatlılar",Link="Cafe/Desserts"},
            new Setting {ID=10,GroupName="Home Page",Value=@"\HomePage\Yemekler.png", Key = "Yemekler",Description = "Yemekler",Link="Cafe/Foods"},



            new Setting {ID=10,GroupName="About",Value=@"\About\Mission.jpg", Key = "Mission",Description = "To create an environment where absolute guest satisfaction is our highest priority.",Link="/"},
            new Setting {ID=10,GroupName="About",Value=@"\About\Vision.jpg", Key = "Vision",Description = "Through a shared commitment to excellence, we are dedicated to the uncompromising quality of our food, service, people and profit, while taking exceptional care of our guests and staff.",Link="/"}
        };

        public static IEnumerable<Setting> Setting => _setting;

    }
}
