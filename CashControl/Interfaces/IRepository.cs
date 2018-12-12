using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashControl.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetById(string id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
        Task Delete(string id);
        Task Save();
    }
}
