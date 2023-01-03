using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk
{
    public class Food
    {
        public Category category { get; set; }
        public string imagePath { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public string media { get; set; }

        public int count { get; set; }

        private static List<Food> instance;
        public static List<Food> GetInstance()
        {
            if (instance == null)
                instance = new List<Food>();

            return instance;
            
        }

    }
}
