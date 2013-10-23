using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Sc2TutsBase.Models;

namespace Sc2TutsBase.Utils
{
	public class Sc2Filter
	{
		readonly List<string> splitEmptyIrrelevantList = new List<string> { "" };



		public DisplayMode DisplayModeSelected { get; set; }
		[DisplayName("Ligue")]
		public List<League> LeaguesSelected { get; set; }
		[DisplayName("Race")]
		public List<Race> RacesSelected { get; set; }
		[DisplayName("Contre")]
		public List<Race> AgainstsSelected { get; set; }
		[DisplayName("Caster")]
		public List<Caster> CastersSelected { get; set; }
		[DisplayName("Type")]
		public List<VideoType> VideoTypeSelected { get; set; }

		public Sc2Filter()
		{
		}
		public Sc2Filter(string filterToken)
		{
			string[] aFilters = filterToken.Split('_');
			//DisplayModeSelected = aFilters[0] == "l" ? DisplayMode.List : DisplayMode.Details;
			VideoTypeSelected = aFilters[0].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseVideoType);
			LeaguesSelected = aFilters[1].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseLeague);
			RacesSelected = aFilters[2].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseRace);
			AgainstsSelected = aFilters[3].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseRace);
			CastersSelected = aFilters[4].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseCaster);
			SearchText = HttpUtility.UrlDecode(aFilters[5]).ToLower();
			
		}

		public void Apply(ref IEnumerable<TutorialEntry> list)
		{
			if (list == null)
				throw new Exception("Faut initialiser la list avant...");

			if (!string.IsNullOrWhiteSpace(SearchText))
			{
				SearchText = Regex.Replace(SearchText, @"(?<race>(p|t|z|P|T|Z){1})v(?<against>(p|t|z|P|T|Z){1})", delegate(Match match)
				{
					string x = match.ToString();
					Race race = ParseRace(match.Groups["race"].ToString().ToLower());
					Race against = ParseRace(match.Groups["against"].ToString().ToLower());
					RacesSelected.Add(race);
					AgainstsSelected.Add(against);
					return string.Empty;
				});
				if (!string.IsNullOrWhiteSpace(SearchText))
				{
					list = list.Where
						(t =>
							t.Author.ToString().ToLower().Contains(SearchText)
							|| t.Description.ToString().ToLower().Contains(SearchText)
							|| t.Title.ToString().ToLower().Contains(SearchText)
						);
				}
			}

			if (VideoTypeSelected.Count > 0)
				list = list.Where(t => VideoTypeSelected.Contains(t.VideoType));
			if (LeaguesSelected.Count > 0)
				list = list.Where(t => t.CurrentLeague.Union(LeaguesSelected).Any());
			if (RacesSelected.Count > 0)
				list = list.Where(t => RacesSelected.Contains(t.Race));
			if (AgainstsSelected.Count > 0)
				list = list.Where(t => AgainstsSelected.Contains(t.Against));
			if (CastersSelected.Count > 0)
				list = list.Where(t => CastersSelected.Contains(t.Author));

			return;
		}

		public string SearchText { get; set; }
		
		private VideoType ParseVideoType(string tokenType)
		{
			return EnumHelper.GetEnumValueFromToken<VideoType>(tokenType);
		}
		private Race ParseRace(string tokenRace)
		{
			return EnumHelper.GetEnumValueFromToken<Race>(tokenRace);
		}
		private Caster ParseCaster(string tokenCaster)
		{
			return EnumHelper.GetEnumValueFromToken<Caster>(tokenCaster);
		}
		private League ParseLeague(string tokenLeague)
		{
			return EnumHelper.GetEnumValueFromToken<League>(tokenLeague);
		}
	}
}