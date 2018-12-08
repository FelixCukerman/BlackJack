using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using EntitiesLayer.Entities;
using System.Data.Entity;

namespace DataAccessLayer.Repositories
{
    public class MoveCardsRepository : IMoveCardsRepository
    {
        private GameContext data;
        public MoveCardsRepository(GameContext data)
        {
            this.data = data;
        }
        public async Task<IEnumerable<MoveCards>> Get()
        {
            return await data.MoveCards.ToListAsync<MoveCards>();
        }
        public async Task<MoveCards> Get(int id)
        {
            return await data.MoveCards.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<MoveCards>> Get(Func<MoveCards, bool> predicate)
        {
            return data.Set<MoveCards>().AsNoTracking().Where(predicate).ToList();
        }
        public async Task Create(MoveCards moveCards)
        {
            data.MoveCards.Add(moveCards);
            await data.SaveChangesAsync();
        }
        public async Task CreateRange(IEnumerable<MoveCards> moveCards)
        {
            data.MoveCards.AddRange(moveCards);
            await data.SaveChangesAsync();
        }
        public async Task Update(MoveCards moveCards)
        {
            data.Entry(moveCards).State = EntityState.Modified;
            await data.SaveChangesAsync();
        }
        public async Task UpdateRange(IEnumerable<MoveCards> moveCards)
        {
            for(int i = 0; i < moveCards.Count(); i++)
            {
                data.Entry(moveCards.ElementAt(i)).State = EntityState.Modified;
            }
            await data.SaveChangesAsync();
        }
        public async Task Delete(MoveCards moveCards)
        {
            data.MoveCards.Remove(moveCards);
            await data.SaveChangesAsync();
        }
        public async Task DeleteRange(IEnumerable<MoveCards> moveCards)
        {
            data.MoveCards.RemoveRange(moveCards);
            await data.SaveChangesAsync(); 
        }
    }
}
