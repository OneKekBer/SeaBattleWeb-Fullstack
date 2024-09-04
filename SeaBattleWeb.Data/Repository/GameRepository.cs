using Microsoft.EntityFrameworkCore;
using SeaBattleWeb.Data.Context;
using SeaBattleWeb.Data.Entities;
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

        public Task<IEnumerable<Game>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Game> GetById(Guid id)
        {
            var game = await _apiDatabase.Games.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Game repository, cant find by id: " + id);

            return game;
        }

        public async Task<IEnumerable<Game>> GetIdleGames()
        {
            var games = await _apiDatabase.Games.Where(item => item.State == GameState.Idle).AsNoTracking().ToListAsync();

            return games;
        }

        public async Task Remove(Game entity)
        {
            throw new NotImplementedException();
        }
    }
}
