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
        
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();
        public GameState State { get; set; } = GameState.Idle;
        public Guid CurrentPlayerId { get; set; } = Guid.Empty;

        public Guid FirstPlayerId { get; set; } = Guid.NewGuid();

        public Guid SecondPlayerId { get; set; } = Guid.NewGuid();
        
        public string FirstPlayerConnectionId { get; set; } = String.Empty;

        public string SecondPlayerConnectionId { get; set; } = String.Empty;

    }
    
}
