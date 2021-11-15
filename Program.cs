using cernejJack.Classes;
using System;
using System.IO;

namespace cernejJack
{
    class Program
    {
        static void Main(string[] args)
        {
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
