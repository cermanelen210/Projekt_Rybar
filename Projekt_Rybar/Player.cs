using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Rybar
{
    internal class Player
    {
        public int coins { get; set; } = 0;
        public List<Fish> inventory { get; set; } = new();
        public Dictionary<string, string> catalog { get; set; } = new();

        
        public Player()
        {
            
        }

    }
}
