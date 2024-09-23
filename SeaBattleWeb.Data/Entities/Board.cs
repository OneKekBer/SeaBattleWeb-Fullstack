using SeaBattleWeb.Data.GameLogic.Models.Board;
using SeaBattleWeb.Data.GameLogic.Models.Values;


namespace SeaBattleWeb.Data.Entities
{   
    public class Board
    {
        public Board()
        {
            
        }

        public Board(Guid userId, Guid gameId)
        {
            UserId = userId;
            GameId = gameId;
        }

        public Panel[,] board = new Panel[9, 9];

        public Panel this[Coordinates coords]
        {
            get => board[coords.Y, coords.X];
            set => board[coords.Y, coords.X] = value;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid UserId { get; init; }

        public Guid GameId {  get; init; }
    }
}
