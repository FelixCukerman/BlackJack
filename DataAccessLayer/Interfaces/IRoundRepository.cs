using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IRoundRepository
    {
        Task<IEnumerable<Round>> Get();
        Task<Round> Get(int id);
        Task Create(Round item);
        Task CreateRange(IEnumerable<Round> items);
        Task Update(Round item);
        Task UpdateRange(IEnumerable<Round> items);
        Task Delete(Round item);
        Task DeleteRange(IEnumerable<Round> items);
    }
}
