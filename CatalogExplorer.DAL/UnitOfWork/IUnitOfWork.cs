using System;
using System.Data;
using CatalogExplorer.DAL.Config;
using OneView.Common.Repositories;

namespace CatalogExplorer.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ExplorerContext GetContext();
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}
