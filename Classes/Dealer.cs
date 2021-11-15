using System;
using System.Collections.Generic;
using System.Text;

namespace cernejJack.Classes
{
    class Dealer
    {
        public Deck deck;
        public Dealer(Deck _deck)
        {
            this.deck = _deck;
        }
        public void kouzlo()
        {
            Console.WriteLine(deck.ahoj);
        }
    }
}
