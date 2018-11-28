using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IMoveRepository
    {
        Task<IEnumerable<Move>> Get();
        Task<Move> Get(int id);
        Task Create(Move item);
        Task CreateRange(IEnumerable<Move> items);
        Task Update(Move item);
        Task UpdateRange(IEnumerable<Move> items);
        Task Delete(Move item);
        Task DeleteRange(IEnumerable<Move> items);
    }
}
