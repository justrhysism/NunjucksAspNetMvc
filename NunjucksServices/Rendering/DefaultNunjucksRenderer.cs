using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Hosting;

namespace NunjucksServices.Rendering
{
    /// <summary>
    /// Default implementation of a DI service that provides convenient access to
    /// server-side prerendering APIs. This is an alternative to prerendering via
    /// the asp-prerender-module tag helper.
    /// </summary>
    internal class DefaultNunjucksRenderer : INunjucksRenderer
    {
        private readonly string _applicationBasePath;
        private readonly CancellationToken _applicationStoppingToken;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INodeServices _nodeServices;

        public DefaultNunjucksRenderer(
            INodeServices nodeServices,
            IApplicationLifetime applicationLifetime,
            IHostingEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _applicationBasePath = hostingEnvironment.ContentRootPath;
            _applicationStoppingToken = applicationLifetime.ApplicationStopping;
            _httpContextAccessor = httpContextAccessor;
            _nodeServices = nodeServices;
        }

        public Task<string> RenderToString(
            string moduleName,
            string exportName = null,
            object customDataParameter = null,
            int timeoutMilliseconds = default(int))
        {
            return Renderer.RenderToString(
                _applicationBasePath,
                _nodeServices,
                _applicationStoppingToken,
                new JavaScriptModuleExport(moduleName) { ExportName = exportName },
                _httpContextAccessor.HttpContext,
                customDataParameter,
                timeoutMilliseconds);
        }
    }
}
