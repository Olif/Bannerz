using Api.Domain;
using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // The repository should be a singleton
            builder.RegisterType<InMemoryBannerRepository>().As<IBannerRepository>().SingleInstance();
            builder.RegisterType<W3CHtmlValidator>().As<IHtmlValidator>();
            builder.RegisterType<BannerIdGenerator>().As<IIdGenerator>().SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
