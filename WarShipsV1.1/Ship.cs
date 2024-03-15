using System;

namespace WarShips
{
    public enum shipDirectionEnum
    {
        up = 1, right = 2, down = 3, left = 4
    }
    internal class Ship
    {
        public int shipSize;
        public int shipLocationX;
        public int shipLocationY;
        public int shipDirection;
        public int healthPoints;

        public Ship(int shipSize)
        {
            this.shipSize = shipSize;
            this.healthPoints = shipSize;
        }

        public void addShip(int shipSize, int locationInX, int locationInY, int shipDirection)
        {
            this.shipSize = shipSize;
            this.shipLocationX = locationInX + 1;
            this.shipLocationY = locationInY + 1;
            this.shipDirection = shipDirection;
        }

        public Boolean checkIfHit(int locationInX, int locationInY)
        {
            locationInX += 1;
            locationInY += 1;
            Boolean iWasHit = false;

            switch (shipDirection)
            {
                case 1:
                    for (int i = (shipSize - 1); i >= 0; i--)
                    {
                        if (locationInX == shipLocationX && locationInY == (shipLocationY - i))
                        {
                            iWasHit = true;
                            break;
                        }
                    }
                    break;

                case 2:
                    for (int i = (shipSize - 1); i >= 0; i--)
                    {
                        if (locationInX == (shipLocationX + i) && locationInY == shipLocationY)
                        {
                            iWasHit = true;
                            break;
                        }
                    }
                    break;

                case 3:
                    for (int i = (shipSize - 1); i >= 0; i--)
                    {
                        if (locationInX == shipLocationX && locationInY == (shipLocationY + i))
                        {
                            iWasHit = true;
                            break;
                        }
                    }
                    break;

                case 4:
                    for (int i = (shipSize - 1); i >= 0; i--)
                    {
                        if (locationInX == (shipLocationX - i) && locationInY == shipLocationY)
                        {
                            iWasHit = true;
                            break;
                        }
                    }
                    break;
            }
            if (iWasHit)
            {
                this.healthPoints--;

                if (this.healthPoints <= 0)
                {
                    Console.WriteLine("\nShip destroyed!");
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
