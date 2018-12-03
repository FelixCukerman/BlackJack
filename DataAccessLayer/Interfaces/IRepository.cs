using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Abstraction;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task Create(T item);
        Task CreateRange(IEnumerable<T> items);
        Task Update(T item);
        Task UpdateRange(IEnumerable<T> items);
        Task Delete(T t);
        Task DeleteRange(IEnumerable<T> items);
    }
}
