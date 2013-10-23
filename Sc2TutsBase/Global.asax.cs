using Sc2TutsBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Tools;

namespace Sc2TutsBase
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);



            Application["TutoList"] = TutsListSerializer.LoadTutsFromFile();

			Log.Log4NetInit();
            Log.InfoFormat("Global.asax", "Application_Start");


		}

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            HttpException httpexception = exception as HttpException;

            Log.ErrorFormat("Application_Error", exception.Message);
        }
	}
}