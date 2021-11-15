using cernejJack.Classes;
using System;

namespace cernejJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Deck deck = new Deck();
            deck.createDeck();

            Console.WriteLine(deck.cards);
        }
    }
}
