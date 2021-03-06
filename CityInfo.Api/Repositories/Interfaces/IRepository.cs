using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T insert(T t);
        T update(T t);
        bool Delete(int id);
        ICollection<T> GetAll();
        T GetById(int id);
    }
}
