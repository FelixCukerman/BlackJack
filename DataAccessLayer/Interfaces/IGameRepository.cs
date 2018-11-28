using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> Get();
        Task<Game> Get(int id);
        Task Create(Game item);
        Task CreateRange(IEnumerable<Game> items);
        Task Update(Game item);
        Task UpdateRange(IEnumerable<Game> items);
        Task Delete(Game item);
        Task DeleteRange(IEnumerable<Game> items);
    }
}
