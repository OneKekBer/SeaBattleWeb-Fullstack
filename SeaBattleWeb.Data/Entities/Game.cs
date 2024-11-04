using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleWeb.Data.Entities
{
    public enum GameState
    {
        Idle = 0, 
        Active = 1,
        Finished = 2
    }

    public class Game
    {
        public Game()
        {
            
        }
        
        public Guid Id { get; init; } = Guid.NewGuid(); public GameState State { get; set; } = GameState.Idle;
        public Guid FirstPlayerId { get; set; } = Guid.Empty;
        public Guid SecondPlayerId { get; set; } = Guid.Empty;
        public Guid CurrentPlayerId { get; set; } = Guid.Empty;
        public List<string> ConnectionIds { get; set; } = new List<string>();
    }
}
