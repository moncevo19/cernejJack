using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace cernejJack.Classes

{
    class Player
    {
        public int chips;
        public List<Card> cards = new List<Card>();
        public string nick = "";
        public string pass = "";

        public Player()
        {

        }

        public void writeCards(string text) // kur... (ani zanadavat si nemuzu kdyz trpim :-( ) proc to nevraci prvni kartu???
        {
            
           
                
            
            Console.WriteLine(text);
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  ┌───────┐");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  |" + this.cards[j].returnValueString() + "     |");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  |       |");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  |   " + this.cards[j].returnColorString() + "   |");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  |       |");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  |    " + this.cards[j].returnValueString() + " |");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  └───────┘");
            }
            Console.WriteLine();
            


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
        public string bet()
        {
            return Console.ReadLine();
        }
    }
}