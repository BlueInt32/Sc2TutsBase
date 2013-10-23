using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Tools;

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
        public string AgainstShort {get { return Against.ToString().Substring(0, 1); }}
		public List<League> CurrentLeague { get; set; }

		public string VideoUrl { get; set; }
        public string YoutubeId { get; set; }
		public string StartTiming { get; set; }

		public VideoType VideoType { get; set; }

		public Caster Author { get; set; }

		public string PathToPdf
		{
			get
			{
				string magicPath = string.Format("~/pdfFiles/{0}.pdf", Id);
				if (File.Exists(HttpContext.Current.Server.MapPath(magicPath)))
				{
					return magicPath.ContentAbsolute();
				}
				else
					return null;
			}
		}

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