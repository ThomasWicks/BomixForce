using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bomix_Force.Service.Interface
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllNoTracking();
        T GetById(int id);
        T GetByIdNoTracking(int id);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
        Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null,
        string includeProperties = "");
        List<string> Insert(T obj);
        List<string> InsertFromJob(T obj);
        List<string> Update(T obj);
        List<string> Delete(int id);
        bool Exists(int id);
    }
}
