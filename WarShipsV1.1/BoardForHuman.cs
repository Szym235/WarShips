using System;
using System.Linq;

namespace WarShips
{
    internal class BoardForHuman : Board
    {
        //auto shooting done by bot
        new public Boolean shoot()
        {
            Random rnd = new Random();
            Boolean botMissed = true;

            while (botMissed)
            {
                Console.SetCursorPosition(0, 1);
                for (int i = 16; i > 0; i--) Console.WriteLine("                                          ");
                int locationInX = rnd.Next(10)+1;
                int locationInY = rnd.Next(10)+1;

                if (shipsOnBoard.Count() == 0) { return false; }

                Console.SetCursorPosition(0, 12);
                Console.Write("      " + (locationInXAlphabetToNumbers)locationInX);
                Console.Write(locationInY + "     ");

                if (boardForEnemy[locationInX + 1, locationInY + 1] != "x" && boardForEnemy[locationInX + 1, locationInY + 1] != "o" && boardForEnemy[locationInX + 1, locationInY + 1] != "#")
                {
                    if (boardForCommander[locationInX + 1, locationInY + 1] == "~")
                    {
                        boardForEnemy[locationInX + 1, locationInY + 1] = "o";
                        boardForCommander[locationInX + 1, locationInY + 1] = "o";
                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Computer missed");
                        botMissed = false;
                    }
                    else
                    {
                        boardForCommander[locationInX + 1, locationInY + 1] = "x";
                        boardForEnemy[locationInX + 1, locationInY + 1] = "x";
                        foreach (Ship shipCheckingHit in shipsOnBoard)
                        {
                            if (shipCheckingHit.checkIfHit(locationInX, locationInY))
                            {
                                if (shipCheckingHit.healthPoints <= 0)
                                {
                                    markWreck(shipCheckingHit.shipSize, shipCheckingHit.shipLocationX, shipCheckingHit.shipLocationY, shipCheckingHit.shipDirection);
                                    shipsOnBoard.Remove(shipCheckingHit);
                                    Console.SetCursorPosition(0, 13);
                                    Console.WriteLine("Computer destroyed the ship!");
                                }
                                Console.SetCursorPosition(0, 14);
                                Console.WriteLine("Computer hit the ship, it can shoot again!");
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < 14; j++)
                    {
                        for (int i = 0; i < 14; i++)
                        {
                            tilesHighlight[j, i] = ConsoleColor.White;
                        }
                    }
                    tilesHighlight[locationInX + 1, locationInY + 1] = ConsoleColor.Red;

                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("-<  A  B  C  D  E  F  G  H  I  J  ");
                    for (int y = 2; y < 12; y++)
                    {
                        Console.Write(" " + (y - 1));
                        if (y < 11) { Console.Write(" "); }

                        for (int x = 2; x < 12; x++)
                        {
                            Console.ForegroundColor = tilesHighlight[x, y];
                            Console.Write(" " + boardForEnemy[x, y] + " ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("\n");
                    }
                    Console.SetCursorPosition(0, 15);
                    Console.ReadKey();
                }
            }
            return true;
        }
    }
}
