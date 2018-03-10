using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;
using Unity.AspNet.Mvc;

namespace CatalogExplorer.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = UnityConfig.GetConfiguredContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            InitialCatalogStructure();

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private void InitialCatalogStructure()
        {
            string[] paths =
            {
                HostingEnvironment.MapPath("~/Catalogs/Creating Digital Images/Resources/Primary Sources"),
                HostingEnvironment.MapPath("~/Catalogs/Creating Digital Images/Resources/Secondary Sources"),
                HostingEnvironment.MapPath("~/Catalogs/Creating Digital Images/Evidence"),
                HostingEnvironment.MapPath("~/Catalogs/Creating Digital Images/Graphic Products/Process"),
                HostingEnvironment.MapPath("~/Catalogs/Creating Digital Images/Graphic Products/Final Product")
            };

            foreach (var path in paths)
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
    }
}
