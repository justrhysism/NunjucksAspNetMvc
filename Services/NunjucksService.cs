using System;
using System.Threading.Tasks;
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
			var result = await _nodeServices.InvokeAsync<string>(
				"./Node/renderNunjucks", template, 
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