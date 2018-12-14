using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using System.Runtime.Caching;
using EntitiesLayer.Entities;

namespace BusinessLogicLayer.Providers
{
    public class HandCardsProvider
    {
        public HandCards Get(User user)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(user.Nickname) as HandCards;
        }

        public List<HandCards> GetAll(List<User> users)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            var handUsers = new List<HandCards>();
            for(int i = 0; i < users.Count; i++)
            {
                handUsers.Add(memoryCache.Get(users[i].Nickname) as HandCards);
            }
            return handUsers;
        }

        public bool Add(HandCards handCards)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(handCards.User.Nickname, handCards, DateTime.Now.AddMinutes(30));
        }

        public void Update(HandCards handCards)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(handCards.User.Nickname, handCards, DateTime.Now.AddMinutes(30));
        }

        public void Delete(User user)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(user.Nickname))
            {
                memoryCache.Remove(user.Nickname);
            }
        }
    }
}
