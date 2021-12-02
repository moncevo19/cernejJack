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
        // public TextWriter sw = new StreamWriter("T:\\docroot\\c#\\cernejJack\\players.csv");
        /*  public class Data
        {
             public string Player { get; set; }
             public string Pass { get; set; }
             public int Chips { get; set; }

         }*/
        public static string csvSoubor = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "players.csv"); //relativni cesta
        static void Main(string[] args)
        {
            
        string path = "T:\\docroot\\c#\\cernejJack\\players.csv";
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
                if (true)//players json je prazdny
                {
                    while (true)
                    {
                        Console.Clear();
                        header();
                    //overuj nejaky kraviny
                        string nick;
                        string pass;
                        Console.WriteLine("Registrace");
                        Console.WriteLine("uzivatelske jmeno: ");
                        nick = Console.ReadLine();

                        string[] usersArrayLines = System.IO.File.ReadAllLines(csvSoubor);
                        for (int i = 0; i < usersArrayLines.Length; i++) //uloz si csv do pole podle radku
                        {
                            string[] uzivatel = usersArrayLines[i].Split(";"); //ulozi do pole uzivatel i-tou radku
                            if (nick == uzivatel[0])
                            {
                                Console.WriteLine("jmeno uz existuje");
                                continue;
                            }
                        }
                        Console.WriteLine("heslo: ");
                        pass = Console.ReadLine();

                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvSoubor, true))
                        {
                            file.WriteLine(nick + ";" + pass + ";" + 100);
                        }
                        //break;
                        /*List<Data> _data = new List<Data>();
                        _data.Add(new Data()
                        {
                            Player = nick,
                            Chips = 100,
                            Pass = pass
                        });
                        string json = System.Text.Json.JsonSerializer.Serialize(_data);
                        File.WriteAllText(path, json);//musim pozdeji zmenit na relativni cestu
                        Console.WriteLine(File.ReadAllText(path));*/


                    }
                }
                else
                {
                    Console.WriteLine("Prihlaseni");
                    string nick = Console.ReadLine();

                    Console.WriteLine("Heslo");
                    string heslo = Console.ReadLine();
                    /*using (TextWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine("{0},{1},{2}", "ahoj", 1, "adf");
                    }*/
                    break;
                    /* var _data = JsonConvert.DeserializeObject<List<Data>>(path);
                     _data.Add(new Data() {
                         Player = "adsf",
                         Chips = 100,
                         Pass = "heslo"
                     });
                     var convertedJson = JsonConvert.SerializeObject(_data, Formatting.Indented);
                     Console.WriteLine(File.ReadAllText(path));*/

                }
                break;
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
/* (chi pouzit jen funkce na praci se soubory (vzdal jsem to potom co jsem delal nekolik hodin v kuse json a nefungoval mi))
 * //public static string csvSoubor = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "users.csv"); //relativni cesta

//funkce na přidání uživatele do csv souboru
        public void pridatZaznam(string data1, int data2, string soubor)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(soubor, true))
                {
                    file.WriteLine(data1.ToLower() + ";" + data2.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nastal error: " + ex);
            }
        }
        //funkce na hledání uživatele
        internal string hledatZaznam(string hledanyVyraz, int pozice = 0)
        {
            try
            {
                string[] radky = System.IO.File.ReadAllLines(csvSoubor);
 
                for (int i = 0; i < radky.Length; i++)
                {
                    string[] pole = radky[i].Split(";");
                    if (zaznamJeStejny(hledanyVyraz.ToLower(), pole, pozice))
                    {
                        //Console.WriteLine("Nalezeno");
                        return pole[1];
                    }
                }
 
                return "Uživatel nebyl nalezen.";
            }
            catch (Exception ex)
            {
                return "Nastal error: " + ex;
            }
        }
        //funkce na ověření hledaného výrazu v souboru
        internal bool zaznamJeStejny(string hledanyVyraz, string[] zaznam, int pozice = 0)
        {
            if (zaznam[pozice].Equals(hledanyVyraz))
            {
                return true;
            }
            return false;
        }
        internal void upravitZaznam(int pozice = 0)
        {
            try
            {
                //nacist aktualni data
                string[] radky = System.IO.File.ReadAllLines(csvSoubor);
 
                //array pro nová data
                string[] data_temp = new string[radky.Length];
 
                //filtrace upravených a stávajících dat
                for (int i = 0; i < radky.Length; i++)
                {
                    string[] radek = radky[i].Split(";");
 
                    if (!(zaznamJeStejny(jmeno, radek)))
                    {
                        data_temp[i] = radek[0] + ";" + radek[1];
                    } else
                    {
                        data_temp[i] = jmeno + ";" + skore.ToString();
                    }
                }
 
                //zapsat upravená do souboru
                File.WriteAllLines(csvSoubor, data_temp);
 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nastal error: " + ex);
            }
        }
 * */