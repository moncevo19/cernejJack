using cernejJack.Classes;
using System;
using System.IO;

namespace cernejJack
{
    class Program
    {
        static void Main(string[] args)
        {

            Deck deck = new Deck(); 
            Player player = new Player();
            Dealer dealer = new Dealer(deck, player);

            deck.createDeck();

            while (true)
            {
                deck.shuffleDeck();

                dealer.giveCardTo(dealer, 2); //dealer da 2 karty sobe

                dealer.giveCardTo(player, 2); //dealer da 2 karty hraci

                Console.WriteLine("dealer:");
                Console.WriteLine(dealer.cards[0].value);//dealer ukaze jednu svoji kartu


                Console.WriteLine("hrac:");
                player.writeCards(); //hrac ukaze karty
                Console.WriteLine(player.sumCards());
                Console.WriteLine("kolik chces vsadit?"); //nahrad UI //co to dat do dealera???
                dealer.bet();
                //Console.WriteLine("hit, stand"); //nahrad UI

                

                Console.ReadKey();
                dealer.destroyCards(player);
                dealer.destroyCards(dealer);
            }//hlavni while

            


            //uvodni obrazovka
            welcomeBJ();
            

            //menu
            menu();




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
        
        static void menu()
        {
            Console.WriteLine("    BLACK JACK ♦♣♠♥");
            Console.WriteLine("Menu");
            Console.WriteLine("-\x1b[1mH\x1b[0mrát");
            Console.WriteLine("-\x1b[1mP\x1b[0mravidla (pro otevření stiskni 'P')");
            Console.WriteLine("-\x1b[1mC\x1b[0mredits");
            Console.WriteLine("-\x1b[1mN\x1b[0mejlepší hráči");
        }
        static void rules()
        {
            Console.WriteLine(@"
    BLACK JACK ♦♣♠♥
-Pro otevření položky napiš zvýrazněné písmeno
-
");
        }


    }//class program
}
