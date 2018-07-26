using System.Threading.Tasks;

namespace NunjucksAspNetMvc.Services
{
	public interface INunjucksService
	{
		Task<string> Render(string template, object data);
		string RenderSync(string template, object data);
	}
}
