using SeaBattleWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleWeb.Data.Repository.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        public Task<IEnumerable<Game>> GetIdleGames();

        public Task AddNewUser(Guid gameId, Guid userId, string connectionId);

        public Task StartGame(Game game);

        public Task RemoveConnectionId(string connectionId);

        public Task ChangeCurrentPlayerId(Guid id);
    }
}
