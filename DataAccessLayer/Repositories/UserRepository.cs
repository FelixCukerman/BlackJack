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
    public class UserRepository : IRepository<User>
    {
        private GameContext data;
        public UserRepository(GameContext data)
        {
            this.data = data;
        }
        public async Task<IEnumerable<User>> Get()
        {
            return await data.Users.ToListAsync<User>();
        }
        public async Task<User> Get(int id)
        {
            return await data.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Create(User user)
        {
            data.Users.Add(user);
            await data.SaveChangesAsync();
        }
        public async Task Update(User user)
        {
            var item = await data.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            item.Nickname = user.Nickname;
            item.UserRole = user.UserRole;
            item.DiscardPile = user.DiscardPile;
            await data.SaveChangesAsync();
        }
        public async Task UpdateRange(IEnumerable<User> users)
        {
            var userList = users.ToList();
            for (int i = 0; i < users.Count(); i++)
            {
                var item = await data.Users.FirstOrDefaultAsync(x => x.Id == userList[i].Id);
                item.Nickname = userList[i].Nickname;
                item.UserRole = userList[i].UserRole;
                item.DiscardPile = userList[i].DiscardPile;
                await data.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var user = await data.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
                data.Users.Remove(user);
            await data.SaveChangesAsync();
        }
    }
}
