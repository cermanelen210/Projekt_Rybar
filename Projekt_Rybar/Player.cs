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
        public int coins = 0; //cislo coinu za prodani ryby

        public List<Fish> inventory = new List<Fish>(); //inventar, kam se ulozej vsechny chycene dosud neporane ryby
        public List<string> catalog_ = new List<string>();
        public Dictionary<string, string> catalog = new Dictionary<string, string>();
        
        public Player()
        {
            
        }

    }
}
