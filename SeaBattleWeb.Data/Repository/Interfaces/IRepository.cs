using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleWeb.Data.Repository.Interfaces
{
    public interface IRepository<T>
    {
        public Task Add(T entity);

        public Task Remove(T entity);

        public Task<T> GetById(Guid id);

        public Task<IEnumerable<T>> GetAll();
    }
}
