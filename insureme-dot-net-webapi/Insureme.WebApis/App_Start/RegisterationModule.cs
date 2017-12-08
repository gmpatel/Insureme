using Autofac;
using Insureme.Configuration;
using Insureme.Configuration.Interfaces;

namespace Insureme.WebApis
{
    public class RegisterationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Config>().As<IConfig>().SingleInstance();
        }
    }
}