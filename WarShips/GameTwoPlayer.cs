using System;

namespace WarShips
{
    internal class GameTwoPlayer
    {
        private Board FirstPlayerBoard, SecondPlayerBoard;

        public GameTwoPlayer()
        {
            this.FirstPlayerBoard = new Board();
            this.SecondPlayerBoard = new Board();
        }

        public Boolean PlayTheGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("First player is placing ships");
            Console.ForegroundColor = ConsoleColor.White;
            FirstPlayerBoard.PlaceYourShips();
            Console.WriteLine("Press any key to hide your ships and switch to the next player");
            Console.ReadKey();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Second player is placing ships");
            Console.ForegroundColor = ConsoleColor.White;
            SecondPlayerBoard.PlaceYourShips();
            Console.WriteLine("Press any key to hide your ships and begin shooting");
            Console.ReadKey();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("First player is shooting");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                if (SecondPlayerBoard.shoot() == false)
                {
                    return true;
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Second player is shooting");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                if (FirstPlayerBoard.shoot() == false)
                {
                    return false;
                }
            }
        }
    }
}
