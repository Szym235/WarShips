using System;
using System.Collections.Generic;
using System.Linq;

namespace WarShips
{
    enum locationInXAlphabetToNumbers
    {
        A = 1, B, C, D, E, F, G, H, I, J
    }

    internal class Board
    {
        protected String[,] boardForEnemy = new String[14, 14];
        protected String[,] boardForCommander = new String[14, 14];

        protected List<Ship> shipsOnBoard = new List<Ship>();
        protected ConsoleColor[,] tilesHighlight = new ConsoleColor[14, 14];

        public Board()
        {
            for (int j = 0; j < 14; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                    boardForCommander[i, j] = "B";
                }
            }

            for (int j = 1; j < 13; j++)
            {
                for (int i = 1; i < 13; i++)
                {
                    boardForCommander[i, j] = "~";
                }
            }

            for (int j = 0; j < 14; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                    tilesHighlight[i, j] = ConsoleColor.White;
                }
            }

            for (int j = 1; j < 13; j++)
            {
                for (int i = 1; i < 13; i++)
                {
                    boardForEnemy[i, j] = "~";
                }
            }
            shipsOnBoard.Add(new Ship(1));
            shipsOnBoard.Add(new Ship(1));
            shipsOnBoard.Add(new Ship(1));
            shipsOnBoard.Add(new Ship(1));
            shipsOnBoard.Add(new Ship(2));
            shipsOnBoard.Add(new Ship(2));
            shipsOnBoard.Add(new Ship(2));
            shipsOnBoard.Add(new Ship(3));
            shipsOnBoard.Add(new Ship(3));
            shipsOnBoard.Add(new Ship(4));
        }

        public void showBoardForCommander()
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("-<- A  B  C  D  E  F  G  H  I  J  ");
            for (int y = 2; y < 12; y++)
            {
                Console.Write(" " + (y - 1));
                if (y < 11) { Console.Write(" "); }

                for (int x = 2; x < 12; x++)
                {
                    Console.ForegroundColor = tilesHighlight[x, y];
                    Console.Write(" " + boardForCommander[x, y] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }
        }

        public void clearHighlight()
        {
            for (int j = 0; j < 14; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                    tilesHighlight[j, i] = ConsoleColor.White;
                }
            }
        }

        public Boolean shoot()
        {
            int locationInX = 1;
            int locationInY = 1;
            while (true)
            {
                //write board
                if (shipsOnBoard.Count() == 0) { return false; }
                for (int j = 0; j < 14; j++)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        tilesHighlight[j, i] = ConsoleColor.White;
                    }
                }
                tilesHighlight[locationInX + 1, locationInY + 1] = ConsoleColor.Red;

                Console.SetCursorPosition(0, 15);
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

                //clear all announcments
                int firstCursorTop = Console.CursorTop;
                for (int i = 6; i > 0; i--) Console.WriteLine("                                          ");
                Console.SetCursorPosition(10, firstCursorTop);

                //write coordinates
                Console.Write("      " + (locationInXAlphabetToNumbers)locationInX);
                Console.Write(locationInY + "     ");


                //arrows and confirm input
                int ReadTheKey = (int)Console.ReadKey().Key;
                if (ReadTheKey == (int)ConsoleKey.LeftArrow)
                {
                    if (locationInX == 1) locationInX = 10;
                    else locationInX -= 1;
                }
                else if (ReadTheKey == (int)ConsoleKey.UpArrow)
                {
                    if (locationInY == 1) locationInY = 10;
                    else locationInY -= 1;
                }
                else if (ReadTheKey == (int)ConsoleKey.RightArrow)
                {
                    if (locationInX == 10) locationInX = 1;
                    else locationInX += 1;
                }
                else if (ReadTheKey == (int)ConsoleKey.DownArrow)
                {
                    if (locationInY == 10) locationInY = 1;
                    else locationInY += 1;
                }
                else if (ReadTheKey == GamesManager.ConfirmKey)
                {
                    if (boardForEnemy[locationInX + 1, locationInY + 1] != "x" && boardForEnemy[locationInX + 1, locationInY + 1] != "o" && boardForEnemy[locationInX + 1, locationInY + 1] != "#")
                    {
                        if (boardForCommander[locationInX + 1, locationInY + 1] == "~")
                        {
                            boardForEnemy[locationInX + 1, locationInY + 1] = "o";
                            boardForCommander[locationInX + 1, locationInY + 1] = "o";
                            Console.WriteLine("\nYou missed");
                            Console.ReadKey();
                            return true;
                        }
                        else
                        {
                            boardForEnemy[locationInX + 1, locationInY + 1] = "x";
                            boardForCommander[locationInX + 1, locationInY + 1] = "x";
                            foreach (Ship shipCheckingHit in shipsOnBoard)
                            {
                                if (shipCheckingHit.checkIfHit(locationInX, locationInY))
                                {
                                    if (shipCheckingHit.healthPoints <= 0)
                                    {
                                        //marking tiles around wreck
                                        markWreck(shipCheckingHit.shipSize, shipCheckingHit.shipLocationX, shipCheckingHit.shipLocationY, shipCheckingHit.shipDirection);
                                        shipsOnBoard.Remove(shipCheckingHit);
                                    }
                                    break;
                                }
                            }

                            Console.WriteLine("\nYou hit the ship, you can shoot again!");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou have already shoot this location");
                        Console.ReadKey();
                    }
                }
            }
        }

        protected Boolean canAddShip(int shipSize, int shipLocationX, int shipLocationY, int shipRotation)
        {
            shipLocationX += 1;
            shipLocationY += 1;
            switch (shipRotation)
            {
                case 1:
                    for (int i = 1; i > ((-shipSize) - 1); i--)
                    {
                        if (boardForCommander[shipLocationX - 1, shipLocationY + i] != "~" ||
                            boardForCommander[shipLocationX, shipLocationY + i] != "~" ||
                            boardForCommander[shipLocationX + 1, shipLocationY + i] != "~")
                        {
                            return false;
                        }
                    }
                    break;

                case 2:
                    for (int i = -1; i < shipSize + 1; i++)
                    {
                        if (boardForCommander[shipLocationX + i, shipLocationY - 1] != "~" ||
                            boardForCommander[shipLocationX + i, shipLocationY] != "~" ||
                            boardForCommander[shipLocationX + i, shipLocationY + 1] != "~")
                        {
                            return false;
                        }
                    }
                    break;

                case 3:
                    for (int i = -1; i < shipSize + 1; i++)
                    {
                        if (boardForCommander[shipLocationX - 1, shipLocationY + i] != "~" ||
                            boardForCommander[shipLocationX, shipLocationY + i] != "~" ||
                            boardForCommander[shipLocationX + 1, shipLocationY + i] != "~")
                        {
                            return false;
                        }
                    }
                    break;

                case 4:
                    for (int i = 1; i > ((-shipSize) - 1); i--)
                    {
                        if (boardForCommander[shipLocationX + i, shipLocationY - 1] != "~" ||
                            boardForCommander[shipLocationX + i, shipLocationY] != "~" ||
                            boardForCommander[shipLocationX + i, shipLocationY + 1] != "~")
                        {
                            return false;
                        }
                    }
                    break;

                default:
                    return true;
            }
            return true;
        }



        public void PlaceYourShips()
        {
            int locationInY = 1;
            int locationInX = 1;

            foreach (Ship shipToPlace in shipsOnBoard)
            {
                Boolean resetAllPlacement = true;

                while (resetAllPlacement)
                {
                    //write board
                    clearHighlight();
                    tilesHighlight[locationInX + 1, locationInY + 1] = ConsoleColor.Blue;

                    showBoardForCommander();

                    if (shipToPlace.shipSize == 1) Console.Write("\n Choose ship's position");
                    else Console.Write("\n Choose ship's head position");
                    Console.Write("     Current ship: ");

                    for (int i = shipToPlace.shipSize; i > 0; i--) Console.Write(shipToPlace.shipSize);
                    Console.Write("    " + "\n");

                    Console.Write("      " + (locationInXAlphabetToNumbers)locationInX);
                    Console.Write(locationInY + "     ");

                    //clear announcements
                    int firstCursorTop = Console.CursorTop;
                    for (int i = 10; i > 0; i--) Console.WriteLine("                                  ");
                    Console.SetCursorPosition(10, firstCursorTop);

                    //input
                    int ReadTheKey = (int)Console.ReadKey().Key;
                    if (ReadTheKey == (int)ConsoleKey.LeftArrow)
                    {
                        if (locationInX == 1) locationInX = 10;
                        else locationInX -= 1;
                    }
                    else if (ReadTheKey == (int)ConsoleKey.UpArrow)
                    {
                        if (locationInY == 1) locationInY = 10;
                        else locationInY -= 1;
                    }
                    else if (ReadTheKey == (int)ConsoleKey.RightArrow)
                    {
                        if (locationInX == 10) locationInX = 1;
                        else locationInX += 1;
                    }
                    else if (ReadTheKey == (int)ConsoleKey.DownArrow)
                    {
                        if (locationInY == 10) locationInY = 1;
                        else locationInY += 1;
                    }
                    else if (ReadTheKey == GamesManager.ConfirmKey)
                    {
                        //if ship has size 1 do auto rotate
                        if (shipToPlace.shipSize == 1 && canAddShip(1, locationInX, locationInY, 1))
                        {
                            shipToPlace.addShip(1, locationInX, locationInY, 1);
                            addShipToBoard(1, locationInX, locationInY, 1);
                            resetAllPlacement = false;
                            break;
                        }

                        //rotate ship
                        else if (shipToPlace.shipSize != 1 && canAddShip(1, locationInX, locationInY, 1))
                        {
                            int shipDirection = 1;
                            Console.WriteLine("\nChoose ship's direction              \n");
                            int secondCursorTop = Console.CursorTop;

                            while (true)
                            {
                                Console.SetCursorPosition(0, secondCursorTop);
                                Console.Write("      " + (shipDirectionEnum)shipDirection + "      ");

                                ReadTheKey = (int)Console.ReadKey().Key;
                                if (ReadTheKey == (int)ConsoleKey.UpArrow)
                                {
                                    shipDirection = 1;
                                }
                                else if (ReadTheKey == (int)ConsoleKey.RightArrow)
                                {
                                    shipDirection = 2;
                                }
                                else if (ReadTheKey == (int)ConsoleKey.DownArrow)
                                {
                                    shipDirection = 3;
                                }
                                else if (ReadTheKey == (int)ConsoleKey.LeftArrow)
                                {
                                    shipDirection = 4;
                                }
                                else if (ReadTheKey == GamesManager.ConfirmKey)
                                {
                                    if (canAddShip(shipToPlace.shipSize, locationInX, locationInY, shipDirection))
                                    {
                                        shipToPlace.addShip(shipToPlace.shipSize, locationInX, locationInY, shipDirection);
                                        addShipToBoard(shipToPlace.shipSize, locationInX, locationInY, shipDirection);
                                        resetAllPlacement = false;
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nYou can't place ship here!");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.ReadKey();
                                        Console.SetCursorPosition(0, Console.CursorTop);
                                        Console.Write("                            ");
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYou can't place ship here!");
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.ReadKey();
                            Console.Write("                            ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }

            //show board once more at the end of placing 
            showBoardForCommander();
            int thirdCursorTop = Console.CursorTop;
            for (int i = 10; i > 0; i--) Console.WriteLine("                                                     ");
            Console.SetCursorPosition(0, thirdCursorTop);
            Console.WriteLine("You placed your last ship");
        }

        protected void addShipToBoard(int shipSize, int locationInX, int locationInY, int shipDirection)
        {
            locationInX += 1;
            locationInY += 1;

            switch (shipDirection)
            {
                case 1:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForCommander[locationInX, locationInY - (i - 1)] = Convert.ToString(shipSize);
                    }
                    break;

                case 2:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForCommander[locationInX + (i - 1), locationInY] = Convert.ToString(shipSize);
                    }
                    break;

                case 3:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForCommander[locationInX, locationInY + (i - 1)] = Convert.ToString(shipSize);
                    }
                    break;

                case 4:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForCommander[locationInX - (i - 1), locationInY] = Convert.ToString(shipSize);
                    }
                    break;
            }
        }

        protected void markWreck(int shipSize, int shipLocationX, int shipLocationY, int shipDirection)
        {
            //marking tile around wreck
            switch (shipDirection) 
            {           
                case 1:       
                    for (int i = 1; i > ((-shipSize) - 1); i--)
                    {
                        boardForEnemy[shipLocationX - 1, shipLocationY + i] = "o";
                        boardForEnemy[shipLocationX, shipLocationY + i] = "o";
                        boardForEnemy[shipLocationX + 1, shipLocationY + i] = "o";
                    }
                    break;

                case 2:
                    for (int i = -1; i < shipSize + 1; i++)
                    {
                        boardForEnemy[shipLocationX + i, shipLocationY - 1] = "o";
                        boardForEnemy[shipLocationX + i, shipLocationY] = "o";
                        boardForEnemy[shipLocationX + i, shipLocationY + 1] = "o";
                    }
                    break;

                case 3:
                    for (int i = -1; i < shipSize + 1; i++)
                    {
                        boardForEnemy[shipLocationX - 1, shipLocationY + i] = "o";
                        boardForEnemy[shipLocationX, shipLocationY + i] = "o";
                        boardForEnemy[shipLocationX + 1, shipLocationY + i] = "o";
                    }
                    break;

                case 4:
                    for (int i = 1; i > ((-shipSize) - 1); i--)
                    {
                        boardForEnemy[shipLocationX + i, shipLocationY - 1] = "o";
                        boardForEnemy[shipLocationX + i, shipLocationY] = "o";
                        boardForEnemy[shipLocationX + i, shipLocationY + 1] = "o";
                    }
                    break;
                }

            //marking tiles of wreck
            switch (shipDirection)
            {
                case 1:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForEnemy[shipLocationX, shipLocationY - (i - 1)] = "#";
                    }
                    break;

                case 2:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForEnemy[shipLocationX + (i - 1), shipLocationY] = "#";
                    }
                    break;

                case 3:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForEnemy[shipLocationX, shipLocationY + (i - 1)] = "#";
                    }
                    break;

                case 4:
                    for (int i = shipSize; i > 0; i--)
                    {
                        boardForEnemy[shipLocationX - (i - 1), shipLocationY] = "#";
                    }
                    break;
            }
        }
    }
}
