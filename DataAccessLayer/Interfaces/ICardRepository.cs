using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> Get();
        Task<Card> Get(int id);
        Task Create(Card item);
        Task CreateRange(IEnumerable<Card> items);
        Task Update(Card item);
        Task UpdateRange(IEnumerable<Card> items);
        Task Delete(Card item);
        Task DeleteRange(IEnumerable<Card> items);
    }
}
