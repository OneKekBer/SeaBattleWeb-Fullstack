using System;
using System.Collections.Generic;
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
        
        public Guid Id { get; init; } = Guid.NewGuid();

        public GameState State { get; set; } = GameState.Idle;
    }
}
