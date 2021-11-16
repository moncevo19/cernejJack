using System;
using System.Collections.Generic;
using System.Text;

namespace cernejJack.Classes
{
    class Dealer
    {
        public List<Card> cards = new List<Card>();
        public Deck deck;
        public Player player;
        public Dealer(Deck _deck, Player _player)
        {
            this.deck = _deck;
            this.player = _player;
        }
        public void giveCardTo(Dealer dealer)
        {
            this.cards.Add(deck.giveCard());
        }
        public void giveCardTo(Dealer dealer, int n)
        {
            this.cards.AddRange((List<Card>)deck.giveCard(n));
        }

        public void giveCardTo(Player player)
        {
            player.cards.Add(deck.giveCard());
        }
        public void giveCardTo(Player player, int n)
        {
            player.cards.AddRange((List<Card>)deck.giveCard(n));
        }

        //public void  
        public void destroyCards(Player player)
        {
            player.cards.Clear();
        }
        public void destroyCards(Dealer dealer)
        {
            dealer.cards.Clear();
        }
        public void playersChips(int n, bool _bool)
        {

            if (_bool)
            {
                this.player.chips += n;
            } else
            {
                this.player.chips -= n;
            }

            if (this.player.chips <= 0)
            {
                Console.WriteLine("konec");//rekne co se bude dit dal
            }
        }
        public void bet()
        {
            
            while (true)
            {
                Console.WriteLine("dej mi penize");
                string bet = this.player.bet();
                if (int.TryParse(bet, out int value))
                {
                    if (value < 0)
                    {
                        Console.WriteLine("byl jsi vyhozen za pokus o kradez");
                    }
                    else if (value == 0)
                    {
                        Console.WriteLine("musis vsadit vic 0");
                    } else
                    {
                        if (this.player.chips - value > 0)
                        {
                            this.player.chips -= value;
                        } else
                        {
                            Console.WriteLine("nemas dost penez");
                        }
                        
                        Console.WriteLine("dik");
                    }
                    
                } else
                {
                    Console.WriteLine("jses uplne retardovana.");
                }
                    
            }
        }
    } //class dealer
}