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
    public class CardRepository : IRepository<Card>
    {
        private GameContext data;
        public CardRepository(GameContext data)
        {
            this.data = data;
        }
        public async Task<IEnumerable<Card>> Get()
        {
            return await data.Cards.ToListAsync<Card>();
        }
        public async Task<Card> Get(int id)
        {
            return await data.Cards.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Create(Card card)
        {
            data.Cards.Add(card);
            await data.SaveChangesAsync();
        }
        public async Task Update(int id, Card card)
        {
            var item = await data.Cards.FirstOrDefaultAsync(x => x.Id == id);
            item.Suit = card.Suit;
            item.User = item.User;
            item.Value = item.Value;
            await data.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var card = await data.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
                data.Cards.Remove(card);
            await data.SaveChangesAsync();
        }
    }
}