using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using OneView.Common.Repositories;

namespace CatalogExplorer.DAL.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// Summary description for Repository
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Private Fields

        public ExplorerContext Context { get; }
        private readonly DbSet<T> _dbSet;

        #endregion Private Fields

        public Repository(ExplorerContext db)
        {
            Context = db;
            _dbSet = Context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        
        public T Get(params object[] ids)
        {
            return _dbSet.Find(ids);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return filter != null ? query.FirstOrDefault(filter) : query.FirstOrDefault();
        }

        public T LastOrDefault(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return filter != null ? query.LastOrDefault(filter) : query.LastOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return filter != null ? await query.FirstOrDefaultAsync(filter) : await query.FirstOrDefaultAsync();
        }
        public IQueryable<T> Select(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        public async Task<IEnumerable<T>> SelectAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }
        public bool Any(Expression<Func<T, bool>> filter = null)
        {
            return _dbSet.Any(filter);
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public void InsertRange(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
        }
        public void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(params object[] ids)
        {
            T item = _dbSet.Find(ids);
            if (item != null)
                _dbSet.Remove(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }
        public int Save()
        {
            return Context.SaveChanges();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}