using System;

namespace WarShips
{
    internal class GamesManager
    {
        public static int ConfirmKey = (int)ConsoleKey.X;
        static void Main(string[] args)
        {
            Console.WriteLine(" Use arrows to navigate\n Use X to confirm");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("*-------- WarShips --------*");
            Console.ReadKey();
            Console.Clear();

            ConsoleColor OnePlayerColor = ConsoleColor.Blue;
            ConsoleColor TwoPlayersColor = ConsoleColor.White;
            int ChoosedOption = 1;
            int ReadTheKey;
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("*-----------------*                               ");
                Console.ForegroundColor = OnePlayerColor;
                Console.WriteLine("   1 One player                         ");
                Console.ForegroundColor = TwoPlayersColor;
                Console.WriteLine("   2 Two players                         ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("*-----------------*                         \n ");
                Console.SetCursorPosition(0, 5);

                ReadTheKey = (int)Console.ReadKey().Key;
                if (ReadTheKey == (int)ConsoleKey.DownArrow && ChoosedOption == 1)
                {
                    ChoosedOption = 2;
                    OnePlayerColor = ConsoleColor.White;
                    TwoPlayersColor = ConsoleColor.Blue;
                }
                else if (ReadTheKey == (int)ConsoleKey.UpArrow && ChoosedOption == 2)
                {
                    ChoosedOption = 1;
                    TwoPlayersColor = ConsoleColor.White;
                    OnePlayerColor = ConsoleColor.Blue;
                }

                //One player game
                else if (ReadTheKey == ConfirmKey && ChoosedOption == 1)
                {

                    Player HumanPlayer = new Player(0);
                    Player BotPlayer = new Player(0);

                    Boolean playAgain = true;
                    while (playAgain)
                    {
                        GameOnePlayer NewGame = new GameOnePlayer();

                        if (NewGame.PlayTheGame())
                        {

                            Console.WriteLine("All ships destroyed! Player won!");
                            HumanPlayer.wins++;
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("All ships destroyed! Computer won!");
                            BotPlayer.wins++;
                            Console.ReadKey();
                        }
                        Console.Clear();

                        Console.WriteLine("Player wins: " + HumanPlayer.wins + "         Computer wins: " + BotPlayer.wins);
                        Console.WriteLine("Do you want to rematch?");

                        Boolean optionSelected = true;
                        while (true)
                        {
                            Console.SetCursorPosition(0, 2);

                            Console.ForegroundColor = (optionSelected ? ConsoleColor.Blue : ConsoleColor.White);
                            Console.Write("   Yes   ");
                            Console.ForegroundColor = (optionSelected ? ConsoleColor.White : ConsoleColor.Red);
                            Console.Write("   No   ");
                            Console.ForegroundColor = ConsoleColor.White;

                            ReadTheKey = (int)Console.ReadKey().Key;
                            if (ReadTheKey == (int)ConsoleKey.LeftArrow)
                            {
                                optionSelected = !optionSelected;
                            }
                            else if (ReadTheKey == (int)ConsoleKey.RightArrow)
                            {
                                optionSelected = !optionSelected;
                            }
                            else if (ReadTheKey == GamesManager.ConfirmKey && optionSelected)
                            {
                                break;
                            }
                            else if (ReadTheKey == GamesManager.ConfirmKey && !optionSelected)
                            {
                                playAgain = false;
                                break;
                            }
                        }
                    }
                }

                //Two players game
                else if (ReadTheKey == ConfirmKey && ChoosedOption == 2)
                {


                    Player FirstPlayer = new Player(0);
                    Player SecondPlayer = new Player(0);

                    Boolean playAgain = true;
                    while (playAgain)
                    {
                        GameTwoPlayer NewGame = new GameTwoPlayer();

                        if (NewGame.PlayTheGame())
                        {

                            Console.WriteLine("All ships destroyed! First player won!");
                            FirstPlayer.wins++;
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("All ships destroyed! Second player won!");
                            SecondPlayer.wins++;
                            Console.ReadKey();
                        }
                        Console.Clear();

                        Console.WriteLine("First player wins: " + FirstPlayer.wins + "         Second player wins: " + SecondPlayer.wins);
                        Console.WriteLine("Do you want to rematch?");

                        Boolean optionSelected = true;
                        while (true)
                        {
                            Console.SetCursorPosition(0, 2);

                            Console.ForegroundColor = (optionSelected ? ConsoleColor.Blue : ConsoleColor.White);
                            Console.Write("   Yes   ");
                            Console.ForegroundColor = (optionSelected ? ConsoleColor.White : ConsoleColor.Red);
                            Console.Write("   No   ");
                            Console.ForegroundColor = ConsoleColor.White;

                            ReadTheKey = (int)Console.ReadKey().Key;
                            if (ReadTheKey == (int)ConsoleKey.LeftArrow)
                            {
                                optionSelected = !optionSelected;
                            }
                            else if (ReadTheKey == (int)ConsoleKey.RightArrow)
                            {
                                optionSelected = !optionSelected;
                            }
                            else if (ReadTheKey == GamesManager.ConfirmKey && optionSelected)
                            {
                                break;
                            }
                            else if (ReadTheKey == GamesManager.ConfirmKey && !optionSelected)
                            {
                                playAgain = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
