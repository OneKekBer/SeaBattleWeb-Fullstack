using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.GameLogic.Models.Board;
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
        public void FillEmptyBoard_WhenIntitalizeNewBoard_BoardContainsEmptyPanels()
        {
            //Arrange
            var board = new Board();
            var shipPlacer = new ShipPlacer(board);
            
            //Act
            shipPlacer.FillEmptyBoard(board);

            //Assert
            Assert.IsType<Panel>(board.board[5,5]);
            Assert.IsType<Panel>(board.board[0, 0]);

            Assert.Equal(PanelState.Empty, board.board[0, 0].PanelState);
        }

        [Fact]
        public void GetShipCoordinates_WhenCorrectShipLength_GetRandomValidCoordinates()
        {
            //Arrange
            var board = new Board();
            var shipPlacer = new ShipPlacer(board);

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
            var shipPlacer = new ShipPlacer(board);
            var cruser = new Cruiser();

            //Act
            shipPlacer.FillEmptyBoard(board);
            var coords = shipPlacer.GetShipCoordinates(cruser.Size);
            shipPlacer.AddShipsToBoard(coords, cruser);

            //Assert
            foreach (var coord in coords) 
            {
                Assert.Equal(PanelState.ContainsShip, board.board[coord.Y, coord.X].PanelState);
                _output.WriteLine($"{coord.Y} {coord.X}");
            }
        }

    }
}
