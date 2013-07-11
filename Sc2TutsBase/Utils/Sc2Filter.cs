using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Sc2TutsBase.Models;

namespace Sc2TutsBase.Utils
{
	public class Sc2Filter
	{
		readonly List<string> splitEmptyIrrelevantList = new List<string> { "" };
		public Sc2Filter()
		{
		}
		public Sc2Filter(string filterToken)
		{
            
			string[] aFilters = filterToken.Split('_');

			LeaguesSelected = aFilters[0].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseLeague);
			RacesSelected = aFilters[1].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseRace);
			AgainstsSelected = aFilters[2].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseRace);
			CastersSelected = aFilters[3].Split('.').Except(splitEmptyIrrelevantList).ToList().ConvertAll(ParseCaster);


		}

		public void Apply(ref IEnumerable<TutorialEntry> list)
		{
			if (list == null)
				throw new Exception("Faut initialiser la list avant...");
			if (LeaguesSelected.Count > 0)
				list = list.Where(t => LeaguesSelected.Contains(t.CurrentLeague));
			if (RacesSelected.Count > 0)
				list = list.Where(t => RacesSelected.Contains(t.Race));
			if (AgainstsSelected.Count > 0)
				list = list.Where(t => AgainstsSelected.Contains(t.Against));
			if (CastersSelected.Count > 0)
				list = list.Where(t => CastersSelected.Contains(t.Author));
			return;
		}

		[DisplayName("Ligue")]
		public List<League> LeaguesSelected { get; set; }
		[DisplayName("Race")]
		public List<Race> RacesSelected { get; set; }
		[DisplayName("Contre")]
		public List<Race> AgainstsSelected { get; set; }
		[DisplayName("Caster")]
		public List<Caster> CastersSelected { get; set; }


		//public League LeagueFilterEnum { get; set; }
		//public Race RaceFilterEnum { get; set; }
		//public Race AgainstFilterEnum { get; set; }
		//public Caster CasterFilterEnum { get; set; }
		//public string Search { get; set; }

		private Race ParseRace(string tokenRace)
		{
			switch (tokenRace)
			{
				case "p":return Race.Protoss;
				case "z":return Race.Zerg;
				case "t":return Race.Terran;
				default:return Race.Protoss;
			}
		}
		private Caster ParseCaster(string tokenCaster)
		{
			switch (tokenCaster)
			{
				case "m":return Caster.Makoz;
				case "a":return Caster.Anoss;
				default:return Caster.Makoz;
			}
		}
		private League ParseLeague(string tokenLeague)
		{
			switch (tokenLeague)
			{
				case "b":return League.Bronze;
				case "s":return League.Silver;
				case "g":return League.Gold;
				case "p":return League.Platinum;
				case "d":return League.Diamond;
				case "m":return League.Master;
				default:return League.Bronze;
			}
		}

	}
}