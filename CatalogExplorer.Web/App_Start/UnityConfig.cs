using System;
using CatalogExplorer.DAL;
using CatalogExplorer.DAL.Repositories;
using CatalogExplorer.DAL.UnitOfWork;
using OneView.Common.Repositories;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Lifetime;

namespace CatalogExplorer.Web
{
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();

            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static System.Web.Http.Dependencies.IDependencyResolver GetDependensyResolver()
        {
            var resolver = new UnityHierarchicalDependencyResolver(Container.Value);

            return resolver;
        }

        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ExplorerContext>()
            .RegisterType<IUnitOfWork, UnitOfWork>(new PerThreadLifetimeManager())
            .RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}