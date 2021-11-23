using System;
using System.Collections.Generic;
using System.Text;

namespace cernejJack.Classes
{
    class Dealer
    {
        public bool gameOverBool = true;
        public List<Card> cards = new List<Card>();
        public Deck deck;
        public Player player;
        public int playersBet = 0;
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
            if (this.gameOverBool)
            {
                player.cards.Add(deck.giveCard());
                if (player.sumCards() > 21)
                {
                    this.gameOver();
                }
            }
            
            
        }
        public void giveCardTo(Player player, int n)
        {
            if (this.gameOverBool)
            {

            
                player.cards.AddRange((List<Card>)deck.giveCard(n));
                if (player.sumCards() > 21)
                {
                    this.gameOver();
                }
            }
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
                Console.WriteLine("kolik chces vsadit?");
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
                            this.playersBet = value;
                            this.player.chips -= value;
                            Console.WriteLine("dik");
                            break;
                        } else
                        {
                            Console.WriteLine("nemas dost penez");
                        }
                        
                        
                    }
                    
                } else
                {
                    Console.WriteLine("jses uplne retardovana.");
                }
                    
            }
        }
        public void playersActions()
        {
            Console.WriteLine("zvol akci");
            string playersAction = Console.ReadLine();
            if (this.player.sumCards() > 21)
            {
                Console.WriteLine("dealer");
                Console.WriteLine(this.cards[0].value);
                
                gameOver();
            }
            if (playersAction == "hit")
            {
               
                this.giveCardTo(player);
                Console.WriteLine("dealer");
                Console.WriteLine(this.cards[0].value);
                this.player.writeCards();
                
                if (this.gameOverBool)
                {
                    this.playersActions();
                }
                
            }
            else if(playersAction == "stand")
            {
                Console.WriteLine("dealer");
                Console.WriteLine(this.cards[0].value);
                
            } 
            else if(playersAction == "double")      
            {
                
                player.chips -= this.playersBet;
                this.playersBet *= 2;
                this.giveCardTo(player);
                Console.WriteLine("dealer");
                Console.WriteLine(this.cards[0].value);
                
            } else
            {
                Console.WriteLine("akce neexistuje");
                this.playersActions();
            }
        }
        public int sumCards()
        {
            int sum = 0;
            int aces = 0;
            for (int i = 0; i < this.cards.Count; i++)
            {
                if (this.cards[i].value == 1)
                {
                    aces++;
                }
                else if (this.cards[i].value > 10)
                {
                    sum += 10;
                }
                else
                {
                    sum += this.cards[i].value;

                }
            }
            while (aces > 0)
            {
                if (sum <= 10 && aces == 1)//reseni es 1/11
                {
                    sum += 11;
                    aces--;
                }
                else
                {
                    sum++;
                    aces--;
                }


            }



            return sum;
        }
        public void dealersMove()
        {
            if (this.sumCards() < 17 && this.sumCards() < this.player.sumCards())
            {
                this.giveCardTo(this);
                this.dealersMove();
            }
        }
        public void writeCards()
        {
            
            
            //this.giveCardTo(this);
            Console.WriteLine("dealer (" + this.sumCards() + ")");
            for (int i = 0; i < this.cards.Count; i++)
            {
                Console.WriteLine(this.cards[i].value);
            }
        }
        public void evaluate()
        {
            Console.WriteLine("//dealer: " + this.sumCards());
            Console.WriteLine("//player: " + this.player.sumCards());
            if (this.gameOverBool)
                {

            
                if (this.sumCards() > 21)
                {
                    
                    this.writeCards();
                    Console.WriteLine("hrac");
                    
                    Console.WriteLine("vyhral jsi");
                    this.playersChips(playersBet * 2, true);
                }
                else if (this.sumCards() == player.sumCards())
                {
                    
                    this.writeCards();
                    Console.WriteLine("hrac");
                    
                    this.playersChips(playersBet, true);
                } else if (this.player.sumCards() > this.sumCards()) 
                {
                    
                    this.writeCards();
                    Console.WriteLine("hrac");
                    
                    Console.WriteLine("vyhral jsi");
                    this.playersChips(playersBet * 2, true);
                }
                else
                {
                    
                    this.writeCards();
                    Console.WriteLine("hrac");
                    
                    Console.WriteLine("prohral jsi");
                }
            }
        }
        public void gameOver()
        {
            Console.WriteLine("prohral jsi");
            this.gameOverBool = false;
            
        }
    } //class dealer
}