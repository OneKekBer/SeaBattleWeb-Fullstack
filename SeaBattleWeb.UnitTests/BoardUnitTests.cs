using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.GameLogic.Models.Board;
using SeaBattleWeb.Data.GameLogic.Models.Values;
using SeaBattleWeb.GameLogic.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SeaBattleWeb.UnitTests
{
    public class BoardUnitTests
    {
        private readonly ITestOutputHelper _output;

        public BoardUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void SerializeAndDeseialize_WhenDeserializeAndSerializeBoard_BoardIsWorkingCorrectly()
        {
            //Arrange
            var board = new Board();
            var shipPlacer = new ShipPlacer();

            //Act
            shipPlacer.FillEmptyBoard(board);

            board.SerializeArray();

            board.DeserializeArray();
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
    }
}
