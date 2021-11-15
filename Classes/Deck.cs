using System;
using System.Collections.Generic;
using System.Text;

namespace cernejJack.Classes
{
    class Deck
    {
        public List<Card> cards;
        public string ahoj = "ahoj";
        public Deck()
        {

        }

        public void createDeck()
        {
            this.ahoj = "adsf";
            for (int i = 1; i <= 52; i++)
            {
               // this.cards[i] = new Card(i%13*4);
            }
        }
    }
}
