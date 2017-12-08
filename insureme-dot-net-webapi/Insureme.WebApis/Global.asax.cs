using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using Insureme.Configuration;
using Insureme.Configuration.Interfaces;
using Insureme.Core.v1.Entities;
using Insureme.DataAccess.Defaults;
using Insureme.DataAccess.Interfaces;
using System.Web;
using Insureme.WebApis.Filters.Logging;

namespace Insureme.WebApis
{
    public static class WebSystem
    {
        public static IContainer Container { get; set; }
        public static string BackEndKey { get; set; }
        public static string ApiKey { get; set; }
        public static bool ApiKeyEnabled { get; set; }
        public static string HostRootUrl { get; set; }
    }


    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(WebSystem.Container = ConfigureContainer());
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiLogHandler());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GetConfiguration(WebSystem.Container);
        }

        public static IContainer ConfigureContainer()
        {
            var cb = new ContainerBuilder();

            cb.RegisterApiControllers(Assembly.GetExecutingAssembly());
            cb.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            cb.RegisterType<Config>().As<IConfig>().SingleInstance();
            cb.RegisterType<DataContext>().As<IDataContext>().InstancePerLifetimeScope();
            cb.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            cb.RegisterType<DataService>().As<IDataService>().InstancePerLifetimeScope();
            
            cb.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            return cb.Build();
        }

        public static void GetConfiguration(IContainer container)
        {
            var dataService = container.Resolve<IDataService>();
            var keysRepository = container.Resolve<IRepository<KeyEntity>>();

            dataService.SetReadCommittedSnapshotIsolation();

            var configuration = dataService.GetDatabaseConfiguration();
            WebSystem.BackEndKey = string.IsNullOrEmpty(configuration.BackEndKey) ? new string('0', 32) : configuration.BackEndKey;
                                                                                                                            
            var key = keysRepository.FindBy(x => x.Name.Equals("WebApp", StringComparison.CurrentCulture)).FirstOrDefault();           
            if (key != null)
            {
                WebSystem.ApiKey = key.Id.ToString();
                WebSystem.ApiKeyEnabled = key.Enabled;
                WebSystem.HostRootUrl = key.HostRootUrl;
            }
            else
            {
                WebSystem.ApiKey = Guid.Empty.ToString();
                WebSystem.ApiKeyEnabled = false;
                WebSystem.HostRootUrl = default(string);
            }
        }
    }
}