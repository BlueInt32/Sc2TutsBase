using Sc2TutsBase.Models;
using Sc2TutsBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sc2TutsBase.App_Start;
using System.Text;

namespace Sc2TutsBase
{
	public class RouteConfig
	{

		public static void RegisterRoutes(RouteCollection routes)
		{
             StringBuilder enumStrBuilder = new StringBuilder();
             string globalRegexPattern = string.Concat(
                 "(", 
                 string.Join(")(", new List<string> 
                 {
                    GetEnumsTokensAsString(typeof(League), ref enumStrBuilder),

                    GetEnumsTokensAsString(typeof(Race),ref enumStrBuilder),
                    GetEnumsTokensAsString(typeof(Race),ref enumStrBuilder),
                    GetEnumsTokensAsString(typeof(Caster),ref enumStrBuilder)
                 }
                ).ToArray(),
            ")"); 
            //= Enum.GetValues(typeof(League)).Cast<League>().Aggregate(string.Empty, el => el.ToString().Substring(0, 1).ToLower(), resultStr => 

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				name: "Filter",
				url: "filter/{filter}",
                
				defaults: new { controller = "Home", action = "Filter", filter = UrlParameter.Optional },
                constraints: new { filter = string.Format(@"[((b|s|g){1}\.(b|s|g){1}\.(b|s|g){1}]____", globalRegexPattern) }
                );
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}

        public static string GetEnumsTokensAsString(Type type, ref StringBuilder enumStrBuilder)
        {
            enumStrBuilder.Clear();
            var regexPartLeague = Enum.GetValues(type);
            foreach (var item in regexPartLeague)
            {
                var memInfo = typeof(League).GetMember(item.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(TokenAttribute), false);
                var token = ((TokenAttribute)attributes[0]).Token;
                enumStrBuilder.AppendFormat("{0}|", token);
            }
            return enumStrBuilder.ToString().Substring(0, regexPartLeague.Length - 1);
        }
	}
}