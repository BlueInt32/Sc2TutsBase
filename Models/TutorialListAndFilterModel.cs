using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sc2TutsBase.Models
{
	public class TutorialListAndFilterModel
	{
		public List<TutorialEntry> TutorialEntries { get; set; }

		[DisplayName("Race")]
		public Race RaceFilter { get; set; }
		[DisplayName("Contre")]
		public Race AgainstFilter { get; set; }
		[DisplayName("Ligue")]
		public League LeagueFilter { get; set; }
		[DisplayName("Caster")]
		public Caster CasterFilter { get; set; }
		public string Search { get; set; }


	}
}