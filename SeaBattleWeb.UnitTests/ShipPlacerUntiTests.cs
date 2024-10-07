using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.GameLogic.Models.Board;
using SeaBattleWeb.Data.GameLogic.Models.Values;
using SeaBattleWeb.GameLogic.Components;
using SeaBattleWeb.GameLogic.Models;
using Xunit.Abstractions;

namespace SeaBattleWeb.UnitTests
{
    public class ShipPlacerUntiTests
    {
        private readonly ITestOutputHelper _output;

        public ShipPlacerUntiTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void FillEmptyBoard_WhenInitializeNewBoard_BoardContainsEmptyPanels()
        {
            //Arrange
            var board = new Board();
            var shipPlacer = new ShipPlacer();

            //Act
            shipPlacer.FillEmptyBoard(board);

            //Assert
            Assert.IsType<Panel>(board[new Coordinates(5, 5)]);
            Assert.IsType<Panel>(board[new Coordinates (0, 0)]);

            // Check if the entire board is filled with empty panels
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Assert.NotNull(board[new Coordinates(i, j)]);
                    Assert.Equal(PanelState.Empty, board[new Coordinates(i, j)].PanelState);
                }
            }
        }


        [Fact]
        public void GetShipCoordinates_WhenCorrectShipLength_GetRandomValidCoordinates()
        {
            //Arrange
            var board = new Board();
            var shipPlacer = new ShipPlacer();

            //Act
            var coordinates = shipPlacer.GetShipCoordinates(5);
            int coordinatesLength = coordinates.Count();

            //Assert
            foreach (var item in coordinates)
            {
                _output.WriteLine($"{item.X} {item.Y}");
            }

            Assert.Equal(5, coordinatesLength);
        }

        [Fact]
        public void AddShipsToBoard_WhenIntitalizeNewBoard_BoardContainsEmptyPanels()
        {
            //Arrange
            var board = new Board();
            var shipPlacer = new ShipPlacer();
            var cruser = new Cruiser();

            //Act
            shipPlacer.FillEmptyBoard(board);
            var coords = shipPlacer.GetShipCoordinates(cruser.Size);
            shipPlacer.AddShipsToBoard(board, coords, cruser);

            //Assert
            foreach (var coord in coords)
            {
                Assert.Equal(PanelState.ContainsShip, board[coord].PanelState);
                _output.WriteLine($"{coord.Y} {coord.X} state:{board[coord].PanelState.ToString()}");
            }
        }

        public void ShootToTitle()
        {
            //Arrange
            var board = new Board();
            var shipPlacer = new ShipPlacer();
            var cruser = new Cruiser();

            //Act
            shipPlacer.FillEmptyBoard(board);
            var coords = shipPlacer.GetShipCoordinates(cruser.Size);
            shipPlacer.AddShipsToBoard(board, coords, cruser);


            foreach (var coord in coords)
            {
                Assert.Equal(PanelState.ContainsShip, board[coord].PanelState);
                _output.WriteLine($"{coord.Y} {coord.X} state:{board[coord].PanelState.ToString()}");
            }

        }
    }
}
