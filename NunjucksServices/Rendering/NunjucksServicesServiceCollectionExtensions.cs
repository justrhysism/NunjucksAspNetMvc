using NunjucksServices.Rendering;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up prerendering features in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class NunjucksServicesServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the dependency injection system to supply an implementation
        /// of <see cref="INunjucksRenderer"/>.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        public static void AddSpaPrerenderer(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddSingleton<INunjucksRenderer, DefaultNunjucksRenderer>();
        }
    }
}
