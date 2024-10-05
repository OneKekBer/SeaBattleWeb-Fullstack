using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.GameLogic.Models.Board;
using SeaBattleWeb.Data.GameLogic.Models.Values;
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
    public class ShipPlacer
    {
        Random random = new Random();
        //List<Coordinates> allShipsPositions = new List<Coordinates>();

        public void FillEmptyBoard(Board board) // пиздец 
        {
            for (int i = 0; i < board.board.GetLength(0); i++)
            {
                for (int j = 0; j < board.board.GetLength(1); j++)
                {
                    board.board[i, j] = new Panel();
                }
            }
        }

        public void ShootToPanel(Board board, Coordinates coords)
        {
            var panel = board.board[coords.X, coords.Y];

            if (panel.PanelState == PanelState.ContainsShip)
            {
                panel.RegisterShot();
            }


        }

        public void AddShipsToBoard(Board board, IEnumerable<Coordinates> coords, Ship ship)
        {
            foreach(var coord in coords)
            {
                board.board[coord.Y, coord.X].PlaceShip(ship);
                
            }
        }

        public IEnumerable<Coordinates> GetShipCoordinates(Board board,int shipSize)
        {
            Coordinates[] directions = {
                new Coordinates(0, 1),  // Right
                new Coordinates(1, 0),  // Down
                new Coordinates(-1, 0), // Up
                new Coordinates(0, -1)  // Left
            };

            const int boardLength = 9;
            var shipCoordinates = new List<Coordinates>();

            for (int attempt = 0; attempt < 50; attempt++)
            {
                var startCoords = new Coordinates(random.Next(0, boardLength - 1), random.Next(0, boardLength - 1));

                foreach (var direction in directions)
                {
                    shipCoordinates.Clear();
                    shipCoordinates.Add(startCoords);
                    bool isPossibleToPlaceShip = true;

                    for (int i = 1; i < shipSize; i++) 
                    {
                        var lastCoord = shipCoordinates.Last();
                        var newCoord = new Coordinates(lastCoord.X + direction.X, lastCoord.Y + direction.Y);

                        if (newCoord.X >= boardLength || newCoord.X < 0 || newCoord.Y >= boardLength || newCoord.Y < 0)
                        {
                            isPossibleToPlaceShip = false;
                            break; 
                        }

                        if (board.board[newCoord.Y, newCoord.X].PanelState != PanelState.Empty)
                        {
                            isPossibleToPlaceShip = false;
                            break;
                        }
                        

                        shipCoordinates.Add(newCoord);
                    }

                    if (isPossibleToPlaceShip && shipCoordinates.Count == shipSize)
                    {
                        return shipCoordinates;
                    }
                }
            }

            throw new Exception("Impossible to place ship!!");
        }


        public IEnumerable<Coordinates> GetShipCoordinates(int shipSize)
        {
            Coordinates[] directions = {
                new Coordinates(0, 1),  // Right
                new Coordinates(1, 0),  // Down
                new Coordinates(-1, 0), // Up
                new Coordinates(0, -1)  // Left
            };

            const int boardLength = 9;
            var shipCoordinates = new List<Coordinates>();

            for (int attempt = 0; attempt < 50; attempt++)
            {
                var startCoords = new Coordinates(random.Next(0, boardLength - 1), random.Next(0, boardLength - 1));

                foreach (var direction in directions)
                {
                    shipCoordinates.Clear();
                    shipCoordinates.Add(startCoords);
                    bool isPossibleToPlaceShip = true;

                    for (int i = 1; i < shipSize; i++)
                    {
                        var lastCoord = shipCoordinates.Last();
                        var newCoord = new Coordinates(lastCoord.X + direction.X, lastCoord.Y + direction.Y);

                        if (newCoord.X >= boardLength || newCoord.X < 0 || newCoord.Y >= boardLength || newCoord.Y < 0)
                        {
                            isPossibleToPlaceShip = false;
                            break;
                        }

                        shipCoordinates.Add(newCoord);
                    }

                    if (isPossibleToPlaceShip && shipCoordinates.Count == shipSize)
                    {
                        return shipCoordinates;
                    }
                }
            }

            throw new Exception("Impossible to place ship!!");
        }

        

        //public List<Coordinates> GetShipOptions(Ship ship)
        //{
        //    Coordinates[] directions = {
        //        new Coordinates(0, 1),
        //        new Coordinates(1, 0),
        //        new Coordinates(-1, 0),
        //        new Coordinates(0, -1)
        //    };//possibile ship directions 

        //    var options = new List<Coordinates>();

        //    bool isShipPossibleToPlace = false;
        //    while (!isShipPossibleToPlace)
        //    {
        //        Coordinates randomCoords = GenerateNewCoords();
        //        options.Add(randomCoords);

        //        //getting random direction
        //        int randomDirectionIndex = random.Next(0, directions.Length);
        //        Coordinates direction = directions[randomDirectionIndex];
        //        var (dirX, dirY) = direction;
        //        Console.WriteLine("direction " + dirX + " " + dirY);

        //        int i = 1;
        //        while (true)
        //        {
        //            try
        //            {
        //                if (i == ship.Size)
        //                {
        //                    isShipPossibleToPlace = true;
        //                    break;
        //                }

        //                if (board[options.Last()].PanelState == PanelState.ContainsShip)
        //                    throw new Exception("impossible to place ship");

        //                var nextCoords = GetNextStep(direction, options.Last());

        //                if (IsShipAround(options.Last(), nextCoords))
        //                    throw new Exception("impossible to place ship");

        //                options.Add(nextCoords);
        //                Console.WriteLine("next coords " + nextCoords);
        //                i++;
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e.Message);
        //                options.Clear();
        //                i = 1;
        //                break;
        //            }
        //        }
        //    }
        //    return options;
        //}
        //приудмать условие выхода чтобы не взорвать ноут

        //public bool IsShipAround(Coordinates lastOption, Coordinates currCoords)
        //{
        //    Coordinates[] circle = [
        //        new Coordinates(1, 0),
        //        new Coordinates(-1, 0),
        //        new Coordinates(0, 1),
        //        new Coordinates(0, -1),
        //        new Coordinates(1, 1),
        //        new Coordinates(-1, 1),
        //        new Coordinates(1, -1),
        //        new Coordinates(-1, -1)
        //    ];

        //    foreach(Coordinates coords in circle)
        //    {
        //        Coordinates circleCoords = currCoords + coords;
        //        if (circleCoords == lastOption) continue;
        //        if (board[circleCoords].PanelState == PanelState.ContainsShip)
        //        {
        //            Console.WriteLine(board[circleCoords].PanelState);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public Coordinates GetNextStep(Coordinates direction, Coordinates currentPosition)
        //{
        //    return direction switch
        //    {
        //        (0, 1) => new Coordinates(currentPosition.X, currentPosition.Y + 1),
        //        (1, 0) => new Coordinates(currentPosition.X + 1, currentPosition.Y),
        //        (-1, 0) => new Coordinates(currentPosition.X - 1, currentPosition.Y),
        //        (0, -1) => new Coordinates(currentPosition.X, currentPosition.Y - 1),
        //        _ => throw new Exception("bad random direction")
        //    };
        //}

        //private Coordinates GenerateNewCoords() 
        //{
        //    return new Coordinates(random.Next(0, board.board.GetLength(0)), random.Next(0, board.board.GetLength(0)));
        //}
    }
}
