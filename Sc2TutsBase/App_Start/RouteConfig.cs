using System.Text.RegularExpressions;
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
		string s = "";
		public static void RegisterRoutes(RouteCollection routes)
		{
             StringBuilder enumStrBuilder = new StringBuilder();


             string globalRegexPattern = string.Concat(
					"^",
					GetEnumsRegexPattern(League.Bronze, ref enumStrBuilder),
					"_",
					GetEnumsRegexPattern(Race.Protoss, ref enumStrBuilder),
					"_",
					GetEnumsRegexPattern(Race.Protoss, ref enumStrBuilder),
					"_",
					GetEnumsRegexPattern(Caster.Anoss, ref enumStrBuilder),
					"$"
			); 
            //= Enum.GetValues(typeof(League)).Cast<League>().Aggregate(string.Empty, el => el.ToString().Substring(0, 1).ToLower(), resultStr => 

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
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

			Console.WriteLine(typeof(League));
			var regexPartLeague = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
			List<string> tokens = (from item in regexPartLeague
								   select typeof(TEnum).GetMember(item.ToString())
			                       into memInfo select memInfo[0].GetCustomAttributes(typeof (TokenAttribute), false)
			                       into attributes select ((TokenAttribute) attributes[0]).Token).ToList();
			//string.Join("|", regexPartLeague.
			string charList = string.Join("|", tokens.ToArray());
			return string.Format(@"(({0})?\.){{0,{1}}}({0})?", charList, regexPartLeague.Count - 1);
        }
	}
}