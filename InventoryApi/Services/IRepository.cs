using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Services
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        long New(T obj);
        void Delete(T obj);
        bool Update(T obj);
        bool Exists(int id);
        bool Save();
    }
}
