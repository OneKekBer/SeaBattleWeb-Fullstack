using SeaBattleWeb.Data.GameLogic.Models.Board;
using SeaBattleWeb.Data.GameLogic.Models.Values;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SeaBattleWeb.Data.Entities
{
    public class Board
    {
        public Board()
        {

        }

        public Board(Guid userId, Guid gameId) : this()
        {
            UserId = userId;
            GameId = gameId;
        }

        [NotMapped]
        public Panel[,] board = new Panel[9, 9];

        public Panel this[Coordinates coords]
        {
            get => board[coords.X, coords.Y];
            set => board[coords.X, coords.Y] = value;
        }

        public void SerializeArray()
        {
            var boardJaggedArray = new Panel[9][];
            for (int i = 0; i < 9; i++)
            {
                boardJaggedArray[i] = new Panel[9];
                for (int j = 0; j < 9; j++)
                {
                    boardJaggedArray[i][j] = board[i, j];
                }
            }

            BoardSerialized = JsonSerializer.Serialize(boardJaggedArray);
        }

        public void DeserializeArray()
        {
            if (!string.IsNullOrEmpty(BoardSerialized))
            {
                var boardJaggedArray = JsonSerializer.Deserialize<Panel[][]>(BoardSerialized);

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        board[i, j] = boardJaggedArray[i][j];
                    }
                }
            }
        }

        public string BoardSerialized { get; set; }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid UserId { get; init; }

        public Guid GameId { get; init; }
    }
}
