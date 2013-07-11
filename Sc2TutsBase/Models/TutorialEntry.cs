using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sc2TutsBase.Models
{
	public class TutorialEntry
	{
		[XmlAttribute]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Race Race { get; set; }
		public Race Against { get; set; }
		public League CurrentLeague { get; set; }

		public string VideoUrl { get; set; }
        public string YoutubeId { get; set; }
		public string StartTiming { get; set; }

		public Caster Author { get; set; }
	}
}