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
            board = new List<List<Panel>>(9);
        }

        public Board(Guid userId, Guid gameId) : this()
        {
            UserId = userId;
            GameId = gameId;
        }

        [NotMapped]
        public List<List<Panel>> board { get; set; }

        public Panel this[Coordinates coords]
        {
            get => board[coords.Y][coords.X];
            set => board[coords.Y][coords.X] = value;
        }

        public void SerializeArray()
        {
            BoardSerialized = JsonSerializer.Serialize(board);
        }

        public void DeserializeArray()
        {
            if (!string.IsNullOrEmpty(BoardSerialized))
            {
                board = JsonSerializer.Deserialize<List<List<Panel>>>(BoardSerialized);
            }
        }

        public string BoardSerialized { get; set; }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid UserId { get; init; }

        public Guid GameId { get; init; }
    }
}
