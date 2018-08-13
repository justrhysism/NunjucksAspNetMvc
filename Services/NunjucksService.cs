using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNetCore.NodeServices;
using NunjucksAspNetMvc.Helpers;

namespace NunjucksAspNetMvc.Services
{
	public class NunjucksService : INunjucksService
	{
		private readonly INodeServices _nodeServices;

		public NunjucksService(INodeServices nodeServices)
		{
			_nodeServices = nodeServices;
		}

		public async Task<string> Render(string template, object data)
		{
		    var scriptPath = HostingEnvironment.MapPath("~/Node/renderNunjucks");
		    var templateDirectory = HostingEnvironment.MapPath("~/Content/templates");


			var result = await _nodeServices.InvokeAsync<string>(
				scriptPath, 
				template, 
				templateDirectory,
				data
			);
			
			return result;
		}

		public string RenderSync(string template, object data)
		{
			return AsyncHelper.RunSync(() => Render(template, data));
		}
	}
}