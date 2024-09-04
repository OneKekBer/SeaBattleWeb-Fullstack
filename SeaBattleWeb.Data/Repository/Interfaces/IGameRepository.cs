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
    }
}
