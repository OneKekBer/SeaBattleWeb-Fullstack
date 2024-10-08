using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.GameLogic.Models.Board;
using SeaBattleWeb.Data.GameLogic.Models.Values;
using SeaBattleWeb.GameLogic.Models.Abstracts;
using System;


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
      
        public void FillEmptyBoard(Board board) // пиздец 
        {
            for (int i = 0; i < board.board.GetLength(0); i++)
            {
                for (int j = 0; j < board.board.GetLength(1); j++)
                {
                    board[new Coordinates(i, j)] = new Panel();
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
                board[coord].PlaceShip(ship);
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

                        if (board[newCoord].PanelState != PanelState.Empty)
                        {
                            isPossibleToPlaceShip = false;
                            break;
                        }

                        if(IsShipAround(board, lastCoord, newCoord))
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

        public bool IsShipAround(Board board, Coordinates lastOption, Coordinates currCoords)
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

            foreach (Coordinates coords in circle)
            {
                Coordinates circleCoords = currCoords + coords;

                if (circleCoords.X >= board.board.GetLength(0) || circleCoords.X < 0 || circleCoords.Y >= board.board.GetLength(0) || circleCoords.Y < 0)
                    continue;
                
                if (circleCoords == lastOption) continue;
                if (board[circleCoords].PanelState == PanelState.ContainsShip)
                {
                    Console.WriteLine(board[circleCoords].PanelState);
                    return true;
                }
            }
            return false;
        }
    }
}
