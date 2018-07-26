using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.WebApi;
using NunjucksAspNetMvc.Services;

namespace NunjucksAspNetMvc
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			var config = GlobalConfiguration.Configuration;
			
			var services = new ServiceCollection();

			services.AddNodeServices();

			var builder = new ContainerBuilder();

			builder.Populate(services);

			// Register your MVC controllers. (MvcApplication is the name of
			// the class in Global.asax.)
			builder.RegisterControllers(typeof(Global).Assembly);

			// OPTIONAL: Register model binders that require DI.
			//builder.RegisterModelBinders(typeof(Global).Assembly);
			//builder.RegisterModelBinderProvider();

			// OPTIONAL: Register web abstractions like HttpContextBase.
			builder.RegisterModule<AutofacWebTypesModule>();

			// OPTIONAL: Enable property injection in view pages.
			builder.RegisterSource(new ViewRegistrationSource());

			// OPTIONAL: Enable property injection into action filters.
			//builder.RegisterFilterProvider();

			// OPTIONAL: Enable action method parameter injection (RARE).
			//builder.InjectActionInvoker();

			builder.RegisterType<NunjucksService>().As<INunjucksService>();
			
			// Set the dependency resolver to be Autofac.
			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
