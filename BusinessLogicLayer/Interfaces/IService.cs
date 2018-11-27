using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task Create(T t);
        Task Update(T t);
        Task UpdateRange(List<T> items);
        Task Delete(int id);
    }
}
