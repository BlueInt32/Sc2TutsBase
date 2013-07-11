using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sc2TutsBase.Filters
{
    public class PostDataFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Client client = new Client
            {
                Id = filterContext.HttpContext.Request.Params["client_id"],
                Email = filterContext.HttpContext.Request.Params["client_email"],
                FirstName = filterContext.HttpContext.Request.Params["client_firstname"]
            };
            filterContext.HttpContext.Session["Client"] = client;
            base.OnActionExecuting(filterContext);
        }
    }
}