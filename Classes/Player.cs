using System;
using System.Collections.Generic;
using System.Text;

namespace cernejJack.Classes

{
    class Player
    {
        public int chips;
        public List<Card> cards = new List<Card>();

        public Player()
        {
            this.chips = 100;  
        }
        public void writeCards()
        {
            
            Console.WriteLine("hrac");
            for (int i = 0; i < this.cards.Count; i++) 
            {
                Console.WriteLine(this.cards[i].value);
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
                } else
                {
                    sum++;
                    aces--;
                }
                

            }
            
            
            
            return sum;
        }
        public string bet()
        {
            return Console.ReadLine();
        }
    }
}
