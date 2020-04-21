using System.Web.Mvc;

namespace LibraryUI.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
			   name: "Admin",
				url: "Admin/{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "LibraryUI.Areas.Admin.Controllers" }
			);
		}
	}
}