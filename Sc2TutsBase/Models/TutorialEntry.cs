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
        public string RaceShort {get { return Race.ToString().Substring(0, 1); }}

		public Race Against { get; set; }
        public string AgainstShort {get { return Race.ToString().Substring(0, 1); }}
		public League CurrentLeague { get; set; }

		public string VideoUrl { get; set; }
        public string YoutubeId { get; set; }
		public string StartTiming { get; set; }

		public Caster Author { get; set; }

		[XmlIgnore]
		public DateTime CreationDate { get; set; }

		[XmlElement("CreationDate")]
		public string CreationDateString
		{
			get { return CreationDate.ToString("dd/MM/yyyy HH:mm:ss"); }
			set { CreationDate = DateTime.Parse(value); }
		}
	}
}