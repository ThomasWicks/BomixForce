using Bomix_Force.Data.Context;
using Bomix_Force.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bomix_Force.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ModelContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(ModelContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAllNoTracking()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public T GetByIdNoTracking(int id)
        {
            var obj = _dbSet.Find(id);
            _context.Entry(obj).State = EntityState.Detached;
            return obj;
        }

        public void Insert(T obj)
        {
            _dbSet.Add(obj);
        }
        public void InsertFromJob(T obj)
        {
            _dbSet.Add(obj);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
