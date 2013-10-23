using System.Web.Mvc;

namespace Sc2TutsBase.Areas.administration
{
	public class administrationAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "administration";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"administration_default",
				"administration/{controller}/{action}/{id}",
				new { area = "administration", controller = "Tutorial", action = "CreateTuto", id = UrlParameter.Optional }
			);
		}
	}
}
