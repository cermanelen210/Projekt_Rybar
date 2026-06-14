using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Rybar
{
    
    internal class Fish
    {
        public string rarita;
        public int cost;
        public string name;
        public int same;
        public static string[] allNames = { "Kapr", "Štika", "Okoun", "Jesater", "Sumec", "Pstruh", "Úhoř", "Lín", "Krokodýl" };

        private Random rnd = new Random();
        
        public Fish()
        {
            this.same = 1;
            switch (rnd.Next(1, 100))
            {
                case int n when n >= 1 && n <= 50:
                    this.rarita = "common";
                    this.name = allNames[rnd.Next(0, 3)];
                    this.cost = 200;
                    break;
                case int n when n > 50 && n <= 85:
                    this.rarita = "rare";
                    this.name = allNames[rnd.Next(3, 6)];
                    this.cost = 1200;
                    break;
                case int n when n > 86 && n <= 100:
                    this.rarita = "epic";
                    this.name = allNames[rnd.Next(6, 9)];
                    this.cost = 4000;
                    break;
                default:
                    this.rarita = "common";
                    this.name = allNames[rnd.Next(0, 3)];       //kdyby se neco pokazilo
                    this.cost = 200;
                    break;
            }
            
        }
       
       
        public void FishInfo() //vypise se u kazde ryby v inventari
        {
            Console.WriteLine($" {this.name} {this.same}x \n  {this.rarita} \n  cost:{this.cost} coins  "); //markdown - dokumentace
        }


    }
}
