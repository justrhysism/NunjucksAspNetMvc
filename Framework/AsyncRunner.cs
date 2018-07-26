using System;
using System.Web.Hosting;
using Autofac;
using Autofac.Core.Lifetime;

namespace NunjucksAspNetMvc.Framework
{
	public interface IAsyncRunner
	{
		void Run<T>(Action<T> action);
	}

	public class AsyncRunner : IAsyncRunner
	{
		public ILifetimeScope LifetimeScope { get; set; }

		public AsyncRunner(ILifetimeScope lifetimeScope)
		{
			LifetimeScope = lifetimeScope;
		}

		public void Run<T>(Action<T> action)
		{
			HostingEnvironment.QueueBackgroundWorkItem(ct =>
			{
				// Create a nested container which will use the same dependency
				// registrations as set for HTTP request scopes.
				using (var container = LifetimeScope.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
				{
					var service = container.Resolve<T>();
					action(service);
				}
			});
		}
	}
}