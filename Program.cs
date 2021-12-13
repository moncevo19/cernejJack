//pokud zbyde cas zahashuj heslo
using cernejJack.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


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
            string[] menuActions = { "h", "p", "c", "n" };
            string playersName = "";
            Deck deck = new Deck();
            Player player = new Player();
            Dealer dealer = new Dealer(deck, player);
            //uvodni obrazovka
            //welcomeBJ();


            void loading(string slovo, int time)
            {
                for (int i = 0; i < slovo.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        Console.Write(slovo[j]);

                    }
                    Thread.Sleep(time);
                    Console.Clear();

                }
                Console.WriteLine(slovo);
                Console.ReadKey();
            }
            //loading("BLACK JACK",250);
            welcome();
           
            while (loginBool)
            {
            StartOfLogin:
                Console.Clear();
                bool _loginBool = false;
                bool registerBool = false;
                header();
                Console.WriteLine("\x1b[1mP\x1b[0mrihlasit/\x1b[1mR\x1b[0megistrovat");
                var tmp = Console.ReadKey().Key;
                if (tmp == ConsoleKey.P)
                {
                    _loginBool = true;
                }
                else if (tmp == ConsoleKey.R)
                {
                    registerBool = true;
                }
                else
                {
                    continue;
                }
                Console.Clear();
                header();


                if (registerBool)
                {
                    while (true)
                    {
                    MultipleUsers:
                        Console.Clear();
                        header();
                        //overuj nejaky kraviny
                        string nick;
                        string pass;
                        Console.WriteLine("Registrace");
                        Console.WriteLine("uzivatelske jmeno: ");
                        nick = Console.ReadLine();
                        if (nick.ToLower() == "z")
                        {
                            goto StartOfLogin;
                        }
                        string[] usersArrayLines = System.IO.File.ReadAllLines(csvSoubor);
                        for (int i = 0; i < usersArrayLines.Length; i++) //uloz si csv do pole podle radku
                        {
                            string[] uzivatel = usersArrayLines[i].Split(";"); //ulozi do pole uzivatel i-tou radku
                            if (nick == uzivatel[0])
                            {
                                Console.WriteLine("jmeno uz existuje");
                                System.Threading.Thread.Sleep(1000);
                                goto MultipleUsers;
                            }
                        }
                        Console.WriteLine("heslo: ");
                        pass = Console.ReadLine();
                        if (pass.ToLower() == "z")
                        {
                            goto StartOfLogin;
                        }
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvSoubor, true))
                        {
                            file.WriteLine(nick + ";" + pass + ";" + 100);
                        }
                        player.nick = nick;
                        player.pass = pass;
                        player.chips = 100;
                        break;


                    }
                }
                else
                {
                    wopopop:
                    bool wopopop = true;
                    Console.Clear();
                    header();
                    Console.WriteLine("Prihlaseni");
                    Console.WriteLine("Jmeno:");
                    string nick = Console.ReadLine();
                    if (nick.ToLower() == "z")
                    {
                        goto StartOfLogin;
                    }
                    string[] usersArrayLines = System.IO.File.ReadAllLines(csvSoubor);
                    
                    for (int i = 0; i < usersArrayLines.Length; i++) //uloz si csv do pole podle radku
                    {
                        string[] uzivatel = usersArrayLines[i].Split(";"); //ulozi do pole uzivatel i-tou radku
                        if (nick == uzivatel[0])
                        {

                            while (true)
                            {
                                Console.Clear();
                                header();
                                Console.WriteLine("Prihlaseni");
                                Console.WriteLine("Jmeno:");
                                Console.WriteLine(nick);
                                Console.WriteLine("heslo:");
                                string pass = Console.ReadLine();
                                if (pass.ToLower() == "z")
                                {
                                    goto StartOfLogin;
                                }
                                if (pass == uzivatel[1])
                                {
                                    Console.WriteLine("uspesne prihlaseno");
                                    Thread.Sleep(250);
                                    player.chips = Int32.Parse(uzivatel[2]);
                                    player.nick = nick;
                                    player.pass = pass;
                                    wopopop = false;
                                    break;
                                }
                            }


                        }

                    }
                    if (wopopop)
                    {
                        goto wopopop;
                    }



                    /*using (TextWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine("{0},{1},{2}", "ahoj", 1, "adf");
                    }*/

                    break;

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
                    Console.WriteLine(playersName);
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
                    dealer.chipsUpdate();
                    Console.Clear();
                    header();


                    deck.createDeck();
                    deck.shuffleDeck();

                    while (true)
                    {
                        dealer.gameOverBool = true;

                        //nahrad UI //co to dat do dealera???
                        dealer.bet();

                        dealer.giveCardTo(dealer, 2); //dealer da 2 karty sobe

                        dealer.giveCardTo(player, 2); //dealer da 2 karty hraci

                        Console.WriteLine("dealerovy karty: ");
                        //Console.WriteLine(dealer.cards[0].value);//dealer ukaze jednu svoji kartu
                        dealer.writeFirstCard();



                            player.writeCards("tvoje karty: "); //hrac ukaze karty
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
                else if (menuAction == "p")
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
                    Console.WriteLine("nejlepsi hraci: ");
                    
                    string[] usersArrayLines = System.IO.File.ReadAllLines(csvSoubor);//uloz si csv do pole podle radku 
                    List<string[]> best = new List<string[]>();

                    foreach (string item in usersArrayLines)
                    {
                        best.Add(item.Split(";"));
                    }
                    var bestUsers = best.OrderBy(x => x[2]);

                    int i = 0;
                    foreach (var item in bestUsers)
                    {
                        i++;
                        Console.WriteLine((i) + ". " + item[0] + " - " + item[2]);
                        
                    }
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
        static void welcome()
        {
            Console.WriteLine(@"﻿
  ____  _            _           _            _    
 |  _ \| |          | |         | |          | |   
 | |_) | | __ _  ___| | __      | | __ _  ___| | __
 |  _ <| |/ _` |/ __| |/ /  _   | |/ _` |/ __| |/ /
 | |_) | | (_| | (__|   <  | |__| | (_| | (__|   < 
 |____/|_|\__,_|\___|_|\_\  \____/ \__,_|\___|_|\_\");
            Thread.Sleep(1500);
        }

        


    }//class program
}