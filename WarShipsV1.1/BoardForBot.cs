using System;

namespace WarShips
{
    internal class BoardForBot : Board
    {
        //auto placing by bot
        new public void PlaceYourShips()
        {
            Random rnd = new Random();

            foreach (Ship shipToPlace in shipsOnBoard)
            {
                Boolean resetAllPlacement = true;

                while (resetAllPlacement)
                {
                    int locationInX = rnd.Next(10)+1;
                    int locationInY = rnd.Next(10)+1;
                    if (shipToPlace.shipSize == 1 && canAddShip(1, locationInX, locationInY, 1))
                    {
                        shipToPlace.addShip(1, locationInX, locationInY, 1);
                        addShipToBoard(1, locationInX, locationInY, 1);
                        resetAllPlacement = false;
                        break;
                    }
                    else if (shipToPlace.shipSize != 1 && canAddShip(1, locationInX, locationInY, 1))
                    {
                        int shipDirection = rnd.Next(4)+1;
                        if (canAddShip(shipToPlace.shipSize, locationInX, locationInY, shipDirection))
                        {
                            shipToPlace.addShip(shipToPlace.shipSize, locationInX, locationInY, shipDirection);
                            addShipToBoard(shipToPlace.shipSize, locationInX, locationInY, shipDirection);
                            resetAllPlacement = false;
                        }
                    }
                }
            }
            Console.WriteLine("Computer placed all ships");
        }
    }
}
