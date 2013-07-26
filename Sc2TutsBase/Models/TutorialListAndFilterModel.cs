using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Sc2TutsBase.Utils;

namespace Sc2TutsBase.Models
{
	public class TutorialListAndFilterModel
	{
        public TutoListViewType TutoListViewType { get; set; }

		public List<TutorialEntry> TutorialEntries { get; set; }
		public string PotentialMessage { get; set; }
		public Sc2Filter Filter { set; get; }

		public bool ShowList { get; set; }
		
	}
}