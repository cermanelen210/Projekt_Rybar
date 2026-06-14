using Microsoft.Win32.SafeHandles;
using System.Numerics;

namespace Projekt_Rybar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            Player player = new Player();
            do
            {
                Console.Clear();
                TextMenu();
                char akce = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Clear();
                switch (akce)
                {
                    case 'R':
                        CatchFish(player);
                        break;
                    case 'I':
                        ShowInventory(player);
                        break;
                    case 'E':
                        (player.coins, player.inventory) = ProdatRyby(player.coins, player.inventory);
                        break;
                    case 'K':
                        Catalog(player.catalog);
                        break;
                    case 'S':
                        // ulozit a ukoncit
                        running = false;
                        break;
                    default:
                        Console.WriteLine("error");
                        Console.ReadLine();
                        break;
                }

            } while (running);           
        }
        
        static void Catalog(Dictionary<string, string> catalog)
        {
            Console.Clear();
            foreach (string fishName in Fish.allNames)
            {
                bool discovered = catalog.Keys.Contains(fishName);

                if (discovered)
                {
                    Fish fish = new Fish();

                    foreach (string fish_catalog in catalog.Keys)
                    {
                        if (fish_catalog == fishName)
                        {
                            fish.name = fish_catalog;
                            fish.rarita = fish_catalog;
                            break;
                        }
                    }
                    switch (fish.rarita)
                    {
                        case "common":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        case "rare":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;

                        case "epic":
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                    }

                    Console.WriteLine(fishName);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("?????");
                }

                Console.ResetColor();
            }
            bool running = true;
            Console.WriteLine("  M - Menu");
            do
            {
                char choise = Char.ToUpper(Console.ReadKey().KeyChar);
                if (choise == 'M')
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("error");
                }
            } while (running);
        }
        static (int, List<Fish>) ProdatRyby(int coins, List<Fish> inventory)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Vítej v rybárně\nZde můžeš prodat své ryby za coiny\n  Jakou Rybu/y chceš prodat? \n \n  M - Menu ");
            Console.WriteLine("------------------------------------------");
            bool rybu_nemas = true;
            do
            {
                Fish choosen_fish = new Fish();                 //vytvori se nova ryba
                string choice_fish = Console.ReadLine();        //uzivatel napise jmeno ryby jako chce prodat
                if (choice_fish.ToUpper() == "M")   //jesi napise m tak pude do menu
                {
                    return (coins, inventory);
                }

                for (int i = 0; i < inventory.Count; i++)       //projde se invenventar jesi nenajde rybu kterou uzivatel napsal
                {
                    if (choice_fish == inventory[i].name)
                    {
                        rybu_nemas = false;
                        choosen_fish = inventory[i];        
                    }
                    else { }
                }
               
                if (rybu_nemas)
                {
                    Console.WriteLine("Tuto rybu nemáš");
                    Console.ReadLine();
                    continue;

                }
                else if (choosen_fish.same == 1)
                {

                    Console.WriteLine("Opravdu chceš rybu prodat?\n  A - Ano\n  N - Ne");
                    bool wrong_key = false;
                    do
                    {
                        char choice = Char.ToUpper(Console.ReadKey().KeyChar);
                        switch (choice)
                        {
                            case 'A':
                                Console.Clear();
                                Console.WriteLine("Ryba prodána");
                                coins += choosen_fish.cost;
                                Console.WriteLine($"+ {choosen_fish.cost} coinu");
                                inventory.Remove(choosen_fish);
                                return (coins, inventory);
                            case 'N':
                                Console.ReadLine();
                                return(coins, inventory);
                            default:
                                Console.WriteLine("error");
                                wrong_key = true;
                                break;
                        }
                    } while (wrong_key);
                }else
                {
                    Console.WriteLine($"Kolik ryb stejného druhu chceš prodat?\n  (1 - {choosen_fish.same})");
                    int zad_cislo = 0;
                    while ((!int.TryParse(Console.ReadLine(),out zad_cislo))|| zad_cislo <1 ||zad_cislo > choosen_fish.same)
                    {
                        Console.WriteLine("error");
                    }
                    Console.Clear();
                    Console.WriteLine("Ryba prodána");
                    coins += (choosen_fish.cost*zad_cislo);
                    Console.WriteLine($"+ {choosen_fish.cost*zad_cislo} coinu");
                    choosen_fish.same -= zad_cislo;
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (choosen_fish.name == inventory[i].name && choosen_fish.same !=0)
                        {
                            inventory.Remove(inventory[i]);
                            inventory.Add(choosen_fish);
                        }
                        else if(choosen_fish.same == 0)
                        {
                            inventory.Remove(inventory[i]);
                        }
                       
                    }
                    return (coins, inventory);
                    
                }
                
            } while (!rybu_nemas);

            return (coins, inventory);
            
        }

        static void TextMenu()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Rybářský simulátor");
            Console.WriteLine("  R - Chytit rybu \n  I - Zobrazit inventář\n  K - Katalog\n  E - Prodat rybu\n  S - Uložit a ukončit");
            Console.WriteLine("-----------------------");
        }
        static Player CatchFish(Player player)
        {
            bool running = true;
            do
            {
                Fish newFish = new Fish();
                Console.Write("Chyril jsi: ");
                Console.WriteLine($"{newFish.name} \n            {newFish.rarita} \n            cost:{newFish.cost} coinu");

                bool found = false;
                foreach(Fish fish in player.inventory)
                {                    
                    if (newFish.name == fish.name)
                    {
                        found = true;
                        fish.same++;
                    }
                    else { }
                }
                if (!found)
                {
                    player.inventory.Add(newFish);
                    //player.catalog_.Add(newFish.name);
                    player.catalog.Add(newFish.name, newFish.rarita);
                }
                Console.WriteLine("-----------------");
                Console.WriteLine("  R - Chytit rybu \n  M - Menu");

                bool running2 = true;
                do
                {
                    char choice = Char.ToUpper(Console.ReadKey().KeyChar);
                    Console.Clear();
                    if (choice =='R')
                    {
                        running2 = false;
                    }
                    else if (choice == 'M')
                    {
                        running2 = false;
                        running = false;
                    }
                    else
                    {
                        Console.WriteLine("  R - Chytit rybu \n  M - Menu");
                        Console.WriteLine("error");
                    }
                } while (running2);    
            } while (running);
        return (player);
        }
        
        static void ShowInventory (Player player) 
        {
            Console.Clear();
            Console.WriteLine("--------------------------\n--------------------------");
            foreach (Fish fish in player.inventory)
            {
                
                switch (fish.rarita)
                {
                    case "common":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "rare":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "epic":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                }
                fish.FishInfo();
                Console.ResetColor();
                Console.WriteLine("--------------------------");
            }
            Console.WriteLine("--------------------------");
            Console.WriteLine("Na ucte mas: " + player.coins + " coinu");
            Console.WriteLine("--------------------------");
            DoMenu();
        }
        static void DoMenu()
        {
            bool running = true;
            Console.WriteLine("  M - Menu");
            do
            {
                char choise = Char.ToUpper(Console.ReadKey().KeyChar);
                if (choise == 'M')
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("error");
                }
            } while (running);
        }
    }
}
