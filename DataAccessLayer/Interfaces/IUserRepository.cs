using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get();
        Task<User> Get(int id);
        Task Create(User item);
        Task CreateRange(IEnumerable<User> items);
        Task Update(User item);
        Task UpdateRange(IEnumerable<User> items);
        Task Delete(User item);
        Task DeleteRange(IEnumerable<User> items);
    }
}
