using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using EntitiesLayer.Entities;

namespace BusinessLogicLayer.Providers
{
    public class DeckProvider
    {
        public Deck Get()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("key") as Deck;
        }

        public bool Add(Deck deck)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("key", deck, DateTime.Now.AddMinutes(30));
        }

        public void Update(Deck deck)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set("key", deck, DateTime.Now.AddMinutes(30));
        }

        public void Delete()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("key"))
            {
                memoryCache.Remove("key");
            }
        }
    }
}
