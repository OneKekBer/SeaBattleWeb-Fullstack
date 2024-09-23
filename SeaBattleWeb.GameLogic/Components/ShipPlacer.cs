using SeaBattle.Values;
using SeaBattleWeb.GameLogic.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum ShipDirections
{
    Top = 0,
    Down = 1,
    Left = 2,
    Right = 3,
}

namespace SeaBattleWeb.GameLogic.Components
{
    internal class ShipPlacer
    {
        private Board board;
        Random random = new Random();
        List<Coordinates> allShipsPositions = new List<Coordinates>();

        public ShipPlacer(Board Board)
        {
            board = Board;
        }
        public void PlaceShip(Ship ship)
        {
            List<Coordinates> options = GetShipOptions(ship);

            foreach (Coordinates coords in options)
            {
                board[coords].PlaceShip(ship);
            }
        }


        public IEnumerable<Coordinates> GetShipCoordinates(int shipSize)
        {
            Coordinates[] directions = {
                new Coordinates(0, 1),
                new Coordinates(1, 0),
                new Coordinates(-1, 0),
                new Coordinates(0, -1)
            };
            bool isPossibleToPlaceShip = true;

            const int boardLength = 9; 
            var shipCoordinates = new List<Coordinates>();

            for(int i = 0; i < 50; i++)
            {
                var startCoords = new Coordinates(random.Next(0, boardLength - 1), random.Next(0, boardLength - 1));
                shipCoordinates.Add(startCoords);

                foreach (var direction in directions)
                {
                    var lastCoord = shipCoordinates.Last();

                    var newCoord = new Coordinates(lastCoord.X + direction.X, lastCoord.Y + direction.Y);

                    if (newCoord.X >= boardLength || newCoord.X < 0 || newCoord.Y >= boardLength || newCoord.Y < 0)
                    {
                        shipCoordinates.RemoveRange(1, shipCoordinates.Count - 1);
                        continue;
                    }
                }

                if (shipCoordinates.Count == shipSize)
                    return shipCoordinates;
            }

            throw new Exception("Impossible to place ship!!");
            
        } 

        public List<Coordinates> GetShipOptions(Ship ship)
        {
            Coordinates[] directions = {
                new Coordinates(0, 1),
                new Coordinates(1, 0),
                new Coordinates(-1, 0),
                new Coordinates(0, -1)
            };//possibile ship directions 

            var options = new List<Coordinates>();

            bool isShipPossibleToPlace = false;
            while (!isShipPossibleToPlace)
            {
                Coordinates randomCoords = GenerateNewCoords();
                options.Add(randomCoords);

                //getting random direction
                int randomDirectionIndex = random.Next(0, directions.Length);
                Coordinates direction = directions[randomDirectionIndex];
                var (dirX, dirY) = direction;
                Console.WriteLine("direction " + dirX + " " + dirY);

                int i = 1;
                while (true)
                {
                    try
                    {
                        if (i == ship.Size)
                        {
                            isShipPossibleToPlace = true;
                            break;
                        }

                        if (board[options.Last()].PanelState == PanelState.ContainsShip)
                            throw new Exception("impossible to place ship");

                        var nextCoords = GetNextStep(direction, options.Last());

                        if (IsShipAround(options.Last(), nextCoords))
                            throw new Exception("impossible to place ship");

                        options.Add(nextCoords);
                        Console.WriteLine("next coords " + nextCoords);
                        i++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        options.Clear();
                        i = 1;
                        break;
                    }
                }
            }
            return options;
        }
        //приудмать условие выхода чтобы не взорвать ноут

        public bool IsShipAround(Coordinates lastOption, Coordinates currCoords)
        {
            Coordinates[] circle = [
                new Coordinates(1, 0),
                new Coordinates(-1, 0),
                new Coordinates(0, 1),
                new Coordinates(0, -1),
                new Coordinates(1, 1),
                new Coordinates(-1, 1),
                new Coordinates(1, -1),
                new Coordinates(-1, -1)
            ];

            foreach(Coordinates coords in circle)
            {
                Coordinates circleCoords = currCoords + coords;
                if (circleCoords == lastOption) continue;
                if (board[circleCoords].PanelState == PanelState.ContainsShip)
                {
                    Console.WriteLine(board[circleCoords].PanelState);
                    return true;
                }
            }
            return false;
        }

        public Coordinates GetNextStep(Coordinates direction, Coordinates currentPosition)
        {
            return direction switch
            {
                (0, 1) => new Coordinates(currentPosition.X, currentPosition.Y + 1),
                (1, 0) => new Coordinates(currentPosition.X + 1, currentPosition.Y),
                (-1, 0) => new Coordinates(currentPosition.X - 1, currentPosition.Y),
                (0, -1) => new Coordinates(currentPosition.X, currentPosition.Y - 1),
                _ => throw new Exception("bad random direction")
            };
        }

        private Coordinates GenerateNewCoords() 
        {
            return new Coordinates(random.Next(0, board.board.GetLength(0)), random.Next(0, board.board.GetLength(0)));
        }
    }
}
