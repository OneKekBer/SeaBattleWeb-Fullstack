using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SeaBattleWeb.Data.Context;
using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.Exceptions;
using SeaBattleWeb.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleWeb.Data.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDatabaseContext _apiDatabase;
        public GameRepository(AppDatabaseContext database)
        {
            _apiDatabase = database;
        }

        public async Task Add(Game entity)
        { 
            await _apiDatabase.Games.AddAsync(entity);
            await _apiDatabase.SaveChangesAsync();
        }

        public async Task AddNewUser(Guid gameId, Guid userId, string connectionId)
        {
            var game = await GetById(gameId);
            
            if (game.FirstPlayerId == Guid.Empty)
            {
                game.FirstPlayerId = userId;
                game.FirstPlayerConnectionId = connectionId;
            }
            else if (game.SecondPlayerId == Guid.Empty)
            {
                game.SecondPlayerId = userId;
                game.SecondPlayerConnectionId = connectionId;
            }
            else
                throw new ArgumentException("Cannot add game more than two players");//custom exception please!!
            
            await _apiDatabase.SaveChangesAsync();
        }

        public Task<IEnumerable<Game>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Game> GetById(Guid id)
        {
            var game = await _apiDatabase.Games.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundInDatabaseException("Game repository, cant find by id: " + id);

            return game;
        }

        public async Task<IEnumerable<Game>> GetIdleGames()
        {
            var games = await _apiDatabase.Games.Where(item => item.Status == GameStatus.Idle).AsNoTracking().Take(20).ToListAsync();

            return games;
        }

        public async Task ChangeCurrentPlayerId(Guid id) // q: should i use repositories for this kind of operations where 
        {
            var game = await GetById(id);
            // game.ChangeCurrentPlayerId();
            await _apiDatabase.SaveChangesAsync();
        }

        public async Task Remove(Game entity)
        {
            throw new NotImplementedException();
        }

        public async Task StartGame(Game game)
        {
            game.Status = GameStatus.Active;

            await _apiDatabase.SaveChangesAsync();
        }

        public Task RemoveConnectionId(string connectionId)
        {
            throw new NotImplementedException();
        }
    }
}
