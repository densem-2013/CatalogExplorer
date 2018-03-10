using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CatalogExplorer.DAL;

namespace OneView.Common.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// Summary description for IRepositoryGeneric
    /// </summary>
    public interface IRepository<T> : IDisposable where T : class
    {
        ExplorerContext Context { get; }
        IEnumerable<T> GetAll();
        T Get(params object[] ids);

        T FirstOrDefault(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null);

        T LastOrDefault(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null);

        IQueryable<T> Select(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            int? page = null,
            int? pageSize = null);

        Task<IEnumerable<T>> SelectAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            int? page = null,
            int? pageSize = null);

        bool Any(Expression<Func<T, bool>> filter = null);
        void Create(T item);
        void InsertRange(IEnumerable<T> items);
        void Update(T item);
        void Delete(params object[] ids);
        void Delete(T item);
        void DeleteRange(IEnumerable<T> items);
        int Save();
    }
}