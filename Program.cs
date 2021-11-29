//pokud zbyde cas zahashuj heslo
using cernejJack.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace cernejJack
{
    class Program
    {
        public class Data
        {
            public string Player { get; set; }
            public string Pass { get; set; }
            public int Chips { get; set; }
            
        }
        static void Main(string[] args)
        {
            string path = "T:\\docroot\\c#\\cernejJack\\players.json";
            string menuAction = "";
            bool menuBool = true;
            bool gameBool = true;
            bool loginBool = true;
            string[] menuActions = { "h","p","c","n"};
            //uvodni obrazovka
            //welcomeBJ();

        
            

            
            
            //Console.WriteLine(File.ReadAllText("players.json"));
            //Console.WriteLine(Directory.GetCurrentDirectory());

            while (loginBool)
            {
                header();
                if (false)//players json je prazdny
                {
                    while (true)
                    {//overuj nejaky kraviny
                        string nick;
                        string pass;
                        Console.WriteLine("Registrace");
                        Console.WriteLine("uzivatelske jmeno: ");
                        nick = Console.ReadLine();
                        Console.WriteLine("heslo: ");
                        pass = Console.ReadLine();
                        List<Data> _data = new List<Data>();
                        _data.Add(new Data()
                        {
                            Player = nick,
                            Chips = 100,
                            Pass = pass
                        });
                        string json = System.Text.Json.JsonSerializer.Serialize(_data);
                        File.WriteAllText(path, json);//musim pozdeji zmenit na relativni cestu
                        Console.WriteLine(File.ReadAllText(path));


                    }
                }
                else
                {
                    var _data = JsonConvert.DeserializeObject<List<Data>>(path);
                    _data.Add(new Data() {
                        Player = "adsf",
                        Chips = 100,
                        Pass = "heslo"
                    });
                    var convertedJson = JsonConvert.SerializeObject(_data, Formatting.Indented);
                    Console.WriteLine(File.ReadAllText(path));

                }
            }

            //menu
            while (gameBool)
            {
                bool error = false;
                
                while (menuBool)
                {
                    Console.Clear();

                    menu();
                    if (error)
                    {
                        Console.WriteLine("neexistujici prikaz");
                        error = false;
                    }
                    menuAction = Console.ReadLine().ToLower();
                    if (menuActions.Contains(menuAction))
                    {
                        break;
                    }
                    else
                    {
                        error = true;
                    }

                    
                }
                
            
            

                if (menuAction == "h")
                {
                    Console.Clear();
                    header();
                    Deck deck = new Deck(); 
                    Player player = new Player();
                    Dealer dealer = new Dealer(deck, player);
            
                    deck.createDeck();
                    deck.shuffleDeck();

                    while (true)
                    {
                        dealer.gameOverBool = true;

                        //nahrad UI //co to dat do dealera???
                        dealer.bet();

                        dealer.giveCardTo(dealer, 2); //dealer da 2 karty sobe

                        dealer.giveCardTo(player, 2); //dealer da 2 karty hraci

                        Console.WriteLine("dealer");
                        Console.WriteLine(dealer.cards[0].value);//dealer ukaze jednu svoji kartu


                        player.writeCards(); //hrac ukaze karty
                        //Console.WriteLine(player.sumCards());

                        dealer.playersActions();
                        dealer.dealersMove();
                        dealer.evaluate();
                        //Console.WriteLine("hit, stand"); //nahrad UI

                        Console.WriteLine("pro navrat do menu stiskni 'm'");
                       
                        if (Console.ReadKey().Key == ConsoleKey.M)
                        {
                            break;
                        }
                        
                        dealer.destroyCards(player);
                        dealer.destroyCards(dealer);
                    }//hlavni while
                } 
                else if(menuAction == "p")
                {
                    Console.Clear();
                    rules();
                }
                else if (menuAction == "c")
                {
                    Console.Clear();
                    header();
                    Console.WriteLine("vyrobeno mnou");
                    Console.ReadKey();
                }
                else if (menuAction == "n")
                {
                    Console.Clear();
                    header();
                    Console.WriteLine("neexistujou");
                    Console.ReadKey();
                }
                else if (menuAction == "u")
                {
                    break;
                }
            }





        }
        static void welcomeBJ()
        {
            //string txt = File.WriteAllText("Graphics/welcomeBJ().txt");
            //vypis veci ze souboru
            //Console.WriteLine(txt);
            System.Threading.Thread.Sleep(1000);
            Console.Clear();

            Console.WriteLine(@"
BLACK JACK ♦♣♠♥
Vítej ve hře Black Jack pro pokračování stiskni jakoukoli klávesu.
");
            Console.ReadKey();
            Console.Clear();

        }
        
        static void header()
        {
            Console.WriteLine("    BLACK JACK ♦♣♠♥");
        }
        static void menu()
        {

            header();
            Console.WriteLine("Menu");
            Console.WriteLine("-\x1b[1mH\x1b[0mrát");
            Console.WriteLine("-\x1b[1mP\x1b[0mravidla (pro otevření stiskni 'P')");
            Console.WriteLine("-\x1b[1mC\x1b[0mredits");
            Console.WriteLine("-\x1b[1mN\x1b[0mejlepší hráči");
            
            
            
        }
        static void rules()
        {
            Console.Clear();
            header();
            Console.WriteLine(@"
-Pro otevření položky napiš zvýrazněné písmeno
-Pravidla hry si najdi na wiki
");
            Console.ReadLine();
        }


    }//class program
}
