using System.Text.RegularExpressions;
using Sc2TutsBase.Models;
using Sc2TutsBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;

namespace Sc2TutsBase
{
	public class RouteConfig
	{
		string s = "";
		public static void RegisterRoutes(RouteCollection routes)
		{
             StringBuilder enumStrBuilder = new StringBuilder();


             string globalRegexPattern = string.Concat(
					"^(",
					GetEnumsRegexPattern(League.Bronze, ref enumStrBuilder),
					"_",
					GetEnumsRegexPattern(Race.Protoss, ref enumStrBuilder),
					"_",
					GetEnumsRegexPattern(Race.Protoss, ref enumStrBuilder),
					"_",
					GetEnumsRegexPattern(Caster.Anoss, ref enumStrBuilder),
					@"_[a-zA-Z0-9{0}áàâäãéèêëíìîïĩóòôöõúüîôûúùýñçÿ\s' \+]*)?",
					"$"
			); 
            //= Enum.GetValues(typeof(League)).Cast<League>().Aggregate(string.Empty, el => el.ToString().Substring(0, 1).ToLower(), resultStr => 



			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
				routes.MapRoute(
                name:"SwitchDisplayMode",
                url: "switch/{mode}",
				defaults: new { controller = "Home", action = "SwitchDisplayMode", mode = UrlParameter.Optional },
				constraints: new { mode = @"(l|d){0,1}" }
                );
            routes.MapRoute(
                name:"404",
                url: "404",
                defaults: new { controller = "Error", action = "Unknown", filter = UrlParameter.Optional}
                );
            routes.MapRoute(
				name: "Filter",
				url: "filter/{filter}",
				defaults: new { controller = "Home", action = "Filter", filter = UrlParameter.Optional },
				constraints: new { filter = globalRegexPattern }
                );
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}

        public static string GetEnumsRegexPattern<TEnum>(TEnum typeEnum, ref StringBuilder enumStrBuilder)
        {
            enumStrBuilder.Clear(); 
			var regexPartLeague = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
            string tokens = EnumHelper.GetTokens<TEnum>("|");
			return string.Format(@"(({0})?\.){{0,{1}}}({0})?", tokens, regexPartLeague.Count - 1);
        }
	}
}