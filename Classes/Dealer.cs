using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
namespace cernejJack.Classes
{

    class Dealer
    {

        public static string csvSoubor = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "players.csv"); //relativni cesta
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
                    /*Console.WriteLine("dealerovy karty: (" + this.sumCards() + ")");
                    this.writeCards("dealerovy karty:");
                    Console.WriteLine("tvoje karty: (" + this.player.sumCards() + ")");
                    this.player.writeCards();*/
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
            string[] usersArrayLines = System.IO.File.ReadAllLines(csvSoubor);
            for (int i = 0; i < usersArrayLines.Length; i++) //uloz si csv do pole podle radku
            {
                string[] uzivatel = usersArrayLines[i].Split(";"); //ulozi do pole uzivatel i-tou radku
                if (this.player.nick == uzivatel[0])
                {
                    if (_bool)
                    {

                        uzivatel[2] = (Int32.Parse(uzivatel[2]) + n).ToString();
                    }
                    else
                    {
                        //uzivatel[2] -= n;
                    }


                }
            }


            if (_bool)
            {
                this.player.chips += n;
                chipsUpdate();
            }
            else
            {
                this.player.chips -= n;
                chipsUpdate();
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
                Console.Clear();
                header();
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
                    }
                    else
                    {
                        if (this.player.chips - value >= 0)
                        {
                            this.playersBet = value;
                            this.player.chips -= value;
                            chipsUpdate();
                            Console.WriteLine("dekuji");
                            Thread.Sleep(500);
                            Console.Clear();
                            header();
                            break;
                        }
                        else
                        {

                            Console.WriteLine("nemas dost penez");
                        }


                    }

                }
                else
                {
                    Console.WriteLine("jses uplne retardovana, uplne.");
                }
                Thread.Sleep(500);

            }
        }
        public void playersActions()
        {
            if (player.cards.Count == 2)
            {
                Console.WriteLine("zvol akci (\x1b[1mS\x1b[0mtand, \x1b[1mH\x1b[0mit, \x1b[1mD\x1b[0mouble)");
            }
            else
            {
                Console.WriteLine("zvol akci (\x1b[1mS\x1b[0mtand, \x1b[1mH\x1b[0mit)");
            }
            
            string playersAction = Console.ReadLine();
            playersAction = playersAction.ToLower();
            if (this.player.sumCards() > 21)
            {

                this.player.writeCards("tvoje karty: ");
                Console.ReadLine();

                gameOver();
            }
            else if (this.player.sumCards() == 21 && this.player.cards.Count == 2)
            {
                Console.WriteLine("Dostal jsi Cernyho Jacka!!!");
                this.playersChips(playersBet * 2, true);
            }
            else if (playersAction == "hit" || playersAction == "h")
            {
                Thread.Sleep(500);
                this.giveCardTo(player);
                wopopop();

                

                if (this.gameOverBool)
                {
                    this.playersActions();
                }

            }
            else if (playersAction == "stand" || playersAction == "s")
            {
                Thread.Sleep(500);


            }
            else if (playersAction == "double" || playersAction == "d")
            {
                Thread.Sleep(500);
                Console.Clear();
                
                player.chips -= this.playersBet;
                chipsUpdate();
                this.playersBet *= 2;
                this.giveCardTo(player);
                wopopop();
            }
            else
            {

                Console.WriteLine("akce neexistuje");
                Thread.Sleep(500);
                this.wopopop();
                

               
                
                this.playersActions();
            }
        }
        public void wopopop()
        {
            if (gameOverBool)
            {
                Console.Clear();
                header();
                Console.WriteLine("dealerovy karty: ");
                this.writeFirstCard();
                Console.WriteLine("tvoje karty: ");
                player.writeCards("");
            }
            
        }
        public void writeFirstCard()
        {
            if (this.cards.Count > 0)
            {
                Console.WriteLine(@"
  ┌───────┐
  |" + this.cards[0].returnValueString() + @"     |
  |       |
  |   " + this.cards[0].returnColorString() + @"   |
  |       |
  |    " + this.cards[0].returnValueString() + @" |
  └───────┘
                        ");
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
            if (this.sumCards() < 17)
            {
                this.giveCardTo(this);
                this.dealersMove();
            }
        }
        public void writeCards(string text)
        {

            /*          
                        //tady budou triky
                        //this.giveCardTo(this);
                        Console.WriteLine("dealerovy karty: (" + this.sumCards() + ")");
                        for (int i = 0; i < this.cards.Count; i++)
                        {
                            *//*
                            //lvl 1
                            Console.WriteLine(@"
                            ┌───────┐
                            | "+this.cards[i].value+@"     |
                            |       |
                            |   "+this.cards[i].returnColorString()+ @"   |
                            |       |
                            |     " + this.cards[i].value + @" |
                            └───────┘
            ");
                            *//*
                            //lvl 2
                            if (this.cards[i].value == 10)
                            {

                                Console.WriteLine(@"
                            ┌───────┐
                            | " + this.cards[i].returnValueString() + @"    |
                            |       |
                            |   " + this.cards[i].returnColorString() + @"   |
                            |       |
                            |    " + this.cards[i].returnValueString() + @" |
                            └───────┘
            ");
                            }
                            else
                            {
                                Console.WriteLine(@"
                            ┌───────┐
                            | " + this.cards[i].returnValueString() + @"     |
                            |       |
                            |   " + this.cards[i].returnColorString() + @"   |
                            |       |
                            |     " + this.cards[i].returnValueString() + @" |
                            └───────┘
            ");

                            }




                            if (i > 0)
                            {
                                Thread.Sleep(250);
                            }
                        }*/
            /*
            //lvl 3 uaaaaaa
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  ┌───────┐");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  | " + this.cards[j].returnValueString() + "     |");
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
                Console.Write("  |     " + this.cards[j].returnValueString() + " |");
            }
            Console.WriteLine();
            for (int j = 0; j < this.cards.Count; j++)
            {
                Console.Write("  └───────┘");
            }*/




            //lvl 4 super uaaaaaaaaaaaaa
            for (int i = 1; i < this.cards.Count + 1; i++)
            {
                
                
                Console.Clear();
                header();
                Console.WriteLine("dealerovy karty: ");
                

                //Console.WriteLine(text);
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  ┌───────┐");
                }
                Console.WriteLine();
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  |" + this.cards[j].returnValueString() + "     |");
                }
                Console.WriteLine();
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  |       |");
                }
                Console.WriteLine();
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  |   " + this.cards[j].returnColorString() + "   |");
                }
                Console.WriteLine();
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  |       |");
                }
                Console.WriteLine();
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  |    " + this.cards[j].returnValueString() + " |");
                }
                Console.WriteLine();
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  └───────┘");
                }
                Thread.Sleep(500);
                Console.WriteLine();
                Console.WriteLine("tvoje karty: ");
                player.writeCards("");
                
            }
            

        }
        public void evaluate()
        {
            
            //Console.WriteLine("//dealer: " + this.sumCards());
            //Console.WriteLine("//player: " + this.player.sumCards());
            if (this.gameOverBool)
            {
                Console.Clear();
                header();
                this.writeCards("");


                if (this.sumCards() > 21)
                {

                    Console.WriteLine("vyhral jsi");
                    this.playersChips(playersBet * 2, true);
                }
                else if (this.sumCards() == player.sumCards())
                {


                    this.playersChips(playersBet, true);
                }
                else if (this.player.sumCards() > this.sumCards())
                {



                    Console.WriteLine("vyhral jsi");
                    this.playersChips(playersBet * 2, true);
                }
                else
                {


                    //this.player.writeCards("tvoje karty: ");

                    this.gameOver();
                }
            }
        }
        public void gameOver()
        {
            //Console.Clear();
            //header();
            //this.player.writeCards("tvoje karty: ");
            if (player.sumCards() > 21)
            {
                Console.Clear();
                header();
                Console.WriteLine("dealerovy karty: ");
                this.writeFirstCard();
                Console.WriteLine("tvoje karty: ");
                player.writeCards("");
                
            }
            
            Console.WriteLine("prohral jsi");
            
            

        
            destroyCards(player);
            destroyCards(this);
            Thread.Sleep(1000);
            this.gameOverBool = false;

        }
        public void chipsUpdate()
        {
            string[] usersArrayLines = System.IO.File.ReadAllLines(csvSoubor);//uloz si csv do pole podle radku
            for (int i = 0; i < usersArrayLines.Length; i++)
            {
                string[] uzivatel = usersArrayLines[i].Split(";"); //ulozi do pole uzivatel i-tou radku
                if (this.player.nick == uzivatel[0])
                {
                    usersArrayLines[i] = this.player.nick + ";" + this.player.pass + ";" + this.player.chips;
                }
            }
            File.WriteAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "players.csv"), String.Empty);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvSoubor, true))
            {
                for (int i = 0; i < usersArrayLines.Length; i++)
                {

                    file.WriteLine(usersArrayLines[i]);
                }
                //file.WriteLine(usersArrayLines);
            }
        }
        public void header()
        {
            Console.WriteLine("    BLACK JACK ♦♣♠♥");
            Console.WriteLine("mas " + this.player.chips + " zetonu");
        }
    } //class dealer
}