using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cernejJack.Classes
{
    class Deck
    {
        public List<Card> cards = new List<Card>();
        public Deck()
        {

        }

        public void createDeck()
        {
            for (int i = 1; i <= 52; i++) //vygeneruj 1 balik karet
            {
                this.cards.Add(new Card(i % 13 + 1, i % 4));
                //cards.Add(new Card(1, 0)); //balik jen es
            }
        }
        public void createDeck(int n) //overloading
        {
            for (int i = 1; i <= 52 * n; i++) //vygeneruj n baliku karet
            {
                cards.Add(new Card(i % 13 + 1, i % 4));
                //cards.Add(new Card(0, 0)); //balik jen es
            }

        }

        public Card giveCard()
        { //vezme prvni item z cards a vrati ho + ho presun na posledni misto
            Card return_ = cards[0];
            this.cards.RemoveAt(0);
            this.cards.Add(return_);
            return return_;

        }
        public IList giveCard(int n)
        {
            List<Card> cardsRet = new List<Card>();
            for (int i = 0; i < n; i++)
            {
                this.cards.Add(this.cards[0]);
                cardsRet.Add(this.cards[0]);
                this.cards.RemoveAt(0);

            }
            return cardsRet.ToList();

        }
        public void shuffleDeck()
        {
            var rnd = new Random();
            this.cards = this.cards.OrderBy(item => rnd.Next()).ToList();

        }
    }
}