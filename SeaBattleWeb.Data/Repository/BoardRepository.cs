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
    public class BoardRepository : IBoardRepository
    {
        private readonly AppDatabaseContext _apiDatabase;
        public BoardRepository(AppDatabaseContext database)
        {
            _apiDatabase = database;
        }

        public async Task Add(Board entity)
        {
            entity.SerializeArray();
            await _apiDatabase.Boards.AddAsync(entity);
            await _apiDatabase.SaveChangesAsync();
        }

        public Task<IEnumerable<Board>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Board> GetById(Guid id)
        {
            var board = await _apiDatabase.Boards.FirstOrDefaultAsync((x) => x.Id == id) ?? throw new Exception($"board GetById not found with id:{id}");
            board.DeserializeArray();
            return board;
        }

        public Task Remove(Board entity)
        {
            throw new NotImplementedException();
        }
    }
}
