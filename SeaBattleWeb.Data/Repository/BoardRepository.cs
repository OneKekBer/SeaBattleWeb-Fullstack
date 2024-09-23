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
    public class BoardRepository : IBoardRepositroy
    {
        private readonly AppDatabaseContext _apiDatabase;
        public BoardRepository(AppDatabaseContext database)
        {
            _apiDatabase = database;
        }

        public async Task Add(Board entity)
        {
            await _apiDatabase.Boards.AddAsync(entity);
            await _apiDatabase.SaveChangesAsync();

            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Board> GetById(Guid id)
        {
            var board = await _apiDatabase.Boards.SingleAsync((x) => x.Id == id) ?? throw new Exception("");

            return board;
        }

        public Task Remove(Board entity)
        {
            throw new NotImplementedException();
        }
    }
}
