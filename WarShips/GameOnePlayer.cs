using System;

namespace WarShips
{
    internal class GameOnePlayer
    {
        private BoardHuman HumanPlayerBoard;
        private BoardBot BotPlayerBoard;

        public GameOnePlayer()
        {
            this.HumanPlayerBoard = new BoardHuman();
            this.BotPlayerBoard = new BoardBot();
        }
        public Boolean PlayTheGame()
        {
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Player is placing ships");
                Console.ForegroundColor = ConsoleColor.White;
                HumanPlayerBoard.PlaceYourShips();
                Console.WriteLine("Press any key switch to the computer");
                Console.ReadKey();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Computer is placing ships");
                Console.ForegroundColor = ConsoleColor.White;
                BotPlayerBoard.PlaceYourShips();
                Console.ReadKey();

                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Player is shooting");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (BotPlayerBoard.shoot() == false)
                    {
                        return true;
                    }

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Computer is shooting");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (HumanPlayerBoard.shoot() == false)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
