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

        public void ChangeCurrentPlayerId()
        {
            CurrentPlayerId =  CurrentPlayerId == FirstPlayer.PLayerId ? FirstPlayer.PLayerId : SecondPlayer.PLayerId;
        } 
        
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid(); public GameState State { get; set; } = GameState.Idle;
        public Player FirstPlayer { get; set; } = new Player();
        public Player SecondPlayer { get; set; } = new Player();
        public Guid CurrentPlayerId { get; set; } = Guid.Empty;
        
        
    }

    public class Player
    {
        public Player()
        {
            
        }
        
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid PLayerId { get; set; } = Guid.Empty;
        
        public string ConnectionId { get; set; } = String.Empty;
    }
}
