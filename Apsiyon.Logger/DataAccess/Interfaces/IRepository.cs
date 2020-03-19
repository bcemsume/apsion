using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Apsiyon.Logger.DataAccess
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IEnumerable<T> All();
    }
}