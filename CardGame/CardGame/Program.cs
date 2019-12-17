using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace CardGame
{
    class Program
    {
        static string Decision; //A string made for your Decisions
        public static bool Contains;
        public bool playerSwitch = false;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Big Value\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("[START]");
                Console.WriteLine("[EXIT]\n");
                Decision = Console.ReadLine();


                using (StringReader reader = new StringReader(Decision))
                {
                    string readText = reader.ReadToEnd();
                    try // Tries to execute the commands, if any of them manage to fail "catch" will catch it
                    {
                        if (Contains = readText.IndexOf("start", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Console.Clear();
                            Game game = new Game();
                            game.Play();
                        }
                        if (Contains = readText.IndexOf("exit", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThat is not a valid command.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(TimeSpan.FromSeconds(1.25));
                            Console.Clear();
                        }
                    }
                    catch (Exception e) // In case "try" fails, this will show an error message with the fault explained. e = The Fault
                    {
                        MessageBox.Show("ERROR:\n\n" + e); // Gives you the error message.
                    }
                }
            }
        }
        static void GameBegin()
        {

        }
    }
    class Card
    {
        public int Value;
        public string Suit;
        public int SuitID;

        public Card(int value, string suit, int suitID)
        {
            Value = value;
            Suit = suit;
            SuitID = suitID;
        }

        public void printCard()
        {
            Console.WriteLine(Suit + " " + Value);
        }

    }

    class Game
    {
        List<Card> CardDeck;
        Random RNG;
        public static string BotUname; // The username of the AI
        public static string Username; // Player Username
        public string Decision; // Official Decision
        public static bool Contains; // Checks to see if it contains...
        public static bool playerTurn = true; // Controls the turns
        public int botDecision; // Controls the AI decisions
        public int DecisionINT; // Random Decision in INT form.
        public static bool finished = false; // finished bool

        public static int PlayerCardValues; // The value of your cards.
        public static int BOTCardValues; // The value of the AI's cards.



        public Game()
        {
            RNG = new Random();
            CardDeck = new List<Card>();
            CreateDeck();
        }

        public void CreateDeck()
        {
            string suit = "Hjärter"; // Heart suit
            int suitID = 1;
            for (int i = 0; i < 52; i++)
            {
                if (i <= 13)
                {
                    suit = "Ruter"; // Heart suit
                    suitID = 2;
                }
                else if (i <= 26 && i > 13)
                {
                    suit = "Spader"; // Heart suit 
                    suitID = 3;
                }
                else if (i <= 39 && i > 26)
                {
                    suit = "Klöver"; // Heart suit
                    suitID = 4;
                }
                CardDeck.Add(new Card(i % 13 + 1, suit, suitID));
            }
            Shuffle();
        }

        public void Play()  // Game begins
        {
            while (true)
            {
                int temp = 0;
                int temp2 = 97;
                int LineLength = 9;
                Random rng = new Random();

                #region BOTNameGenerator
                DecisionINT = rng.Next(0, 6);

                if (DecisionINT >= 4)
                {
                    BotUname = "Jay";
                }
                else if (DecisionINT < 4)
                {
                    BotUname = "Karl";
                }
                #endregion

                #region UsernameGenerator
                DecisionINT = rng.Next(0, 6); // Randomizes your username

                if (DecisionINT >= 4)
                {
                    Username = "Heman";
                }
                else if (DecisionINT < 4)
                {
                    Username = "Noob";
                }
                #endregion


                #region playerCardVariables&WriteOutVariables
                Card playerCard = null; // States the variable for the first card the player can draw.
                Card playerCard2 = null; // States the variable for the second card the player can draw.
                Card playerCard3 = null; // States the variable for the third card the player can draw.

                Card WriteOut = null; // States the variable for the writeout command.
                #endregion
                #region CardDrawing&WritingOut

                #region writingOutText
                Console.ForegroundColor = ConsoleColor.Green;

                Console.SetCursorPosition(3, 0);
                Console.WriteLine("Username: {0}", Username); // Writes out the Username you choose in the beginning of the game.

                Console.SetCursorPosition(4, 4);
                Console.Write("Your Card: "); // Show you where you card are.



                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(105, 4);
                Console.Write("AI Card: \n"); // This part is not necessary but it looks better.


                Console.SetCursorPosition(103, 0);
                Console.WriteLine("AI Name: {0}", BotUname); // Shows you the name of the AI you are up against.
                Console.ForegroundColor = ConsoleColor.White;
                #endregion

                #region playerCardDrawing

                Console.SetCursorPosition(4, 5);
                Console.WriteLine("1."); // Writes out a number for you.
                Console.SetCursorPosition(7, 5);
                playerCard = DrawCard();
                playerCard.printCard();

                Console.SetCursorPosition(4, 6);
                Console.WriteLine("2."); // Writes out a number for you.
                Console.SetCursorPosition(7, 6);
                playerCard2 = DrawCard();
                playerCard2.printCard();

                Console.SetCursorPosition(4, 7);
                Console.WriteLine("3."); // Writes out a number for you.
                Console.SetCursorPosition(7, 7);
                playerCard3 = DrawCard();
                playerCard3.printCard();
                #endregion

                #region BOTCardDrawing
                Card playerBOTCard = null; // States the variable for the first card the bot can draw.
                Card playerBOTCard2 = null; // States the variable for the second card the bot can draw.
                Card playerBOTCard3 = null; // States the variable for the third card the bot can draw.

                playerBOTCard = DrawCard();
                playerBOTCard2 = DrawCard();
                playerBOTCard3 = DrawCard();

                #endregion

                #region Layout
                for (int i = 0; i < LineLength; i++) // The Game Layout
                {
                    Console.SetCursorPosition(22, i);
                    Console.WriteLine("|");
                    Console.SetCursorPosition(96, i);
                    Console.WriteLine("|");
                    Console.SetCursorPosition(temp, 8);
                    temp++;
                    Console.WriteLine("_");
                    if (i <= 6)
                    {
                        Console.SetCursorPosition(temp, 8);
                        temp++;
                        Console.WriteLine("_");
                        if (i <= 5)
                        {
                            Console.SetCursorPosition(temp, 8);
                            temp++;
                            Console.WriteLine("_");
                        }
                    }
                    Console.SetCursorPosition(temp2, 8);
                    temp2++;
                    Console.WriteLine("_");
                    if (i <= 6)
                    {
                        Console.SetCursorPosition(temp2, 8);
                        temp2++;
                        Console.WriteLine("_");
                        if (i <= 5)
                        {
                            Console.SetCursorPosition(temp2, 8);
                            temp2++;
                            Console.WriteLine("_");
                        }
                    }
                }
                #endregion

                #endregion


                Console.SetCursorPosition(105, 5);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HIDDEN");
                Console.SetCursorPosition(105, 6);
                Console.WriteLine("HIDDEN");
                Console.SetCursorPosition(105, 7);
                Console.WriteLine("HIDDEN");

                #region showCards

                Console.ForegroundColor = ConsoleColor.Cyan;
                WriteOut = DrawCard();
                Console.SetCursorPosition(53, 2);
                WriteOut.printCard();
                Console.ForegroundColor = ConsoleColor.White;

                #endregion

                while (finished != true)
                {
                    #region ClearLine
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine("                                  ");
                    Console.WriteLine("                                  ");
                    #endregion

                    if (playerTurn == true)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1.25));
                        Options();
                        Console.SetCursorPosition(0, 18);
                        Decision = Console.ReadLine();

                        using (StringReader reader = new StringReader(Decision))
                        {
                            string readText = reader.ReadToEnd();
                            #region Player
                            if (Contains = readText.IndexOf("throwa", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                #region playerCard
                                Console.SetCursorPosition(4, 5);
                                Console.WriteLine("               ");
                                Console.SetCursorPosition(4, 5);
                                Console.Write("1.");
                                Console.SetCursorPosition(7, 5);
                                playerCard = DrawCard();
                                playerCard.printCard();

                                Console.SetCursorPosition(4, 6);
                                Console.WriteLine("               ");
                                Console.SetCursorPosition(4, 6);
                                Console.Write("2.");
                                Console.SetCursorPosition(7, 6);
                                playerCard2 = DrawCard();
                                playerCard2.printCard();

                                Console.SetCursorPosition(4, 7);
                                Console.WriteLine("               ");
                                Console.SetCursorPosition(4, 7);
                                Console.Write("3.");
                                Console.SetCursorPosition(7, 7);
                                playerCard3 = DrawCard();
                                playerCard3.printCard();
                                #endregion

                                switchCard();
                            }
                            if (Contains = readText.IndexOf("throwo", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                Console.Write("Kort att kasta ut(Nummer): ");
                                Decision = Console.ReadLine();
                                switchCard();
                                switch (Decision)
                                {
                                    case "1":
                                        {
                                            Console.SetCursorPosition(4, 5);
                                            Console.WriteLine("               ");
                                            Console.SetCursorPosition(4, 5);
                                            Console.WriteLine("1.");
                                            Console.SetCursorPosition(7, 5);
                                            playerCard = DrawCard();
                                            playerCard.printCard();
                                            break;
                                        }

                                    case "2":
                                        {
                                            Console.SetCursorPosition(4, 6);
                                            Console.WriteLine("               ");
                                            Console.SetCursorPosition(4, 6);
                                            Console.WriteLine("2.");
                                            Console.SetCursorPosition(7, 6);
                                            playerCard2 = DrawCard();
                                            playerCard2.printCard();
                                            break;
                                        }

                                    case "3":
                                        {
                                            Console.SetCursorPosition(4, 7);
                                            Console.WriteLine("               ");
                                            Console.SetCursorPosition(4, 7);
                                            Console.WriteLine("3.");
                                            Console.SetCursorPosition(7, 7);
                                            playerCard3 = DrawCard();
                                            playerCard3.printCard();
                                            break;

                                        }
                                }
                            }
                            if (Contains = readText.IndexOf("stop", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                finished = true;
                                PlayerCardValues = playerCard.Value + playerCard2.Value + playerCard3.Value;
                                BOTCardValues = playerBOTCard.Value + playerBOTCard2.Value + playerBOTCard3.Value;
                                stop();
                            }
                            if (Contains = readText.IndexOf("wait", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                wait();
                            }

                            #endregion
                        }
                        playerTurn = false;
                    }
                    if (playerTurn == false)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1.25));
                        #region BOT
                        switch (botDecision = rng.Next(0, 4))
                        {
                            case 1:
                                Console.SetCursorPosition(105, 5);
                                playerBOTCard = DrawCard();

                                Console.SetCursorPosition(105, 6);
                                playerBOTCard2 = DrawCard();

                                Console.SetCursorPosition(105, 7);
                                playerBOTCard3 = DrawCard();
                                Thread.Sleep(TimeSpan.FromSeconds(1.25));
                                switchCard();
                                break;
                            case 2:
                                switchCard();
                                switch (botDecision = rng.Next(0, 4))
                                {
                                    case 1:
                                        Console.SetCursorPosition(105, 5);
                                        playerBOTCard = DrawCard();
                                        break;
                                    case 2:
                                        Console.SetCursorPosition(105, 6);
                                        playerBOTCard2 = DrawCard();
                                        break;
                                    case 3:
                                        Console.SetCursorPosition(105, 7);
                                        playerBOTCard3 = DrawCard();
                                        break;
                                }
                                Thread.Sleep(TimeSpan.FromSeconds(1.25));
                                break;
                            case 3:
                                botDecision = rng.Next(0, 20);

                                if (botDecision <= 15)
                                {
                                    finished = true;
                                    PlayerCardValues = playerCard.Value + playerCard2.Value + playerCard3.Value;
                                    BOTCardValues = playerBOTCard.Value + playerBOTCard2.Value + playerBOTCard3.Value;
                                    stop();
                                }
                                else
                                {
                                    wait();
                                }
                                break;
                        }
                        playerTurn = true;
                        #endregion
                    }
                }
            }
        }

        public Card DrawCard()
        {
            Card theCard = CardDeck.First();
            CardDeck.RemoveAt(0);
            return theCard;
        }

        public void Shuffle()
        {
            for (int i = 0; i < 200; i++)
            {
                SwitchCards();
            }
        }

        private void SwitchCards()
        {
            int card1 = RNG.Next(CardDeck.Count);
            int card2 = RNG.Next(CardDeck.Count);
            Card temp = CardDeck[card1];
            CardDeck[card1] = CardDeck[card2];
            CardDeck[card2] = temp;
        }

        public static void Options() // This method will write out all the options you have.
        {
            Console.SetCursorPosition(4, 11);
            Console.WriteLine("Val: \n");
            Console.WriteLine("ThrowA");
            Console.WriteLine("ThrowO");
            Console.WriteLine("Wait");
            Console.WriteLine("Stop");
        }


        #region writeOutText

        public static void switchCard()
        {
            if (playerTurn == true)
            {
                Console.SetCursorPosition(32, 5);
                Console.WriteLine("{0} threw out one or more cards and picked up a new one!", Username);
                Thread.Sleep(TimeSpan.FromSeconds(1.25));
            }
            if (playerTurn == false)
            {
                Console.SetCursorPosition(32, 5);
                Console.WriteLine("{0} threw out one or more cards and picked up a new one!", BotUname);
                Thread.Sleep(TimeSpan.FromSeconds(1.25));
            }
            Console.SetCursorPosition(32, 5);
            Console.WriteLine("                                                             ");
        }
        public static void wait()
        {
            if (playerTurn == true)
            {
                Console.SetCursorPosition(47, 5);
                Console.WriteLine("{0} waited one round!", Username);
                Thread.Sleep(TimeSpan.FromSeconds(1.25));
            }
            if (playerTurn == false)
            {
                Console.SetCursorPosition(47, 5);
                Console.WriteLine("{0} waited one round!", BotUname);
                Thread.Sleep(TimeSpan.FromSeconds(1.25));
            }
            Console.SetCursorPosition(32, 5);
            Console.WriteLine("                                                             ");
        }

        #endregion

        public static void stop() // When someone stops the game.
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Someone stopped the game..."); // When someone stops the game.
            Thread.Sleep(TimeSpan.FromSeconds(1.25));
            if (PlayerCardValues < BOTCardValues)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("'##:::'##::'#######::'##::::'##::::'##:::::'##:'####:'##::: ##:");
                Console.WriteLine(". ##:'##::'##.... ##: ##:::: ##:::: ##:'##: ##:. ##:: ###:: ##:");
                Console.WriteLine(":. ####::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ####: ##:");
                Console.WriteLine("::. ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ## ## ##:");
                Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##. ####:");
                Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##:. ###:");
                Console.WriteLine("::: ##::::. #######::. #######:::::. ###. ###::'####: ##::. ##:");
                Console.WriteLine(":::..::::::.......::::.......:::::::...::...:::....::..::::..::");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
            else if (PlayerCardValues > BOTCardValues)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("'##:::'##::'#######::'##::::'##::::'##::::::::'#######:::'######::'########:");
                Console.WriteLine(". ##:'##::'##.... ##: ##:::: ##:::: ##:::::::'##.... ##:'##... ##: ##.....::");
                Console.WriteLine(":. ####::: ##:::: ##: ##:::: ##:::: ##::::::: ##:::: ##: ##:::..:: ##:::::::");
                Console.WriteLine("::. ##:::: ##:::: ##: ##:::: ##:::: ##::::::: ##:::: ##:. ######:: ######:::");
                Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##::::::: ##:::: ##::..... ##: ##...::::");
                Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##::::::: ##:::: ##:'##::: ##: ##:::::::");
                Console.WriteLine("::: ##::::. #######::. #######::::: ########:. #######::. ######:: ########:");
                Console.WriteLine(":::..::::::.......::::.......::::::........:::.......::::......:::........::");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
        }
    }
}
