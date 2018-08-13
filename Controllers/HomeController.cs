using System;
using System.Web.Hosting;
using System.Web.Mvc;
using NunjucksAspNetMvc.Services;

namespace NunjucksAspNetMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly INunjucksService _nunjucksService;

		public HomeController(INunjucksService nunjucksService)
		{
			_nunjucksService = nunjucksService;
		}
		
		public ActionResult Index()
		{
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";

			return View();
		}
		
		public ActionResult Nunjucks()
		{
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";

			// Get Nunjucks
			var result = _nunjucksService.RenderSync(
			    "button.njk", 
				new { Type = "button", Label = "Button Now " + DateTime.Now.ToShortTimeString() }
			);
			ViewData["Nunjucks"] = result;
			
			return View();
		}
		
		
		//public ActionResult NunjucksSpeedTest()
		//{
		//	var mvcName = typeof(Controller).Assembly.GetName();
		//	var isMono = Type.GetType("Mono.Runtime") != null;

		//	ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
		//	ViewData["Runtime"] = isMono ? "Mono" : ".NET";

		//	// Get Nunjucks 1
		//	var timeStart1 = DateTime.Now.Ticks;
		//	var result1 = _nunjucksService.RenderSync(
		//		"<p>Time 1: {{time}} <i>(Templated by Nunjucks)</i></p>", 
		//		new { Time = DateTime.Now.ToShortTimeString() }
		//	);
		//	var timeEnd1 = DateTime.Now.Ticks;
		//	ViewData["Nunjucks1"] = result1;
		//	var timeSpan1 = TimeSpan.FromTicks(timeEnd1 - timeStart1);
		//	ViewData["RenderTime1"] = timeSpan1.Milliseconds + "ms";

		//	// Get Nunjucks 2
		//	var timeStart2 = DateTime.Now.Ticks;
		//	var result2 = _nunjucksService.RenderSync(
		//		"<p>Time 2: {{time}} <i>(Templated by Nunjucks)</i></p>", 
		//		new { Time = DateTime.Now.ToShortTimeString() }
		//	);
		//	var timeEnd2 = DateTime.Now.Ticks;
		//	ViewData["Nunjucks2"] = result2;
		//	var timeSpan2 = TimeSpan.FromTicks(timeEnd2 - timeStart2);
		//	ViewData["RenderTime2"] = timeSpan2.Milliseconds + "ms";
			
		//	return View();
		//}
	}
}
